//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.03.2020
// Time: 15:27
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
#if USE_LISTENER
    using System.Collections;
    using System.Collections.Concurrent;
#endif
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
#if USE_LISTENER
    using System.Threading;
    using System.Threading.Tasks;
#endif
using System.Windows.Forms;
using Dapper;
#if USE_LISTENER
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Npgsql;
#endif
using Syncfusion.Data;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGrid.Interactivity;
using DocumentFlow.Code;
using DocumentFlow.Code.Implementation;
using DocumentFlow.Code.Core;
using DocumentFlow.Controls;
using DocumentFlow.Controls.Renderers;
using DocumentFlow.Core;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;
using DocumentFlow.Interfaces;
using DocumentFlow.Properties;

namespace DocumentFlow
{
    public partial class ViewerControl : UserControl, ISettings, IBrowser
    {
        public class CustomComparer : IComparer<object>, ISortDirection
        {
            private readonly PropertyInfo property;

            public CustomComparer(Type entityType, string mappingName)
            {
                property = entityType.GetProperty(mappingName);
            }

            public int Compare(object x, object y)
            {
                int res;
                if (x is IParent parent_x && y is IParent parent_y && (parent_x.is_folder != parent_y.is_folder))
                {
                    res = parent_x.is_folder ? -1 : 1;
                }
                else
                {
                    if (x is IDocumentInfo && y is IDocumentInfo)
                    {
                        var obj_x = property.GetValue(x);
                        var obj_y = property.GetValue(y);
                        if (obj_x is IComparable cmp_x && obj_y is IComparable cmp_y)
                        {
                            res = cmp_x.CompareTo(cmp_y);
                        }
                        else
                        {
                            res = CompareString(obj_x, obj_y);
                        }
                    }
                    else if (x is Group grp_x && y is Group grp_y)
                    {
                        res = CompareString(grp_x.Key, grp_y.Key);
                    }
                    else
                    {
                        res = x.ToString().CompareTo(y.ToString());
                    }
                }

                return SortDirection == ListSortDirection.Ascending ? res : -res;
            }

            public ListSortDirection SortDirection { get; set; }

            private int CompareString(object x, object y)
            {
                if (x == null && y == null)
                    return 0;
                else if (x == null)
                    return -1;
                else if (y == null)
                    return 1;
                else
                    return x.ToString().CompareTo(y.ToString());
            }
        }

#if USE_SETTINGS
        private const string settingsFile = "viewer.json";
        private string settingsPath;
#endif
        private DataType dataType;
        private DateRanges fromDate;
        private DateRanges toDate;
        private readonly GridColumnCollection columns;
        private Dictionary<string, ToolStripData> toolStripData;
        private Guid? parentId;
        private UserActionCollection commandCollection;
        private string doubleClickCommand;
        private readonly BrowserMode mode;
#if TURN_MOVETOEND
        private bool moveToEnd;
#endif

#if USE_LISTENER
        private bool initializing;
        private readonly CancellationTokenSource listenerToken;
        private readonly ConcurrentQueue<NotifyMessage> notifies = new ConcurrentQueue<NotifyMessage>();
#endif

        private GridColumn column = null;
        private SfDataGrid grid = null;
        private Group group = null;

        public ViewerControl() : this(BrowserMode.Main) { }

