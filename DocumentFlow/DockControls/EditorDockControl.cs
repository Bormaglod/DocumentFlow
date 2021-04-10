//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.06.2019
// Time: 18:44
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
#if USE_LISTENER
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
#endif
using Dapper;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;
using WeifenLuo.WinFormsUI.Docking;
using DocumentFlow.Controls.Editor.Code;
using DocumentFlow.Core;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Code;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.Implementation;
using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;
using DocumentFlow.Data.Repositories;
using DocumentFlow.Interfaces;
using DocumentFlow.Properties;
using DocumentFlow.Reports;

namespace DocumentFlow
{
    public partial class EditorDockControl : DockContent, IContentPage, IDependentViewer
    {
        public class GridFileSizeColumn : GridNumericColumn
        {
            protected override object GetFormattedValue(object record, object value)
            {
                if (decimal.TryParse(value.ToString(), out var originalValue))
                {
                    long kb = Convert.ToInt64(originalValue / 1024);
                    return $"{kb} КБ";
                }

                return value;
            }
        }

        private readonly ICommandFactory commandFactory;
        private readonly IContainerPage containerPage;
        private readonly IBrowserParameters parameters;
        private readonly Command command;
        private Guid current;
        private object entity;
        private Status status;
        private ContainerData controlContainer;
        private System.ComponentModel.BindingList<DocumentRefs> documents;
        private readonly List<ViewerControl> childs = new();
        private bool creatingPage;
        private readonly string ftpPath;
        private readonly DocumentRefEditorForm refEditor;
        private EditorData editorData;
        private readonly IBrowser ownerBrowser;
        private IDatabase database;
        private readonly List<ToolStripItem> actionItems = new();
#if USE_LISTENER
        private readonly CancellationTokenSource listenerToken;
        private readonly ConcurrentQueue<NotifyMessage> notifies = new();
#endif

        public EditorDockControl(IContainerPage container, IBrowser browser, ICommandFactory commandFactory, Guid id, Command commandEditor, IBrowserParameters browserParameters)
        {
            InitializeComponent();

            ownerBrowser = browser;
            if (commandEditor.Editor() == null)
            {
                throw new MissingImpException($"В команде [{commandEditor.name}] не определен редактор.");
            }

            this.commandFactory = commandFactory;

            containerPage = container;
            parameters = browserParameters;
            command = commandEditor;
            current = id;

            CreateEditor();
            CreatePageControls();

            ftpPath = Path.Combine(Settings.Default.FtpPath, command.EntityKind.code, current.ToString());
            refEditor = new DocumentRefEditorForm(ftpPath);

            containerPage.Twain.CapturingImage += Twain_CapturingImage;

#if USE_LISTENER
            timerDatabaseListen.Start();

            listenerToken = new CancellationTokenSource();
            _ = CreateListener(listenerToken.Token);
#endif
        }

        public static string Code(Guid id) => $"editor [{id}]";

        #region IContentPage

        Guid IContentPage.CommandId => command.id;

        #endregion

        #region IPage implementation

        string IPage.Code => Code(current);

        Guid IPage.Id => current;

        string IPage.Header => Text;

        IContainerPage IPage.Container => containerPage;

        void IPage.Rebuild()
        {
            controlContainer.Clear();
            ClearActionButtons();

            List<TabSplitterPage> removedTabs = tabSplitterContainer1.SecondaryPages
                .OfType<TabSplitterPage>()
                .Where(x => x.Tag != null && x.Tag.ToString() != "system")
                .ToList();
            foreach (var tab in removedTabs)
            {
                tabSplitterContainer1.SecondaryPages.Remove(tab);
            }

            CreatePageControls();
        }

        #endregion

        #region IDependentViewer implementation

