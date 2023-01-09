//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//
// Версия 2022.8.21
//  - для решения проблемы невостановления сохраненных настроек таблицы:
//      - удален метод OnLoad
//      - добавлен метод Browser_Load 
//      - вызов LoadBrowserSettings перенесен из RefreshPage в Browser_Load
// Версия 2022.8.28
//  - добавлен метод RefreshGrid
// Версия 2022.9.2
//  - метод AllowColumnsConfigure заменен на AllowColumnsCustomize
//  - добавлен метод ConfigureColumns
//  - добавлен метод ConfigureVisibleStatusColumns
//  - все операции с видимостью колонок (методы ConfigureColumns и
//    ConfigureVisibleStatusColumns) перенесены в Browser_Load и
//    RegreshPage (этот метод вызывается только при ручном обновлении)
// Версия 2022.9.3
//  - удалена возможность настройки каждой папки, теперь они все
//    настраиваются единообразно:
//      - удалены процедуры ChangeColumnsVisible, ChangeColumnsVisible,
//        AllowColumnVisibility, AllowColumnVisibility, IsColumnVisible и
//        IsAllowVisibilityColumn
//      - удалены поля visible и visibility
// Версия 2022.11.16
//  - мелкие исправления
// Версия 2022.11.26
//  - изменен метод GetColumnsAfter и AddColumns - ранее можно было
//    вставить столбцы только после одного столбца, теперь после любого
// Версия 2022.12.2
//  - для столбца отображающего checkbox добавлена возможность использования
//    трёхвариантного выбора - при условии, что отображаемое поле имеет
//    тип bool?
// Версия 2022.12.6
//  - интерфейс IFilter обзавелся новым свойством IOwnerIdentifier, поэтому
//    в методе Refresh(Guid?) добавлена инициализация этого свойства
// Версия 2022.12.11
//  - добавлен метод DocumentIsReadOnly
// Версия 2022.12.21
//  - добавлен пункт меню "Создать на основании"
//  - в конструктор добавлен параметр и поле IEnumerable<ICreationBased>? creations
//  - добавлен метод MenuCreateBasedOn_Click
// Версия 2023.1.5
//  - добавлено поле moveToEnd и метод MoveToEnd
//  - в методе Refresh(Guid?) реализовано перемещение в конец таблицы,
//    если moveToEnd равно true
// Версия 2023.1.8
//  - отключено выделение последней строки при вызове и установке флага
//    moveToEnd из-за некорректного поведения скроллинга в конец таблицы
//  - добавлен класс SettingsKeyRegex
//  - добавлен параметр в конструктор IStandaloneSettings
//  - метод Browser_Load изменен с учётом использования поля settings
//  - удалены поле BrowserSettings settings и свойство Settings - значения
//    берутся из параметра конструктора settings
//  - удалён метод AllowColumnsCustomize
//  - удалён метод Browser_Load - содержимое перенесено в Refresh(Guid?)
//  - добавлен метод ApplySettings
//  - множественные изменения, связанные с изменением механизма
//    чтения/записи настроек
// Версия 2023.1.9
//  - если класс-потомок реализует свой IAppSettings и с помощью метода
//    ApplySettings запрещает дальнейшую инициализацию настроек, то
//    не вызывается метод ConfigureColumns, который в свою очередь
//    вызывается из RefreshColumns - исправлено
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.Renderers;
using DocumentFlow.Controls.Settings;
using DocumentFlow.Core;
using DocumentFlow.Core.Reflection;
using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Core.Repository;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Dialogs;
using DocumentFlow.Infrastructure;
using DocumentFlow.Properties;
using DocumentFlow.ReportEngine;
using DocumentFlow.ReportEngine.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

using FastReport;
using FastReport.Utils;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
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

namespace DocumentFlow.Controls.PageContents;