        public ViewerControl(BrowserMode browserMode)
        {
            InitializeComponent();

            mode = browserMode;

            columns = new GridColumnCollection(gridContent, menuVisibleColumns);
            columns.ChangeColumnVisible += ChangeColumnVisible;

#if USE_LISTENER
            initializing = false;

            timerDatabaseListen.Start();

            listenerToken = new CancellationTokenSource();
            _ = CreateListener(listenerToken.Token);
#endif
        }

#if USE_LISTENER
        private void Listener(CancellationToken token)
        {
            using (var conn = new NpgsqlConnection(Db.ConnectionString))
            {
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
                    if (message.EntityId == ExecutedCommand.EntityKind?.id)
                    {
                        if (message.Destination == MessageDestination.List && OwnerId != null && message.ObjectId != OwnerId)
                            return;

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
        }

        private async Task CreateListener(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;

            try
            {
                await Task.Run(() => Listener(token), token).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
            }
        }
#endif

        #region IBrowser implementation

        public event EventHandler<ChangeParentEventArgs> ChangeParent;
        public Func<IBrowser, IEditorCode> CreateEditor;

        BrowserMode IBrowser.Mode => mode;

        object IBrowser.CurrentRow => gridContent.SelectedItem;

        IToolBar IBrowser.ToolBar => (IToolBar)toolStripData["toolbar"];
        IContextMenu IBrowser.ContextMenuRecord => (IContextMenu)toolStripData["record"];
        IContextMenu IBrowser.ContextMenuRow => (IContextMenu)toolStripData["row"];
        IContextMenu IBrowser.ContextMenuGrid => (IContextMenu)toolStripData["grid"];

        IUserActionCollection IBrowser.Commands => commandCollection;

        bool IBrowser.AllowGrouping
        {
            get => gridContent.AllowGrouping;
            set
            {
                gridContent.AllowGrouping = value;
                gridContent.ShowGroupDropArea = value;
            }
        }

        bool IBrowser.AllowSorting
        {
            get => gridContent.AllowSorting;
            set => gridContent.AllowSorting = value;
        }

        DataType IBrowser.DataType
        {
            get => dataType;
            set => dataType = value;
        }

        DateRanges IBrowser.FromDate
        {
            get => fromDate;
            set => fromDate = value;
        }

        DateRanges IBrowser.ToDate
        {
            get => toDate;
            set => toDate = value;
        }

        bool IBrowser.CommandBarVisible
        {
            get => panelCommandBar.Visible;
            set => panelCommandBar.Visible = value;
        }

        IColumnCollection IBrowser.Columns => columns;

        void IBrowser.CreateColumns(Action<IColumnCollection> createColumnsAction)
        {
            createColumnsAction(columns);
        }

        void IBrowser.CreateStatusColumnRenderer()
        {
            gridContent.CellRenderers["RowHeader"] = new CustomRowHeaderCellRenderer(gridContent);
        }

        IGroupColumnCollection IBrowser.CreateGroups()
        {
            return new GroupColumnCollection(gridContent);
        }

        void IBrowser.DefineDoubleClickCommand(string name) => doubleClickCommand = name;

        void IBrowser.MoveToEnd()
        {
#if TURN_MOVETOEND
            moveToEnd = true;
#endif
        }

        T IBrowser.ExecuteSqlCommand<T>(string sql, object param)
        {
            using (var conn = Db.OpenConnection())
            {
                return conn.QuerySingleOrDefault<T>(sql, param);
            }
        }

        #endregion

        #region ISettings implementation

        void ISettings.LoadSettings()
        {
#if USE_SETTINGS
            string appFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            AssemblyName name = Assembly.GetExecutingAssembly().GetName();
            settingsPath = Path.Combine(appFolder, name.Name, name.Version.ToString(), schema.Name);

            string fileName = Path.Combine(settingsPath, settingsFile);
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                schema.Viewer = JsonConvert.DeserializeObject<DatasetViewer>(json);
            }
#endif
        }

        void ISettings.SaveSettings()
        {
#if USE_SETTINGS
            if (schema.Viewer != null)
            {
                string json = JsonConvert.SerializeObject(schema.Viewer);
                if (!System.IO.Directory.Exists(settingsPath))
                {
                    System.IO.Directory.CreateDirectory(settingsPath);
                }

                string fileName = Path.Combine(settingsPath, settingsFile);
                File.WriteAllText(fileName, json);
            }
#endif
        }

        #endregion

        [Browsable(false)]
        public IBrowserParameters Parameters
        {
            get
            {
                return new BrowserParameters()
                {
                    ParentId = parentId,
                    OwnerId = OwnerId,
                    DateFrom = dateTimePickerFrom.Checked ? dateTimePickerFrom.Value.StartOfDay() : dateTimePickerFrom.Value.BeginningOfTime(),
                    DateTo = dateTimePickerTo.Checked ? dateTimePickerTo.Value.EndOfDay() : dateTimePickerTo.Value.EndOfTime(),
                    OrganizationId = (comboOrg.SelectedItem as NameDataItem)?.id
                };
            }
        }

        public Guid InfoId => gridContent.SelectedItem is IIdentifier<Guid> row ? row.id : Guid.Empty;

        public IContainerPage ContainerForm { get; set; }
        public ICommandFactory CommandFactory { get; set; }
        public Command ExecutedCommand { get; set; }
        public Guid? OwnerId { get; set; }

        public void InitializeViewer()
        {
#if USE_LISTENER
            initializing = false;
#endif

            ((ISettings)this).LoadSettings();

            if (ExecutedCommand == null)
            {
                throw new Exception("Не определена команда для создания окна.");
            }

            if (Parent is Form form)
            {
                form.Text = ExecutedCommand.name;
            }

#if TURN_MOVETOEND
            moveToEnd = false;
#endif

            gridContent.ColumnHeaderContextMenu = contextHeaderMenu;
            gridContent.RowHeaderContextMenu = contextRowMenu;
            gridContent.RecordContextMenu = contextRecordMenu;
            gridContent.GroupDropAreaContextMenu = contextGroupMenu;
            gridContent.GroupDropAreaItemContextMenu = contextGroupItemMenu;
            gridContent.GroupCaptionContextMenu = contextGroupCaptionMenu;

            gridContent.SortClickAction = Settings.Default.SortClickAction;
            gridContent.ShowSortNumbers = Settings.Default.ShowSortNumbers;

            gridContent.Style.ProgressBarStyle.ForegroundStyle = GridProgressBarStyle.Tube;
            gridContent.Style.ProgressBarStyle.TubeForegroundEndColor = Color.White;
            gridContent.Style.ProgressBarStyle.TubeForegroundStartColor = Color.SkyBlue;
            gridContent.Style.ProgressBarStyle.AllowForegroundSegments = true;
            gridContent.Style.ProgressBarStyle.ProgressTextColor = Color.FromArgb(68, 68, 68);

            gridContent.AllowGrouping = false;
            gridContent.AllowSorting = true;
            gridContent.ShowGroupDropArea = false;

            columns.Clear();
            gridContent.StackedHeaderRows.Clear();

            fromDate = DateRanges.FirstMonthDay;
            toDate = DateRanges.LastMonthDay;
            dataType = DataType.None;

            parentId = null;

            commandCollection = new UserActionCollection(this, CommandFactory);
            toolStripData = new Dictionary<string, ToolStripData>()
            {
                { "toolbar", new ToolBarData(toolStrip1, commandCollection) },
                { "record", new ContextMenuData(contextRecordMenu, commandCollection) },
                { "row", new ContextMenuData(contextRowMenu, commandCollection) },
                { "grid", new ContextMenuData(contextGridMenu, commandCollection) }
            };

            panelCommandBar.Height = 30;
            panelCommandBar.Visible = true;

            menuVisibleColumns.DropDownItems.Clear();

            ExecutedCommand.Browser().Initialize(this);

            if (dataType == DataType.None)
            {
                throw new Exception($"Не определен тип DataType для окна: {ExecutedCommand}.");
            }

            switch (dataType)
            {
                case DataType.Directory:
                    panelDocument.Visible = false;
                    breadcrumb1.Visible = ExecutedCommand.EntityKind != null && ExecutedCommand.EntityKind.has_group;
                    break;
                case DataType.Document:
                    InitializeDocumentViewer();
                    break;
                case DataType.Report:
                    InitializeDocumentViewer();

                    IEnumerable<IUserAction> all = null;
                    UserActionComparer commandComparer = new UserActionComparer();
                    foreach (ToolStripData item in toolStripData.Values)
                    {
                        all = all == null ? item.Commands : all.Union(item.Commands, commandComparer);
                    }

                    foreach (IUserAction item in all)
                    {
                        item.SetVisible(item.Code == "refresh" || item.Code == "copy-text");
                    }

                    break;
                default:
                    break;
            }

            RefreshCurrenView();

            bool can_copy = false;
            bool has_group = false;
            if (ExecutedCommand.EntityKind != null)
            {
                has_group = ExecutedCommand.EntityKind.has_group;
                can_copy = ExecutedCommand.EntityKind.copy_ability;
            }

            buttonAddFolder.Visible = has_group;
            menuAddFolder.Visible = has_group;
            buttonCopy.Enabled = can_copy;
            menuCopy.Enabled = can_copy;

            gridContent.ColumnResizing += Grid_ColumnResizing;

            gridContent.CellRenderers.Remove("Image");
            gridContent.CellRenderers.Add("Image", new CustomGridImageCellRenderer(gridContent));

            gridContent.CellRenderers.Remove("TableSummary");
            gridContent.CellRenderers.Add("TableSummary", new CustomGridTableSummaryRenderer(columns));

            gridContent.CellRenderers.Remove("GroupSummary");
            gridContent.CellRenderers.Add("GroupSummary", new CustomGridTableSummaryRenderer(columns));

#if USE_LISTENER
            initializing = true;
#endif
        }

        public void OnClosing()
        {
#if USE_LISTENER
            listenerToken.Cancel();
            timerDatabaseListen.Stop();
#endif
        }

        private void InitializeDocumentViewer()
        {
            panelDirectory.Visible = false;
            dateTimePickerFrom.Checked = fromDate != DateRanges.None;
            dateTimePickerTo.Checked = toDate != DateRanges.None;
            dateTimePickerFrom.Value = DateTime.Today.FromDateRanges(fromDate);
            dateTimePickerTo.Value = DateTime.Today.FromDateRanges(toDate);

            using (var conn = Db.OpenConnection())
            {
                IEnumerable<NameDataItem> orgs = conn.Query<NameDataItem>("select id, name from organization");
                comboOrg.DataSource = orgs;

                Guid def_org = conn.Query<Guid>("select id from organization where default_org").SingleOrDefault();
                comboOrg.SelectedItem = orgs.FirstOrDefault(x => x.id == def_org);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="id">Идентификатор записи. Для action = Create, это значение должно соответствовать полю parent_id, для Edit - полю id.</param>
        private void GroupAction(Guid? id, CommandAction action)
        {
            if (string.IsNullOrEmpty(ExecutedCommand.EntityKind?.code))
            {
                return;
            }

            GroupEditor editor = new GroupEditor(ExecutedCommand.EntityKind.code);

            bool result = false;
            switch (action)
            {
                case CommandAction.Create:
                    result = editor.Create(ExecutedCommand.EntityKind.id, id);
                    break;
                case CommandAction.Edit:
                    result = editor.Edit(id.Value);
                    break;
            }

            if (result)
            {
                RefreshCurrenView();
            }
        }

        private void Grid_ColumnResizing(object sender, ColumnResizingEventArgs e)
        {
            GridColumn column = gridContent.Columns[e.ColumnIndex];

            IColumn c = columns.Where(x => x.FieldName == column.MappingName).SingleOrDefault();
            if (c != null)
            {
                c.Width = Convert.ToInt32(e.Width);
            }
        }

        private void RefreshCurrenView()
        {
            if (ExecutedCommand.Browser() is IBrowserOperation operation)
            {
                using (var conn = Db.OpenConnection())
                {
                    IBrowser browser = this;
                    try
                    {
                        var list = operation.Select(conn, browser.Parameters);
                        Type entityType = list.GetType().GetGenericArguments().First();
                        Type genericType = typeof(BindingList<>).MakeGenericType(entityType);

                        if (gridContent.SortComparers.Count == 0)
                        {
                            foreach (GridColumn column in gridContent.Columns)
                            {
                                gridContent.SortComparers.Add(
                                    new SortComparer()
                                    {
                                        Comparer = new CustomComparer(entityType, column.MappingName),
                                        PropertyName = column.MappingName
                                    });
                            }
                        }

                        gridContent.DataSource = Activator.CreateInstance(genericType, list);
                    }
                    catch (Exception e)
                    {
                        throw new SqlExecuteException("При попытке выполнения команды SELECT произошла ошибка", e);
                    }
                }
            }

            //FIX: По кактим-то причинам перемещение в конец таблицы не происходит при первом открытии окна. Повторное открытие этого или какого другого устраняет проблему
            //FIX: Присвоение нового значения gridContent.DataSource не приводит к изменению gridContent.RowCount
            //FIX: ОТКЛЮЧЕНО
#if TURN_MOVETOEND
            if (moveToEnd && gridContent.DataSource is IBindingList bindingList)
            {
                if (gridContent.RowCount > 0)
                {
                    gridContent.SelectedIndex = gridContent.RowCount - 2;

                    gridContent.TableControl.ScrollRows.ScrollInView(gridContent.RowCount - 2);
                    gridContent.TableControl.ScrollRows.UpdateScrollBar();
                }
            }
#endif
        }

#if USE_LISTENER
        private int GetRowIndex(IList list, Guid id)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is IIdentifier<Guid> item && item.id == id)
                {
                    return i;
                }
            }

            return -1;
        }