        void IDependentViewer.AddDependentViewer(string commandCode)
        {
            using var conn = Db.OpenConnection();
            var command = commandFactory.Commands.SingleOrDefault(x => x.code == commandCode);
            if (command == null)
            {
                return;
            }

            var viewer = new ViewerControl(BrowserMode.Dependent)
            {
                CommandFactory = commandFactory,
                ExecutedCommand = command,
                OwnerId = current,
                Dock = DockStyle.Fill
            };

            try
            {
                viewer.InitializeViewer();

                var page = new TabSplitterPage
                {
                    Text = command.name
                };

                childs.Add(viewer);
                page.Controls.Add(viewer);

                tabSplitterContainer1.SecondaryPages.Insert(tabSplitterContainer1.SecondaryPages.Count - 2, page);
                tabSplitterContainer1.SecondaryPages.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                ExceptionHelper.MesssageBox(e);
            }
        }

        void IDependentViewer.AddDependentViewers(string[] commandCodes)
        {
            IDependentViewer editor = this;
            for (int i = 0; i < commandCodes.Length; i++)
            {
                editor.AddDependentViewer(commandCodes[i]);
            }
        }

        #endregion

#if USE_LISTENER
        private void Listener(CancellationToken token)
        {
            using var conn = new NpgsqlConnection(Db.ConnectionString);
            conn.Open();
            conn.Notification += (o, e) =>
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                        {
                            new JsonStringEnumConverter()
                        }
                };
                NotifyMessage message = JsonSerializer.Deserialize<NotifyMessage>(e.Payload, options);
                if (message.Destination == MessageDestination.Object && message.ObjectId == current)
                {
                    notifies.Enqueue(message);
                }
            };

            using (var cmd = new NpgsqlCommand("LISTEN db_notification", conn))
            {
                cmd.ExecuteNonQuery();
            }

            while (!token.IsCancellationRequested)
            {
                conn.Wait();
            }

            while (conn.FullState.HasFlag(ConnectionState.Fetching)) ;

