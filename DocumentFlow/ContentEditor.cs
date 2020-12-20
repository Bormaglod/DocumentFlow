﻿//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
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
    using System.Threading;
    using System.Threading.Tasks;
    using Npgsql;
#endif
using Dapper;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;
using DocumentFlow.Core;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Code;
using DocumentFlow.Code.Data;
using DocumentFlow.Code.System;
using DocumentFlow.Code.Implementation;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;
using DocumentFlow.Printing;
using DocumentFlow.Properties;

namespace DocumentFlow
{
    public partial class ContentEditor : ToolWindow, IPage, IDependentViewer
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
        private readonly List<ViewerControl> childs = new List<ViewerControl>();
        private bool creatingPage;
        private readonly string ftpPath;
        private readonly DocumentRefEditor refEditor;
        private EditorData editorData;
        private readonly IBrowser ownerBrowser;
#if USE_LISTENER
        private CancellationTokenSource listenerToken;
        private readonly ConcurrentQueue<NotifyMessage> notifies = new ConcurrentQueue<NotifyMessage>();
#endif

        public ContentEditor(IContainerPage container, IBrowser browser, ICommandFactory commandFactory, Guid id, Command commandEditor, IBrowserParameters browserParameters)
        {
            InitializeComponent();

            ownerBrowser = browser;
            if (commandEditor.Editor() == null)
            {
                throw new Exception($"В команде [{commandEditor.name}] не определен редактор.");
            }

            this.commandFactory = commandFactory;

            containerPage = container;
            parameters = browserParameters;
            command = commandEditor;
            current = id;

            controlContainer = new ContainerData(tabSplitterMaster);

            CreateEditor();
            CreatePageControls();
            CreateChildDataGrids();

#if USE_LISTENER
            timerDatabaseListen.Start();

            listenerToken = new CancellationTokenSource();
            _ = CreateListener(listenerToken.Token);
#endif

            ftpPath = Path.Combine(Settings.Default.FtpPath, command.EntityKind.code, current.ToString());
            refEditor = new DocumentRefEditor(ftpPath);

            using (var conn = Db.OpenConnection())
            {
                string sql = "select * from print_kind_form pkf join print_form pf on (pf.id = pkf.print_form_id) where entity_kind_id = :id";
                var forms = conn.Query<PrintKindForm, PrintForm, PrintKindForm>(sql, (kind, form) =>
                {
                    kind.PrintForm = form;
                    return kind;
                }, new { id = command.entity_kind_id });

                if (forms.Any())
                {
                    foreach (var f in forms)
                    {
                        Image image = null;
                        if (f.PrintForm.picture_id.HasValue)
                        {
                            Picture p = conn.QuerySingle<Picture>("select * from picture where id = :id", new { id = f.PrintForm.picture_id });
                            image = p.GetImageSmall();
                        }
                        else
                            image = Resources.icons8_preview_16;

                        var item = new ToolStripMenuItem(f.PrintForm.name, image, buttonPrint_ButtonClick)
                        {
                            Tag = f.PrintForm
                        };

                        buttonPrint.DropDownItems.Add(item);
                    }

                    buttonPrint.Tag = forms.FirstOrDefault(x => x.default_form)?.PrintForm;
                }
                else
                    buttonPrint.Enabled = false;
            }
        }

        #region IPage implementation

        Guid IPage.Id => command.Id; 

        Guid IPage.InfoId => current;

        IContainerPage IPage.Container => containerPage;

        void IPage.Rebuild() 
        {
            controlContainer.Clear();
            panelItemActions.Items.Clear();

            List<TabSplitterPage> removedTabs = tabSplitterContainer1.SecondaryPages
                .OfType<TabSplitterPage>()
                .Where(x => x.Tag.ToString() != "system")
                .ToList();
            foreach (var tab in removedTabs)
            {
                tabSplitterContainer1.SecondaryPages.Remove(tab);
            }

            CreatePageControls();
        }

        #endregion