        private void RefreshRow(Guid id)
        {
            if (gridContent.DataSource is IList list && ExecutedCommand.Browser() is IBrowserOperation operation)
            {
                int index = GetRowIndex(list, id);
                if (index != -1)
                {
                    using (var conn = Db.OpenConnection())
                    {
                        var new_row = operation.Select(conn, id, Parameters);
                        if (new_row != null)
                        {
                            list[index] = new_row;
                        }
                    }
                }
            }
        }

        private void AddRow(Guid id)
        {
            if (gridContent.DataSource is IList list && ExecutedCommand.Browser() is IBrowserOperation operation)
            {
                int index = GetRowIndex(list, id);
                if (index != -1)
                {
                    using (var conn = Db.OpenConnection())
                    {
                        var new_row = operation.Select(conn, id, Parameters);
                        if (new_row != null)
                        {
                            list.Add(new_row);
                        }
                    }
                }
            }
        }

        private void DeleteRow(Guid id)
        {
            if (gridContent.DataSource is IList list)
            {
                int index = GetRowIndex(list, id);
                if (index != -1)
                {
                    list.RemoveAt(index);
                }
            }
        }
#endif

        private void ChangeColumnVisible(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                IColumn column = columns.OfType<GridColumnData>().FirstOrDefault(x => x.MenuItem == item);
                if (column != null)
                {
                    column.Visible = item.Checked;
                }
            }
        }

