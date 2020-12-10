//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.03.2020
// Time: 15:27
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
#if USE_LISTENER
    using System.Collections.Concurrent;
#endif
using System.ComponentModel;
using System.Drawing;
using System.Data;
#if USE_SETTINGS
    using System.IO;
#endif
using System.Linq;
using System.Reflection;
#if USE_LISTENER
    using System.Threading;
    using System.Threading.Tasks;
#endif
using System.Windows.Forms;
using Dapper;
#if USE_LISTENER
    using Newtonsoft.Json;
    using Npgsql;
#endif
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGrid.Interactivity;
using DocumentFlow.Code;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.Data;
using DocumentFlow.Code.Implementation;
using DocumentFlow.Code.System;
using DocumentFlow.Core;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;
using DocumentFlow.Controls;
using DocumentFlow.Controls.Renderers;


namespace DocumentFlow
{
    public partial class ViewerControl : UserControl, ISettings, IBrowser
    {
        private class ToolBarData : IToolBar
        {
            private ViewerControl viewer;
            private ToolStrip buttonStrip;
            private ToolStripItemDisplayStyle buttonStyle;
            private ButtonIconSize iconSize;

            public ToolBarData(ViewerControl viewer, ToolStrip toolStrip, ToolStripItemDisplayStyle style, ButtonIconSize size)
            {
                this.viewer = viewer;

                buttonStrip = toolStrip;
                buttonStyle = style;
                iconSize = size;
            }

            public ToolStripItemDisplayStyle ButtonStyle
            {
                get => buttonStyle;
                set
                {
                    if (buttonStyle != value)
                    {
                        buttonStyle = value;
                        foreach (ToolStripItem item in buttonStrip.Items)
                        {
                            item.DisplayStyle = buttonStyle;
                        }
                    }
                }
            }