            using (var cmd = new NpgsqlCommand("UNLISTEN db_notification", conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private async Task CreateListener(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;

            try
            {
                await Task.Run(() => Listener(token), token);
            }
            catch (TaskCanceledException)
            {
            }
        }
#endif

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
#if USE_LISTENER
            listenerToken.Cancel();
            timerDatabaseListen.Stop();
            notifies.Clear();
#endif

            foreach (var viewer in childs)
            {
                viewer.OnClosing();
            }

            using var conn = Db.OpenConnection();
            using var transaction = conn.BeginTransaction();
            try
            {
                conn.Execute("unlock_document", new { document_id = current }, transaction, commandType: CommandType.StoredProcedure);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show($"При попытке сохранить запись получено сообщение об ошибке: [{ExceptionHelper.Message(ex)}]. Вы по прежнему хотите закрыть окно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private IInformation GetInformation()
        {
            return new Information
            {
                Id = current,
                Created = dateTimeCreate.Value,
                Changed = dateTimeUpdate.Value,
                Author = textBoxCreator.Text,
                Editor = textBoxUpdater.Text,
                StatusCode = status.code
            };
        }

        private void CreateEditor()
        {
            if (command.Editor() is IDataOperation operation)
            {
                using var conn = Db.OpenConnection();
                using var transaction = conn.BeginTransaction();
                try
                {
                    if (current == Guid.Empty)
                    {
                        current = (Guid)operation.Insert(conn, transaction, parameters, editorData);
                    }

                    conn.Execute("lock_document", new { document_id = current }, transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ExceptionHelper.MesssageBox(e);
                }
            }

            var fileSizeColumn = new GridFileSizeColumn
            {
                AllowEditing = false,
                AllowGrouping = false,
                AllowResizing = true,
                HeaderText = "Размер",
                MappingName = "length",
                Width = 150D
            };

            gridDocuments.Columns.Add(fileSizeColumn);

            database = new Database();
        }

        /// <summary>
        /// Получает одну строку из БД используя запрос указанный в editor.Dataset.Select.
        /// Запрос должен возвращать одну и только одну запись и иметь параметр id.
        /// Если запрос выполнен успешно, то в переменной row будет находится соответствующая строка данных,
        /// а переменная context содержать значения всех полей этой строки
        /// </summary>
        private void ReadDataEntity()
        {
            if (command.Editor() is IDataOperation operation)
            {
                using var conn = Db.OpenConnection();
                entity = operation.Select(conn, Identifier.Get(current), parameters);
                if (editorData != null)
                {
                    editorData.Entity = entity as IIdentifier;
                }
            }
        }

        private void PopulateControls()
        {
            using var conn = Db.OpenConnection();
            IControlVisible visible = null;
            IControlEnabled enabled = null;
            if (!creatingPage)
            {
                visible = command.Editor() as IControlVisible;
                enabled = command.Editor() as IControlEnabled;
            }

            ((IContainer)controlContainer).Populate(conn, entity, enabled, visible);
        }

        private void UpdateCurrentStatusInfo()
        {
            switch (entity)
            {
                case IDirectory directory:
                    TabText = $"{command.EntityKind.name} - {directory.name}";
                    break;
                case IDocument document:
                    TabText = $"{command.EntityKind.name} {document.document_name}";
                    break;
                default:
                    return;
            }

            using var conn = Db.OpenConnection();
            var info = command.EntityKind.Get(conn, current);
            if (info == null)
                throw new RecordNotFoundException(current);

            status = Statuses.Get(conn, info.status_id);

            buttonStatus.Image = status.Picture.GetImageLarge();
            buttonStatus.Text = status.note;

            dateTimeCreate.Value = info.date_created;
            dateTimeUpdate.Value = info.date_updated;

            textBoxCreator.Text = info.user_created;
            textBoxUpdater.Text = info.user_updated;

            textBoxID.Text = current.ToString();
        }

        private void ClearActionButtons()
        {
            foreach (var item in actionItems)
            {
                toolStripMain.Items.Remove(item);
            }

            actionItems.Clear();
        }

        private void CreateActionButtons()
        {
            ClearActionButtons();
            using (var conn = Db.OpenConnection())
            {
                var list = ChangingStatuses.Get(conn, command.EntityKind, status);

                foreach (var cs in list)
                {
                    if (!ChangingStatuses.AccessAllowed(conn, current, cs))
                    {
                        continue;
                    }

                    var button = new ToolStripButton
                    {
                        Image = cs.Picture?.GetImageLarge(),
                        Text = cs.name,
                        DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                        ImageScaling = ToolStripItemImageScaling.None,
                        TextImageRelation = TextImageRelation.ImageAboveText,
                        Tag = cs
                    };

                    button.Click += ActionButton_Click;

                    toolStripMain.Items.Insert(toolStripMain.Items.IndexOf(toolStripSeparator5), button);
                    actionItems.Add(button);
                }
            }

            toolStripSeparator5.Visible = actionItems.Count > 0;
        }

        private void ActionButton_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripButton button && button.Tag is ChangingStatus cs && UpdateEntity())
            {
                using var conn = Db.OpenConnection();
                using var transaction = conn.BeginTransaction();
                try
                {
                    if (command.Editor() is IChangingStatus changingStatus)
                    {
                        if (!changingStatus.CanChange(database, entity, cs.FromStatus.code, cs.ToStatus.code))
                        {
                            return;
                        }
                    }

                    conn.Execute($"update {command.EntityKind.code} set status_id = :status_id where id = :id", new { status_id = cs.to_status_id, id = current }, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionHelper.MesssageBox(ex);
                }
            }

            RefreshPage();
        }

        private bool UpdateEntity()
        {
            if (entity == null)
            {
                return false;
            }

            if (command.Editor() is IDataOperation operation)
            {
                using var conn = Db.OpenConnection();
                using var transaction = conn.BeginTransaction();
                try
                {
                    operation.Update(conn, transaction, editorData);
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ExceptionHelper.MesssageBox(e);
                }
            }

            return false;
        }

        private void EditDocumentRefs()
        {
            if (gridDocuments.CurrentItem is DocumentRefs refs)
            {
                try
                {
                    refEditor.Edit(refs);
                }
                catch (CanceledException ex)
                {
                    if (ex.NeedRemoveReference)
                    {
                        documents.Remove(refs);
                    }

                    return;
                }
                catch (Exception e)
                {
                    ExceptionHelper.MesssageBox(e);
                    return;
                }

                using var conn = Db.OpenConnection();
                using var transaction = conn.BeginTransaction();
                try
                {
                    refs.Update(transaction);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ExceptionHelper.MesssageBox(e);
                }

                gridDocuments.Refresh();
            }
        }

        private void AddDocument(DocumentRefs refs)
        {
            using var conn = Db.OpenConnection();
            using var transaction = conn.BeginTransaction();
            try
            {
                refs.Insert(transaction);
                transaction.Commit();

                documents.Add(refs);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ExceptionHelper.MesssageBox(ex);
            }
        }

        private void SaveDocument(PdfDocument pdf)
        {
            if (pdf == null)
                return;

            string source = Path.Combine(Path.GetTempPath(), $"scan_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.pdf");
            pdf.Save(source);

            try
            {
                DocumentRefs refs = refEditor.Create(current, source);
                if (refs != null)
                {
                    AddDocument(refs);
                }
            }
            catch (CanceledException)
            {
                return;
            }
        }

        private void CreateChildDataGrids()
        {
            documents = DocumentReferences.Get(current);
            gridDocuments.DataSource = documents;
        }

        private void RefreshPage()
        {
            ReadDataEntity();
            SuspendLayout();
            try
            {
                UpdateCurrentStatusInfo();
                PopulateControls();
                CreateActionButtons();
                if (command.Editor() is IActionStatus action)
                {
                    action.ActionStatusChanged(editorData, database, GetInformation(), ActionStatus.Refresh);
                }
            }
            finally
            {
                ResumeLayout();
                PerformLayout();
            }

            CreateChildDataGrids();
        }

        private void CreatePrintedForms()
        {
            using var conn = Db.OpenConnection();
            IReportCollection forms = ((IEditor)editorData).Reports;
            if (forms.Forms.Any())
            {
                foreach (IReportForm f in forms.Forms)
                {
                    var form = PrintedForms.Get(conn, f.Name);
                    if (form == null)
                    {
                        continue;
                    }

                    Image image = null;
                    if (form.Picture != null)
                    {
                        image = form.Picture.GetImageSmall();
                    }
                    else
                    {
                        image = Resources.icons8_preview_16;
                    }

                    var item = new ToolStripMenuItem(form.name, image, buttonPrint_ButtonClick)
                    {
                        Tag = new Tuple<IReportForm, PrintedForm>(f, form)
                    };

                    buttonPrint.DropDownItems.Add(item);

                    if (f == forms.Default)
                    {
                        buttonPrint.Tag = new Tuple<IReportForm, PrintedForm>(f, form);
                    }
                }
            }
            else
                buttonPrint.Enabled = false;
        }

        private void CreatePageControls()
        {
            ReadDataEntity();

            creatingPage = true;
            tabSplitterMaster.SuspendLayout();
            try
            {
                UpdateCurrentStatusInfo();
                controlContainer = new ContainerData(tabSplitterMaster, GetInformation);
                editorData = new EditorData(controlContainer, ownerBrowser, commandFactory, parameters, toolStripMain)
                {
                    Entity = entity as IIdentifier,
                    EditorCode = command.Editor(),
                    GetInfo = GetInformation
                };

                command.Editor().Initialize(editorData, database, this);
                PopulateControls();
                CreateActionButtons();
                CreatePrintedForms();
                if (command.Editor() is IActionStatus action)
                {
                    action.ActionStatusChanged(editorData, database, GetInformation(), ActionStatus.Initialize);
                }
            }
            finally
            {
                tabSplitterMaster.ResumeLayout();
                tabSplitterMaster.PerformLayout();
                creatingPage = false;
            }

            CreateChildDataGrids();
        }

        private void CreatePdfDocument(IList<Image> images)
        {
            if (!images.Any())
                return;

            using var pdf = new PdfDocument();
            pdf.PageSettings.Margins.All = 0;

            foreach (Image image in images)
            {
                PdfSection section = pdf.Sections.Add();
                section.PageSettings.Size = PdfPageSize.A4;
                section.PageSettings.Width = Length.FromDpi(image.Width, image.HorizontalResolution).ToPoint();
                section.PageSettings.Height = Length.FromDpi(image.Height, image.VerticalResolution).ToPoint();

                PdfPage page = section.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                PdfBitmap pdfImage = new(image);
                graphics.DrawImage(pdfImage, 0, 0);
            }

            SaveDocument(pdf);
        }

        private void Twain_CapturingImage(object sender, CupturingImageEventArgs e)
        {
            if (e.DestinationId != current)
                return;

            CreatePdfDocument(new Image[] { e.Image });
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            UpdateEntity();
            UpdateCurrentStatusInfo();
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            UpdateEntity();
            Close();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var refs = refEditor.Create(current);
                AddDocument(refs);
            }
            catch (CanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                ExceptionHelper.MesssageBox(ex);
                return;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e) => EditDocumentRefs();

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (gridDocuments.CurrentItem is DocumentRefs refs)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить файл?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        refEditor.Delete(refs);
                    }
                    catch (CanceledException)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        ExceptionHelper.MesssageBox(ex);
                        return;
                    }

                    documents.Remove(refs);
                }
            }
        }

        private void buttonOpenFolder_Click(object sender, EventArgs e) => Process.Start($"ftp://{Settings.Default.FtpHost}{ftpPath}");

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (gridDocuments.CurrentItem is DocumentRefs refs)
            {
                try
                {
                    string file = refEditor.GetLocalFileName(refs);
                    Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });
                }
                catch (CanceledException ex)
                {
                    if (ex.NeedRemoveReference)
                    {
                        documents.Remove(refs);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHelper.MesssageBox(ex);
                }
            }
        }

        private void gridDocuments_CellDoubleClick(object sender, CellClickEventArgs e) => EditDocumentRefs();

        private void buttonRefresh_Click(object sender, EventArgs e) => RefreshPage();

        private void timerDatabaseListen_Tick(object sender, EventArgs e)
        {
#if USE_LISTENER
            if (notifies.TryDequeue(out NotifyMessage message))
            {
                switch (message.Action)
                {
                    case "refresh":
                        RefreshPage();
                        break;
                    case "delete":
                        Close();
                        break;
                    default:
                        break;
                }
            }
#endif
        }

        private void buttonStatus_Click(object sender, EventArgs e) => HistoryForm.ShowWindow(current);

        private void buttonPrint_ButtonClick(object sender, EventArgs e)
        {
            if (sender is ToolStripItem item && item.Tag is Tuple<IReportForm, PrintedForm> forms)
            {
                using Report report = Report.FromText(forms.Item2.schema_form);
                foreach (var cmd in report.ReportDictionary.Parameters)
                {
                    cmd.Value = forms.Item1.GetParameter(cmd.Name);
                }

                PdfDocument doc = report.GeneratePdf(forms.Item2.name);
                using MemoryStream stream = new();
                doc.Save(stream);
                using PdfLoadedDocument loadedDocument = new(stream);
                PreviewForm.PreviewPdf(current, loadedDocument, forms.Item2.name, doc.DocumentInformation.Title);
            }
        }

        private void buttonCustomization_Click(object sender, EventArgs e) => commandFactory.OpenCodeEditor(command);

        private void buttonScan_Click(object sender, EventArgs e)
        {
            try
            {
                containerPage.Twain.Capture(current);
            }
            catch (Exception ex)
            {
                ExceptionHelper.MesssageBox(ex);
            }
        }

        private void tabSplitterMaster_VisibleChanged(object sender, EventArgs e)
        {
            if (editorData.ToolBar is ToolBarData toolBar)
            {
                toolBar.UpdateButtonVisibleStatus();
            }
        }

        private void buttonMultipleScan_Click(object sender, EventArgs e)
        {
            TwainForm twainForm = new (containerPage.Twain);
            if (twainForm.ShowDialog() == DialogResult.OK)
            {
                CreatePdfDocument(twainForm.Images);
            }
        }
    }
}
