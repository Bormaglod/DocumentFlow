//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.09.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Controls.Tools;
using DocumentFlow.Data;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Tools;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Interfaces;
using DocumentFlow.Properties;
using DocumentFlow.Settings;
using DocumentFlow.Tools;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Toolkit.Uwp.Notifications;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DocumentFlow.Controls.PageContents;

[ToolboxItem(false)]
public partial class EditorPage : UserControl, IEditorPage
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

    private readonly IToolBar toolBar;
    private readonly IServiceProvider services;
    private readonly IDockingManager dockingManager;
    private readonly ThumbnailRowSettings settings;

    private IEditor? editor;

    public EditorPage(IServiceProvider services, IDockingManager dockingManager, IOptions<LocalSettings> options)
    {
        InitializeComponent();

        this.services = services;
        this.dockingManager = dockingManager;

        settings = options.Value.PreviewRows.ThumbnailRow;

        toolBar = new PageToolBar(editorToolStrip1)
        {
            Buttons = new Dictionary<ToolStripItem, (Image, Image)>
            {
                [buttonSave] = (Resources.icons8_save_16, Resources.icons8_save_30),
                [buttonSaveAndClose] = (Resources.icons8_save_close_16, Resources.icons8_save_close_30),
                [buttonAccept] = (Resources.icons8_document_accept_16, Resources.icons8_document_accept_30),
                [buttonAcceptAndClose] = (Resources.icons8_doc_accept_close_16, Resources.icons8_doc_accept_close_30),
                [buttonPrint] = (Resources.icons8_print_16, Resources.icons8_print_30),
                [buttonRefresh] = (Resources.icons8_refresh_16, Resources.icons8_refresh_30)
            }
        };

        var repository = services.GetRequiredService<IDocumentRefsRepository>();

        buttonAddDoc.Enabled = repository.HasPrivilege(Privilege.Insert);
        buttonEditDoc.Enabled = repository.HasPrivilege(Privilege.Update);
        buttonDeleteDoc.Enabled = repository.HasPrivilege(Privilege.Delete);
        menuCreateDocument.Enabled = buttonAddDoc.Enabled;
        menuEditDocument.Enabled = buttonEditDoc.Enabled;
        menuDeleteDocument.Enabled = buttonDeleteDoc.Enabled;

        services.GetRequiredService<IHostApp>().OnAppNotify += App_OnAppNotify;
    }

    public IToolBar ToolBar => toolBar;

    public IEditor Editor
    {
        get
        {
            if (editor != null)
            {
                return editor;
            }

            throw new NullReferenceException(nameof(Editor));
        }

        set
        {
            editor = value;
            editor.HeaderChanged += Editor_HeaderChanged;
            editor.EditorPage = this;
            if (editor is Control control)
            {
                control.Dock = DockStyle.Fill;
                container.Panel1.Controls.Add(control);
            }
        }
    }

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

    private string BacketName
    {
        get
        {
            var name = Editor.DocumentInfo.GetType().Name.Underscore();
            return name.Replace('_', '-');
        }
    }

    /// <summary>
    /// Метод вызывается для извещения странице о том, что она в самое ближайшее время
    /// будет закрыта.
    /// </summary>
    public void NotifyPageClosing()
    {
        foreach (var page in NestedBrowsers)
        {
            page.NotifyPageClosing();
        }
    }

    public void RefreshPage()
    {
        if (Editor.DocumentInfo.Id != Guid.Empty)
        {
            Editor.Reload();
            if (Editor.DocumentInfo.Deleted)
            {
                Editor.EnabledEditor = false;
            }

            RefreshInfo(Editor.DocumentInfo);
            RefreshDocumentRefs(Editor.DocumentInfo.Id);
        }
        else
        {
            Editor.Create();
        }
    }

    protected bool Save(bool sendNotify)
    {
        MessageAction action;
        if (Editor.DocumentInfo.Id == Guid.Empty)
        {
            action = MessageAction.Add;
        }
        else
        {
            action = MessageAction.Refresh;
        }

        try
        {
            var document = Editor.Save();
            RefreshInfo(document);

            if (sendNotify)
            {
                var app = services.GetRequiredService<IHostApp>();
                app.SendNotify(MessageDestination.Object, document.GetType().Name.Underscore(), document.Id, action);
            }
        }
        catch (RepositoryException e)
        {
            MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        if (action == MessageAction.Add)
        {
            Editor.RegisterNestedBrowsers();
        }

        return true;
    }

    #region IEditor interface implemented

    void IEditorPage.RefreshPage(Guid doumentId)
    {
        var document = Editor.Load(doumentId);
        if (document.Deleted)
        {
            Editor.EnabledEditor = false;
        }

        RefreshInfo(document);
        RefreshDocumentRefs(document.Id);
        
        Editor.RegisterNestedBrowsers();

        UpdateAcceptButtons();
    }

    void IEditorPage.CreatePage()
    {
        Editor.Create();
        UpdateAcceptButtons();
    }

    void IEditorPage.RegisterNestedBrowser<B>()
    {
        if (Editor.DocumentInfo.Id == Guid.Empty)
        {
            return;
        }

        var browser = services.GetService<B>();
        if (browser is Control control)
        {
            TabPage page = new(control.Text);
            control.Dock = DockStyle.Fill;
            page.Controls.Add(control);

            // Insert не добавляет страницу - решение отсюда...
            // https://stackoverflow.com/questions/1532301/visual-studio-tabcontrol-tabpages-insert-not-working
            tabControl1.CreateControl();
            tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 2, page);

            browser.ReadOnly = !Editor.EnabledEditor;

            tabControl1.SelectedIndex = 0;

            browser.UpdatePage(Editor.DocumentInfo);
        }
    }

    void IEditorPage.RegisterReport<R>()
    {
        var report = services.GetRequiredService<R>();
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

    #endregion

    private void UpdateAcceptButtons()
    {
        buttonAccept.Visible = Editor.AcceptSupported;
        buttonAcceptAndClose.Visible = Editor.AcceptSupported;
    }

    private bool Accept()
    {
        if (Save(false))
        {
            try
            {
                Editor.Accept();

                var app = services.GetRequiredService<IHostApp>();
                app.SendNotify(MessageDestination.Object, Editor.DocumentInfo.GetType().Name.Underscore(), Editor.DocumentInfo.Id, MessageAction.Refresh);

                return true;
            }
            catch (RepositoryException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        return false;
    }

    private void RefreshInfo(IDocumentInfo document)
    {
        textId.Text = document.Id.ToString();
        dateTimeCreate.Value = document.DateCreated;
        dateTimeUpdate.Value = document.DateUpdated;

        var users = services.GetRequiredService<IUserAliasRepository>();
        textAuthor.Text = users.Get(document.UserCreatedId).Name;
        textEditor.Text = users.Get(document.UserUpdatedId).Name;
    }

    private void RefreshDocumentRefs(Guid id)
    {
        var repository = services.GetRequiredService<IDocumentRefsRepository>();

        if (repository.HasPrivilege(Privilege.Select))
        {
            var list = repository.GetByOwner(id);
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

    private void EditDocumentRefs()
    {
        if (gridDocuments.CurrentItem is DocumentRefs refs)
        {
            var dialog = services.GetRequiredService<IDocumentRefDialog>();
            if (dialog.Edit(refs))
            {
                services
                    .GetRequiredService<IDocumentRefsRepository>()
                    .Update(refs);

                gridDocuments.Refresh();
            }
        }
    }

    /*private void OpenReport(IReport report)
    {
        T doc = document ?? CreateDocument();

        UpdateDocument(doc);

        report.Show(doc);
    }*/

    private void OpenReportClick(object? sender, EventArgs e)
    {
        if (sender is ToolStripItem tool && tool.Tag is IReport report)
        {
            //OpenReport(report);
            report.Show(Editor.DocumentInfo);
        }
    }

    private void App_OnAppNotify(object? sender, NotifyEventArgs e)
    {
        switch (e.Action)
        {
            case MessageAction.Refresh:
                switch (e.Destination)
                {
                    case MessageDestination.Object:
                        if (e.ObjectId == Editor.DocumentInfo.Id && dockingManager.IsVisibility(this))
                        {
                            Editor.Reload();
                        }

                        break;
                }

                break;
        }
    }

    private void Editor_HeaderChanged(object? sender, EventArgs e)
    {
        Text = Editor.Header;
        dockingManager.UpdateHeader(this);
    }

    private void GridDocuments_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
    {
        switch (e.Column.MappingName)
        {
            case nameof(DocumentRefs.Id):
            case nameof(DocumentRefs.OwnerId):
                e.Cancel = true;
                break;
            case nameof(DocumentRefs.FileLength):
                e.Column = new GridFileSizeColumn() { MappingName = nameof(DocumentRefs.FileLength), HeaderText = "Размер", Width = 150 };
                break;
            case nameof(DocumentRefs.Note):
                e.Column.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
                break;
            case nameof(DocumentRefs.FileName):
                e.Column.Width = 300;
                break;
            case nameof(DocumentRefs.ThumbnailExist):
                e.Column.Width = 100;
                break;
        }
    }

    private void ButtonRefresh_Click(object sender, EventArgs e) => RefreshPage();

    private void ButtonOpenDoc_Click(object sender, EventArgs e)
    {
        if (gridDocuments.CurrentItem is DocumentRefs refs && refs.S3object != null)
        {
            FileHelper.OpenFile(services, refs.FileName, BacketName, refs.S3object);
        }
    }

    private void ButtonAddDoc_Click(object sender, EventArgs e)
    {
        if (Editor.DocumentInfo.Id == Guid.Empty)
        {
            MessageBox.Show("Для добавления сопутствующих документов необходимо сначала сохранить текущий", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var dialog = services.GetRequiredService<IDocumentRefDialog>();
        if (dialog.Create(Editor.DocumentInfo.Id, out var refs))
        {
            refs.S3object = $"{refs.OwnerId}_{refs.FileName}";

            using var s3 = services.GetRequiredService<IS3Object>();
            s3.PutObject(BacketName, refs.S3object, dialog.FileNameWithPath);

            if (dialog.CreateThumbnailImage)
            {
                refs.CreateThumbnailImage(dialog.FileNameWithPath, settings.ImageSize);
            }

            var res = services
                .GetRequiredService<IDocumentRefsRepository>()
                .Add(refs);

            if (gridDocuments.DataSource is IList<DocumentRefs> list)
            {
                list.Add(res);
            }
        }
    }

    private void ButtonEditDoc_Click(object sender, EventArgs e) => EditDocumentRefs();

    private void ButtonDeleteDoc_Click(object sender, EventArgs e)
    {
        if (gridDocuments.CurrentItem is DocumentRefs refs && refs.FileName != null)
        {
            if (MessageBox.Show($"Вы действительно хотите удалить документ?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var repo = services.GetRequiredService<IDocumentRefsRepository>();
                repo.Wipe(refs);

                if (!string.IsNullOrEmpty(refs.S3object))
                {
                    using var s3 = services.GetRequiredService<IS3Object>();
                    s3.RemoveObject(BacketName, refs.S3object);
                }

                if (gridDocuments.DataSource is IList<DocumentRefs> list)
                {
                    list.Remove(refs);
                }
            }
        }
    }

    private void GridDocuments_CellDoubleClick(object sender, CellClickEventArgs e) => EditDocumentRefs();

    private void ButtonSave_Click(object sender, EventArgs e) => Save(true);

    private void ButtonSaveAndClose_Click(object sender, EventArgs e)
    {
        if (Save(true))
        {
            dockingManager.Close(this);
        }
    }

    private void ButtonDocumentRefresh_Click(object sender, EventArgs e) => RefreshDocumentRefs(Editor.DocumentInfo.Id);

    private void ButtonAccept_Click(object sender, EventArgs e) => Accept();

    private void ButtonAcceptAndClose_Click(object sender, EventArgs e)
    {
        if (Accept())
        {
            dockingManager.Close(this);
        }
    }

    private async void ButtonSaveDoc_Click(object sender, EventArgs e)
    {
        if (gridDocuments.CurrentItem is DocumentRefs refs && refs.FileName != null && refs.S3object != null)
        {
            saveFileDialog1.FileName = refs.FileName;
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using var s3 = services.GetRequiredService<IS3Object>();
                await s3
                    .GetObject(BacketName, refs.S3object, saveFileDialog1.FileName)
                    .ContinueWith(task =>
                    {
                        ToastOperations.DownloadFileCompleted(saveFileDialog1.FileName);
                    });
            }
        }
    }
}