            public ButtonIconSize IconSize
            {
                get => iconSize;
                set
                {
                    if (iconSize != value)
                    {
                        iconSize = value;
                        foreach (ToolStripButton item in buttonStrip.Items.OfType<ToolStripButton>())
                        {
                            Command command = viewer.commandCollection.GetToolStripCommand(item);
                            if (command != null)
                            {
                                switch (iconSize)
                                {
                                    case ButtonIconSize.Small:
                                        item.Image = command.Picture.GetImageSmall();
                                        break;
                                    case ButtonIconSize.Large:
                                        item.Image = command.Picture.GetImageLarge();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Этот класс содержит список кнопок (или элементов меню). Эта группа отделяется рт других с помощью сепаратора, который находится
        /// в начале группы.
        /// </summary>
        private class CommandGroup
        {
            private ToolStripSeparator separator;
            private readonly List<ToolStripItem> items;

            public CommandGroup(string groupName) : this(groupName, true) { }

            public CommandGroup(string groupName, bool createSeparator = false)
            {
                Name = groupName;
                items = new List<ToolStripItem>();
                if (createSeparator)
                    separator = new ToolStripSeparator();
            }

            public string Name { get; }

            public void Add(ToolStripItem item)
            {
                if (item is ToolStripSeparator s)
                    separator = s;
                else
                {
                    items.Add(item);
                    item.VisibleChanged += Item_VisibleChanged;
                }
            }
            public override string ToString() => Name;

            public void UpdateSeparatorVisibility()
            {
                if (separator != null)
                {
                    separator.Visible = items.Any(x => x.Visible);
                }
            }

            private void Item_VisibleChanged(object sender, EventArgs e)
            {
                UpdateSeparatorVisibility();
            }
        }

        private class CommandGroupCollection : IEnumerable<CommandGroup>
        {
            private List<CommandGroup> groups = new List<CommandGroup>();
            private List<ToolStrip> toolStrips = new List<ToolStrip>();

            public IEnumerator<CommandGroup> GetEnumerator() => groups.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => groups.GetEnumerator();

            public void Add(ToolStripItem item, string toolbarGroupName)
            {
                ToolStrip toolStrip = item.Owner;
                if (toolStrips.FirstOrDefault(x => x == toolStrip) == null)
                {
                    toolStrips.Add(toolStrip);
                }

                CommandGroup g = groups.FirstOrDefault(x => x.Name == toolbarGroupName);
                if (g == null)
                {
                    g = new CommandGroup(toolbarGroupName);
                    groups.Add(g);
                }

                g.Add(item);
            }

            public ToolStrip GetToolStrip(string toolStripName) => toolStrips.FirstOrDefault(x => x.Tag.ToString() == toolStripName);
        }

        private class CommandTools : ICommand, ICommandAdded
        {
            private ViewerControl viewer;
            private List<ToolStripItem> tools = new List<ToolStripItem>();

            public CommandTools(Command command, ViewerControl viewer)
            {
                this.viewer = viewer;

                Command = command;
            }

            public event EventHandler<GettingParametersEventArgs> GettingParameters;
            public event EventHandler<ExecuteEventArgs> Click;

            public Command Command { get; }

            public bool Contains(ToolStripItem toolStripItem) => tools.Contains(toolStripItem);

            public void AddTool(ToolStripItem toolStripItem)
            {
                tools.Add(toolStripItem);
            }

            public void Execute()
            {
                Click?.Invoke(this, new ExecuteEventArgs(viewer));
            }

            public IDictionary<string, object> GetParameters()
            {
                if (GettingParameters != null)
                {
                    GettingParametersEventArgs args = new GettingParametersEventArgs(viewer);
                    GettingParameters.Invoke(this, args);
                    return args.Parameters;
                }

                return null;
            }

            string ICommand.Code => Command.code;

            ICommand ICommand.SetEnabled(bool enabled)
            {
                foreach (ToolStripItem item in tools)
                {
                    item.Enabled = enabled;
                }

                return this;
            }

            ICommand ICommand.SetVisible(bool visible)
            {
                foreach (ToolStripItem item in tools)
                {
                    item.Visible = visible;
                }

                return this;
            }

            ICommandAdded ICommandAdded.SetIcon(string name)
            {
                foreach (ToolSplitItem item in tools)
                {
                    if (item.GetCurrentParent() is ContextMenuStripEx)
                    {
                        item.Image = Command.Picture.GetImageSmall();
                    }
                    else
                    {
                        item.Image = viewer.toolBarData.IconSize == ButtonIconSize.Small ? Command.Picture.GetImageSmall() : Command.Picture.GetImageLarge();
                    }
                }

                return this;
            }
        }

        private class CommandCollection : ICommandCollection
        {
            private List<CommandTools> commandTools = new List<CommandTools>();
            private CommandGroupCollection groupItems = new CommandGroupCollection();
            private ViewerControl viewer;

            public CommandCollection(ViewerControl viewer)
            {
                this.viewer = viewer;
            }

            IEnumerator IEnumerable.GetEnumerator() => commandTools.GetEnumerator();

            public IEnumerator<ICommand> GetEnumerator() => commandTools.GetEnumerator();

            public void AddToolStrip(ToolStrip toolStrip)
            {
                foreach (var item in toolStrip.Items.OfType<ToolStripItem>().Where(x => x.Tag != null))
                {
                    AddToolStripItem(item);
                }
            }

            private void AddToolStripItem(ToolStripItem toolStripItem)
            {
                string[] tag = toolStripItem.Tag.ToString().Split('|');

                if (!string.IsNullOrEmpty(tag[0]))
                {
                    Command cmd = viewer.CommandFactory.Commands.FirstOrDefault(x => x.code == tag[0]);
                    if (cmd == null)
                        return;

                    CommandTools tools = commandTools.FirstOrDefault(x => x.Command.Id == cmd.Id);
                    if (tools == null)
                    {
                        tools = new CommandTools(cmd, viewer);
                        commandTools.Add(tools);
                    }

                    tools.AddTool(toolStripItem);
                }

                groupItems.Add(toolStripItem, tag[1]);
            }

            void ICommandCollection.OpenDocument(Guid id) => viewer.CommandFactory.OpenDocument(id);

            void ICommandCollection.OpenDiagram(Guid id) => viewer.CommandFactory.Execute("open-diagram", id);

            ICommand ICommandCollection.Get(string name)
            {
                return commandTools.FirstOrDefault(x => x.Command.code == name);
            }

            ICommandAdded ICommandCollection.Add(CommandMethod method, string name, params string[] toolStripNames)
            {
                switch (method)
                {
                    case CommandMethod.Sql:
                        break;
                    case CommandMethod.Embedded:
                        return AddEmbededCommand(name, toolStripNames);
                    case CommandMethod.UserDefined:
                        return AddUserDefinedCommand(name, toolStripNames);
                    default:
                        break;
                }

                return null;
            }

            public Command GetToolStripCommand(ToolStripItem toolStripItem)
            {
                return commandTools.FirstOrDefault(x => x.Contains(toolStripItem))?.Command;
            }

            public void UpdateSeparatorVisibility()
            {
                foreach (var item in groupItems)
                {
                    item.UpdateSeparatorVisibility();
                }
            }

            private CommandTools CreateTools(string name)
            {
                Command cmd = viewer.CommandFactory.Commands.FirstOrDefault(x => x.code == name);
                if (cmd == null)
                    throw new CommandNotFoundException($"Команда {name} не существует.");

                CommandTools tools = commandTools.FirstOrDefault(x => x.Command.Id == cmd.Id);
                if (tools == null)
                {
                    tools = new CommandTools(cmd, viewer);
                    commandTools.Add(tools);
                }

                return tools;
            }

            private IEnumerable<ToolStripItem> CreateToolStripItem(CommandTools tools, params string[] toolStripNames)
            {
                List<ToolStripItem> items = new List<ToolStripItem>();
                foreach (string toolStripName in toolStripNames)
                {
                    ToolStrip toolStrip = groupItems.GetToolStrip(toolStripName);
                    if (toolStrip == null)
                        throw new IncorrectToolStripNameException($"Панель кнопок или контекстное меню указанное как {toolStripName} отсутствует.");

                    ToolStripItem item;
                    if (toolStrip is ContextMenuStripEx)
                    {
                        item = new ToolStripMenuItem(tools.Command.name, tools.Command.Picture.GetImageSmall());
                    }
                    else
                    {
                        item = new ToolStripButton()
                        {
                            Text = tools.Command.name,
                            Image = viewer.toolBarData.IconSize == ButtonIconSize.Small ? tools.Command.Picture.GetImageSmall() : tools.Command.Picture.GetImageLarge(),
                            DisplayStyle = viewer.toolBarData.ButtonStyle,
                            ImageScaling = ToolStripItemImageScaling.None,
                            TextImageRelation = TextImageRelation.ImageAboveText
                        };
                    }

                    toolStrip.Items.Add(item);
                    tools.AddTool(item);
                    groupItems.Add(item, "user-defined");

                    items.Add(item);
                }

                return items;
            }

            private ICommandAdded AddUserDefinedCommand(string name, params string[] toolStripNames)
            {
                CommandTools tools = CreateTools(name);
                foreach (ToolStripItem item in CreateToolStripItem(tools, toolStripNames))
                {
                    item.Click += UserDefinedCommand_Click;
                }

                return tools;
            }

            private ICommandAdded AddEmbededCommand(string name, params string[] toolStripNames)
            {
                CommandTools tools = CreateTools(name);
                foreach (ToolStripItem item in CreateToolStripItem(tools, toolStripNames))
                {
                    item.Click += EmbededCommand_Click;
                }

                return tools;
            }

            private void UserDefinedCommand_Click(object sender, EventArgs e)
            {
                if (sender is ToolStripItem item)
                {
                    commandTools.FirstOrDefault(x => x.Contains(item))?.Execute();
                }
            }

            private void EmbededCommand_Click(object sender, EventArgs e)
            {
                if (sender is ToolStripItem item)
                {
                    CommandTools command = commandTools.FirstOrDefault(x => x.Contains(item));

                    if (viewer.gridContent.SelectedItem is IDocumentInfo row)
                    {
                        viewer.CommandFactory.Execute(command.Command.code, command.GetParameters());
                    }
                }
            }
        }

#if USE_SETTINGS
        private const string settingsFile = "viewer.json";
        private string settingsPath;
#endif
        private DataType dataType;
        private DateRanges fromDate;
        private DateRanges toDate;
        private GridColumnCollection columns;
        private ToolBarData toolBarData;
        private Guid? parentId;
        private CommandCollection commandCollection;
        private string doubleClickCommand;
        private BrowserMode mode;

#if USE_LISTENER
        private bool initializing;
        private CancellationTokenSource listenerToken;
        private readonly ConcurrentQueue<NotifyMessage> notifies = new ConcurrentQueue<NotifyMessage>();
#endif

        private GridColumn column = null;
        private SfDataGrid grid = null;
        private Syncfusion.Data.Group group = null;
        private int rowIndex = -1;
        private int columnIndex = -1;
        private object record = null;

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
                    NotifyMessage message = JsonConvert.DeserializeObject<NotifyMessage>(e.Payload);
                    if (message.EntityId == ExecutedCommand.EntityKind.Id)
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

        IToolBar IBrowser.ToolBar => toolBarData;

        ICommandCollection IBrowser.Commands => commandCollection;

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
                    OrganizationId = (comboOrg.SelectedItem as ComboBoxDataItem)?.id
                };
            }
        }

        public Guid InfoId
        {
            get
            {
                if (gridContent.SelectedItem is IIdentifier<Guid> row)
                    return row.id;
                else
                    return Guid.Empty;
            }
        }

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

            gridContent.ColumnHeaderContextMenu = contextHeaderMenu;
            gridContent.RowHeaderContextMenu = contextRowMenu;
            gridContent.RecordContextMenu = contextRecordMenu;
            gridContent.GroupDropAreaContextMenu = contextGroupMenu;
            gridContent.GroupDropAreaItemContextMenu = contextGroupItemMenu;
            gridContent.GroupCaptionContextMenu = contextGroupCaptionMenu;

            gridContent.SortClickAction = Properties.Settings.Default.SortClickAction;
            gridContent.ShowSortNumbers = Properties.Settings.Default.ShowSortNumbers;

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

            toolBarData = new ToolBarData(this, toolStrip1, ToolStripItemDisplayStyle.ImageAndText, ButtonIconSize.Large);

            panelCommandBar.Height = 30;
            panelCommandBar.Visible = true;

            menuVisibleColumns.DropDownItems.Clear();
            CreateCommands();

            ExecutedCommand.Browser().Initialize(this);

            if (dataType == DataType.None)
            {
                throw new Exception($"Не определен тип DataType для окна: {ExecutedCommand}.");
            }

            switch (dataType)
            {
                case DataType.Directory:
                    panelDocument.Visible = false;
                    breadcrumb1.Visible = ExecutedCommand.EntityKind.has_group;
                    break;
                case DataType.Document:
                    InitializeDocumentViewer();
                    break;
                case DataType.Report:
                    InitializeDocumentViewer();
                    foreach (ToolStripItem item in toolStrip1.Items)
                    {
                        if (item != buttonRefresh)
                            item.Visible = false;
                    }

                    foreach (ToolStripItem item in contextRecordMenu.Items)
                    {
                        if (item != menuCopyClipboard)
                            item.Visible = false;
                    }

                    break;
                default:
                    break;
            }

            RefreshCurrenView();

            if (ExecutedCommand.EntityKind != null)
            {
                buttonAddFolder.Visible = ExecutedCommand.EntityKind.has_group;
                menuAddFolder.Visible = ExecutedCommand.EntityKind.has_group;
            }

            gridContent.ColumnResizing += Grid_ColumnResizing;

            using (var conn = Db.OpenConnection())
            {
                bool can_copy = false;
                if (ExecutedCommand.EntityKind != null)
                {
                    Guid? id = conn.Query<Guid?>("select copy_entity(:kind_id, null)", new { kind_id = ExecutedCommand.EntityKind.Id }).SingleOrDefault();
                    can_copy = id.HasValue;
                }

                buttonCopy.Enabled = can_copy;
                menuCopy.Enabled = can_copy;
            }

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
                IEnumerable<ComboBoxDataItem> orgs = conn.Query<ComboBoxDataItem>("select id, name from organization");
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
                    result = editor.Create(ExecutedCommand.EntityKind.Id, id);
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
            using (var conn = Db.OpenConnection())
            {
                IBrowser browser = this;
                try
                {
                    var list = ExecutedCommand.Browser().Select(conn, browser.Parameters);
                    Type entityType = list.GetType().GetGenericArguments().First();
                    Type genericType = typeof(BindingList<>).MakeGenericType(entityType);
                    gridContent.DataSource = Activator.CreateInstance(genericType, list);
                }
                catch (Exception e)
                {
                    throw new SqlExecuteException("При попытке выполнения команды SELECT произошла ошибка", e);
                }
            }
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
            if (gridContent.DataSource is IList list)
            {
                int index = GetRowIndex(list, id);
                if (index != -1)
                {
                    using (var conn = Db.OpenConnection())
                    {
                        var new_row = ExecutedCommand.Browser().SelectById(conn, id, Parameters);
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
            if (gridContent.DataSource is IList list)
            {
                int index = GetRowIndex(list, id);
                if (index != -1)
                {
                    using (var conn = Db.OpenConnection())
                    {
                        var new_row = ExecutedCommand.Browser().SelectById(conn, id, Parameters);
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

        private void CreateCommands()
        {
            commandCollection = new CommandCollection(this);
            commandCollection.AddToolStrip(toolStrip1);
            commandCollection.AddToolStrip(contextRecordMenu);
            commandCollection.AddToolStrip(contextRowMenu);
            commandCollection.AddToolStrip(contextGridMenu);
        }

        private void RefreshEntities(object sender, EventArgs e) => RefreshCurrenView();

        private void Edit(Guid id) => CommandFactory.Execute("edit-record", this, id, ExecutedCommand, Parameters);

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

                string code = string.IsNullOrEmpty(doubleClickCommand) ? "edit-record" : doubleClickCommand;
                if (code == "edit-record")
                    Edit(row.id);
                else
                    CommandFactory.Execute(code, row.id);
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e) => CommandFactory.Execute("add-record", this, Guid.Empty, ExecutedCommand, Parameters);

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
                    Edit(row.id);
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

                using (var conn = Db.OpenConnection())
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            ExecutedCommand.Browser().Delete(conn, transaction, row.id);
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
                            Guid? id = conn.Query<Guid?>("select copy_entity(:kind_id, :copy_id)", new { kind_id = ExecutedCommand.EntityKind.Id, copy_id = row.id }, transaction).FirstOrDefault();
                            transaction.Commit();
                            if (id.HasValue && MessageBox.Show("Открыть окно для редактрования?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Edit(id.Value);
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
                HistoryWindow win = new HistoryWindow(row.id);
                win.ShowDialog();
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
            SelectDateRangeWindow win = new SelectDateRangeWindow();
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
                rowIndex = e.RowIndex;
                columnIndex = e.ColumnIndex;
                record = (e.ContextMenuInfo as RowContextMenuInfo).Row;
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
            CommandFactory.Execute("open-browser-code", ExecutedCommand);
        }

        private void gridContent_VisibleChanged(object sender, EventArgs e)
        {
            commandCollection.UpdateSeparatorVisibility();
        }
    }
}
