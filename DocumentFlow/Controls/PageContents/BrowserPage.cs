//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Controls.Renderers;
using DocumentFlow.Controls.Tools;
using DocumentFlow.Data;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Tools;
using DocumentFlow.Dialogs;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Interfaces;
using DocumentFlow.Properties;
using DocumentFlow.ReportEngine;
using DocumentFlow.Settings;
using DocumentFlow.Tools;
using DocumentFlow.Tools.Reflection;

using FastReport;
using FastReport.Utils;

using Humanizer;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGrid.Interactivity;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.Input.Enums;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

using SyncEnums = Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Controls.PageContents;

[ToolboxItem(false)]
public abstract partial class BrowserPage<T> : UserControl, IBrowserPage
    where T : class, IIdentifier<Guid>
{
    [GeneratedRegex("^I(.+)Browser$")]
    private static partial Regex SettingsKeyRegex();

    private class CustomComparer : IComparer<object>, ISortDirection
    {
        private readonly PropertyInfo? property;

        public CustomComparer(Type entityType, string mappingName)
        {
            property = entityType.GetProperty(mappingName);
        }

        public int Compare(object? x, object? y)
        {
            if (x == y || property == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            int res;
            if (x is IDirectory parent_x && y is IDirectory parent_y && (parent_x.IsFolder != parent_y.IsFolder))
            {
                res = parent_x.IsFolder ? -1 : 1;
            }
            else
            {
                if (x is IDocumentInfo && y is IDocumentInfo)
                {
                    res = CompareObject(property.GetValue(x), property.GetValue(y));
                }
                else if (x is Syncfusion.Data.Group grp_x && y is Syncfusion.Data.Group grp_y)
                {
                    res = CompareObject(grp_x.Key, grp_y.Key);
                }
                else
                {
                    res = CompareString(x, y);
                }
            }

            return SortDirection == ListSortDirection.Ascending ? res : -res;
        }

        public ListSortDirection SortDirection { get; set; }

        private static int CompareObject(object? x, object? y)
        {
            if (x is IComparable cmp_x && y is IComparable cmp_y)
            {
                return cmp_x.CompareTo(cmp_y);
            }
            else
            {
                return CompareString(x, y);
            }
        }

        private static int CompareString(object? x, object? y)
        {
            if (x == y) return 0;

            string? x_str = x?.ToString();
            if (x_str == null)
                return -1;

            string? y_str = y?.ToString();
            if (y_str == null)
                return 1;

            return x_str.CompareTo(y_str);
        }
    }

    public delegate string ShowToolTipMethod(T document);

    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;
    private readonly IRepository<Guid, T> repository;
    private readonly IRowHeaderImage? rowHeaderImage;
    private readonly IBreadcrumb? navigator;
    private readonly IFilter? filter;
    private readonly IToolBar toolBar;
    private readonly IContextMenu contextMenu;
    private readonly IEnumerable<ICreationBased>? creations;
    private GroupColumnCollection? groupColumnCollection;

    private ShowToolTipMethod? showToolTip;
    private BrowserSettings settings;
    private IDocumentInfo? owner = null;
    private Guid? parentId = null;
    private bool readOnly = false;
    private readonly Type browserType;
    private bool moveToEnd = false;

    private SfDataGrid? grid = null;
    private GridColumn? column = null;
    private Syncfusion.Data.Group? group = null;
    private T? record = default;

    private readonly List<GridColumn> hiddens = new();

    protected BrowserPage(
        IServiceProvider services,
        IPageManager pageManager,
        IRepository<Guid, T> repository,
        IConfiguration configuration,
        IRowHeaderImage? rowHeaderImage = null,
        IBreadcrumb? navigator = null,
        IFilter? filter = null,
        IEnumerable<ICreationBased>? creations = null)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;
        this.repository = repository;
        this.rowHeaderImage = rowHeaderImage;
        this.navigator = navigator;
        this.filter = filter;
        this.creations = creations;

        settings = new(); ;

        var section = configuration.GetSection(GetType().Name);
        section.Bind(settings);

        if (filter != null)
        {
            if (filter.Settings != null)
            {
                section.GetSection("Filter").Bind(filter.Settings);
            }

            filter.SettingsLoaded();
        }

        services.GetRequiredService<IHostApp>().OnAppNotify += App_OnAppNotify;

        toolBar = new PageToolBar(toolStrip1)
        {
            Buttons = new Dictionary<ToolStripItem, (Image, Image)>()
            {
                [buttonCreate] = (Resources.icons8_file_add_16, Resources.icons8_file_add_30),
                [buttonEdit] = (Resources.icons8_file_edit_16, Resources.icons8_file_edit_30),
                [buttonDelete] = (Resources.icons8_file_delete_16, Resources.icons8_file_delete_30),
                [buttonCopy] = (Resources.icons8_copy_edit_16, Resources.icons8_copy_edit_30),
                [buttonCreateGroup] = (Resources.icons8_folder_add_16, Resources.icons8_folder_add_30),
                [buttonRefresh] = (Resources.icons8_refresh_16, Resources.icons8_refresh_30),
                [buttonSettings] = (Resources.icons8_settings_16, Resources.icons8_settings_30),
                [buttonPrint] = (Resources.icons8_print_16, Resources.icons8_print_30),
                [buttonWipe] = (Resources.trash_16, Resources.trash_32)
            }
        };

        contextMenu = new ContextMenu(contextRecordMenu.Items);

        browserType = GetType().GetInterfaces().First(x => x.Name == $"I{GetType().Name}");

        ConfigureBrowser();
    }

    public IToolBar ToolBar => toolBar;
    public IContextMenu ContextMenu => contextMenu;
    public IGroupColumnCollection? GroupColumnCollection => groupColumnCollection;

    public ShowToolTipMethod? ShowToolTip
    {
        get => showToolTip;

        set
        {
            gridContent.ShowToolTip = value != null;
            showToolTip = value;
        }
    }

    public bool AllowSorting
    {
        get => gridContent.AllowSorting;
        set => gridContent.AllowSorting = value;
    }

    public Guid? ParentId
    {
        get => parentId;
        set
        {
            if (parentId != value)
            {
                parentId = value;
                OnChangeParent();
            }
        }
    }

    public Guid? OwnerDocument => owner?.Id;

    /// <summary>
    /// Свойство определяет возможность редактирования записей окна.
    /// </summary>
    public bool ReadOnly
    {
        get => readOnly;
        set
        {
            readOnly = value;
            toolStrip1.Enabled = !value;
            ConfigureCommands();
        }
    }

    protected BrowserSettings Settings => settings;

    protected T? CurrentDocument => gridContent.SelectedItem as T;

    public void RegisterReport(IReport report)
    {
        ToolStripMenuItem menuItem = new(report.Title)
        {
            Tag = report
        };

        //menuItem.Click += OpenReportClick;

        buttonPrint.DropDownItems.Add(menuItem);

        if (buttonPrint.DropDownItems.Count == 1)
        {
            buttonPrint.Tag = report;
        }
    }

    /// <summary>
    /// Метод предназначен для обновления текущего окна с уже установленными значениями фильтра,
    /// параметров репозитория и т.д. Он вызывается при нажатии на кнопку "Обновить"
    /// </summary>
    public void RefreshPage()
    {
        RefreshColumns();
        RefreshView();
    }

    /// <summary>
    /// Метод вызывается для обновления данных окна с установкой всех необходимых
    /// параметров (фильтра, репозитория и т.д.). Он вызывается при создании окна.
    /// </summary>
    /// <param name="owner">Документ по отношению к которому записи этого окна являются зависимыми.</param>
    public void UpdatePage(IDocumentInfo? owner)
    {
        this.owner = owner;
        if (filter != null)
        {
            filter.OwnerId = owner?.Id;
        }

        if (settings.Columns != null)
        {
            foreach (var item in settings.Columns)
            {
                item.Hidden = hiddens.FirstOrDefault(x => x.MappingName == item.Name) != null;
            }
        }

        CustomizeColumns(settings);

        if (settings.Groups != null && groupColumnCollection != null)
        {
            groupColumnCollection.SetSelectedGroups(settings.Groups.OrderBy(x => x.Order).Select(x => x.Name));
        }

        RefreshColumns();
        RefreshView();

        if (moveToEnd && gridContent.RowCount > 0)
        {
            int rows = 0;

            if (gridContent.View.GroupDescriptions.Count > 0)
            {
                rows = gridContent.View.TopLevelGroup.DisplayElements.Count;
            }
            else
            {
                rows = gridContent.View.Records.Count;
            }

            // FIX: Неправильное поведение при скроллинге окна при установленнном выделении
            // Если количество записей больше, чем помещается в окно, то при установке выделения не
            // происходит перемещение окна в конец таблицы
            // gridContent.SelectedIndex = rows - 2;
            gridContent.TableControl.ScrollRows.ScrollInView(rows);
            gridContent.TableControl.ScrollRows.UpdateScrollBar();
        }
    }

    public void NotifyPageClosing()
    {
        UpdateSettingsColumn();
        if (groupColumnCollection != null)
        {
            var groups = new List<GroupSettings>();
            foreach (var grp in groupColumnCollection.SelectedGroups)
            {
                groups.Add(new GroupSettings()
                {
                    Order = grp.Order,
                    Name = grp.Name
                });
            }

            settings.Groups = groups;
        }

        settings.Save(GetType().Name, filter);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }

    protected virtual void DoBeforeRefreshPage() { }

    protected virtual bool DocumentIsReadOnly(T document) => false;

    protected virtual void DoContextRecordMenuOpening() { }

    protected void MoveToEnd() => moveToEnd = true;

    protected void RefreshColumns()
    {
        ConfigureVisibleStatusColumns();
        ConfigureColumns();
    }

    protected void RefreshView()
    {
        DoBeforeRefreshPage();

        var list = repository switch
        {
            IDirectoryRepository<T> directoryRepository => directoryRepository.GetByParent(ParentId, filter),
            IOwnedRepository<Guid, T> ownedRepository => ownedRepository.GetByOwner(owner?.Id, filter),
            _ => repository.GetListUserDefined(filter),
        };

        try
        {
            gridContent.DataSource = new ObservableCollection<T>(list);
        }
        catch
        {
            gridContent.DataSource = null;
            gridContent.DataSource = list;
        }

        ConfigureCommands();
    }

    protected GridTextColumn CreateText(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, bool visible = true, bool hidden = true)
    {
        var gridColumn = new GridTextColumn()
        {
            MappingName = memberExpression.ToMember().Name,
            HeaderText = headerText,
            Visible = visible
        };

        SetDefaultColumnProperty(gridColumn, width, hidden);

        return gridColumn;
    }

    protected GridNumericColumn CreateNumeric(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, bool visible = true, bool hidden = true, FormatMode mode = FormatMode.Numeric, int decimalDigits = 0, string? format = null)
    {
        NumberFormatInfo numberFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        numberFormat.NumberDecimalDigits = decimalDigits;
        numberFormat.PercentDecimalDigits = decimalDigits;

        var gridColumn = new GridNumericColumn()
        {
            MappingName = memberExpression.ToMember().Name,
            HeaderText = headerText,
            FormatMode = mode,
            NumberFormatInfo = numberFormat,
            Visible = visible
        };

        if (!string.IsNullOrEmpty(format))
        {
            gridColumn.Format = format;
        }

        SetDefaultColumnProperty(gridColumn, width, hidden);

        return gridColumn;
    }

    protected GridNumericColumn CreateCurrency(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, bool visible = true, bool hidden = true, string? format = null)
    {
        var column = CreateNumeric(memberExpression, headerText, width, visible, hidden, FormatMode.Currency, 2, format);
        column.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
        return column;
    }

    protected GridCheckBoxColumn CreateBoolean(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, bool visible = true, bool hidden = true)
    {
        var member = memberExpression.ToMember();
        bool isNullable = false;
        if (member is PropertyInfo prop)
        {
            isNullable = prop.PropertyType == typeof(bool?);
        }

        var gridColumn = new GridCheckBoxColumn()
        {
            MappingName = member.Name,
            HeaderText = headerText,
            Visible = visible,
            AllowThreeState = isNullable
        };

        SetDefaultColumnProperty(gridColumn, width, hidden);

        return gridColumn;
    }

    protected GridProgressBarColumn CreateProgress(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, bool visible = true, bool hidden = true)
    {
        var gridColumn = new GridProgressBarColumn()
        {
            MappingName = memberExpression.ToMember().Name,
            HeaderText = headerText,
            Visible = visible,
            Maximum = 100,
            Minimum = 0,
            ValueMode = SyncEnums.ProgressBarValueMode.Percentage
        };

        SetDefaultColumnProperty(gridColumn, width, hidden);

        return gridColumn;
    }

    protected GridDateTimeColumn CreateDateTime(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, string format = "dd.MM.yyyy HH:mm:ss", bool visible = true, bool hidden = true)
    {
        var gridColumn = new GridDateTimeColumn()
        {
            MappingName = memberExpression.ToMember().Name,
            HeaderText = headerText,
            Visible = visible,
            Pattern = DateTimePattern.Custom,
            Format = format
        };

        SetDefaultColumnProperty(gridColumn, width, hidden);

        return gridColumn;
    }

    protected GridImageColumn CreateImage(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, bool visible = true, bool hidden = true)
    {
        var gridColumn = new GridImageColumn()
        {
            MappingName = memberExpression.ToMember().Name,
            HeaderText = headerText,
            Visible = visible
        };

        SetDefaultColumnProperty(gridColumn, width, hidden);

        return gridColumn;
    }

    protected virtual GridColumn[] GetColumnsAfter(string mappingName) => Array.Empty<GridColumn>();

    protected void AddColumns(GridColumn[] columns)
    {
        for (int i = 0; i < columns.Length; i++)
        {
            AddColumn(columns[i], hiddens.Contains(columns[i]));

            var inserting = GetColumnsAfter(columns[i].MappingName);

            for (int j = 0; j < inserting.Length; j++)
            {
                AddColumn(inserting[j], hiddens.Contains(inserting[j]));
            }
        }
    }

    protected void AddSortColumns(Dictionary<GridColumn, ListSortDirection> columns)
    {
        foreach (var item in columns)
        {
            SortColumnDescription sort = new()
            {
                ColumnName = item.Key.MappingName,
                SortDirection = item.Value
            };

            gridContent.SortColumnDescriptions.Add(sort);
        }
    }

    protected virtual void OnChangeParent() { }

    protected virtual void BrowserCellStyle(T document, string column, CellStyleInfo style) { }

    protected virtual void BrowserImageStyle(T document, string column, ImageCellStyle style) { }

    protected virtual void ConfigureColumns() { }

    protected IGroupColumnCollection CreateGrouping()
    {
        gridContent.AllowGrouping = true;
        gridContent.ShowGroupDropArea = true;

        groupColumnCollection = new GroupColumnCollection(gridContent);
        return groupColumnCollection;
    }

    protected void RefreshGrid() => gridContent.Refresh();

    protected ISummary CreateSummaryRow(SyncEnums.VerticalPosition position, bool withGroups = false)
    {
        gridContent.TableSummaryRows.Clear();
        gridContent.GroupSummaryRows.Clear();

        GridTableSummaryRow tableSummaryRow = new()
        {
            Name = "TableRowSummary",
            ShowSummaryInRow = false,
            Position = position
        };

        gridContent.TableSummaryRows.Add(tableSummaryRow);

        GridSummaryRow? groupSummaryRow = null;
        if (withGroups)
        {
            groupSummaryRow = new GridSummaryRow()
            {
                Name = "GroupRowSummary",
                ShowSummaryInRow = false
            };

            gridContent.GroupSummaryRows.Add(groupSummaryRow);
        }

        return new SummaryRowData(tableSummaryRow, groupSummaryRow);
    }

    protected void ClearStackedRows() => gridContent.StackedHeaderRows.Clear();

    protected void CreateStackedColumns(string header, string[] columns)
    {
        StackedHeaderRow? stackedHeaderRow = gridContent.StackedHeaderRows.FirstOrDefault();
        if (stackedHeaderRow == null)
        {
            stackedHeaderRow = new();
            gridContent.StackedHeaderRows.Add(stackedHeaderRow);
        }

        var childColumns = string.Join(',', columns);
        stackedHeaderRow.StackedColumns.Add(new StackedColumn() { ChildColumns = childColumns, HeaderText = header });
    }

    protected void CreateStackedColumns(string header, GridColumn[] columns)
    {
        StackedHeaderRow? stackedHeaderRow = gridContent.StackedHeaderRows.FirstOrDefault();
        if (stackedHeaderRow == null)
        {
            stackedHeaderRow = new();
            gridContent.StackedHeaderRows.Add(stackedHeaderRow);
        }

        var childColumns = string.Join(',', columns.Select(c => c.MappingName));
        stackedHeaderRow.StackedColumns.Add(new StackedColumn() { ChildColumns = childColumns, HeaderText = header });
    }

    protected void RefreshRow(T row)
    {
        if (gridContent.DataSource is IList<T> list)
        {
            T loadedRow = repository.Get(row.Id, ignoreAdjustedQuery: true);
            list[list.IndexOf(row)] = loadedRow;
        }
    }

    /// <summary>
    /// Этот метод необходим для включения отображения строки предпросмотра. Он должен вызываться только после создания всех
    /// колонок.
    /// </summary>
    /// <param name="mode">Способ определения высоты строки предпросмотра.</param>
    /// <param name="height">Высота строки предпросмотра, если mode = <see cref="PreviewRowHeightMode.Fixed"/></param>
    protected void AllowDrawPreviewRow(SyncEnums.PreviewRowHeightMode mode = SyncEnums.PreviewRowHeightMode.Fixed, int height = 28)
    {
        gridContent.DrawPreviewRow += GridContentDrawPreviewRow;
        gridContent.PreviewRowExpanding += GridContent_PreviewRowExpanding;
        gridContent.ShowPreviewRow = true;
        gridContent.PreviewRowHeightMode = mode;
        gridContent.PreviewRowHeight = height;
    }

    protected virtual bool OnDrawPreviewRow(Graphics graphics, T row, Rectangle bounds, PreviewRowStyleInfo style) => false;

    //protected virtual void OnActionPreviewDoubleClick(int x, int y) { }*/

    /// <summary>
    /// Метод возвращает true, если для строки возможно показать дополнительную информацию в строке предпросмотра. Для определения
    /// содержимого этой строки надо переопределить метод <see cref="OnDrawPreviewRow(Graphics, T, Rectangle, PreviewRowStyleInfo)"/>.
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    protected virtual bool CanExpandPreview(T row) => false;

    private void UpdateSettingsColumn()
    {
        var list = new List<ColumnSettings>();
        foreach (var item in gridContent.Columns.Where(x => x is not GridUnboundColumn))
        {
            ColumnSettings column = new()
            {
                Name = item.MappingName,
                Header = item.HeaderText,
                Visible = item.Visible,
                Width = item.AutoSizeColumnsMode == SyncEnums.AutoSizeColumnsMode.None ? (double.IsNormal(item.Width) ? Convert.ToInt32(item.Width) : 0) : null,
                AutoSizeMode = item.AutoSizeColumnsMode,
                Hidden = hiddens.Contains(item)
            };

            list.Add(column);
        }

        settings.Columns = list;
    }

    private void CustomizeColumns(BrowserSettings b_settings)
    {
        if (b_settings.Columns == null)
        {
            return;
        }

        // FIX: При установленном ShowPreviewRow и попытке присвоить GridColumn.Width какое-либо значение выпадает ошибка
        // Исключение типа "System.ArgumentOutOfRangeException" возникло в Syncfusion.GridCommon.WinForms.dll, но не было обработано в коде пользователя 13 out of range 0 to 12
        // Поэтому настройка колонок для данного случая временно отключена
        // Проверено в Syncfusion версий 20.4.0.43, 20.4.0.44, 20.4.0.48
        if (gridContent.ShowPreviewRow)
        {
            return;
        }

        foreach (var (item, column) in from item in b_settings.Columns
                                       let column = gridContent.Columns.FirstOrDefault(x => x.MappingName == item.Name)
                                       select (item, column))
        {
            if (column == null)
            {
                continue;
            }

            if (item.AutoSizeMode == SyncEnums.AutoSizeColumnsMode.None && item.Width != null)
            {
                int n = item.Width.Value;
                column.Width = n;
            }
            else
            {
                column.Width = double.NaN;
            }

            column.HeaderText = item.Header;
            column.AutoSizeColumnsMode = item.AutoSizeMode;
        }
    }

    private void AddColumn(GridColumn gridColumn, bool hidden)
    {
        gridContent.Columns.Add(gridColumn);

        var item = new ToolStripMenuItem()
        {
            Text = gridColumn.HeaderText,
            CheckOnClick = true,
            Checked = gridColumn.Visible,
            Tag = gridColumn
        };

        if (!hidden)
        {
            item.Enabled = false;
        }

        item.Click += OnChangeColumnVisible;
        menuVisibleColumns.DropDownItems.Add(item);

        gridContent.SortComparers.Add(
            new SortComparer()
            {
                Comparer = new CustomComparer(typeof(T), gridColumn.MappingName),
                PropertyName = gridColumn.MappingName
            });
    }

    private void SetDefaultColumnProperty(GridColumn column, int width, bool hidden)
    {
        if (width > 0)
        {
            column.Width = width;
        }

        if (hidden)
        {
            hiddens.Add(column);
        }
    }

    private void ConfigureBrowser()
    {
        if (repository is not ISettingDocumentRefs setting || setting.Enabled)
        {
            // Свойству Format обязательно необходимо устанавливать значение. 
            // Объекты, которые содежатся в gridContent.DataSource, реализуют интерфейс INotifyPropertyChanged.
            // Если при изменении свойства объекта вызвать метод NotifyPropertyChanged приводящий к вызову события PropertyChanged,
            // то gridContent обновит содержимое соответствующей строки (или ячейки). При этом в процессе обновления
            // вызывается метод Syncfusion.WinForms.DataGrid.Data.QueryableCollectionViewWrapper.CanUpdateSummary в 
            // котором происходит обращение к свойству Format колонки GridUnboundColumn, а при создании этой колонки
            // свойство инициализируется в null.
            var attach = new GridUnboundColumn()
            {
                MappingName = "AttachmentImage",
                HeaderText = string.Empty,
                Format = string.Empty,
                Width = gridContent.RowHeight,
                AllowFiltering = false,
                AllowSorting = false,
                AllowResizing = false,
                AllowEditing = false,
                AllowDragging = false,
                AllowGrouping = false,
                AllowHeaderTextWithImage = false
            };

            gridContent.Columns.Add(attach);
        }

        gridContent.ColumnHeaderContextMenu = contextHeaderMenu;
        gridContent.RowHeaderContextMenu = contextRowMenu;
        gridContent.RecordContextMenu = contextRecordMenu;
        gridContent.GroupDropAreaContextMenu = contextGroupMenu;
        gridContent.GroupDropAreaItemContextMenu = contextGroupItemMenu;
        gridContent.GroupCaptionContextMenu = contextGroupCaptionMenu;

        gridContent.Style.ProgressBarStyle.AllowForegroundSegments = true;

        gridContent.CellRenderers["RowHeader"] = new CustomRowHeaderCellRenderer(gridContent, rowHeaderImage);

        gridContent.CellRenderers.Remove("Unbound");
        gridContent.CellRenderers.Add("Unbound", new CustomUnboundCellRenderer(gridContent));

        gridContent.CellRenderers.Remove("TableSummary");
        gridContent.CellRenderers.Add("TableSummary", new CustomGridTableSummaryRenderer());

        gridContent.CellRenderers.Remove("GroupSummary");
        gridContent.CellRenderers.Add("GroupSummary", new CustomGridTableSummaryRenderer());

        panelTools.AutoSize = true;
        if (navigator is Control controlNavigator)
        {
            Panel panelNavigator = new()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(4, 3, 4, 3),
                Margin = new Padding(4, 3, 4, 3),
                Height = 30,
                BackColor = SystemColors.Window
            };

            controlNavigator.Dock = DockStyle.Fill;

            panelNavigator.Controls.Add(controlNavigator);

            navigator.CrumbClick += DataNavigator_CrumbClick;
            panelTools.Controls.Add(panelNavigator);
        }

        if (navigator != null)
        {
            navigator.ShowButtonRefresh = false;
        }

        if (filter != null && filter is Control control)
        {
            control.Dock = DockStyle.Top;
            panelTools.Controls.Add(control);
        }

        if (creations != null)
        {
            foreach (var item in creations.Where(x => x.CanCreateDocument(typeof(T))))
            {
                ToolStripMenuItem menuItem = new(item.DocumentName);
                menuItem.Click += MenuCreateBasedOn_Click;
                menuItem.Tag = item;
                menuCreateBasedOn.DropDownItems.Add(menuItem);
            }
        }

        var descr = browserType.GetCustomAttribute<DescriptionAttribute>();
        if (descr != null)
        {
            Text = descr.Description;
        }
        else
        {
            var attr = browserType.GetCustomAttribute<MenuItemAttribute>();
            if (attr != null)
            {
                Text = attr.Name;
            }
        }

        buttonCreateGroup.Visible = navigator != null;
        menuCreateGroup.Visible = navigator != null;
        menuCreateGroup2.Visible = navigator != null;

        gridContent.TableControl.KeyUp += GridContentKeyUp;
    }

    private void MenuCreateBasedOn_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem menu && menu.Tag is ICreationBased creation && gridContent.SelectedItem is IDocumentInfo row)
        {
            try
            {
                var doc = creation.Create(row);
                pageManager.ShowEditor(creation.DocumentEditorType, doc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ExceptionHelper.Message(ex), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void ConfigureCommands()
    {
        bool canEdit = (browserType != null && pageManager.PageCanEdited(browserType)) && !ReadOnly;
        menuAddRecord.Enabled = canEdit;
        menuCreateGroup.Enabled = canEdit;
        menuDeleteRow.Enabled = canEdit;
        menuCreate.Enabled = canEdit;
        menuEdit.Enabled = canEdit;
        menuDelete.Enabled = canEdit;
        menuCopy.Enabled = canEdit;
        menuCreateGroup2.Enabled = canEdit;

        buttonCreate.Enabled = canEdit;
        buttonDelete.Enabled = canEdit;
        buttonEdit.Enabled = canEdit;
        buttonCopy.Enabled = canEdit;
        buttonCreateGroup.Enabled = canEdit;
        buttonWipe.Enabled = canEdit;
    }

    private void OnChangeColumnVisible(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem item && item.Tag is GridColumn column)
        {
            column.Visible = item.Checked;
        }
    }

    private void ShowEditor(T? editableRow = null)
    {
        if (browserType != null)
        {
            var document = editableRow as IDocumentInfo;
            var dro = editableRow != null && DocumentIsReadOnly(editableRow);
            pageManager.ShowAssociateEditor(browserType, editableRow?.Id, owner, ParentId, readOnly || (document?.Deleted ?? false) || dro);
        }
    }

    private void Edit(T editableRow)
    {
        if (
            editableRow is IDirectory dir && dir.IsFolder &&
            repository is IDirectoryRepository<T>)
        {
            var dialog = services.GetRequiredService<IGroupDialog>();
            dialog.Edit(dir, (code, name) =>
            {
                try
                {
                    dir.Code = code;
                    dir.ItemName = name;
                    repository.Update(editableRow);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionHelper.Message(e), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            });
        }
        else
        {
            ShowEditor(editableRow);
        }
    }

    private void EditSelectedRow()
    {
        if (gridContent.SelectedItem is T row)
        {
            if (row is IDirectory dir && dir.IsFolder)
            {
                ParentId = row.Id;
                navigator?.Push(dir);

                RefreshView();
            }
            else
            {
                Edit(row);
            }
        }
    }

    private void CopySelectedRow()
    {
        if (gridContent.DataSource is IList<T> list && gridContent.SelectedItem is T row)
        {
            if (row is IDirectory dir && dir.IsFolder)
            {
                MessageBox.Show("Копию группы создавать нельзя. Создайте её вручную.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                T copy = repository.CopyFrom(row);
                if (browserType != null)
                {
                    if (MessageBox.Show("Открыть окно для редактрования?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        pageManager.ShowAssociateEditor(browserType, copy.Id, owner, ParentId, false);
                    }
                }

                list.Add(copy);
            }
            catch (RepositoryException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void ExecuteRepositoryFunction(T row, Action<T> func)
    {
        if (gridContent.DataSource is IList<T> list)
        {
            try
            {
                func(row);
                list[list.IndexOf(row)] = repository.Get(row.Id, ignoreAdjustedQuery: true);
            }
            catch (RepositoryException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void Delete(T row)
    {
        bool delete;
        if (row is IDirectory dir && dir.IsFolder)
        {
            delete = MessageBox.Show("Пометка на Удаление группы приведет к аналогичному действию для всего содержимого группы. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        else
        {
            delete = MessageBox.Show("Вы действительно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        if (!delete)
            return;

        ExecuteRepositoryFunction(row, repository.Delete);
    }

    private void Undelete(T row)
    {
        if (MessageBox.Show($"Снять с \"{row}\" пометку на удаление?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            return;

        ExecuteRepositoryFunction(row, repository.Undelete);
    }

    private void SwapMarkedSelectedRow()
    {
        if (gridContent.SelectedItem is IDocumentInfo row)
        {
            if (row.Deleted)
            {
                Undelete((T)row);
            }
            else
            {
                Delete((T)row);
            }

            gridContent.TableControl.Focus();
        }
    }

    private void Accept(T row)
    {
        if (repository is IDocumentRepository<T> docs)
        {
            ExecuteRepositoryFunction(row, docs.Accept);
        }
    }

    private void CancelAcceptance(T row)
    {
        if (repository is IDocumentRepository<T> docs)
        {
            ExecuteRepositoryFunction(row, docs.CancelAcceptance);
        }
    }

    private void AddRow(IList<T> list, T document)
    {
        bool add = false;
        switch (document)
        {
            case IDirectory aDir:
                add = ParentId == aDir.ParentId && (owner == null || owner.Id == aDir.OwnerId);
                break;
            case IDocumentInfo aDoc:
                add = owner == null || owner.Id == aDoc.OwnerId;
                break;
        }

        if (add)
        {
            list.Add(document);
        }
    }

    private void RefreshBrowserList(NotifyEventArgs e)
    {
        Guid idObj = e.Document?.Id ?? e.ObjectId;
        if (idObj == Guid.Empty || idObj == owner?.Id)
        {
            RefreshView();
        }
    }

    private void DataNavigator_CrumbClick(object? sender, CrumbClickEventArgs e)
    {
        if (navigator == null)
        {
            return;
        }

        switch (e.Kind)
        {
            case ToolButtonKind.Up:
                navigator.Pop();
                ParentId = navigator.Peek();
                break;
            case ToolButtonKind.Refresh:
                ParentId = navigator.Peek();
                break;
            case ToolButtonKind.Home:
                navigator.Clear();
                ParentId = null;
                break;
        }

        RefreshView();
    }

    private void ConfigureVisibleStatusColumns()
    {
        // FIX: При установленном ShowPreviewRow и попытке присвоить GridColumn.Width какое-либо значение выпадает ошибка
        // Исключение типа "System.ArgumentOutOfRangeException" возникло в Syncfusion.GridCommon.WinForms.dll, но не было обработано в коде пользователя 13 out of range 0 to 12
        // Поэтому настройка колонок для данного случая временно отключена
        // Проверено в Syncfusion версий 20.4.0.43, 20.4.0.44, 20.4.0.48
        if (gridContent.ShowPreviewRow)
        {
            return;
        }

        foreach (var column in gridContent.Columns)
        {
            if (settings.Columns != null)
            {
                var item = settings.Columns.FirstOrDefault(x => x.Name == column.MappingName);
                if (item != null)
                {
                    column.Visible = item.Visible;
                }
            }

            var menu = menuVisibleColumns.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(x => x.Tag == column);
            if (menu == null)
            {
                continue;
            }

            menu.Checked = column.Visible;
        }
    }

    private void App_OnAppNotify(object? sender, NotifyEventArgs e)
    {
        if (e.EntityName == typeof(T).Name.Underscore() && gridContent.DataSource is IList<T> list)
        {
            switch (e.Action)
            {
                case MessageAction.Add:
                    T addingDoc = (T?)e.Document ?? repository.Get(e.ObjectId, ignoreAdjustedQuery: true);
                    AddRow(list, addingDoc);

                    break;
                case MessageAction.Delete:
                    switch (e.Destination)
                    {
                        case MessageDestination.Object:
                            var row = list.Cast<T>().FirstOrDefault(x => x.Id == e.ObjectId);
                            if (row != null)
                            {
                                list.Remove(row);
                            }

                            break;
                        case MessageDestination.List:
                            RefreshBrowserList(e);
                            break;
                        default:
                            break;
                    }

                    break;
                case MessageAction.Refresh:
                    switch (e.Destination)
                    {
                        case MessageDestination.Object:
                            T refreshDoc = (T?)e.Document ?? repository.Get(e.ObjectId, ignoreAdjustedQuery: true);
                            var row = list.Cast<T>().FirstOrDefault(x => x.Id == refreshDoc.Id);
                            if (row != null)
                            {
                                bool change = false;
                                switch (refreshDoc)
                                {
                                    case IDirectory rDir:
                                        change = ParentId == rDir.ParentId && (owner == null || owner.Id == rDir.OwnerId);
                                        break;
                                    case IDocumentInfo rDoc:
                                        change = owner == null || owner.Id == rDoc.OwnerId;
                                        break;
                                }

                                if (change)
                                {
                                    list[list.IndexOf(row)] = refreshDoc;
                                }
                                else
                                {
                                    list.Remove(row);
                                }
                            }
                            else
                            {
                                AddRow(list, refreshDoc);
                            }

                            break;
                        case MessageDestination.List:
                            RefreshBrowserList(e);
                            break;
                        default:
                            break;
                    }

                    break;

                default:
                    break;
            }
        }
    }

    private void ButtonDelete_Click(object sender, EventArgs e) => SwapMarkedSelectedRow();

    private void GridContent_ToolTipOpening(object sender, ToolTipOpeningEventArgs e)
    {
        if (ShowToolTip != null && e.Record is T row)
        {
            e.ToolTipInfo.Items[0].Text = ShowToolTip(row);
        }
        else
        {
            e.Cancel = true;
        }
    }

    private void MenuCopyClipboard_Click(object sender, EventArgs e)
    {
        if (gridContent.CurrentCell == null || gridContent.SelectedItem == null)
            return;

        string propName = gridContent.CurrentCell.Column.MappingName;

        Type type = gridContent.SelectedItem.GetType();
        PropertyInfo? prop = type.GetProperty(propName);
        if (prop != null)
        {
            object? value = prop.GetValue(gridContent.SelectedItem);
            if (value != null)
            {
                Clipboard.SetText(value.ToString() ?? string.Empty);
            }
        }
    }

    private void MenuSortAsc_Click(object sender, EventArgs e)
    {
        if (grid != null && column != null)
        {
            grid.SortColumnDescriptions.Clear();
            grid.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = column.MappingName, SortDirection = ListSortDirection.Ascending });
        }
    }

    private void MenuSortDesc_Click(object sender, EventArgs e)
    {
        if (grid != null && column != null)
        {
            grid.SortColumnDescriptions.Clear();
            grid.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = column.MappingName, SortDirection = ListSortDirection.Descending });
        }
    }

    private void MenuClearSort_Click(object sender, EventArgs e)
    {
        grid?.ClearSorting();
    }

    private void MenuHideGroupArea_Click(object sender, EventArgs e)
    {
        if (grid != null)
        {
            grid.ShowGroupDropArea = false;
        }
    }

    private void MenuExpandAll_Click(object sender, EventArgs e)
    {
        if (grid != null)
        {
            grid.ExpandAllGroup();
            menuExpandAll.Enabled = false;
            menuCollapseAll.Enabled = true;
        }
    }

    private void MenuCollapseAll_Click(object sender, EventArgs e)
    {
        if (grid != null)
        {
            grid.CollapseAllGroup();
            menuCollapseAll.Enabled = false;
            menuExpandAll.Enabled = true;
        }
    }

    private void MenuClearGroups_Click(object sender, EventArgs e)
    {
        if (grid != null)
        {
            grid.ClearGrouping();
            menuClearGroups.Enabled = false;
        }
    }

    private void MenuExpandItem_Click(object sender, EventArgs e)
    {
        if (grid != null)
        {
            grid.ExpandAllGroup();
            menuExpandItem.Enabled = false;
            menuCollapseItem.Enabled = true;
        }
    }

    private void MenuCollapseItem_Click(object sender, EventArgs e)
    {
        if (grid != null)
        {
            grid.CollapseAllGroup();
            menuCollapseItem.Enabled = false;
            menuExpandItem.Enabled = true;
        }
    }

    private void MenuClearGroupItems_Click(object sender, EventArgs e)
    {
        if (grid != null && column != null)
        {
            grid.GroupColumnDescriptions.Remove(grid.GroupColumnDescriptions.FirstOrDefault(x => x.ColumnName == column.MappingName));
        }
    }

    private void MenuGroupCaptionExpand_Click(object sender, EventArgs e)
    {
        if (grid != null && group != null)
        {
            grid.ExpandGroup(group);
        }
    }

    private void MenuGroupCaptionCollapse_Click(object sender, EventArgs e)
    {
        if (grid != null && group != null)
        {
            grid.CollapseGroup(group);
        }
    }

    private void GridContent_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
    {
        if (e.ContextMenutype == SyncEnums.ContextMenuType.GroupCaption)
        {
            grid = (e.ContextMenuInfo as RowContextMenuInfo)?.DataGrid;
            group = (e.ContextMenuInfo as RowContextMenuInfo)?.Row as Syncfusion.Data.Group;
        }

        if (e.ContextMenutype == SyncEnums.ContextMenuType.GroupDropAreaItem || e.ContextMenutype == SyncEnums.ContextMenuType.ColumnHeader)
        {
            grid = (e.ContextMenuInfo as ColumnContextMenuInfo)?.DataGrid;
            column = (e.ContextMenuInfo as ColumnContextMenuInfo)?.Column;
        }

        if (e.ContextMenutype == SyncEnums.ContextMenuType.GroupDropArea)
        {
            grid = (e.ContextMenuInfo as GroupDropAreaContextMenuInfo)?.DataGrid;
        }

        if (e.ContextMenutype == SyncEnums.ContextMenuType.Record || e.ContextMenutype == SyncEnums.ContextMenuType.RowHeader)
        {
            grid = (e.ContextMenuInfo as RowContextMenuInfo)?.DataGrid;
            record = (e.ContextMenuInfo as RowContextMenuInfo)?.Row as T;
        }
    }

    private void DocumenuMenuItem_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem menuItem && menuItem.Tag is DocumentRefs doc && CurrentDocument != null && doc.S3object != null)
        {
            try
            {
                var name = DocumentRefs.GetBucketForEntity(CurrentDocument);
                FileHelper.OpenFile(services, doc.FileName, name, doc.S3object);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ExceptionHelper.Message(ex), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void ContextRecordMenu_Opening(object sender, CancelEventArgs e)
    {
        menuDocuments.DropDownItems.Clear();
        if (record != null)
        {
            menuId.Text = $"Идентификатор: {{{record.Id}}}";
            menuId.Tag = record;

            var docs = services.GetRequiredService<IDocumentRefsRepository>().GetByOwner(record.Id);

            foreach (var doc in docs)
            {
                var menuItem = new ToolStripMenuItem
                {
                    Text = string.IsNullOrEmpty(doc.Note) ? doc.FileName : doc.Note,
                    Tag = doc
                };

                menuItem.Click += DocumenuMenuItem_Click;
                menuDocuments.DropDownItems.Add(menuItem);
            }

            menuDocuments.Visible = docs.Any();
            separatorDocuments.Visible = docs.Any();
        }
        else
        {
            menuDocuments.Visible = false;
            separatorDocuments.Visible = false;
        }

        menuId.Visible = record != null;
        separatorId.Visible = record != null;

        bool isDoc = record is IAccountingDocument;
        menuAccept.Visible = isDoc;
        menuAcceptRow.Visible = isDoc;
        menuCancelAcceptance.Visible = isDoc;
        menuCancelAccepanceRow.Visible = isDoc;
        separatorAcceptance.Visible = isDoc;
        separatorAcceptanceRow.Visible = isDoc;

        DoContextRecordMenuOpening();
    }

    private void GridContent_CellDoubleClick(object sender, CellClickEventArgs e)
    {
        if (e.DataRow is PreviewDataRow)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                //OnActionPreviewDoubleClick(e.MouseEventArgs.X, e.MouseEventArgs.Y);
            }
        }
        else
        {
            EditSelectedRow();
        }
    }

    private void ButtonRefresh_Click(object sender, EventArgs e) => RefreshPage();

    private void ButtonCreate_Click(object sender, EventArgs e) => ShowEditor();

    private void ButtonEdit_Click(object sender, EventArgs e)
    {
        if (gridContent.SelectedItem is T row)
        {
            Edit(row);
        }
    }

    private void ButtonCopy_Click(object sender, EventArgs e) => CopySelectedRow();

    private void ButtonCreateGroup_Click(object sender, EventArgs e)
    {
        if (
            gridContent.DataSource is IList<T> list &&
            repository is IDirectoryRepository<T> repo)
        {
            var dialog = services.GetRequiredService<IGroupDialog>();
            dialog.Create((code, name) =>
            {
                try
                {
                    var newId = repo.AddFolder(ParentId, code, name);
                    var folder = repository.Get(newId);
                    list.Add(folder);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionHelper.Message(e), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            });
        }
    }

    private void GridContent_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
    {
        if (e.DataRow.RowData is T row && e.Column != null)
        {
            BrowserCellStyle(row, e.Column.MappingName, e.Style);
        }
    }

    private void ButtonSettings_Click(object sender, EventArgs e)
    {
        /*if (settings != null)
        {
            var b_settings = settings.Get<BrowserSettings>();
            UpdateSettingsColumn(b_settings);

            var form = new BrowserCustomizationForm(b_settings);
            if (form.ShowDialog() == DialogResult.OK && form.Columns != null)
            {
                CustomizeColumns(b_settings);
            }
        }*/
    }

    private void MenuAcceptRow_Click(object sender, EventArgs e)
    {
        if (gridContent.SelectedItem is T row)
        {
            Accept(row);
        }
    }

    private void MenuCancelAccepanceRow_Click(object sender, EventArgs e)
    {
        if (gridContent.SelectedItem is T row)
        {
            CancelAcceptance(row);
        }
    }

    private void GridContent_QueryImageCellStyle(object sender, QueryImageCellStyleEventArgs e)
    {
        if (e.Record is T row)
        {
            var style = new ImageCellStyle()
            {
                DisplayText = e.DisplayText,
                Image = e.Image,
                TextImageRelation = e.TextImageRelation,
                ImageLayout = e.ImageLayout
            };

            BrowserImageStyle(row, e.Column.MappingName, style);
            e.DisplayText = style.DisplayText;
            e.Image = style.Image;
            e.TextImageRelation = style.TextImageRelation;
            e.ImageLayout = style.ImageLayout;
        }
    }

    private void ButtonWipe_Click(object sender, EventArgs e)
    {
        if (gridContent.DataSource is IList<T> list)
        {
            int res = MessageChoiceDialog.ShowDialog(
                "Удаление",
                "Безвозвратное удаление записи (записей).",
                new string[]
                {
                    "Удалить только текущую запись",
                    "Удалить все помеченные на удаление записи",
                    "Отменить всё. Я передумал(а)."
                },
                2);

            if (res == 0)
            {
                if (gridContent.SelectedItem is T row)
                {
                    bool delete = true;
                    if (row is IDirectory dir && dir.IsFolder)
                    {
                        delete = MessageBox.Show("Удаление группы приведет к аналогичному действию для всего содержимого группы. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
                    }

                    if (delete)
                    {
                        if (row is IDocumentInfo doc && !doc.Deleted)
                        {
                            MessageBox.Show("Запись не помечена на удаление. Операция прервана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        repository.Wipe(row);
                        list.Remove(row);
                    }
                }
            }
            else if (res == 1)
            {
                if (repository is IDirectoryRepository<T> repoDir)
                {
                    repoDir.WipeParented(owner?.Id, ParentId);
                }
                else
                {
                    if (owner == null)
                    {
                        repository.WipeAll();
                    }
                    else
                    {
                        repository.WipeAll(owner.Id);
                    }
                }

                RefreshView();
            }
        }
    }

    private void GridContentDrawPreviewRow(object? sender, DrawPreviewRowEventArgs e)
    {
        if (e.Record is RecordEntry entry && entry.Data is T row)
        {
            e.Handled = OnDrawPreviewRow(e.Graphics, row, e.Bounds, e.Style);
        }
        else
        {
            e.Handled = false;
        }
    }

    private void GridContent_PreviewRowExpanding(object? sender, PreviewRowExpandingEventArgs e)
    {
        if (e.Record is T row)
        {
            e.Cancel = !CanExpandPreview(row);
        }
        else
        {
            e.Cancel = true;
        }
    }

    private void ToolStripPrintList_Click(object sender, EventArgs e)
    {
        var list = gridContent.View.Records.Select(x => x.Data).OfType<T>();

        string dataName = typeof(T).Name;

        FastReport.Report report = new();
        report.RegisterData(list, dataName);
        report.GetDataSource(dataName).Enabled = true;

        ReportPage page1 = new()
        {
            Name = "Page1",
            LeftMargin = settings.Page.Settings.LeftMargin,
            RightMargin = settings.Page.Settings.RightMargin,
            TopMargin = settings.Page.Settings.TopMargin,
            BottomMargin = settings.Page.Settings.BottomMargin,
            PaperWidth = settings.Page.Settings.PaperWidth,
            PaperHeight = settings.Page.Settings.PaperHeight,
            MirrorMargins = settings.Page.Settings.MirrorMargins,
            ReportTitle = new()
            {
                Name = "ReportTitle1",
                Height = Units.Centimeters * 1.0f
            },
            PageFooter = new()
            {
                Name = "PageFooter1",
                Height = Units.Centimeters * 0.5f
            }
        };

        int width = settings.Page.Settings.PrintableWidth;

        TextObject textTitle = new()
        {
            Bounds = new RectangleF(0, 0, Units.Millimeters * width, Units.Centimeters * 1.0f),
            Font = settings.Page.Fonts.Title.CreateFont(),
            Text = Text,
            Name = "TextTitle"
        };

        page1.ReportTitle.Objects.Add(textTitle);

        DataBand data = new()
        {
            Name = "Data1",
            Height = Units.Centimeters * 0.5f,
            DataSource = report.GetDataSource(dataName),
            Header = new()
            {
                Name = "Header1",
                Height = Units.Centimeters * 0.5f
            }
        };

        page1.Bands.Add(data);

        int n = 1;
        float x = 0;
        float widthColumns = (float)gridContent.Columns.Where(x => x.Visible).Sum(x => x.ActualWidth);
        foreach (var column in gridContent.Columns.Where(x => x.Visible))
        {
            if (column is GridUnboundColumn)
            {
                continue;
            }

            float w = Units.Millimeters * (float)column.ActualWidth * width / widthColumns;

            TextObject textHeader = new()
            {
                Bounds = new RectangleF(x, 0, w, Units.Centimeters * 0.5f),
                Font = settings.Page.Fonts.Header.CreateFont(),
                Border = new()
                {
                    Lines = BorderLines.All
                },
                FillColor = Color.WhiteSmoke,
                Text = column.HeaderText,
                Name = $"Text{n++}",
                VertAlign = VertAlign.Center
            };

            data.Header.Objects.Add(textHeader);

            TextObject textData = new()
            {
                Bounds = new RectangleF(x, 0, w, Units.Centimeters * 0.5f),
                Font = settings.Page.Fonts.Base.CreateFont(),
                Border = new()
                {
                    Lines = BorderLines.All
                },
                Text = $"[{dataName}.{column.MappingName}]",
                Name = $"Text{n++}",
                VertAlign = VertAlign.Center
            };

            data.Objects.Add(textData);

            x += w;
        }

        TextObject textTime = new()
        {
            Bounds = new RectangleF(0, 0, Units.Centimeters * 3.0f, Units.Centimeters * 0.5f),
            Font = settings.Page.Fonts.Footer.CreateFont(),
            Text = DateTime.Now.ToString("dd.MM.yyyy, HH:mm"),
            Name = "TextTime",
            VertAlign = VertAlign.Bottom
        };

        page1.PageFooter.Objects.Add(textTime);

        TextObject textPage = new()
        {
            Bounds = new RectangleF(Units.Millimeters * (width - 20), 0, Units.Centimeters * 2.0f, Units.Centimeters * 0.5f),
            Font = settings.Page.Fonts.Footer.CreateFont(),
            Text = "[Page#] / [TotalPages#]",
            Name = "TextPage",
            VertAlign = VertAlign.Bottom,
            HorzAlign = HorzAlign.Right
        };

        page1.PageFooter.Objects.Add(textPage);

        report.Pages.Add(page1);
        report.Prepare();

        var ls = services.GetRequiredService<IOptions<LocalSettings>>().Value;

        string file = BaseReport.CreatePdfDocument(report.Report, ls.Report.Resolution);
        services
            .GetRequiredService<IPreviewReportForm>()
            .ShowReport(file, Text);
    }

    private void GridContentKeyUp(object? sender, KeyEventArgs e)
    {
        switch (e.Modifiers)
        {
            case Keys.Alt:
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        SwapMarkedSelectedRow();
                        e.Handled = true;
                        break;
                    case Keys.Enter:
                        EditSelectedRow();
                        e.Handled = true;
                        break;
                }

                break;
            case Keys.Control | Keys.Alt:
                switch (e.KeyCode)
                {
                    case Keys.Insert:
                        CopySelectedRow();
                        e.Handled = true;
                        break;
                }

                break;
        }
    }

    private void MenuId_Click(object sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem menu && menu.Tag is T rec)
        {
            Clipboard.SetText(rec.Id.ToString());
            MessageBox.Show($"Идентификатор записи {{{rec.Id}}} скопирован в буфер обмена.", "Идентификатор скопирован", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void MenuGroupsSettings_Click(object sender, EventArgs e)
    {
        if (groupColumnCollection != null)
        {
            var dialog = new GroupColumnsDialog(groupColumnCollection);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                groupColumnCollection.SetSelectedGroups(dialog.Selected);
            }
        }
    }
}
