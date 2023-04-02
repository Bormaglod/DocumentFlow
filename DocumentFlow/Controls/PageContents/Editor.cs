//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.09.2021
//
// Версия 2022.8.28
//  - добавлен метод RefreshPage()
//  - рефакторинг
// Версия 2022.11.26
//  - в методе RefreshData вызов RefreshDataSource заменён на
//    RefreshDataSourceOnLoad
// Версия 2023.1.15
//  - исправлен вызов DocumentRefEditorForm.Create, теперь он возвращает
//    в случае успеха true, а остальные параметры передаются через out
//  - рефакторинг
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.ReportEngine.Infrastructure перемещено в DocumentFlow.Infrastructure.ReportEngine
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.2.23
//  - добавлена ссылка на DocumentFlow.Core.Exceptions
// Версия 2023.2.4
//  - добавлено поле controls и его инициализация в конструкторе
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Editor;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Core;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Core.Reflection;
using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Dialogs;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.ReportEngine;
using DocumentFlow.Properties;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;

using System.Collections.ObjectModel;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace DocumentFlow.Controls.PageContents;

public partial class Editor<T> : UserControl, IEditorPage
    where T : class, IDocumentInfo, new()
{
    public class GridFileSizeColumn : GridNumericColumn
    {
        protected override object GetFormattedValue(object record, object value)
        {
            if (value != null && long.TryParse(value.ToString(), out var originalValue))
            {
                return originalValue.Bytes().Humanize();
            }

            return value ?? string.Empty;
        }
    }

    private readonly PageToolBar toolbar;
    private readonly IRepository<Guid, T> repository;
    private readonly IPageManager pageManager;
    private Guid? defaultParentId;
    private Guid? ownerId;
    private bool readOnly;
    private bool isClosing = false;
    private readonly IControls<T> controls;

    private T? document;

    public Editor(IRepository<Guid, T> repository, IPageManager pageManager)
    {
        InitializeComponent();

        toolbar = new PageToolBar(editorToolStrip1, new Dictionary<ToolStripItem, (Image, Image)>
        {
            [buttonSave] = (Resources.icons8_save_16, Resources.icons8_save_30),
            [buttonSaveAndClose] = (Resources.icons8_save_close_16, Resources.icons8_save_close_30),
            [buttonAccept] = (Resources.icons8_document_accept_16, Resources.icons8_document_accept_30),
            [buttonAcceptAndClose] = (Resources.icons8_doc_accept_close_16, Resources.icons8_doc_accept_close_30),
            [buttonPrint] = (Resources.icons8_print_16, Resources.icons8_print_30),
            [buttonRefresh] = (Resources.icons8_refresh_16, Resources.icons8_refresh_30)
        });

        this.repository = repository;
        this.pageManager = pageManager;

        buttonAddDoc.Enabled = repository.HasPrivilege("document_refs", Privilege.Insert);
        buttonEditDoc.Enabled = repository.HasPrivilege("document_refs", Privilege.Update);
        buttonDeleteDoc.Enabled = repository.HasPrivilege("document_refs", Privilege.Delete);
        menuCreateDocument.Enabled = buttonAddDoc.Enabled;
        menuEditDocument.Enabled = buttonEditDoc.Enabled;
        menuDeleteDocument.Enabled = buttonDeleteDoc.Enabled;

        controls = Services.Provider.GetService<IControls<T>>()!;
        if (controls is IControls ctrls)
        {
            ctrls.Container = container.Panel1.Controls;
            ctrls.EditorPage = this;
        }
    }

    public IToolBar Toolbar => toolbar;

    public Guid? Id => document?.Id;

    public IControls<T> EditorControls => controls;

    protected T Document => document ?? throw new ArgumentNullException(nameof(Document), "Значение Document не определено.");

    protected Guid? DefaultParentId => defaultParentId;

    protected Guid? OwnerId => ownerId;

    protected bool IsCreating { get; set; }

    public IEnumerable<IBrowserPage> NestedBrowsers
    {
        get
        {
            foreach (var item in tabControl1.TabPages.OfType<TabPage>().Where(p => p.Controls.Count > 0))
            {
                if (item.Controls[0] is IBrowserPage page)
                {
                    yield return page;
                }
            }
        }
    }

    public void SetEntityParameters(Guid? id, Guid? owner_id, Guid? parent_id, bool readOnly)
    {
        if (!repository.HasPrivilege(Privilege.Update))
        {
            readOnly = true;
        }

        this.readOnly = readOnly;
        this.ownerId = owner_id;

        defaultParentId = parent_id;

        IsCreating = id == null;

        RefreshData(id);
        RegisterNestedBrowsers();

        CurrentApplicationContext.Context.App.OnAppNotify += App_OnAppNotify;
    }

    public void OnPageClosing()
    {
        foreach (var page in NestedBrowsers)
        {
            page.OnPageClosing();
        }
    }

    public void RegisterReport(IReport report)
    {
        ToolStripMenuItem menuItem = new(report.Title)
        {
            Tag = report
        };

        menuItem.Click += OpenReportClick;

        buttonPrint.DropDownItems.Add(menuItem);

        if (buttonPrint.DropDownItems.Count == 1)
        {
            buttonPrint.Tag = report;
        }
    }

    public bool Save() => Save(true);

    public void RefreshPage() => RefreshData(document?.Id);

    protected DfCheckBox CreateCheckBox(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, bool allowThreeState = false)
    {
        return new DfCheckBox(memberExpression.ToMember().Name,
                              header,
                              headerWidth,
                              allowThreeState);
    }

    protected DfChoice<P> CreateChoice<P>(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth = default, bool required = false, IReadOnlyDictionary<P, string>? choices = null)
        where P : struct, IComparable
    {
        var choice = new DfChoice<P>(memberExpression.ToMember().Name,
                                     header,
                                     headerWidth,
                                     editorWidth)
        { Required = required };
        if (choices != null)
        {
            choice.SetChoiceValues(choices);
        }

        return choice;
    }

    protected DfChoice<P> CreateChoice<P>(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth = default, bool required = false, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad, Func<IEnumerable<IChoice<P>>?>? data = null)
        where P : struct, IComparable
    {
        var choice = new DfChoice<P>(memberExpression.ToMember().Name,
                                     header,
                                     headerWidth,
                                     editorWidth)
        { Required = required, RefreshMethod = refreshMethod };
        if (data != null)
        {
            choice.SetDataSource(data);
        }

        return choice;
    }

    protected DfComboBox<P> CreateComboBox<P>(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth = default, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad, Func<IEnumerable<P>?>? data = null)
        where P : class, IDocumentInfo
    {
        var combo = new DfComboBox<P>(memberExpression.ToMember().Name,
                                      header,
                                      headerWidth,
                                      editorWidth)
        { RefreshMethod = refreshMethod };
        if (data != null)
        {
            combo.SetDataSource(data);
        }

        return combo;
    }

    protected DfComboBox<P> CreateComboBox<P, I>(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth = default, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad, Func<IEnumerable<P>?>? data = null)
        where P : class, IDocumentInfo
        where I : IEditorPage
    {
        var combo = CreateComboBox(memberExpression, header, headerWidth, editorWidth, refreshMethod, data);
        combo.OpenAction = pageManager.ShowEditor<I, P>;
        return combo;
    }

    public DfCurrencyTextBox CreateCurrencyTextBox(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth = default, bool enabled = true, bool visible = true, bool defaultAsNull = true)
    {
        return new DfCurrencyTextBox(memberExpression.ToMember().Name,
                                     header,
                                     headerWidth,
                                     editorWidth)
        { Enabled = enabled, Visible = visible, DefaultAsNull = defaultAsNull };
    }

    protected DfDateTimePicker CreateDateTimePicker(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth = default, bool enabled = true, bool visible = true, bool defaultAsNull = true, bool required = true, DateTimePickerFormat format = DateTimePickerFormat.Long)
    {
        return new DfDateTimePicker(memberExpression.ToMember().Name,
                                 header,
                                 headerWidth,
                                 editorWidth)
        { Enabled = enabled, Visible = visible, DefaultAsNull = defaultAsNull, Required = required, Format = format };
    }

    protected DfDirectorySelectBox<P> CreateDirectorySelectBox<P>(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth = default, bool required = false, bool readOnly = false, Guid? rootIdentifier = null, bool showOnlyFolder = false, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad, Func<IEnumerable<P>?>? data = null)
        where P : class, IDirectory
    {
        var box = new DfDirectorySelectBox<P>(memberExpression.ToMember().Name,
                                              header,
                                              headerWidth,
                                              editorWidth)
        { Required = required, RootIdentifier = rootIdentifier, ShowOnlyFolder = showOnlyFolder, ReadOnly = readOnly, RefreshMethod = refreshMethod };
        if (data != null)
        {
            box.SetDataSource(data);
        }

        return box;
    }

    protected DfDirectorySelectBox<P> CreateDirectorySelectBox<P, I>(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth = default, bool required = false, bool readOnly = false, Guid? rootIdentifier = null, bool showOnlyFolder = false, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad, Func<IEnumerable<P>?>? data = null, bool openById = false)
        where P : class, IDirectory
        where I : IEditorPage
    {
        var box = CreateDirectorySelectBox(memberExpression, header, headerWidth, editorWidth, required, readOnly, rootIdentifier, showOnlyFolder, refreshMethod, data);
        if (openById)
        {
            box.OpenAction = (t) => pageManager.ShowEditor<I>(t.Id);
        }
        else
        {
            box.OpenAction = pageManager.ShowEditor<I, P>;
        }

        return box;
    }

    protected DfDocumentSelectBox<P> CreateDocumentSelectBox<P>(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth = default, bool required = false, bool disableCurrent = false, bool readOnly = false, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad, Func<IEnumerable<P>?>? data = null)
        where P : class, IAccountingDocument
    {
        var box = new DfDocumentSelectBox<P>(memberExpression.ToMember().Name,
                                             header,
                                             headerWidth,
                                             editorWidth)
        { Required = required, RefreshMethod = refreshMethod, DisableCurrentItem = disableCurrent, ReadOnly = readOnly };
        if (data != null)
        {
            box.SetDataSource(data);
        }

        return box;
    }

    protected DfDocumentSelectBox<P> CreateDocumentSelectBox<P, I>(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth = default, bool required = false, bool disableCurrent = false, bool readOnly = false, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad, Func<IEnumerable<P>?>? data = null, bool openById = false)
        where P : class, IAccountingDocument
        where I : IEditorPage
    {
        var box = CreateDocumentSelectBox(memberExpression, header, headerWidth, editorWidth, required, disableCurrent, readOnly, refreshMethod, data);
        if (openById)
        {
            box.OpenAction = (t) => pageManager.ShowEditor<I>(t.Id);
        }
        else
        {
            box.OpenAction = pageManager.ShowEditor<I, P>;
        }

        return box;
    }

    protected DfDocumentSelectBox<P> CreateDocumentSelectBox<P>(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth = default, bool required = false, bool disableCurrent = false, bool readOnly = false, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad, Func<IEnumerable<P>?>? data = null, Action<P>? open = null)
        where P : class, IAccountingDocument
    {
        var box = CreateDocumentSelectBox(memberExpression, header, headerWidth, editorWidth, required, disableCurrent, readOnly, refreshMethod, data);
        if (open != null)
        {
            box.OpenAction = open;
        }

        return box;
    }

    protected DfIntegerTextBox<P> CreateIntegerTextBox<P>(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth, bool enabled = true, bool visible = true, bool defaultAsNull = true)
        where P : struct, IComparable<P>
    {
        return new DfIntegerTextBox<P>(memberExpression.ToMember().Name,
                                       header,
                                       headerWidth,
                                       editorWidth)
        { Enabled = enabled, Visible = visible, DefaultAsNull = defaultAsNull };
    }

    protected DfMaskedTextBox<P> CreateMaskedTextBox<P>(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth, string? mask = null, bool defaultAsNull = true)
        where P : struct, IComparable<P>
    {
        return new DfMaskedTextBox<P>(memberExpression.ToMember().Name,
                                      header,
                                      headerWidth,
                                      editorWidth,
                                      mask)
        { DefaultAsNull = defaultAsNull };
    }

    protected DfMultiSelectionComboBox CreateMultiSelectionComboBox(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth, bool enabled = true, bool visible = true, bool defaultAsNull = true, Func<IEnumerable<IItem>?>? data = null)
    {
        var combo = new DfMultiSelectionComboBox(memberExpression.ToMember().Name,
                                                 header,
                                                 headerWidth,
                                                 editorWidth)
        { Enabled = enabled, Visible = visible, DefaultAsNull = defaultAsNull };
        if (data != null)
        {
            combo.SetDataSource(data);
        }

        return combo;
    }

    protected DfNumericTextBox CreateNumericTextBox(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth = default, bool enabled = true, bool visible = true, bool defaultAsNull = true, int digits = 0)
    {
        return new DfNumericTextBox(memberExpression.ToMember().Name,
                                    header,
                                    headerWidth,
                                    editorWidth)
        { Enabled = enabled, Visible = visible, DefaultAsNull = defaultAsNull, NumberDecimalDigits = digits };
    }

    protected DfPercentTextBox CreatePercentTextBox(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth, bool enabled = true, bool visible = true, bool defaultAsNull = true, int digits = 3)
    {
        return new DfPercentTextBox(memberExpression.ToMember().Name,
                                    header,
                                    headerWidth,
                                    editorWidth)
        { Enabled = enabled, Visible = visible, DefaultAsNull = defaultAsNull, PercentDecimalDigits = digits };
    }

    protected DfState CreateState(Expression<Func<T, object?>> memberExpression, string header, int headerWidth)
    {
        return new DfState(memberExpression.ToMember().Name,
                                 header,
                                 headerWidth);
    }

    protected DfTextBox CreateTextBox(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth, bool enabled = true, bool defaultAsNull = true, bool readOnly = false)
    {
        return new DfTextBox(memberExpression.ToMember().Name,
                             header,
                             headerWidth,
                             editorWidth) { Enabled = enabled, DefaultAsNull = defaultAsNull, ReadOnly = readOnly };
    }

    protected DfTextBox CreateMultilineTextBox(Expression<Func<T, object?>> memberExpression, string header, int headerWidth, int editorWidth, bool enabled = true, bool defaultAsNull = true, int height = 75)
    {
        return new DfTextBox(memberExpression.ToMember().Name,
                             header,
                             headerWidth,
                             editorWidth) { Enabled = enabled, DefaultAsNull = defaultAsNull, Multiline = true, Height = height };
    }

    protected DfToggleButton CreateToggleButton(Expression<Func<T, object?>> memberExpression, string header, int headerWidth)
    {
        return new DfToggleButton(memberExpression.ToMember().Name,
                                 header,
                                 headerWidth);
    }

    protected Panel CreatePanel(Control[] controls)
    {
        var panel = new Panel()
        {
            Dock = DockStyle.Top,
            Height = 32
        };

        panel.Controls.AddRange(controls);

        return panel;
    }

    protected void AddControls(IList<Control> controls)
    {
        SuspendLayout();

        for (int i = 0; i < controls.Count; i++)
        {
            controls[i].TabIndex = i;
            container.Panel1.Controls.Add(controls[i]);

            controls[i].BringToFront();

            if (controls[i] is BaseControl control)
            {
                control.EditorPage = this;
            }
        }

        ResumeLayout(false);
    }

    protected void RegisterNestedBrowser<B, C>()
        where B : IBrowser<C>
        where C : IDocumentInfo
    {
        if (Id == null || Id == Guid.Empty)
        {
            return;
        }

        if (NestedBrowsers.FirstOrDefault(x => x is IBrowser<C>) != null)
        {
            return;
        }

        var service = Services.Provider.GetService<B>();
        if (service != null && service is IBrowser<C> browser && service is Control control)
        {
            TabPage page = new(browser.Text);
            control.Dock = DockStyle.Fill;
            page.Controls.Add(control);

            // Insert не добавляет страницу - решение отсюда...
            // https://stackoverflow.com/questions/1532301/visual-studio-tabcontrol-tabpages-insert-not-working
            tabControl1.CreateControl();
            tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 2, page);

            browser.ReadOnly = readOnly;

            tabControl1.SelectedIndex = 0;
        }

        if (service is IBrowserPage browserPage)
        {
            browserPage.Refresh(Id);
        }
    }

    protected virtual void RegisterNestedBrowsers() { }

    protected virtual void DoAfterRefreshData() { }

    protected virtual void DoBeforeSave() { }

    protected virtual void DoCreatedDocument(T document) { }

    protected virtual bool ConfirmSaveDocument() => true;

    protected void SetNestedBrowserStatus(bool readOnly)
    {
        foreach (var browser in NestedBrowsers)
        {
            browser.ReadOnly = readOnly;
        }
    }

    protected bool Save(bool sendNotify)
    {
        var controls = container.Panel1.Childs().ToArray();

        if (!ConfirmSaveDocument())
        {
            return false;
        }

        UpdateDocument(controls, Document);

        DoBeforeSave();

        var db = Services.Provider.GetService<IDatabase>();
        using var conn = db!.OpenConnection();
        using var transaction = conn.BeginTransaction();

        MessageAction action;
        try
        {
            if (Document.Id == Guid.Empty)
            {
                document = repository.Add(Document, transaction);
                action = MessageAction.Add;
            }
            else
            {
                repository.Update(Document, transaction);
                action = MessageAction.Refresh;
            }

            foreach (var grid in controls.OfType<IGridDataSource>())
            {
                if (action == MessageAction.Add)
                {
                    grid.SetOwner(Document.Id);
                }

                grid.UpdateData(transaction);
            }

            transaction.Commit();

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            ExceptionHelper.MesssageBox(ex);
            return false;
        }

        RefreshData(Document.Id);
        if (action == MessageAction.Add)
        {
            RegisterNestedBrowsers();
        }

        if (sendNotify)
        {
            CurrentApplicationContext.Context.App.SendNotify(typeof(T).Name.Underscore(), Document, action);
        }

        IsCreating = false;

        return true;
    }

    private bool Accept()
    {
        if (Save(false) && repository is IDocumentRepository<T> repo)
        {
            try
            {
                repo.Accept(Document);
                CurrentApplicationContext.Context.App.SendNotify(typeof(T).Name.Underscore(), Document, MessageAction.Refresh);
                return true;
            }
            catch (RepositoryException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        return false;
    }

    private T CreateDocument()
    {
        T doc = new();
        if (doc.OwnerId == null)
        {
            doc.OwnerId = ownerId;
        }

        if (doc is IDirectory dir && dir.ParentId == null)
        {
            dir.ParentId = defaultParentId;
        }

        DoCreatedDocument(doc);

        return doc;
    }

    private void UpdateDocument(T doc)
    {
        var controls = container.Panel1.Childs().ToArray();
        UpdateDocument(controls, doc);
    }

    private static void UpdateDocument(Control[] controls, T doc)
    {
        foreach (var item in controls.OfType<IBindingControl>().Where(x => x.AllowSaving))
        {
            PropertyInfo? prop = typeof(T).GetProperty(item.PropertyName);
            prop?.SetValue(doc, item.Value);
        }
    }

    private void RefreshInfo(T entity)
    {
        textId.Text = entity.Id.ToString();
        dateTimeCreate.Value = entity.DateCreated;
        dateTimeUpdate.Value = entity.DateUpdated;

        var users = Services.Provider.GetService<IUserAliasRepository>();
        textAuthor.Text = users!.GetById(entity.UserCreatedId).Name;
        textEditor.Text = users!.GetById(entity.UserUpdatedId).Name;

        UpdateText(entity);
    }

    private void UpdateText(T? entity = null)
    {
        string readOnlyText = readOnly ? " (только для чтения)" : string.Empty;

        var attr = typeof(T).GetCustomAttribute<DescriptionAttribute>();
        if (attr != null)
        {
            if (entity == null)
            {
                Text = $"{attr.Name} (новый)";
            }
            else
            {
                Text = $"{attr.Name} - {entity}{readOnlyText}";
            }
        }
        else
        {
            Text = $"{entity?.ToString() ?? typeof(T).Name}{readOnlyText}";
        }
    }

    private void RefreshDocumentRefs(Guid? id)
    {
        if (repository.HasPrivilege("document_refs", Privilege.Select))
        {
            var docs = Services.Provider.GetService<IDocumentRefsRepository>();
            var list = docs?.GetByOwner(id);
            if (list != null)
            {
                gridDocuments.DataSource = new ObservableCollection<DocumentRefs>(list);
            }
            else
            {
                gridDocuments.DataSource = null;
            }
        }
    }

    private void RefreshData(Guid? id, bool refreshNestedBrowsers = true)
    {
        if (id != null && id != Guid.Empty)
        {
            document = repository.GetById(id.Value);
            RefreshInfo(document);
            RefreshDocumentRefs(id);
        }
        else
        {
            document ??= CreateDocument();
            UpdateText();
        }

        bool is_doc = document is BaseDocument;
        buttonAccept.Visible = is_doc;
        buttonAcceptAndClose.Visible = is_doc;

        var controls = container.Panel1.Childs().ToArray();

        // Метод RefreshDataSource интерфейса IGridDataSource требует наличие установленного 
        // свойства owner_id. Если это ствойство не установлено, то элемент управления 
        // может быть пуст - это нормально, если документ только-что создан.
        // Для вновь созданного документа установка свойства owner_id будет ещё раз осуществляется
        // при сохранении в методе Save
        foreach (IDataSourceControl item in controls.OfType<IDataSourceControl>())
        {
            if (item is IGridDataSource dataSource && document != null)
            {
                dataSource.SetOwner(document.Id);
            }

            item.RefreshDataSourceOnLoad();
        }

        foreach (var item in controls.OfType<IBindingControl>())
        {
            PropertyInfo? prop = typeof(T).GetProperty(item.PropertyName);
            if (prop != null)
            {
                object? value = prop.GetValue(document);
                if (value == null)
                {
                    item.ClearValue();
                }
                else
                {
                    item.Value = value;
                }
            }
        }

        if (readOnly)
        {
            foreach (var item in container.Panel1.Controls.OfType<IAccess>())
            {
                item.ReadOnly = true;
            }
        }

        if (refreshNestedBrowsers)
        {
            foreach (var page in NestedBrowsers)
            {
                page.RefreshPage();
            }
        }

        DoAfterRefreshData();
    }

    private void EditDocumentRefs()
    {
        if (gridDocuments.CurrentItem is DocumentRefs refs)
        {
            if (DocumentRefEditorForm.Edit(refs))
            {
                var repo = Services.Provider.GetService<IDocumentRefsRepository>();
                repo?.Update(refs);

                gridDocuments.Refresh();
            }
        }
    }

    private void OpenReport(IReport report)
    {
        T doc = document ?? CreateDocument();

        UpdateDocument(doc);

        report.Show(doc);
    }

    private void OpenReportClick(object? sender, EventArgs e)
    {
        if (sender is ToolStripItem tool && tool.Tag is IReport report)
        {
            OpenReport(report);
        }
    }

    private void App_OnAppNotify(object? sender, NotifyEventArgs e)
    {
        if (e.EntityName == typeof(T).Name.Underscore())
        {
            switch (e.Action)
            {
                case MessageAction.Refresh:
                    switch (e.Destination)
                    {
                        case MessageDestination.Object:
                            if (document != null && e.ObjectId == document.Id && !isClosing)
                            {
                                RefreshData(document.Id, false);
                            }

                            break;
                    }

                    break;
            }
        }
    }

    private void GridDocuments_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
    {
        switch (e.Column.MappingName)
        {
            case "Id":
            case "OwnerId":
                e.Cancel = true;
                break;
            case "FileLength":
                e.Column = new GridFileSizeColumn() { MappingName = "FileLength", HeaderText = "Размер", Width = 150 };
                break;
            case "Note":
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
            case "FileName":
                e.Column.Width = 300;
                break;
            case "ThumbnailExist":
                e.Column.Width = 100;
                break;
        }
    }

    private void ButtonOpenDoc_Click(object sender, EventArgs e)
    {
        if (gridDocuments.CurrentItem is DocumentRefs refs)
        {
            FileHelper.OpenFile(refs);
        }
    }

    private void ButtonRefresh_Click(object sender, EventArgs e)
    {
        if (document != null)
        {
            RefreshData(document.Id);
        }
    }

    private void ButtonAddDoc_Click(object sender, EventArgs e)
    {
        if (document == null)
        {
            return;
        }

        if (DocumentRefEditorForm.Create(document.Id, out var refs, out var _))
        {
            var repo = Services.Provider.GetService<IDocumentRefsRepository>();
            if (repo != null)
            {
                var res = repo!.Add(refs!);

                if (gridDocuments.DataSource is IList<DocumentRefs> list)
                {
                    list.Add(res);
                }
            }
        }
    }

    private void ButtonEditDoc_Click(object sender, EventArgs e) => EditDocumentRefs();

    private void ButtonDeleteDoc_Click(object sender, EventArgs e)
    {
        if (gridDocuments.CurrentItem is DocumentRefs refs && refs.FileName != null && document != null)
        {
            if (MessageBox.Show($"Вы действительно хотите удалить документ?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var repo = Services.Provider.GetService<IDocumentRefsRepository>();
                if (repo != null)
                {
                    repo.Wipe(refs);

                    if (gridDocuments.DataSource is IList<DocumentRefs> list)
                    {
                        list.Remove(refs);
                    }
                }
            }
        }
    }

    private void GridDocuments_CellDoubleClick(object sender, CellClickEventArgs e) => EditDocumentRefs();

    private void ButtonSave_Click(object sender, EventArgs e) => Save(true);

    private void ButtonSaveAndClose_Click(object sender, EventArgs e)
    {
        isClosing = true;
        try
        {
            if (Save(true))
            {
                pageManager.ClosePage(this);
            }
        }
        finally
        {
            isClosing = false;
        }
    }

    private void ButtonDocumentRefresh_Click(object sender, EventArgs e)
    {
        if (document == null)
        {
            return;
        }

        RefreshDocumentRefs(document.Id);
    }

    private void ButtonAccept_Click(object sender, EventArgs e) => Accept();

    private void ButtonAcceptAndClose_Click(object sender, EventArgs e)
    {
        if (Accept())
        {
            pageManager.ClosePage(this);
        }
    }

    private void ButtonSaveDoc_Click(object sender, EventArgs e)
    {
        if (gridDocuments.CurrentItem is DocumentRefs refs && refs.FileName != null && document != null && refs.FileContent != null)
        {
            saveFileDialog1.FileName = refs.FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using FileStream stream = new(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);
                stream.Write(refs.FileContent, 0, refs.FileContent.Length);
            }
        }
    }
}
