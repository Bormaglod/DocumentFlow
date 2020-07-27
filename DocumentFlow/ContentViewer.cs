//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.03.2020
// Time: 15:27
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
#if USE_SETTINGS
    using System.IO;
#endif
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using NHibernate;
    using NHibernate.Transform;
    using Npgsql;
    using Syncfusion.Windows.Forms.Tools;
    using Syncfusion.WinForms.DataGrid;
    using Syncfusion.WinForms.DataGrid.Enums;
    using Syncfusion.WinForms.DataGrid.Events;
    using Syncfusion.WinForms.DataGrid.Interactivity;
    using DocumentFlow.Core;
    using DocumentFlow.DataSchema;
    using DocumentFlow.DataSchema.Types.Converters;
    using DocumentFlow.Data.Core;
    using DocumentFlow.Data.Entities;
    using DocumentFlow.Controls;
    using DocumentFlow.Controls.Extensions;
    using DocumentFlow.Controls.Renderers;
    using DocumentFlow.DataSchema.Types.Core;

    public partial class ContentViewer : UserControl, IPage, ISettings
    {
        private class ToolbarGroup
        {
            private ToolStripSeparator separator;
            private readonly List<ToolStripItem> items;

            public ToolbarGroup(string groupName) : this(groupName, true) { }

            public ToolbarGroup(string groupName, bool createSeparator)
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
                    items.Add(item);
            }

            public void AddItemsToTollbar(ToolStrip bar)
            {
                bar.Items.Add(separator);
                foreach (ToolStripItem item in items)
                {
                    bar.Items.Add(item);
                }
            }

            public void Refresh()
            {
                if (separator != null)
                    separator.Visible = items.Any(x => x.Visible);
            }

            public override string ToString() => Name;
        }

        private class ToolbarGroups
        {
            private List<ToolbarGroup> toolbar { get; } = new List<ToolbarGroup>();
            private List<ToolbarGroup> menu { get; } = new List<ToolbarGroup>();

            private void Add(List<ToolbarGroup> list, ToolStrip toolStrip, ToolbarGroup toolbarGroup)
            {
                toolbarGroup.AddItemsToTollbar(toolStrip);
                list.Add(toolbarGroup);
            }

            private void Add(List<ToolbarGroup> list, ToolStripItem item, string toolbarGroupName, bool createSeparator)
            {
                ToolbarGroup g = list.FirstOrDefault(x => x.Name == toolbarGroupName);
                if (g == null)
                {
                    g = new ToolbarGroup(toolbarGroupName, createSeparator);
                    list.Add(g);
                }

                g.Add(item);
            }

            public void AddToolbar(ToolStrip toolStrip, ToolbarGroup toolbarGroup) => Add(toolbar, toolStrip, toolbarGroup);

            public void AddMenu(ToolStrip toolStrip, ToolbarGroup toolbarGroup) => Add(menu, toolStrip, toolbarGroup);

            public void AddToolbarItem(ToolStripItem item, string toolbarGroupName) => Add(toolbar, item, toolbarGroupName, false);

            public void AddMenuItem(ToolStripItem item, string toolbarGroupName) => Add(menu, item, toolbarGroupName, false);

            public void Refresh()
            {
                RefreshGroup(toolbar);
                RefreshGroup(menu);
            }

            private void RefreshGroup(IEnumerable<ToolbarGroup> groups)
            {
                foreach (ToolbarGroup bar in groups)
                {
                    bar.Refresh();
                }
            }
        }

        private class UserToolButton
        {
            public UserCommand UserCommand { get; set; }
            public Command Command { get; set; }
            public Picture Picture { get; set; }
            public string Code => Command == null ? UserCommand.Name : Command.Code;
        }

#if USE_SETTINGS
        private const string settingsFile = "viewer.json";
        private string settingsPath;
#endif
        private readonly ICommandFactory commands;
        private readonly Dictionary<GridColumn, DatasetColumn> dataColumns = new Dictionary<GridColumn, DatasetColumn>();
        private DatasetSchema schema;
        private Guid? ParentEntity;
        private readonly Command command;
        private readonly Dictionary<string, List<ToolStripItem>> commandItems = new Dictionary<string, List<ToolStripItem>>();
        private readonly ToolbarGroups groupItems = new ToolbarGroups();
        private Guid? owner;