        void IDependentViewer.AddDependentViewer(string commandCode)
        {
            using (var conn = Db.OpenConnection())
            {
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
        }

        void IDependentViewer.AddDependentViewers(string[] commandCodes)
        {
            IDependentViewer editor = this;
            for (int i = 0; i < commandCodes.Length; i++)
            {
                editor.AddDependentViewer(commandCodes[i]);
            }
        }

#if USE_LISTENER
        private void Listener(CancellationToken token)
        {
            using (var conn = new NpgsqlConnection(Db.ConnectionString))
            {
                conn.Open();
                conn.Notification += (o, e) =>
                {
                    NotifyMessage message = JsonSerializer.Deserialize<NotifyMessage>(e.Payload);
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

                while (conn.FullState.HasFlag(System.Data.ConnectionState.Fetching)) ;

                using (var cmd = new NpgsqlCommand("UNLISTEN db_notification", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        async private Task CreateListener(CancellationToken token)
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
            base.OnClosing(e);
#if USE_LISTENER
            listenerToken.Cancel();
            timerDatabaseListen.Stop();
#endif

            foreach (var viewer in childs)
            {
                viewer.OnClosing();
            }

            using (var conn = Db.OpenConnection())
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute("unlock_document", new { document_id = current }, transaction, commandType: CommandType.StoredProcedure);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        if (MessageBox.Show($"При попытке сохранить запись получено сообщение об ошибке: [{ExceptionHelper.Message(ex)}]. Вы по прежнему хотите закрыть окно", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        private void CreateEditor()
        {
            using (var conn = Db.OpenConnection())
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        if (current == Guid.Empty)
                        {
                            current = command.Editor().Insert<Guid>(conn, transaction, parameters, editorData);
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
        }

        /// <summary>
        /// Получает одну строку из БД используя запрос указанный в editor.Dataset.Select.
        /// Запрос должен возвращать одну и только одну запись и иметь параметр id.
        /// Если запрос выполнен успешно, то в переменной row будет находится соответствующая строка данных,
        /// а переменная context содержать значения всех полей этой строки
        /// </summary>
        private void ReadDataEntity()
        {
            using (var conn = Db.OpenConnection())
            {
                entity = command.Editor().SelectById(conn, current, parameters);
                if (editorData != null)
                {
                    editorData.Entity = entity as IIdentifier;
                }
            }
        }

        private void PopulateControls()
        {
            using (var conn = Db.OpenConnection())
            {
                DataFieldParameter getVisible = null;
                if (!creatingPage)
                {
                    getVisible = (f) => { return command.Editor().GetVisibleValue(f, status.code); };
                }

                DataFieldParameter getEnable = (f) => { return command.Editor().GetEnabledValue(f, status.code); };

                ((IContainer)controlContainer).Populate(conn, entity, getEnable, getVisible);
            }
        }

        private void UpdateCurrentStatusInfo()
        {
            if (entity is IDirectory directory)
            {
                Text = $"{command.EntityKind.name} - {directory.name}";
            }
            else if (entity is IDocument document)
            {
                Text = $"{command.EntityKind.name} {document.document_name}";
            }
            else
            {
                return;
            }

            using (var conn = Db.OpenConnection())
            {
                var info = conn.Query($"select di.status_id, di.date_created, di.date_updated, uc.name as user_created, uu.name as user_updated from {command.EntityKind.code} di join user_alias uc on uc.id  = di.user_created_id join user_alias uu on uu.id  = di.user_updated_id where di.id = :id", new { id = current }).SingleOrDefault();
                if (info == null)
                    throw new RecordNotFoundException(current);

                status = conn.Query<Status, Picture, Status>("select * from status s left join picture p on (p.id = s.picture_id) where s.id = :id", (status, picture) =>
                {
                    status.Picture = picture;
                    return status;
                }, new { id = info.status_id }).SingleOrDefault();

                buttonStatus.Image = status.Picture.GetImageLarge();
                buttonStatus.Text = status.note;

                dateTimeCreate.Value = info.date_created;
                dateTimeUpdate.Value = info.date_updated;

                textBoxCreator.Text = info.user_created;
                textBoxUpdater.Text = info.user_updated;

                textBoxID.Text = current.ToString();
            }
        }

        private void CreateActionButtons()
        {
            panelItemActions.Items.Clear();
            using (var conn = Db.OpenConnection())
            {
                var list = conn.Query<ChangingStatus, Picture, ChangingStatus>("select * from changing_status cs left join picture p on (cs.picture_id = p.id) where cs.transition_id = :transition_id and cs.from_status_id = :status_id and not cs.is_system order by cs.order_index", (cs, picture) =>
                {
                    cs.Picture = picture;
                    return cs;
                }, new { command.EntityKind.transition_id, status_id = status.id }).ToList();

                foreach (var cs in list)
                {
                    bool access = conn.QuerySingle<bool>("select access_changing_status(:id, :changing_status_id)", new { id = current, changing_status_id = cs.Id });
                    if (!access)
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

                    panelItemActions.Items.Add(button);
                }
            }

            panelItemActions.Visible = panelItemActions.Items.Count > 0;
            toolStripSeparator5.Visible = panelItemActions.Items.Count > 0;
        }

        private void ActionButton_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripButton button && button.Tag is ChangingStatus cs && UpdateEntity())
            {
                using (var conn = Db.OpenConnection())
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            conn.Execute($"update {command.EntityKind.code} set status_id = :status_id where id = :id", new { status_id = cs.to_status_id, id = current }, transaction);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            ExceptionHelper.MesssageBox(ex);
                        }
                    }
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

            using (var conn = Db.OpenConnection())
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        command.Editor().Update(conn, transaction, editorData);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        ExceptionHelper.MesssageBox(e);
                    }

                }
            }

            return false;
        }

        private void EditDocumentRefs()
        {
            if (gridDocuments.CurrentItem is DocumentRefs refs)
            {
                if (refEditor.Edit(refs))
                {
                    using (var conn = Db.OpenConnection())
                    {
                        using (var transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                conn.Query("update document_refs set file_name = :file_name, note = :note, crc = :crc, length = :length where id = :id", refs, transaction);
                                transaction.Commit();
                            }
                            catch (Exception e)
                            {
                                transaction.Rollback();
                                ExceptionHelper.MesssageBox(e);
                            }
                        }
                    }
                }

                gridDocuments.Refresh();
            }
        }

        private void AddDocument(DocumentRefs refs)
        {
            using (var conn = Db.OpenConnection())
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Query("insert into document_refs (owner_id, file_name, note, crc, length) values (:owner_id, :file_name, :note, :crc, :length)", refs, transaction);
                        transaction.Commit();

                        documents.Add(refs);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ExceptionHelper.MesssageBox(ex);
                    }
                }
            }
        }

        private void SaveDocument(PdfDocument pdf)
        {
            if (pdf == null)
                return;

            string source = Path.GetTempPath() + "scan_" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString() + ".pdf";
            pdf.SaveToFile(source);

            DocumentRefs refs = refEditor.Create(current, source);
            if (refs != null)
            {
                AddDocument(refs);
            }
        }

        private void CreateChildDataGrids()
        {
            using (var conn = Db.OpenConnection())
            {
                documents = new System.ComponentModel.BindingList<DocumentRefs>(
                    conn.Query<DocumentRefs>("select * from document_refs where owner_id = :owner_id", new { owner_id = current }).ToList()
                    );

                gridDocuments.DataSource = documents;
            }
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
            }
            finally
            {
                ResumeLayout();
                PerformLayout();
            }

            CreateChildDataGrids();
        }

        private void CreatePageControls()
        {
            ReadDataEntity();

            creatingPage = true;
            tabSplitterMaster.SuspendLayout();
            try
            {
                
                editorData = new EditorData(controlContainer, ownerBrowser, commandFactory, parameters, toolStripMain)
                {
                    Entity = entity as IIdentifier
                };

                command.Editor().Initialize(editorData, this);
                UpdateCurrentStatusInfo();
                PopulateControls();
                CreateActionButtons();
            }
            finally
            {
                tabSplitterMaster.ResumeLayout();
                tabSplitterMaster.PerformLayout();
                creatingPage = false;
            }

            CreateChildDataGrids();
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
            var refs = refEditor.Create(current);
            if (refs == null)
            {
                return;
            }

            AddDocument(refs);
        }

        private void buttonEdit_Click(object sender, EventArgs e) => EditDocumentRefs();

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (gridDocuments.CurrentItem is DocumentRefs refs)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить файл?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    refEditor.Delete(refs);
                    documents.Remove(refs);
                }
            }
        }

        private void buttonOpenFolder_Click(object sender, EventArgs e) => Process.Start($"ftp://{Settings.Default.FtpHost}{ftpPath}");

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (gridDocuments.CurrentItem is DocumentRefs refs)
            {
                if (refEditor.GetLocalFileName(refs, out var file))
                {
                    Process.Start(file);
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

        private void buttonStatus_Click(object sender, EventArgs e) => HistoryWindow.ShowWindow(current);

        private void buttonPrint_ButtonClick(object sender, EventArgs e)
        {
            if (sender is ToolStripItem item && item.Tag is PrintForm form)
            {
                Printer.Preview(form, entity);
            }
        }

        private void buttonCustomization_Click(object sender, EventArgs e) => commandFactory.Execute("open-browser-code", command);

        private void twain32_AcquireCompleted(object sender, EventArgs e)
        {
            try
            {
                if (twain32.ImageCount > 0)
                {
                    PdfDocument pdf = new PdfDocument();
                    for (int i = 0; i < twain32.ImageCount; i++)
                    {
                        Image image = twain32.GetImage(i);
                        PdfImage pdfImage = PdfImage.FromImage(image);

                        PdfUnitConvertor uinit = new PdfUnitConvertor();

                        SizeF sizeImage = new SizeF(image.Width / image.HorizontalResolution, image.Height / image.VerticalResolution);
                        SizeF pageSize = UnitConverter.Convert(sizeImage, Core.GraphicsUnit.Inch, Core.GraphicsUnit.Point);

                        PdfPageBase page = pdf.Pages.Add(pageSize, new PdfMargins(0f));

                        page.Canvas.DrawImage(pdfImage, new PointF(0, 0));
                    }

                    SaveDocument(pdf);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.MesssageBox(ex);
            }
        }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            try
            {
                twain32.Acquire();
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
    }
}
