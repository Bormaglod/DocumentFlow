//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.06.2019
// Time: 18:44
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Flee.PublicTypes;
    using Newtonsoft.Json;
    using NHibernate;
    using NHibernate.Transform;
    using Npgsql;
    using Spire.Pdf;
    using Syncfusion.Windows.Forms.Tools;
    using Syncfusion.WinForms.DataGrid.Events;
    using DocumentFlow.Controls.Editor.Core;
    using DocumentFlow.Core;
    using DocumentFlow.Data.Core;
    using DocumentFlow.Data.Entities;
    using DocumentFlow.DataSchema;
    using DocumentFlow.Printing;
    using DocumentFlow.Properties;

    public partial class ContentEditor : UserControl, IPage
    {
        class FieldExpressionData
        {
            public IEditorExpression Editor { get; set; }
            public string Destination { get; set; }
            public IDynamicExpression Expression { get; set; }
            public string Query { get; set; }
        }

        private Guid current;
        private Guid? parent;
        private Guid? owner;
        private readonly EntityKind kind;
        private readonly DatasetEditor editor;
        private IDictionary row;
        private Status status;
        private readonly BindingList<DocumentRefs> documents;
        private readonly string destLocalPath;
        private readonly string destFtpPath;
        private readonly ICommandFactory commands;
        private ExpressionContext context;
        private List<string> locked = new List<string>();
        private CancellationTokenSource listenerToken;
        private readonly ConcurrentQueue<NotifyMessage> notifies = new ConcurrentQueue<NotifyMessage>();
        private List<FieldExpressionData> expressions = new List<FieldExpressionData>();
        private DocumentRefEditor refEditor;
        bool binding = false;

        public ContentEditor(ICommandFactory commandFactory, EditorParams parameters)
        {
            InitializeComponent();
            NewSession();

            kind = parameters.Kind;
            editor = parameters.Editor.Clone();
            current = parameters.Id;
            parent = parameters.Parent;
            owner = parameters.Owner;

            commands = commandFactory;

            if (string.IsNullOrEmpty(editor.Dataset.InsertDefault(kind.Code)))
                throw new DatasetCommandException("Не указана команда Insert.");

            if (string.IsNullOrEmpty(editor.Dataset.Update))
                throw new DatasetCommandException("Не указана команда Update.");

            PrepareEditor();
            RefreshPage(true);

            documents = new BindingList<DocumentRefs>(
                Session.QueryOver<DocumentRefs>()
                    .Where(x => x.OwnerId == current)
                    .List());
            gridDocuments.DataSource = documents;

            destLocalPath = Path.Combine(Settings.Default.DocumentsFolder, kind.Code, current.ToString());
            destFtpPath = Path.Combine(Settings.Default.FtpPath, kind.Code, current.ToString());
            refEditor = new DocumentRefEditor(destLocalPath, destFtpPath);

            IList<PrintForm> forms = Session.QueryOver<PrintKindForm>()
                    .Where(x => x.EntityKind == kind)
                    .Select(x => x.PrintForm)
                    .List<PrintForm>();

            if (forms.Any())
            {
                foreach (PrintForm f in forms)
                {
                    Image image = null;
                    if (f.Picture == null)
                        image = Resources.icons8_preview_16;
                    else
                        image = f.Picture.GetImageSmall();

                    ToolStripMenuItem item = new ToolStripMenuItem(f.Name, image, buttonPrint_ButtonClick)
                    {
                        Tag = f
                    };

                    buttonPrint.DropDownItems.Add(item);
                }

                buttonPrint.Tag = Session.QueryOver<PrintKindForm>()
                    .Where(x => x.EntityKind == kind && x.DefaultForm)
                    .Select(x => x.PrintForm)
                    .SingleOrDefault<PrintForm>();
            }
            else
                buttonPrint.Enabled = false;

            CreateChildDataGrids();

            timerDatabaseListen.Start();

            listenerToken = new CancellationTokenSource();
            CreateListener(listenerToken.Token);
        }

        Guid IPage.Id => current;

        Guid IPage.ContentId => current;

        void IPage.OnClosed()
        {
            listenerToken.Cancel();
            timerDatabaseListen.Stop();

            using (var transaction = Session.BeginTransaction())
            {
                try
                {
                    IQuery query = Session.CreateSQLQuery("select unlock_document(:document_id)")
                        .SetGuid("document_id", current);
                    query.ExecuteUpdate();

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    if (MessageBox.Show($"При попытке сохранить запись получено сообщение об ошибке: [{ExceptionHelper.Message(e)}]. Вы по прежнему хотите закрыть окно", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        throw;
                    }
                }
            }
        }

        protected ISession Session { get; private set; }

        private void NewSession() => Session = Db.OpenSession();

        private void Listener(CancellationToken token)
        {
            var conn = new NpgsqlConnection(Db.ConnectionString);
            conn.Open();
            conn.Notification += (o, e) =>
            {
                NotifyMessage message = JsonConvert.DeserializeObject<NotifyMessage>(e.Payload);
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
        }

        async private void CreateListener(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;

            await Task.Run(() => Listener(token));
        }

        private void PrepareEditor()
        {
            using (var transaction = Session.BeginTransaction())
            {
                try
                {
                    IQuery query;
                    if (current == Guid.Empty)
                    {
                        string insert = editor.Dataset.InsertDefault(kind.Code);

                        query = Session.CreateSQLQuery(insert);
                        if (owner != null)
                            Db.CreateQueryParameters(query, editor.GetTypes(), ("owner_id", owner));
                        current = query.UniqueResult<Guid>();

                        if (current == Guid.Empty)
                        {
                            throw new DatasetCommandException($"Запрос '{insert}' должен содержать выражение returning для получения значения первичного ключа.");
                        }

                        if (editor.Dataset.GenerateDefaultValue)
                        {
                            List<string> updates = new List<string>();
                            if (owner != null)
                                updates.Add("owner_id = :owner_id");

                            if (parent != null && kind.HasGroup)
                                updates.Add("parent_id = :parent_id");

                            if (updates.Any())
                            {
                                string update = $"update {editor.Dataset.Name ?? kind.Code} set {string.Join(",", updates)} where id = :id";
                                query = Session.CreateSQLQuery(update)
                                    .SetGuid("id", current);
                                if (owner != null)
                                    query.SetParameter("owner_id", owner);

                                if (parent != null && kind.HasGroup)
                                    query.SetParameter("parent_id", parent);

                                query.ExecuteUpdate();
                            }
                        }
                    }

                    query = Session.CreateSQLQuery("select lock_document(:document_id)")
                            .SetGuid("document_id", current);
                    query.ExecuteUpdate();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionHelper.MesssageBox(ex);
                }
            }
        }

        private object GetValueFromQuery(string queryText)
        {
            IDictionary row = Db.ExecuteSelect(Session, queryText, editor.GetTypes(), (x) => context.Variables.ContainsKey(x) ? context.Variables[x] : null).SingleOrDefault();
            if (row != null)
            {
                string key = row.Keys.OfType<string>().Single();
                return row[key];
            }

            return null;
        }

        /// <summary>
        /// Получает одну строку из БД используя запрос указанный в editor.Dataset.Select.
        /// Запрос должен возвращать одну и только одну запись и иметь параметр id.
        /// Если запрос выполнен успешно, то в переменной row будет находится соответствующая строка данных,
        /// а переменная context содержать значения всех полей этой строки
        /// </summary>
        private void ReadDataEntity()
        {
            row = Session.CreateSQLQuery(editor.Dataset.Select)
                .SetResultTransformer(Transformers.AliasToEntityMap)
                .SetGuid("id", current)
                .UniqueResult<IDictionary>();
            if (row == null)
                throw new Exception($"Запрос {editor.Dataset.Select} с id = {current} вернул NULL.");

            EditorContext ec = new EditorContext(Session, current, editor)
            {
                ActionPopupate = PopulateControl
            };

            context = new ExpressionContext(ec);
            ec.Variables = context.Variables;

            foreach (string field in row.Keys)
            {
                if (row[field] != null)
                    context.Variables.Add(field, row[field]);
            }

            expressions.Clear();
            foreach (IEditorExpression c in editor.GetControls().OfType<IEditorExpression>())
            {
                CreateExpressionList(c);
            }
        }

        private void CreateExpressionList(IEditorExpression editor)
        {
            if (editor.Expressions.Count == 0)
                return;

            for (int i = 0; i < editor.Expressions.Count; i++)
            {
                if (string.IsNullOrEmpty(editor.Expressions[i].Expression) && string.IsNullOrEmpty(editor.Expressions[i].SQLExpression))
                    continue;

                FieldExpressionData data = new FieldExpressionData()
                {
                    Editor = editor,
                    Destination = editor.Expressions[i].Destination,
                    Expression = string.IsNullOrEmpty(editor.Expressions[i].Expression) ? null : context.CompileDynamic(editor.Expressions[i].Expression),
                    Query = string.IsNullOrEmpty(editor.Expressions[i].SQLExpression) ? null : editor.Expressions[i].SQLExpression
                };

                expressions.Add(data);
            }
        }

        private void UnbindEvents()
        {
            if (binding)
            {
                foreach (IBindingEditorControl c in editor.GetControls().OfType<IBindingEditorControl>())
                {
                    c.ValueChanged -= BindingEditor_ValueChanged;
                }

                binding = false;
            }
        }

        private void BindEvents()
        {
            if (binding)
                return;

            foreach (IBindingEditorControl c in editor.GetControls().OfType<IBindingEditorControl>())
            {
                c.ValueChanged += BindingEditor_ValueChanged;
            }

            binding = true;
        }

        private bool GetConditionResult(string status_name, Condition condition, ExpressionContext context)
        {
            if (condition == null)
                return true;

            bool res = condition.States == null ? false : condition.States.Contains(status_name);
            if (!string.IsNullOrEmpty(condition.ExpressionIfEqual))
            {
                IGenericExpression<bool> expression = context.CompileGeneric<bool>(condition.ExpressionIfEqual);
                res = res || expression.Evaluate();
            }
            else if (!string.IsNullOrEmpty(condition.ExpressionIfEqualQuery))
            {
                object value = GetValueFromQuery(condition.ExpressionIfEqualQuery);
                if (value != null && value is bool exprValue)
                {
                    res = res || exprValue;
                }
                else
                    res = false;
            }

            return res;
        }

        private void PopulateControl(IEditorControl control, IDictionary data)
        {
            if (control is IPopulated populated)
            {
                populated.Populate(Session, data, editor.GetTypes(), status.Id);
            }

            foreach (ControlCondition condition in editor.Conditions)
            {
                if (condition.Controls.Contains(control.Name))
                {
                    if (condition.Enable != null)
                    {
                        control.Enabled = GetConditionResult(status.Code, condition.Enable, context);
                    }

                    if (condition.Visible != null)
                    {
                        control.Visible = GetConditionResult(status.Code, condition.Visible, context);
                    }
                }
            }
        }

        private void PopulateControls()
        {
            var controls = editor.GetControls();
            if (controls != null)
            {
                foreach (IEditorControl control in controls)
                {
                    PopulateControl(control, row);
                }
            }
        }

        private void UpdateCurrentStatusInfo()
        {
            string name = string.Empty;
            if (row.Contains("name"))
                name = row["name"]?.ToString() ?? string.Empty;

            Text = $"{kind.Name} - {name}";

            IDictionary info = Session.CreateSQLQuery($"select di.status_id, di.date_created, di.date_updated, uc.name as user_created, uu.name as user_updated from {editor.Dataset.Name ?? kind.Code} di join user_alias uc on uc.id  = di.user_created_id join user_alias uu on uu.id  = di.user_updated_id where di.id = :id")
                .SetGuid("id", current)
                .SetResultTransformer(Transformers.AliasToEntityMap)
                .UniqueResult<IDictionary>();

            if (info == null)
                throw new RecordNotFoundException(current);

            status = Session.Get<Status>(info["status_id"]);

            buttonStatus.Image = status.Picture.GetImageLarge();
            buttonStatus.Text = status.Note;

            dateTimeCreate.Value = (DateTime)info["date_created"];
            dateTimeUpdate.Value = (DateTime)info["date_updated"];

            textBoxCreator.Text = info["user_created"].ToString();
            textBoxUpdater.Text = info["user_updated"].ToString();

            textBoxID.Text = current.ToString();
        }

        private void CreateActionButtons()
        {
            panelItemActions.Items.Clear();

            IList<ChangingStatus> list = Session.QueryOver<ChangingStatus>()
                .Where(x => x.Transition == kind.Transition && x.FromStatus == status && !x.IsSystem)
                .OrderBy(x => x.OrderIndex).Asc
                .List();
            foreach (ChangingStatus cs in list)
            {
                bool access = Session.CreateSQLQuery("select access_changing_status(:id, :changing_status_id)")
                     .SetGuid("id", current)
                     .SetGuid("changing_status_id", cs.Id)
                     .UniqueResult<bool>();

                if (!access)
                    continue;

                ToolStripButton button = new ToolStripButton()
                {
                    Image = cs.Picture?.GetImageLarge(),
                    Text = cs.Name,
                    DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                    ImageScaling = ToolStripItemImageScaling.None,
                    TextImageRelation = TextImageRelation.ImageAboveText,
                    Tag = cs
                };

                button.Click += ActionButton_Click;

                panelItemActions.Items.Add(button);
            }

            panelItemActions.Visible = panelItemActions.Items.Count > 0;
            toolStripSeparator5.Visible = panelItemActions.Items.Count > 0;
        }

        private void BindingEditor_ValueChanged(object sender, EventArgs e)
        {
            IBindingEditorControl editor_control = (IBindingEditorControl)sender;
            if (locked.Contains(editor_control.DataField))
                return;

            locked.Add(editor_control.DataField);
            try
            {
                context.Variables[editor_control.DataField] = editor_control.Value;
                foreach (FieldExpressionData data in expressions.Where(x => x.Editor == editor_control))
                {
                    IBindingEditorControl result = null;
                    if (!string.IsNullOrEmpty(data.Destination))
                    {
                        result = editor
                            .GetControls()
                            .OfType<IBindingEditorControl>()
                            .FirstOrDefault(x => string.Compare(x.DataField, data.Destination, true) == 0);
                        if (result == null)
                        {
                            throw new Exception($"В схеме указано поле {data.Destination}, но оно отсутсвует в реализации класса {data.Expression.Owner.GetType().Name}");
                        }
                    }

                    if (data.Expression != null)
                    {
                        if (result != null)
                            result.Value = data.Expression.Evaluate();
                        else
                            data.Expression.Evaluate();
                    }
                    else
                    {
                        object value = GetValueFromQuery(data.Query);
                        if (value != null)
                        {
                            result.Value = value;
                        }
                    }

                    if (result != null && result.Value != null)
                        context.Variables[result.DataField] = result.Value;
                }
            }
            finally
            {
                locked.Remove(editor_control.DataField);
            }
        }

        private void ActionButton_Click(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            ChangingStatus cs = button.Tag as ChangingStatus;

            if (UpdateEntity())
            {
                using (var transaction = Session.BeginTransaction())
                {
                    try
                    {
                        string name = editor.Dataset.Name ?? kind.Code;
                        Session.CreateSQLQuery($"update {name} set status_id = :status_id where id = :id")
                            .SetInt32("status_id", cs.ToStatus.Id)
                            .SetGuid("id", current)
                            .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ExceptionHelper.MesssageBox(ex);
                    }
                }
            }

            RefreshPage(false);
        }

        private bool UpdateEntity()
        {
            using (var transaction = Session.BeginTransaction())
            {
                IQuery query = Session.CreateSQLQuery(editor.Dataset.Update);

                var controls = editor.GetControls().OfType<IBindingEditorControl>().ToList();
                foreach (Match match in Regex.Matches(editor.Dataset.Update, "(?<!:):([a-zA-Z_]+)"))
                {
                    string prop = match.Groups[1].Value;
                    if (prop == "id")
                    {
                        query.SetGuid("id", current);
                        continue;
                    }

                    var c = controls.FirstOrDefault(x => x.DataField == prop);
                    if (c == null)
                    {
                        if (prop == "parent_id")
                            Db.SetQueryParameter(query, prop, parent, parent.GetType());
                        else if (prop == "owner_id")
                            Db.SetQueryParameter(query, prop, owner, owner.GetType());
                        else
                            throw new Exception($"В запросе '{editor.Dataset.Update}' присутствует поле {prop} значение для которого не установлено.");
                    }
                    else
                    {
                        Db.SetQueryParameter(query, prop, c.Value, c.ValueType);
                    }
                }

                try
                {
                    query.ExecuteUpdate();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionHelper.MesssageBox(ex);
                }
            }

            return false;
        }

        private void EditDocumentRefs()
        {
            DocumentRefs refs = gridDocuments.CurrentItem as DocumentRefs;
            if (refs == null)
                return;

            var result = refEditor.Edit(refs);
            using (var transaction = Session.BeginTransaction())
            {
                try
                {

                    switch (result)
                    {
                        case DocumentRefEditor.EditingResult.Ok:
                            Session.SaveOrUpdate(refs);
                            transaction.Commit();
                            break;
                        case DocumentRefEditor.EditingResult.RemovalRequired:
                            Session.Delete(refs);
                            transaction.Commit();
                            documents.Remove(refs);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ExceptionHelper.MesssageBox(e);
                }
            }

            gridDocuments.Refresh();
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
                documents.Add(refs);
            }
        }

        private void CreateChildDataGrids()
        {
            if (editor?.Childs == null)
                return;

            foreach (ChildViewerData child in editor.Childs)
            {
                EntityKind e = Session.QueryOver<EntityKind>().Where(x => x.Code == child.Name).SingleOrDefault();
                if (e == null)
                    continue;

                if (!string.IsNullOrEmpty(child.Visible))
                {
                    IGenericExpression<bool> expression = context.CompileGeneric<bool>(child.Visible);
                    if (!expression.Evaluate())
                        continue;
                }

                ContentViewer viewer = new ContentViewer(commands, e.Id, current)
                {
                    Dock = DockStyle.Fill
                };

                TabSplitterPage page = new TabSplitterPage
                {
                    Text = e.Name
                };

                page.Controls.Add(viewer);

                tabSplitterContainer1.SecondaryPages.Insert(tabSplitterContainer1.SecondaryPages.Count - 2, page);
                tabSplitterContainer1.SecondaryPages.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Процедура обновляет текущую страницу: производится запрос к БД, обновляются поля данных и панель кнопок. Если createControls = true,
        /// то после чтения данных из БД, создаются элементы управления.
        /// </summary>
        /// <param name="createControls">true, создать элементы управления</param>
        private void RefreshPage(bool createControls)
        {
            ReadDataEntity();

            if (createControls)
            {
                UnbindEvents();
                foreach (IEditorControl ec in editor.Controls)
                {
                    ec.CreateControl(Session, tabSplitterMaster, context);
                }
            }

            UpdateCurrentStatusInfo();
            CreateActionButtons();
            PopulateControls();
            //UpdatePrintItems();

            if (createControls)
            {
                BindEvents();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e) => UpdateEntity();

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (UpdateEntity())
            {
                ((TabPageAdv)Parent).Close();
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            DocumentRefs refs = refEditor.Create(current);
            if (refs == null)
                return;

            using (var transaction = Session.BeginTransaction())
            {
                try
                {
                    Session.SaveOrUpdate(refs);
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

        private void buttonEdit_Click(object sender, EventArgs e) => EditDocumentRefs();

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (gridDocuments.CurrentItem is DocumentRefs refs)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить файл?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    refEditor.Delete(refs);

                    using (var transaction = Session.BeginTransaction())
                    {
                        try
                        {
                            Session.Delete(refs);
                            transaction.Commit();
                            documents.Remove(refs);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            ExceptionHelper.MesssageBox(ex);
                        }
                    }
                }
            }
        }

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(destLocalPath))
            {
                Process.Start(destLocalPath);
            }
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (gridDocuments.CurrentItem is DocumentRefs refs)
            {
                using (var transaction = Session.BeginTransaction())
                {
                    try
                    {
                        switch (refEditor.GetLocalFileName(refs, out string file))
                        {
                            case DocumentRefEditor.EditingResult.Ok:
                                Session.SaveOrUpdate(refs);
                                transaction.Commit();
                                Process.Start(file);
                                break;
                            case DocumentRefEditor.EditingResult.RemovalRequired:
                                Session.Delete(refs);
                                transaction.Commit();
                                documents.Remove(refs);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ExceptionHelper.MesssageBox(ex);
                    }
                }
            }
        }

        private void gridDocuments_CellDoubleClick(object sender, CellClickEventArgs e) => EditDocumentRefs();

        private void buttonScanPortrait_Click(object sender, EventArgs e) => SaveDocument(WIAScanner.Scan(false));

        private void buttonScanLandscape_Click(object sender, EventArgs e) => SaveDocument(WIAScanner.Scan(true));

        private void buttonRefresh_Click(object sender, EventArgs e) => RefreshPage(false);

        private void timerDatabaseListen_Tick(object sender, EventArgs e)
        {
            if (notifies.TryDequeue(out NotifyMessage message))
            {
                switch (message.Action)
                {
                    case "refresh":
                        RefreshPage(false);
                        break;
                    case "delete":
                        ((TabPageAdv)Parent).Close();
                        break;
                    default:
                        break;
                }
            }
        }

        private void buttonStatus_Click(object sender, EventArgs e)
        {
            HistoryWindow win = new HistoryWindow(Session, current);
            win.ShowDialog();
        }

        private void buttonPrint_ButtonClick(object sender, EventArgs e)
        {
            if (sender is ToolStripItem item && item.Tag is PrintForm form)
            {
                Printer.Preview(form, Session, row);
            }
        }
    }
}