#if USE_LISTENER
        private CancellationTokenSource listenerToken;
        private readonly ConcurrentQueue<NotifyMessage> notifies = new ConcurrentQueue<NotifyMessage>();
#endif
        private GridColumn column = null;
        private SfDataGrid grid = null;
        private Syncfusion.Data.Group group = null;
        private int rowIndex = -1;
        private int columnIndex = -1;
        private object record = null;

        public ContentViewer(ICommandFactory commandFactory, Command cmd, Guid? ownerId)
        {
            InitializeComponent();
            NewSession();

            commands = commandFactory;
            command = Session.Get<Command>(cmd.Id);
            owner = ownerId;

            CreateViewer();

#if USE_LISTENER
            timerDatabaseListen.Start();

            listenerToken = new CancellationTokenSource();
            _ = CreateListener(listenerToken.Token);
#endif
        }

        Guid IPage.Id => command.Id;

        Guid IPage.ContentId => GetCurrentId();

        void IPage.OnClosed()
        {
#if USE_LISTENER
            listenerToken.Cancel();
            timerDatabaseListen.Stop();
#endif
        }

        protected ISession Session { get; private set; }

        private void NewSession() => Session = Db.OpenSession();

#if USE_LISTENER
        private void Listener(CancellationToken token)
        {
            using (var conn = new NpgsqlConnection(Db.ConnectionString))
            {
                conn.Open();
                conn.Notification += (o, e) =>
                {
                    NotifyMessage message = JsonConvert.DeserializeObject<NotifyMessage>(e.Payload);
                    if (message.EntityId == command.EntityKind.Id)
                    {
                        if (message.Destination == MessageDestination.List && owner != null && message.ObjectId != owner)
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
                    conn.WaitAsync(token);
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

        private void CreateViewer()
        {
            try
            {
                schema = JsonConvert.DeserializeObject<DatasetSchema>(command.DataSchema, new ControlConverter());
            }
            catch (UnknownTypeException e)
            {
                MessageBox.Show(e.Message, "Ошибка JSON-данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ((ISettings)this).LoadSettings();

            Text = command.Name;

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

            if (schema?.Viewer != null)
            {
                gridContent.AllowGrouping = schema.Viewer.AllowGrouping;
                gridContent.AllowSorting = schema.Viewer.AllowSorting;
                gridContent.ShowGroupDropArea = schema.Viewer.AllowGrouping;

                switch (schema.Viewer.DataType)
                {
                    case DataType.Directory:
                        panelDocument.Visible = false;
                        breadcrumb1.Visible = command.EntityKind.HasGroup;
                        break;
                    case DataType.Document:
                        panelDirectory.Visible = false;
                        dateTimePickerFrom.Checked = schema.Viewer.FromDate != DateRanges.None;
                        dateTimePickerTo.Checked = schema.Viewer.ToDate != DateRanges.None;
                        dateTimePickerFrom.Value = DateTime.Today.FromDateRanges(schema.Viewer.FromDate);
                        dateTimePickerTo.Value = DateTime.Today.FromDateRanges(schema.Viewer.ToDate);

                        IList<ComboBoxDataItem> orgs = Session.CreateSQLQuery("select id, name from organization")
                            .SetResultTransformer(Transformers.AliasToBean<ComboBoxDataItem>())
                            .List<ComboBoxDataItem>();

                        Guid def_org = Session.CreateSQLQuery("select id from organization where default_org")
                            .UniqueResult<Guid>();

                        comboOrg.DataSource = orgs;
                        comboOrg.SelectedItem = orgs.FirstOrDefault(x => x.Id == def_org);

                        break;
                    default:
                        break;
                }

                panelCommandBar.Height = 30;
                panelCommandBar.Visible = schema.Viewer.CommandBarVisible;

                buttonAddFolder.Visible = command.EntityKind.HasGroup;
                menuAddFolder.Visible = command.EntityKind.HasGroup;

                if (schema.Viewer?.Columns != null)
                {
                    if (schema.Viewer.Columns.Any(x => x.DataField == "status_id"))
                        gridContent.CellRenderers["RowHeader"] = new CustomRowHeaderCellRenderer(() => Session, gridContent, "status_id");
                }
            }

            gridContent.CellRenderers.Remove("Image");
            gridContent.CellRenderers.Add("Image", new CustomGridImageCellRenderer(gridContent));

            CreateColumns();
            RefreshCurrenView();

            if (gridContent.AllowGrouping && schema?.Viewer?.Groups != null)
            {
                foreach (string columnName in schema.Viewer.Groups)
                {
                    GroupColumnDescription groupColumn = new GroupColumnDescription()
                    {
                        ColumnName = columnName,
                        SortGroupRecords = false
                    };

                    gridContent.GroupColumnDescriptions.Add(groupColumn);
                }
            }

            gridContent.ColumnResizing += Grid_ColumnResizing;

            CreateCommands();
            if (schema?.Viewer?.Toolbar != null)
            {
                SetButtonStyle(schema.Viewer.Toolbar.ButtonStyle);
                SetIconSize(schema.Viewer.Toolbar.IconSize);
            }

            bool can_copy = false;
            if (command.EntityKind != null)
            {
                Guid? id = Session.CreateSQLQuery("select copy_entity(:kind_id, null)")
                    .SetGuid("kind_id", command.EntityKind.Id)
                    .UniqueResult<Guid?>();
                can_copy = id.HasValue;
            }

            buttonCopy.Enabled = can_copy;
            menuCopy.Enabled = can_copy;
        }

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

        private void SetButtonStyle(ToolStripItemDisplayStyle style)
        {
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                item.DisplayStyle = style;
            }
        }

        private void SetIconSize(ButtonIconSize iconSize)
        {
            foreach (ToolStripButton item in toolStrip1.Items.OfType<ToolStripButton>())
            {
                UserToolButton button = item.Tag as UserToolButton;
                switch (iconSize)
                {
                    case ButtonIconSize.Small:
                        item.Image = button.Picture.GetImageSmall();
                        break;
                    case ButtonIconSize.Large:
                        item.Image = button.Picture.GetImageLarge();
                        break;
                    default:
                        break;
                }
            }
        }

        private void GroupAction(CommandAction action)
        {
            string table = string.IsNullOrEmpty(schema?.Viewer?.Dataset.Name) ? schema.Name : schema.Viewer.Dataset.Name;
            GroupEditor editor = new GroupEditor(table);

            bool result = false;
            switch (action)
            {
                case CommandAction.Create:
                    result = editor.Create(command.EntityKind.Id, ParentEntity);
                    break;
                case CommandAction.Edit:
                    result = editor.Edit(GetCurrentId());
                    break;
            }

            if (result)
                RefreshCurrenView();
        }

        private Guid GetCurrentId() => GetCurrentFieldValue("id", Guid.Empty);

        private int GetCurrentStatus() => GetCurrentFieldValue("status_id", 0);

        private T GetCurrentFieldValue<T>(string fieldName, T defaultValue)
        {
            if (gridContent.SelectedItem is DataRowView row && gridContent.DataSource is DataTable dt)
            {
                if (dt.Columns.Contains(fieldName))
                {
                    return (T)row[fieldName];
                }

                throw new FieldNotFoundException(schema.Viewer.Dataset.Select, fieldName);
            }

            return defaultValue;
        }

        private void Grid_ColumnResizing(object sender, ColumnResizingEventArgs e)
        {
            IList<DatasetColumn> list = schema?.Viewer?.Columns;
            if (list == null)
                return;

            GridColumn column = gridContent.Columns[e.ColumnIndex];
            DatasetColumn c = list.Where(x => x.DataField == column.MappingName).SingleOrDefault();
            if (c != null)
            {
                c.Width = Convert.ToInt32(e.Width);
            }
        }

        private void CreateQueryParameters(NpgsqlCommand command, params (string paramName, object paramValue, Type type)[] variables)
        {
            Dictionary<string, (object value, Type type)> vars = new Dictionary<string, (object, Type)>() {
                { "parent_id", (ParentEntity, typeof(Guid?)) },
                { "owner_id", (owner, typeof(Guid?)) },
                { "from_date", (dateTimePickerFrom.Value, typeof(DateTime)) },
                { "to_date", (dateTimePickerTo.Value.EndOfDay(), typeof(DateTime)) },
                { "organization_id", ((comboOrg.SelectedItem as ComboBoxDataItem)?.Id, typeof(Guid?)) }
            };

            foreach (var (paramName, paramValue, type) in variables)
            {
                vars.Add(paramName, (paramValue, type));
            }

            foreach (Match match in Regex.Matches(command.CommandText, Db.ParameterPattern))
            {
                string prop = match.Groups[1].Value;
                command.CreateParameter(prop, vars[prop].value, vars[prop].type);
            }
        }

        private void RefreshCurrenView()
        {
            if (string.IsNullOrEmpty(schema?.Viewer?.Dataset.Select))
                return;

            using (NpgsqlConnection connection = new NpgsqlConnection(Db.ConnectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand(schema.Viewer.Dataset.Select, connection);
                CreateQueryParameters(command);

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataTable dt = ds.Tables[0];
                dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

                gridContent.DataSource = dt;
            }
        }

        private void RefreshRow(Guid id)
        {
            if (string.IsNullOrEmpty(schema?.Viewer?.Dataset.SelectByID))
            {
                MessageBox.Show("Отсутствует команда запроса по указанному ID записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dt = gridContent.DataSource as DataTable;
            var cur_row = dt.Rows.Find(id);
            if (cur_row != null)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(Db.ConnectionString))
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand(schema.Viewer.Dataset.SelectByID, connection);
                    CreateQueryParameters(command, ("id", id, typeof(Guid)));

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    var new_row = ds.Tables[0].Rows[0];
                    foreach (DataColumn item in ds.Tables[0].Columns)
                    {
                        cur_row[item.ColumnName] = new_row[item.ColumnName];
                    }
                }
            }
        }

        private void AddRow(Guid id)
        {
            if (string.IsNullOrEmpty(schema?.Viewer?.Dataset.SelectByID))
            {
                MessageBox.Show("Отсутствует команда запроса по указанному ID записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(Db.ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(schema.Viewer.Dataset.SelectByID, connection);
                CreateQueryParameters(command, ("id", id, typeof(Guid)));

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                System.Data.DataRow added_row = ds.Tables[0].Rows[0];

                DataTable dt = gridContent.DataSource as DataTable;
                System.Data.DataRow new_row = dt.NewRow();

                foreach (DataColumn item in ds.Tables[0].Columns)
                {
                    new_row[item.ColumnName] = added_row[item.ColumnName];
                }

                dt.Rows.Add(new_row);
            }
        }

        private void DeleteRow(Guid id)
        {
            DataTable dt = gridContent.DataSource as DataTable;
            var cur_row = dt.Rows.Find(id);
            if (cur_row != null)
            {
                dt.Rows.Remove(cur_row);
            }
        }

        private void ChangeColumnVisible(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item && item.Tag is DatasetColumn column)
            {
                GridColumn gridColumn = gridContent.Columns.Where(x => x.MappingName == column.DataField).SingleOrDefault();
                if (gridColumn != null)
                {
                    gridColumn.Visible = item.Checked;
                    column.Visible = item.Checked;
                }
            }
        }

        private void CreateSortedColumns()
        {
            if (schema?.Viewer?.Sorts != null)
            {
                foreach (ColumnSort item in schema.Viewer.Sorts)
                {
                    SortColumnDescription sort = new SortColumnDescription()
                    {
                        ColumnName = item.ColumnName,
                        SortDirection = item.SortDirection
                    };

                    gridContent.SortColumnDescriptions.Add(sort);
                }
            }
        }

        private void CreateColumns()
        {
            if (schema?.Viewer?.Columns == null)
                return;

            foreach (DatasetColumn c in schema.Viewer.Columns)
            {
                GridColumn column = gridContent.CreateColumn(c);
                dataColumns.Add(column, c);

                ToolStripMenuItem item = new ToolStripMenuItem()
                {
                    Text = c.Text,
                    CheckOnClick = true,
                    Checked = c.Visible,
                    Enabled = c.Hideable,
                    Tag = c
                };

                item.Click += ChangeColumnVisible;
                menuVisibleColumns.DropDownItems.Add(item);
            }

            gridContent.CreateStackedColumns(schema?.Viewer?.StackedColumns);
            gridContent.CreateSummaryRow(schema?.Viewer?.Columns);
            CreateSortedColumns();
        }

        private Guid? GetCurrentGroupEntity()
        {
            Guid guid = breadcrumb1.Peek();
            return guid == Guid.Empty ? (Guid?)null : guid;
        }

        private void AddCommandItem(string code, ToolStripItem item)
        {
            if (commandItems.ContainsKey(code))
            {
                commandItems[code].Add(item);
            }
            else
            {
                commandItems.Add(code, new List<ToolStripItem>() { item });
            }
        }

        private void CreateEmbededCommands(ToolStrip toolStrip, IList<Command> commands)
        {
            foreach (var item in toolStrip.Items.OfType<ToolStripItem>().Where(x => x.Tag != null))
            {
                string[] tag = item.Tag.ToString().Split('|');

                if (!string.IsNullOrEmpty(tag[0]))
                {
                    Command cmd = commands.FirstOrDefault(x => x.Code == tag[0]);
                    if (cmd == null)
                        continue;

                    item.Tag = new UserToolButton()
                    {
                        UserCommand = null,
                        Command = cmd,
                        Picture = cmd.Picture
                    };

                    AddCommandItem(cmd.Code, item);
                }

                if (toolStrip is ContextMenuStrip)
                    groupItems.AddMenuItem(item, tag[1]);
                else
                    groupItems.AddToolbarItem(item, tag[1]);
            }
        }

        private T CreateUserToolItem<T>(UserCommand userCommand) where T : ToolStripItem, new()
        {
            Picture image = null;
            T item = null;
            Picture unknown = Session.Get<Picture>(new Guid("00e5691b-1e20-4f15-991a-aaf896bcded8")); // unknown (Не известно)

            switch (userCommand.Method)
            {
                case CommandMethod.Sql:
                    image = Session.QueryOver<Picture>()
                        .Where(x => x.Code == userCommand.Icon.ToLower())
                        .SingleOrDefault() ?? unknown;

                    item = new T()
                    {
                        Text = string.IsNullOrEmpty(userCommand.Title) ? userCommand.Name : userCommand.Title,
                        Tag = new UserToolButton()
                        {
                            UserCommand = userCommand,
                            Command = null,
                            Picture = image
                        },
                        DisplayStyle = userCommand.ShowTitle ? ToolStripItemDisplayStyle.ImageAndText : ToolStripItemDisplayStyle.Image
                    };

                    item.Click += SqlCommand_Click;

                    break;
                case CommandMethod.Embedded:
                    Command cmd = Session.QueryOver<Command>()
                        .Where(x => x.Code == userCommand.Command.ToLower())
                        .SingleOrDefault();

                    image = image ?? cmd.Picture ?? unknown;

                    item = new T()
                    {
                        Text = string.IsNullOrEmpty(cmd.Name) ? cmd.Code : cmd.Name,
                        TextImageRelation = TextImageRelation.ImageAboveText,
                        ImageScaling = ToolStripItemImageScaling.None,
                        Tag = new UserToolButton
                        {
                            UserCommand = userCommand,
                            Command = cmd,
                            Picture = image
                        },
                        DisplayStyle = userCommand.ShowTitle ? ToolStripItemDisplayStyle.ImageAndText : ToolStripItemDisplayStyle.Image
                    };

                    item.Click += EmbededCommand_Click;

                    break;
            }

            if (item != null)
            {
                if (item is ToolStripButton button)
                {
                    ToolStripItemDisplayStyle style = ToolStripItemDisplayStyle.ImageAndText;
                    ButtonIconSize size = ButtonIconSize.Large;
                    if (schema?.Viewer?.Toolbar != null)
                    {
                        style = schema.Viewer.Toolbar.ButtonStyle;
                        size = schema.Viewer.Toolbar.IconSize;
                    }

                    button.DisplayStyle = style;

                    switch (size)
                    {
                        case ButtonIconSize.Small:
                            item.Image = image.GetImageSmall();
                            break;
                        case ButtonIconSize.Large:
                            button.Image = image.GetImageLarge();
                            break;
                        default:
                            break;
                    }
                }
                else
                    item.Image = image.GetImageSmall();
            }

            AddCommandItem(((UserToolButton)(item.Tag)).Code, item);

            return item;
        }

        private void EmbededCommand_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem item && item.Tag is UserToolButton command)
            {
                List<(string, object)> values = new List<(string, object)>();
                if (command.UserCommand.Parameters != null)
                {
                    foreach (string param in command.UserCommand.Parameters)
                    {
                        values.Add((param, GetCurrentFieldValue<object>(param, null)));
                    }
                }

                commands.Execute(command.Command.Code, GetCurrentId(), values);
            }
        }

        private void SqlCommand_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CreateCommandButtons()
        {
            foreach (CommandGroup g in schema.Viewer.CommandGroups)
            {
                if (g.Commands.Where(x => x.InsertInToolbar).Any())
                {
                    ToolbarGroup toolbar = new ToolbarGroup(g.Name);
                    foreach (UserCommand item in g.Commands.Where(x => x.InsertInToolbar))
                    {
                        ToolStripButton button = CreateUserToolItem<ToolStripButton>(item);
                        toolbar.Add(button);
                    }

                    groupItems.AddToolbar(toolStrip1, toolbar);
                }
            }
        }

        private void CreateCommandMenuItems()
        {
            foreach (CommandGroup g in schema.Viewer.CommandGroups)
            {
                if (g.Commands.Where(x => x.InsertInContextMenu).Any())
                {
                    ToolbarGroup toolbar = new ToolbarGroup(g.Name);
                    foreach (UserCommand item in g.Commands.Where(x => x.InsertInContextMenu))
                    {
                        ToolStripMenuItem menu = CreateUserToolItem<ToolStripMenuItem>(item);
                        toolbar.Add(menu);
                    }

                    groupItems.AddMenu(contextRecordMenu, toolbar);
                }
            }
        }

        private void UpdateAvailabilityCommands()
        {
            if (schema?.Viewer?.CommandVisible != null)
            {
                foreach (string cmd in commandItems.Keys)
                {
                    if (schema.Viewer.CommandVisible.Contains(cmd))
                        continue;

                    foreach (ToolStripItem item in commandItems[cmd])
                    {
                        item.Visible = false;
                    }
                }

                groupItems.Refresh();
            }
        }

        private void CreateCommands()
        {
            IList<Command> commands = Session.QueryOver<Command>().List();

            CreateEmbededCommands(toolStrip1, commands);
            CreateEmbededCommands(contextRecordMenu, commands);

            if (schema?.Viewer?.CommandGroups != null)
            {
                CreateCommandButtons();
                CreateCommandMenuItems();
            }

            UpdateAvailabilityCommands();
        }

        private void RefreshEntities(object sender, EventArgs e) => RefreshCurrenView();

        private void Edit()
        {
            Guid id = GetCurrentId();
            if (id != Guid.Empty)
            {
                Edit(id);
            }
        }

        private void Edit(Guid id) => commands.Execute("edit-record", new EditorParams() { Id = id, Parent = ParentEntity, Owner = owner, Kind = command.EntityKind, Editor = schema?.Editor });

        private void breadcrumb1_CrumbClick(object sender, CrumbClickEventArgs e)
        {
            switch (e.Kind)
            {
                case ToolButtonKind.Up:
                    breadcrumb1.Pop();
                    ParentEntity = GetCurrentGroupEntity();
                    RefreshCurrenView();
                    break;
                case ToolButtonKind.Refresh:
                    ParentEntity = GetCurrentGroupEntity();
                    RefreshCurrenView();
                    break;
                case ToolButtonKind.Home:
                    breadcrumb1.Clear();
                    ParentEntity = null;
                    RefreshCurrenView();
                    break;
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
            DatasetColumn c = dataColumns[e.Column];
            if (!string.IsNullOrEmpty(c.NegativeValueColor))
            {
                bool negative = false;
                switch (c.Type)
                {
                    case DatasetColumnType.Integer:
                        negative = Convert.ToInt32(e.DisplayText) < 0;
                        break;
                    case DatasetColumnType.Numeric:
                        negative = Convert.ToDecimal(e.DisplayText) < 0;
                        break;
                }

                if (negative)
                    e.Style.TextColor = ColorTranslator.FromHtml(c.NegativeValueColor);
            }
        }

        private void gridContent_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            if (gridContent.SelectedItem is DataRowView row)
            {
                DataTable dt = gridContent.DataSource as DataTable;
                if (dt.Columns.Contains("status_id"))
                {
                    int status = Convert.ToInt32(row["status_id"]);
                    if (status == 500)
                    {
                        ParentEntity = (Guid)row["id"];
                        breadcrumb1.Push(ParentEntity.Value, row["name"].ToString());
                        RefreshCurrenView();
                        return;
                    }
                }

                string code = string.IsNullOrEmpty(schema?.Viewer.DoubleClickCommand) ? "edit-record" : schema.Viewer.DoubleClickCommand;
                if (code == "edit-record")
                    Edit();
                else
                    commands.Execute(code, GetCurrentId());
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e) => commands.Execute("add-record", new EditorParams() { Id = Guid.Empty, Parent = ParentEntity, Owner = owner, Kind = command.EntityKind, Editor = schema?.Editor });

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (GetCurrentStatus() == 500)
                GroupAction(CommandAction.Edit);
            else
                Edit();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string deleteSql = schema?.Editor.Dataset.DeleteDefault(command.EntityKind.Code);
            if (deleteSql == null)
            {
                MessageBox.Show("Не указана команда для удаления (editor/dataset/delete)");
                return;
            }

            bool delete;
            if (GetCurrentStatus() == 500)
            {
                delete = MessageBox.Show("Удаление группы приведет к удалению всего содержимого группы. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            }
            else
            {
                delete = MessageBox.Show("Вы действительно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            }

            if (!delete)
                return;

            using (var transaction = Session.BeginTransaction())
            {
                try
                {
                    Session.CreateSQLQuery(deleteSql)
                        .SetGuid("id", GetCurrentId())
                        .ExecuteUpdate();
                    transaction.Commit();
                    RefreshCurrenView();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionHelper.MesssageBox(ex);
                }
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            using (var transaction = Session.BeginTransaction())
            {
                try
                {
                    Guid? id = Session.CreateSQLQuery("select copy_entity(:kind_id, :copy_id)")
                            .SetGuid("kind_id", command.EntityKind.Id)
                            .SetGuid("copy_id", GetCurrentId())
                            .UniqueResult<Guid?>();
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

        private void buttonAddFolder_Click(object sender, EventArgs e) => GroupAction(CommandAction.Create);

        private void buttonHistory_Click(object sender, EventArgs e)
        {
            if (gridContent.SelectedItem is DataRowView row)
            {
                HistoryWindow win = new HistoryWindow(Session, (Guid)row["id"]);
                win.ShowDialog();
            }
        }

        private void menuCopyClipboard_Click(object sender, EventArgs e)
        {
            if (gridContent.CurrentCell == null || gridContent.CurrentItem == null)
                return;

            string propName = gridContent.CurrentCell.Column.MappingName;
            DataRowView row = gridContent.SelectedItem as DataRowView;
            object value = row[propName];
            if (value != null)
                Clipboard.SetText(value.ToString());
        }

        private void timerDatabaseListen_Tick(object sender, EventArgs e)
        {
#if USE_LISTENER
            if (notifies.TryDequeue(out NotifyMessage message))
            {
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
    }
}