public abstract partial class Browser<T> : UserControl, IBrowserPage
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
            if (x is IDirectory parent_x && y is IDirectory parent_y && (parent_x.is_folder != parent_y.is_folder))
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
                else if (x is Syncfusion.Data.Group grp_x && y is Syncfusion.Data.Group grp_y)
                {
                    res = CompareString(grp_x.Key, grp_y.Key);
                }
                else
                {
                    res = CompareString(x, y);
                }
            }

            return SortDirection == ListSortDirection.Ascending ? res : -res;
        }

        public ListSortDirection SortDirection { get; set; }

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

    private readonly PageToolBar toolbar;
    private readonly BrowserContextMenu contextMenu;
    private readonly IRepository<Guid, T> repository;
    private readonly IPageManager pageManager;
    private readonly IRowHeaderImage? rowHeaderImage;
    private readonly IBreadcrumb? navigator;
    private readonly IFilter? filter;
    private readonly IEnumerable<ICreationBased>? creations;
    private readonly IStandaloneSettings? settings;
    private ShowToolTipMethod? showToolTip;
    private Guid? root_id = null;
    private Guid? parent_id = null;
    private Guid? owner_id = null;
    private bool readOnly = false;
    private readonly Type? browserType;
    private bool moveToEnd = false;

    private GridColumn? column = null;
    private SfDataGrid? grid = null;
    private Syncfusion.Data.Group? group = null;
    private T? record = default;

    private readonly List<GridColumn> hiddens = new();

    protected Browser(
        IRepository<Guid, T> repository,
        IPageManager pageManager,
        IRowHeaderImage? rowHeaderImage = null,
        IBreadcrumb? navigator = null,
        IFilter? filter = null,
        IEnumerable<ICreationBased>? creations = null,
        IStandaloneSettings? settings = null)
    {
        InitializeComponent();

        this.repository = repository;
        this.pageManager = pageManager;
        this.rowHeaderImage = rowHeaderImage;
        this.navigator = navigator;
        this.filter = filter;
        this.creations = creations;
        this.settings = settings;

        toolbar = new PageToolBar(toolStrip1, new Dictionary<ToolStripItem, (Image, Image)>
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
        });

        contextMenu = new BrowserContextMenu(contextRecordMenu);

        browserType = null;
        foreach (var item in GetType().GetInterfaces())
        {
            var type = item.GetInterfaces().FirstOrDefault(i => i.IsGenericType && typeof(IBrowser<>) == i.GetGenericTypeDefinition());
            if (type != null)
            {
                browserType = item;
                break;
            }
        }

        ConfigureBrowser(settings);
    }

    public IRowHeaderImage? RowHeaderImage => rowHeaderImage;

    public IBreadcrumb? Navigator => navigator;

    public IToolBar Toolbar => toolbar;

    public IContextMenu ContextMenu => contextMenu;

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

    public Guid? RootId => root_id;

    public Guid? ParentId
    {
        get => parent_id;
        set
        {
            parent_id = value;

            if (parent_id.HasValue)
            {
                if (!root_id.HasValue)
                {
                    root_id = parent_id;
                }
            }
            else
            {
                root_id = null;
            }

            OnChangeParent();
        }
    }

    public Guid? OwnerDocument => owner_id;

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

    protected abstract string HeaderText { get; }

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

    public void RefreshPage()
    {
        RefreshColumns();
        RefreshView();
    }

    public void Refresh(Guid? owner)
    {
        owner_id = owner;
        if (filter != null)
        {
            filter.OwnerIdentifier = owner;
        }

        ApplySettings(out bool canceledSettings);
        if (!canceledSettings && settings != null)
        {
            var b_settings = settings.Get<BrowserSettings>();
            filter?.Configure(settings);

            if (b_settings.Columns != null)
            {
                foreach (var item in b_settings.Columns)
                {
                    item.Hidden = hiddens.FirstOrDefault(x => x.MappingName == item.Name) != null;
                }
            }

            CustomizeColumns(b_settings);
        }

        RefreshColumns();

        CurrentApplicationContext.Context.App.OnAppNotify += App_OnAppNotify;

        RefreshView();

        if (moveToEnd && gridContent.RowCount > 0)
        {
            int rows = 0;
            if (gridContent.ShowRowHeader)
            {
                rows += gridContent.StackedHeaderRows.Count + 1;
            }

            if (gridContent.View is CollectionViewAdv collection)
            {
                rows += collection.Count;
            }

            // FIX: Неправильное поведение при скроллинге окна при установленнном выделении
            // Если количество записей больше, чем помещается в окно, то при установке выделения не
            // происходит перемещение окна в конец таблицы
            // gridContent.SelectedIndex = rows - 2;
            gridContent.TableControl.ScrollRows.ScrollInView(rows - 2);
            gridContent.TableControl.ScrollRows.UpdateScrollBar();
        }
    }

    public void OnPageClosing()
    {
        WriteSettings(out bool canceledSettings);
        if (!canceledSettings && settings != null)
        {
            UpdateSettingsColumn();
            settings.Write<BrowserSettings>();
            filter?.WriteConfigure(settings);
        }
    }

    protected virtual void DoBeforeRefreshPage() { }

    protected virtual bool DocumentIsReadOnly(T document) => false;

    protected virtual void ApplySettings(out bool canceledSettings)
    {
        canceledSettings = false;
    }

    protected virtual void WriteSettings(out bool canceledSettings)
    {
        canceledSettings = false;
    }

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
            IOwnedRepository<Guid, T> ownedRepository => ownedRepository.GetByOwner(OwnerDocument, filter),
            _ => repository.GetAllDefault(filter),
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
        var member = ReflectionHelper.GetMember(memberExpression);
        if (member != null)
        {
            var gridColumn = new GridTextColumn()
            {
                MappingName = member.Name,
                HeaderText = headerText,
                Visible = visible
            };

            SetDefaultData(gridColumn, width, hidden);

            return gridColumn;
        }

        throw new NullReferenceException("Параметр memberExpression должен содержать имя поля, но оно не найдено в классе.");
    }

    protected GridNumericColumn CreateNumeric(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, bool visible = true, bool hidden = true, FormatMode mode = FormatMode.Numeric, int decimalDigits = 0, string? format = null)
    {
        var member = ReflectionHelper.GetMember(memberExpression);
        if (member != null)
        {
            NumberFormatInfo numberFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
            numberFormat.NumberDecimalDigits = decimalDigits;
            numberFormat.PercentDecimalDigits = decimalDigits;

            var gridColumn = new GridNumericColumn()
            {
                MappingName = member.Name,
                HeaderText = headerText,
                FormatMode = mode,
                NumberFormatInfo = numberFormat,
                Visible = visible
            };

            if (!string.IsNullOrEmpty(format))
            {
                gridColumn.Format = format;
            }

            SetDefaultData(gridColumn, width, hidden);

            return gridColumn;
        }

        throw new NullReferenceException("Параметр memberExpression должен содержать имя поля, но оно не найдено в классе.");
    }

    protected GridNumericColumn CreateCurrency(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, bool visible = true, bool hidden = true, string? format = null)
    {
        var column = CreateNumeric(memberExpression, headerText, width, visible, hidden, FormatMode.Currency, 2, format);
        column.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        return column;
    }

    protected GridCheckBoxColumn CreateBoolean(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, bool visible = true, bool hidden = true)
    {
        var member = ReflectionHelper.GetMember(memberExpression);
        if (member != null)
        {
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

            SetDefaultData(gridColumn, width, hidden);

            return gridColumn;
        }

        throw new NullReferenceException("Параметр memberExpression должен содержать имя поля, но оно не найдено в классе.");
    }

    protected GridProgressBarColumn CreateProgress(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, bool visible = true, bool hidden = true)
    {
        var member = ReflectionHelper.GetMember(memberExpression);
        if (member != null)
        {
            var gridColumn = new GridProgressBarColumn()
            {
                MappingName = member.Name,
                HeaderText = headerText,
                Visible = visible,
                Maximum = 100,
                Minimum = 0,
                ValueMode = ProgressBarValueMode.Percentage
            };

            SetDefaultData(gridColumn, width, hidden);

            return gridColumn;
        }

        throw new NullReferenceException("Параметр memberExpression должен содержать имя поля, но оно не найдено в классе.");
    }

    protected GridDateTimeColumn CreateDateTime(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, string format = "dd.MM.yyyy HH:mm:ss", bool visible = true, bool hidden = true)
    {
        var member = ReflectionHelper.GetMember(memberExpression);
        if (member != null)
        {
            var gridColumn = new GridDateTimeColumn()
            {
                MappingName = member.Name,
                HeaderText = headerText,
                Visible = visible,
                Pattern = DateTimePattern.Custom,
                Format = format
            };

            SetDefaultData(gridColumn, width, hidden);

            return gridColumn;
        }

        throw new NullReferenceException("Параметр memberExpression должен содержать имя поля, но оно не найдено в классе.");
    }

    protected GridImageColumn CreateImage(Expression<Func<T, object?>> memberExpression, string headerText, int width = 0, bool visible = true, bool hidden = true)
    {
        var member = ReflectionHelper.GetMember(memberExpression);
        if (member != null)
        {
            var gridColumn = new GridImageColumn()
            {
                MappingName = member.Name,
                HeaderText = headerText,
                Visible = visible
            };

            SetDefaultData(gridColumn, width, hidden);

            return gridColumn;
        }

        throw new NullReferenceException("Параметр memberExpression должен содержать имя поля, но оно не найдено в классе.");
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

    protected void AllowGrouping()
    {
        gridContent.AllowGrouping = true;
        gridContent.ShowGroupDropArea = true;
    }

    protected ISummary CreateSummaryRow(VerticalPosition position, bool withGroups = false)
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

    protected IGroupColumnCollection CreateGroups()
    {
        gridContent.GroupColumnDescriptions.Clear();
        return new GroupColumnCollection(gridContent);
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
            T loadedRow = repository.GetById(row.id);
            list[list.IndexOf(row)] = loadedRow;
        }
    }

    protected void AllowDrawPreviewRow(PreviewRowHeightMode mode = PreviewRowHeightMode.Fixed, int height = 28)
    {
        gridContent.DrawPreviewRow += GridContentDrawPreviewRow;
        gridContent.PreviewRowExpanding += GridContent_PreviewRowExpanding;
        gridContent.ShowPreviewRow = true;
        gridContent.PreviewRowHeightMode = mode;
        gridContent.PreviewRowHeight = height;
    }

    protected void RefreshGrid() => gridContent.Refresh();

    protected virtual bool OnDrawPreviewRow(Graphics graphics, T row, Rectangle bounds, PreviewRowStyleInfo style) => false;

    protected virtual void OnActionPreviewDoubleClick(int x, int y) { }

    protected virtual bool CanExpandPreview(T row) => false;

    private void UpdateSettingsColumn(BrowserSettings? b_settings = null)
    {
        if (settings != null)
        {
            b_settings ??= settings.Get<BrowserSettings>();

            var list = new List<BrowserColumn>();
            foreach (var item in gridContent.Columns)
            {
                BrowserColumn column = new(item)
                {
                    Hidden = hiddens.Contains(item)
                };

                list.Add(column);
            }

            b_settings.Columns = list;
        }
    }

    private void CustomizeColumns(BrowserSettings b_settings)
    {
        if (b_settings.Columns == null)
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

            if (item.AutoSizeMode == AutoSizeColumnsMode.None && item.Width != null)
            {
                column.Width = item.Width.Value;
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

    private void SetDefaultData(GridColumn column, int width, bool hidden)
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

    private void ConfigureBrowser(IStandaloneSettings? settings = null)
    {
        gridContent.ColumnHeaderContextMenu = contextHeaderMenu;
        gridContent.RowHeaderContextMenu = contextRowMenu;
        gridContent.RecordContextMenu = contextRecordMenu;
        gridContent.GroupDropAreaContextMenu = contextGroupMenu;
        gridContent.GroupDropAreaItemContextMenu = contextGroupItemMenu;
        gridContent.GroupCaptionContextMenu = contextGroupCaptionMenu;

        gridContent.Style.ProgressBarStyle.AllowForegroundSegments = true;

        gridContent.CellRenderers["RowHeader"] = new CustomRowHeaderCellRenderer(gridContent, RowHeaderImage);

        gridContent.CellRenderers.Remove("TableSummary");
        gridContent.CellRenderers.Add("TableSummary", new CustomGridTableSummaryRenderer());

        gridContent.CellRenderers.Remove("GroupSummary");
        gridContent.CellRenderers.Add("GroupSummary", new CustomGridTableSummaryRenderer());

        panelTools.AutoSize = true;
        if (Navigator is Control controlNavigator)
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

            Navigator.CrumbClick += DataNavigator_CrumbClick;
            panelTools.Controls.Add(panelNavigator);
        }

        if (navigator != null)
        {
            navigator.ShowButtonRefresh = false;
        }

        if (filter != null)
        {
            filter.Control.Dock = DockStyle.Top;
            panelTools.Controls.Add(filter.Control);
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

        Text = HeaderText;

        buttonCreateGroup.Visible = Navigator != null;
        menuCreateGroup.Visible = Navigator != null;
        menuCreateGroup2.Visible = Navigator != null;

        gridContent.TableControl.KeyUp += GridContentKeyUp;

        if (settings != null)
        {
            string settingsKey;
            if (browserType != null)
            {
                Match m = SettingsKeyRegex().Match(browserType.Name);
                if (m.Success)
                {
                    settingsKey = m.Groups[1].Value.Underscore();
                }
                else
                {
                    settingsKey = browserType.Name;
                }
            }
            else
            {
                settingsKey = typeof(T).Name;
            }

            settings.Configure(settingsKey);
        }
    }

    private void MenuCreateBasedOn_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem menu && menu.Tag is ICreationBased creation && gridContent.SelectedItem is T row)
        {
            try
            {
                Guid id = creation.Create(row);
                pageManager.ShowEditor(creation.DocumentEditorType, id);
            }
            catch (Exception ex)
            {
                ExceptionHelper.MesssageBox(ex);
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
            pageManager.ShowEditor(browserType, editableRow?.id, owner_id, parent_id, readOnly || (document?.deleted ?? false) || dro);
        }
    }

    private void Edit(T editableRow)
    {
        if (
            gridContent.DataSource is IList<T> list &&
            editableRow is IDirectory dir && dir.is_folder &&
            repository is IDirectoryRepository<T> repo)
        {

            var folderEditor = new GroupEditorForm<T>(repo, editableRow);
            T? res = folderEditor.ShowFolderDialog();
            if (res != default)
            {
                list[list.IndexOf(editableRow)] = res;
            }
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
            if (row is IDirectory dir && dir.is_folder)
            {
                ParentId = row.id;
                if (navigator != null)
                {
                    navigator.Push(dir);
                }

                RefreshView();
            }
            else
            {
                Edit(row);
            }
        }
    }

    private void ButtonDelete_Click(object sender, EventArgs e) => SwapMarkedSelectedRow();

    private void CopySelectedRow()
    {
        if (gridContent.DataSource is IList<T> list && gridContent.SelectedItem is T row)
        {
            if (row is IDirectory dir && dir.is_folder)
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
                        pageManager.ShowEditor(browserType, copy.id, owner_id, parent_id, false);
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
                list[list.IndexOf(row)] = repository.GetById(row.id);
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
        if (row is IDirectory dir && dir.is_folder)
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
            if (row.deleted)
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
                add = parent_id == aDir.parent_id && (owner_id == null || owner_id == aDir.owner_id);
                break;
            case IDocumentInfo aDoc:
                add = owner_id == null || owner_id == aDoc.owner_id;
                break;
        }

        if (add)
        {
            list.Add(document);
        }
    }

    private void DataNavigator_CrumbClick(object? sender, CrumbClickEventArgs e)
    {
        if (Navigator == null)
        {
            return;
        }

        switch (e.Kind)
        {
            case ToolButtonKind.Up:
                Navigator.Pop();
                ParentId = Navigator.Peek();
                break;
            case ToolButtonKind.Refresh:
                ParentId = Navigator.Peek();
                break;
            case ToolButtonKind.Home:
                Navigator.Clear();
                ParentId = null;
                break;
        }

        RefreshView();
    }

    private void RefreshBrowserList(NotifyEventArgs e)
    {
        Guid idObj = e.Document?.id ?? e.ObjectId;
        if (idObj == Guid.Empty || idObj == OwnerDocument)
        {
            RefreshView();
        }
    }

    private void ConfigureVisibleStatusColumns()
    {
        if (settings == null)
        {
            return;
        }

        var b_settings = settings.Get<BrowserSettings>();
        foreach (var column in gridContent.Columns)
        {
            if (b_settings.Columns != null)
            {
                var item = b_settings.Columns.FirstOrDefault(x => x.Name == column.MappingName);
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
                    T addingDoc = (T?)e.Document ?? repository.GetById(e.ObjectId);
                    AddRow(list, addingDoc);

                    break;
                case MessageAction.Delete:
                    switch (e.Destination)
                    {
                        case MessageDestination.Object:
                            var row = list.Cast<T>().FirstOrDefault(x => x.id == e.ObjectId);
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
                            T refreshDoc = (T?)e.Document ?? repository.GetById(e.ObjectId);
                            var row = list.Cast<T>().FirstOrDefault(x => x.id == refreshDoc.id);
                            if (row != null)
                            {
                                bool change = false;
                                switch (refreshDoc)
                                {
                                    case IDirectory rDir:
                                        change = parent_id == rDir.parent_id && (owner_id == null || owner_id == rDir.owner_id);
                                        break;
                                    case IDocumentInfo rDoc:
                                        change = owner_id == null || owner_id == rDoc.owner_id;
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
        if (grid != null)
        {
            grid.ClearSorting();
        }
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
        if (e.ContextMenutype == ContextMenuType.GroupCaption)
        {
            grid = (e.ContextMenuInfo as RowContextMenuInfo)?.DataGrid;
            group = (e.ContextMenuInfo as RowContextMenuInfo)?.Row as Syncfusion.Data.Group;
        }

        if (e.ContextMenutype == ContextMenuType.GroupDropAreaItem || e.ContextMenutype == ContextMenuType.ColumnHeader)
        {
            grid = (e.ContextMenuInfo as ColumnContextMenuInfo)?.DataGrid;
            column = (e.ContextMenuInfo as ColumnContextMenuInfo)?.Column;
        }

        if (e.ContextMenutype == ContextMenuType.GroupDropArea)
        {
            grid = (e.ContextMenuInfo as GroupDropAreaContextMenuInfo)?.DataGrid;
        }

        if (e.ContextMenutype == ContextMenuType.Record || e.ContextMenutype == ContextMenuType.RowHeader)
        {
            grid = (e.ContextMenuInfo as RowContextMenuInfo)?.DataGrid;
            record = (e.ContextMenuInfo as RowContextMenuInfo)?.Row as T;
        }
    }

    private void DocumenuMenuItem_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem menuItem && menuItem.Tag is DocumentRefs doc)
        {
            try
            {
                FileHelper.OpenFile(doc);
            }
            catch (Exception ex)
            {
                ExceptionHelper.MesssageBox(ex);
            }
        }
    }

    private void ContextRecordMenu_Opening(object sender, CancelEventArgs e)
    {
        menuDocuments.DropDownItems.Clear();
        if (record != null)
        {
            menuId.Text = $"Идентификатор: {{{record.id}}}";
            menuId.Tag = record;

            var service = Services.Provider.GetService<IDocumentRefsRepository>();
            var docs = service!.GetByOwner(record.id);

            foreach (var doc in docs)
            {
                var menuItem = new ToolStripMenuItem
                {
                    Text = string.IsNullOrEmpty(doc.note) ? doc.file_name : doc.note,
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
    }

    private void GridContent_CellDoubleClick(object sender, CellClickEventArgs e)
    {
        if (e.DataRow is PreviewDataRow)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                OnActionPreviewDoubleClick(e.MouseEventArgs.X, e.MouseEventArgs.Y);
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
            var editor = new GroupEditorForm<T>(repo, parent_id);
            var folder = editor.ShowFolderDialog();
            if (folder != null)
            {
                list.Add(folder);
            }
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
        if (settings != null)
        {
            var b_settings = settings.Get<BrowserSettings>();
            UpdateSettingsColumn(b_settings);

            var form = new BrowserCustomizationForm(b_settings);
            if (form.ShowDialog() == DialogResult.OK && form.Columns != null)
            {
                CustomizeColumns(b_settings);
            }
        }
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
            int res = MessageChoiceForm<int>.ShowDialog(
                "Удаление",
                "Безвозвратное удаление записи (записей).",
                new Dictionary<int, string>()
                {
                    [1] = "Удалить только текущую запись",
                    [2] = "Удалить все помеченные на удаление записи",
                    [0] = "Отменить всё. Я передумал(а)."
                },
                0);

            if (res == 1)
            {
                if (gridContent.SelectedItem is T row)
                {
                    bool delete = true;
                    if (row is IDirectory dir && dir.is_folder)
                    {
                        delete = MessageBox.Show("Удаление группы приведет к аналогичному действию для всего содержимого группы. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
                    }

                    if (delete)
                    {
                        if (row is IDocumentInfo doc && !doc.deleted)
                        {
                            MessageBox.Show("Запись не помечена на удаление. Операция прервана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        repository.Wipe(row.id);
                        list.Remove(row);
                    }
                }
            }
            else if (res == 2)
            {
                if (repository is IDirectoryRepository<T> repoDir)
                {
                    repoDir.WipeParented(OwnerDocument, ParentId);
                }
                else
                {
                    repository.WipeAll(OwnerDocument);
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
        if (gridContent.DataSource is IList<T> list && settings != null)
        {
            var b_settings = settings.Get<BrowserSettings>();

            string dataName = typeof(T).Name.Underscore();

            FastReport.Report report = new();
            report.RegisterData(list, dataName);
            report.GetDataSource(dataName).Enabled = true;



            FastReport.ReportPage page1 = new()
            {
                Name = "Page1",
                LeftMargin = b_settings.Page.Settings.LeftMargin,
                RightMargin = b_settings.Page.Settings.RightMargin,
                TopMargin = b_settings.Page.Settings.TopMargin,
                BottomMargin = b_settings.Page.Settings.BottomMargin,
                PaperWidth = b_settings.Page.Settings.PaperWidth,
                PaperHeight = b_settings.Page.Settings.PaperHeight,
                MirrorMargins = b_settings.Page.Settings.MirrorMargins,
                ReportTitle = new()
                {
                    Name = "ReportTitle1",
                    Height = Units.Centimeters * 1.0f
                }
            };

            int width = b_settings.Page.Settings.PrintableWidth;

            TextObject textTitle = new()
            {
                Bounds = new RectangleF(0, 0, Units.Millimeters * width, Units.Centimeters * 1.0f),
                Font = b_settings.Page.Fonts.Title.CreateFont(),
                Text = HeaderText,
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
                float w = Units.Millimeters * (float)column.ActualWidth * width / widthColumns;

                TextObject textHeader = new()
                {
                    Bounds = new RectangleF(x, 0, w, Units.Centimeters * 0.5f),
                    Font = b_settings.Page.Fonts.Header.CreateFont(),
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
                    Font = b_settings.Page.Fonts.Base.CreateFont(),
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

            report.Pages.Add(page1);
            report.Prepare();

            string file = BaseReport.CreatePdfDocument(report);
            PreviewReportForm.ShowReport(file, HeaderText);
        }
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
            Clipboard.SetText(rec.id.ToString());
            MessageBox.Show($"Идентификатор записи {{{rec.id}}} скопирован в буфер обмена.", "Идентификатор скопирован", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