        private void RefreshEntities(object sender, EventArgs e) => RefreshCurrenView();

        private void breadcrumb1_CrumbClick(object sender, CrumbClickEventArgs e)
        {
            var oldParent = parentId ?? Guid.Empty;
            switch (e.Kind)
            {
                case ToolButtonKind.Up:
                    breadcrumb1.Pop();
                    parentId = GetCurrentGroupEntity();
                    break;
                case ToolButtonKind.Refresh:
                    parentId = GetCurrentGroupEntity();
                    break;
                case ToolButtonKind.Home:
                    breadcrumb1.Clear();
                    parentId = null;
                    break;
            }

            var newParent = parentId ?? Guid.Empty;
            ChangeParent?.Invoke(this, new ChangeParentEventArgs(oldParent, newParent));

            RefreshCurrenView();

            Guid? GetCurrentGroupEntity()
            {
                Guid guid = breadcrumb1.Peek();
                return guid == Guid.Empty ? (Guid?)null : guid;
            }
        }

        private void SearchData(object sender, EventArgs e)
        {
            if (sender is TextBoxExt text)
            {
                gridContent.SearchController.Search(text.Text);
            }
        }

        private void gridContent_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            IColumn c = columns.FirstOrDefault(x => x.FieldName == e.Column.MappingName);
            if (c == null)
                return;

