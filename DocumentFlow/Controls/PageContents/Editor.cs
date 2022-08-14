//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.09.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Core;
using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Core.Repository;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Dialogs;
using DocumentFlow.Infrastructure;
using DocumentFlow.Properties;
using DocumentFlow.ReportEngine.Infrastructure;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;

using System.Collections.ObjectModel;
using System.IO;
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
    private Guid? owner_id;
    private bool readOnly;
    private bool isClosing = false;

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
    }

    public IToolBar Toolbar => toolbar;

    public Guid? Id => document?.id;

    protected T Document => document ?? throw new ArgumentNullException(nameof(Document), "Значение Document не определено.");

    protected Guid? DefaultParentId => defaultParentId;

    protected Guid? OwnerId => owner_id;

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
        this.owner_id = owner_id;

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
            if (Document.id == Guid.Empty)
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
                    grid.SetOwner(Document.id);
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

        RefreshData(Document.id);
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
        if (doc.owner_id == null)
        {
            doc.owner_id = owner_id;
        }

        if (doc is IDirectory dir && dir.parent_id == null)
        {
            dir.parent_id = defaultParentId;
        }

        DoCreatedDocument(doc);

        return doc;
    }

    private void UpdateDocument(T doc)
    {
        var controls = container.Panel1.Childs().ToArray();
        UpdateDocument(controls, doc);
    }

    private void UpdateDocument(Control[] controls, T doc)
    {
        foreach (var item in controls.OfType<IBindingControl>().Where(x => x.AllowSaving))
        {
            PropertyInfo? prop = typeof(T).GetProperty(item.PropertyName);
            if (prop != null)
            {
                prop.SetValue(doc, item.Value);
            }
        }
    }

    private void RefreshInfo(T entity)
    {
        textId.Text = entity.id.ToString();
        dateTimeCreate.Value = entity.date_created;
        dateTimeUpdate.Value = entity.date_updated;

        var users = Services.Provider.GetService<IUserAliasRepository>();
        textAuthor.Text = users!.GetById(entity.user_created_id).name;
        textEditor.Text = users!.GetById(entity.user_updated_id).name;

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
            if (document == null)
            {
                document = CreateDocument();
            }

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
                dataSource.SetOwner(document.id);
            }

            item.RefreshDataSource();
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
                if (repo != null)
                {
                    repo.Update(refs);
                }

                gridDocuments.Refresh();
            }
        }
    }

    private void OpenReport(IReport report)
    {
        T? doc = document;
        if (doc == null)
        {
            doc = CreateDocument();
        }

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
                            if (document != null && e.ObjectId == document.id && !isClosing)
                            {
                                RefreshData(document.id, false);
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
            case "id":
            case "owner_id":
                e.Cancel = true;
                break;
            case "file_length":
                e.Column = new GridFileSizeColumn() { MappingName = "file_length", HeaderText = "Размер", Width = 150 };
                break;
            case "note":
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
            case "file_name":
                e.Column.Width = 300;
                break;
            case "thumbnail_exist":
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
            RefreshData(document.id);
        }
    }

    private void ButtonAddDoc_Click(object sender, EventArgs e)
    {
        if (document == null)
        {
            return;
        }

        var refs = DocumentRefEditorForm.Create(document.id);
        if (refs.Document != null)
        {
            var repo = Services.Provider.GetService<IDocumentRefsRepository>();
            if (repo != null)
            {
                var res = repo!.Add(refs.Document);

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
        if (gridDocuments.CurrentItem is DocumentRefs refs && refs.file_name != null && document != null)
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

        RefreshDocumentRefs(document.id);
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
        if (gridDocuments.CurrentItem is DocumentRefs refs && refs.file_name != null && document != null && refs.file_content != null)
        {
            saveFileDialog1.FileName = refs.file_name;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using FileStream stream = new(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);
                stream.Write(refs.file_content, 0, refs.file_content.Length);
            }
        }
    }
}