            if (!string.IsNullOrEmpty(c.NegativeValueColor))
            {
                bool negative = false;
                switch (c.ColumnType)
                {
                    case DataColumnType.Integer:
                        negative = Convert.ToInt32(e.DisplayText) < 0;
                        break;
                    case DataColumnType.Numeric:
                        negative = Convert.ToDecimal(e.DisplayText) < 0;
                        break;
                }

                if (negative)
                    e.Style.TextColor = ColorTranslator.FromHtml(c.NegativeValueColor);
            }
        }

        private void gridContent_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            if (gridContent.SelectedItem is IDocumentInfo row)
            {
                if (row.status_id == 500 && gridContent.SelectedItem is IDirectory dir)
                {
                    var oldParent = parentId ?? Guid.Empty;
                    parentId = dir.id;
                    breadcrumb1.Push(parentId.Value, dir.name);
                    var newParent = parentId ?? Guid.Empty;
                    ChangeParent?.Invoke(this, new ChangeParentEventArgs(oldParent, newParent));
                    RefreshCurrenView();
                    return;
                }
            }

            if (gridContent.SelectedItem is IIdentifier<Guid> data)
            {
                string code = string.IsNullOrEmpty(doubleClickCommand) ? "edit-record" : doubleClickCommand;
                if (code == "edit-record")
                    CommandFactory.OpenEditor(this, data, ExecutedCommand, Parameters);
                else
                    CommandFactory.Execute(code, data);
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e) => CommandFactory.OpenEditor(this, Guid.Empty, ExecutedCommand, Parameters);

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (gridContent.SelectedItem is IDocumentInfo row)
            {
                if (row.status_id == 500)
                {
                    GroupAction(row.id, CommandAction.Edit);
                }
                else
                {
                    CommandFactory.OpenEditor(this, row, ExecutedCommand, Parameters);
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (gridContent.SelectedItem is IDocumentInfo row)
            {
                bool delete;
                if (row.status_id == 500)
                {
                    delete = MessageBox.Show("Удаление группы приведет к удалению всего содержимого группы. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
                }
                else
                {
                    delete = MessageBox.Show("Вы действительно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
                }

                if (!delete)
                    return;

                if (ExecutedCommand.Browser() is IBrowserOperation operation)
                {
                    using (var conn = Db.OpenConnection())
                    {
                        using (var transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                operation.Delete(conn, transaction, row.id);
                                transaction.Commit();
#if !USE_LISTENER
                                RefreshCurrenView();
#endif
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
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (gridContent.SelectedItem is IDocumentInfo row)
            {
                using (var conn = Db.OpenConnection())
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            Guid id = conn.QueryFirst<Guid>("select copy_entity(:kind_id, :copy_id)", new { kind_id = ExecutedCommand.EntityKind.id, copy_id = row.id }, transaction);
                            transaction.Commit();
                            if (id != default && MessageBox.Show("Открыть окно для редактрования?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                CommandFactory.OpenEditor(this, id, ExecutedCommand, Parameters);
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
        }

        private void buttonAddFolder_Click(object sender, EventArgs e) => GroupAction(parentId, CommandAction.Create);

        private void buttonHistory_Click(object sender, EventArgs e)
        {
            if (gridContent.SelectedItem is IDocumentInfo row)
            {
                HistoryWindow.ShowWindow(row.id);
            }
        }

        private void menuCopyClipboard_Click(object sender, EventArgs e)
        {
            if (gridContent.CurrentCell == null || gridContent.SelectedItem == null)
                return;

            string propName = gridContent.CurrentCell.Column.MappingName;

            Type type = gridContent.SelectedItem.GetType();
            PropertyInfo prop = type.GetProperty(propName);
            object value = prop.GetValue(gridContent.SelectedItem);
            if (value != null)
                Clipboard.SetText(value.ToString());
        }

        private void timerDatabaseListen_Tick(object sender, EventArgs e)
        {
#if USE_LISTENER
            if (notifies.TryDequeue(out NotifyMessage message))
            {
                if (!initializing)
                {
                    return;
                }

                switch (message.Action)
                {
                    case "refresh":
                        switch (message.Destination)
                        {
                            case MessageDestination.Object:
                                RefreshRow(message.ObjectId);
                                break;
                            case MessageDestination.List:
                                RefreshCurrenView();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "add":
                        AddRow(message.ObjectId);
                        break;
                    case "delete":
                        DeleteRow(message.ObjectId);
                        break;
                    default:
                        break;
                }
            }
#endif
        }

        private void buttonSelectRange_Click(object sender, EventArgs e)
        {
            var win = new SelectDateRangeWindow();
            if (win.ShowDialog() == DialogResult.OK)
            {
                dateTimePickerFrom.Value = win.DateFrom;
                dateTimePickerTo.Value = win.DateTo;
            }
        }

        private void menuSortAsc_Click(object sender, EventArgs e)
        {
            if (grid != null && column != null)
            {
                grid.SortColumnDescriptions.Clear();
                grid.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = column.MappingName, SortDirection = ListSortDirection.Ascending });
            }
        }

        private void menuSortDesc_Click(object sender, EventArgs e)
        {
            if (grid != null && column != null)
            {
                grid.SortColumnDescriptions.Clear();
                grid.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = column.MappingName, SortDirection = ListSortDirection.Descending });
            }
        }

        private void menuClearSort_Click(object sender, EventArgs e)
        {
            if (grid != null)
            {
                grid.ClearSorting();
            }
        }

        private void gridContent_ContextMenuOpening(object sender, Syncfusion.WinForms.DataGrid.Events.ContextMenuOpeningEventArgs e)
        {
            if (e.ContextMenutype == ContextMenuType.GroupCaption)
            {
                grid = (e.ContextMenuInfo as RowContextMenuInfo).DataGrid;
                group = (e.ContextMenuInfo as RowContextMenuInfo).Row as Syncfusion.Data.Group;
            }

            if (e.ContextMenutype == ContextMenuType.GroupDropAreaItem || e.ContextMenutype == ContextMenuType.ColumnHeader)
            {
                grid = (e.ContextMenuInfo as ColumnContextMenuInfo).DataGrid;
                column = (e.ContextMenuInfo as ColumnContextMenuInfo).Column;
            }

            if (e.ContextMenutype == ContextMenuType.GroupDropArea)
            {
                grid = (e.ContextMenuInfo as GroupDropAreaContextMenuInfo).DataGrid;
            }

            if (e.ContextMenutype == ContextMenuType.Record || e.ContextMenutype == ContextMenuType.RowHeader)
            {
                grid = (e.ContextMenuInfo as RowContextMenuInfo).DataGrid;
            }
        }

        private void contextHeaderMenu_Opening(object sender, CancelEventArgs e)
        {
            if (gridContent.SortColumnDescriptions.Count == 0)
            {
                menuClearSort.Enabled = false;
            }

            menuSortAsc.Enabled = true;
            menuSortDesc.Enabled = true;
            foreach (SortColumnDescription x in gridContent.SortColumnDescriptions)
            {
                if (x.ColumnName == column.MappingName)
                {
                    if (x.SortDirection == ListSortDirection.Ascending)
                    {
                        menuSortAsc.Enabled = false;
                        menuSortDesc.Enabled = true;
                    }
                    else
                    {
                        menuSortAsc.Enabled = true;
                        menuSortDesc.Enabled = false;
                    }
                }
            }
        }

        private void contextGroupMenu_Opening(object sender, CancelEventArgs e)
        {
            if (gridContent.GroupColumnDescriptions.Count == 0)
            {
                menuExpandAll.Enabled = false;
                menuCollapseAll.Enabled = false;
                menuClearGroups.Enabled = false;
            }
        }

        private void menuHideGroupArea_Click(object sender, EventArgs e)
        {
            if (grid != null)
            {
                grid.ShowGroupDropArea = false;
            }
        }

        private void menuExpandAll_Click(object sender, EventArgs e)
        {
            if (grid != null)
            {
                grid.ExpandAllGroup();
                menuExpandAll.Enabled = false;
                menuCollapseAll.Enabled = true;
            }
        }

        private void menuCollapseAll_Click(object sender, EventArgs e)
        {
            if (grid != null)
            {
                grid.CollapseAllGroup();
                menuCollapseAll.Enabled = false;
                menuExpandAll.Enabled = true;
            }
        }

        private void menuClearGroups_Click(object sender, EventArgs e)
        {
            if (grid != null)
            {
                grid.ClearGrouping();
                menuClearGroups.Enabled = false;
            }
        }

        private void menuExpandItem_Click(object sender, EventArgs e)
        {
            if (grid != null)
            {
                grid.ExpandAllGroup();
                menuExpandItem.Enabled = false;
                menuCollapseItem.Enabled = true;
            }
        }

        private void menuCollapseItem_Click(object sender, EventArgs e)
        {
            if (grid != null)
            {
                grid.CollapseAllGroup();
                menuCollapseItem.Enabled = false;
                menuExpandItem.Enabled = true;
            }
        }

        private void menuClearGroupItems_Click(object sender, EventArgs e)
        {
            if (grid != null && column != null)
            {
                grid.GroupColumnDescriptions.Remove(grid.GroupColumnDescriptions.FirstOrDefault(x => x.ColumnName == column.MappingName));
            }
        }

        private void contextGroupCaptionMenu_Opening(object sender, CancelEventArgs e)
        {
            menuGroupCaptionExpand.Enabled = true;
            menuGroupCaptionCollapse.Enabled = true;

            if (group.IsExpanded)
            {
                menuGroupCaptionExpand.Enabled = false;
            }
            else
            {
                menuGroupCaptionCollapse.Enabled = false;
            }
        }

        private void menuGroupCaptionExpand_Click(object sender, EventArgs e)
        {
            if (grid != null && group != null)
            {
                grid.ExpandGroup(group);
            }
        }

        private void menuGroupCaptionCollapse_Click(object sender, EventArgs e)
        {
            if (grid != null && group != null)
            {
                grid.CollapseGroup(group);
            }
        }

        private void buttonCustomization_Click(object sender, EventArgs e)
        {
            CommandFactory.OpenCodeEditor(ExecutedCommand);
        }

        private void gridContent_VisibleChanged(object sender, EventArgs e)
        {
            toolStripData["toolbar"].UpdateButtonVisibleStatus();
        }

        private void contextRecordMenu_Opening(object sender, CancelEventArgs e)
        {
            menuDocuments.DropDownItems.Clear();
            if (gridContent.SelectedItem is IDocumentInfo row)
            {
                using (var conn = Db.OpenConnection())
                {
                    documentsKind = conn.QueryFirst<string>("select ek.code from document_info di join entity_kind ek on (ek.id = di.entity_kind_id) where di.id = :id", new { row.id });

                    var docs = conn.Query<DocumentRefs>("select * from document_refs where owner_id = :id", new { row.id });
                    foreach (var doc in docs)
                    {
                        var menuItem = new ToolStripMenuItem
                        {
                            Text = doc.note,
                            Tag = doc
                        };

                        menuItem.Click += DocumenuMenuItem_Click;
                        menuDocuments.DropDownItems.Add(menuItem);
                    }

                    menuDocuments.Visible = docs.Any();
                    toolStripSeparator9.Visible = docs.Any();
                }
            }
            else
            {
                menuDocuments.Visible = false;
                toolStripSeparator9.Visible = false;
            }
        }

        string documentsKind;
        private void DocumenuMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem && menuItem.Tag is DocumentRefs doc)
            {
                string ftpPath = Path.Combine(Settings.Default.FtpPath, documentsKind, doc.owner_id.ToString());
                DocumentRefEditor refEditor = new DocumentRefEditor(ftpPath);

                try
                {
                    string file = refEditor.GetLocalFileName(doc);
                    Process.Start(file);
                }
                catch (CanceledException ex)
                {
                    if (ex.NeedRemoveReference)
                    {
                        menuDocuments.DropDownItems.Remove(menuItem);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHelper.MesssageBox(ex);
                }
            }
        }
    }
}
