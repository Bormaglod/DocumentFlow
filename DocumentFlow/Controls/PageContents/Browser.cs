﻿//-----------------------------------------------------------------------
//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.Renderers;
using DocumentFlow.Controls.Settings;
using DocumentFlow.Core;
using DocumentFlow.Core.Exceptions;
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
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocumentFlow.Controls.PageContents;

public abstract partial class Browser<T> : UserControl, IBrowserPage
    where T : class, IIdentifier<Guid>
{
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
                else if (x is Group grp_x && y is Group grp_y)
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
    private ShowToolTipMethod? showToolTip;
    private Guid? root_id = null;
    private Guid? parent_id = null;
    private Guid? owner_id = null;
    private bool readOnly = false;
    private readonly Type? browserType;

    private GridColumn? column = null;
    private SfDataGrid? grid = null;
    private Group? group = null;
    private T? record = default;

    private readonly List<GridColumn> hiddens = new();
    private readonly List<GridColumn> visible = new();
    private readonly List<GridColumn> visibility = new();

    private BrowserSettings? settings;

    protected Browser(
        IRepository<Guid, T> repository,
        IPageManager pageManager,
        IRowHeaderImage? rowHeaderImage = null,
        IBreadcrumb? navigator = null,
        IFilter? filter = null)
    {
        InitializeComponent();

        settings = CreateBrowserSettings();

        this.repository = repository;
        this.pageManager = pageManager;
        this.rowHeaderImage = rowHeaderImage;
        this.navigator = navigator;
        this.filter = filter;

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

        ConfigureBrowser();
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

    protected BrowserSettings Settings => settings ?? CreateBrowserSettings();

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
        LoadBrowserSettings();
        RefreshView();
        ConfigureCommands();
    }

    public void Refresh(Guid? owner)
    {
        owner_id = owner;

        CurrentApplicationContext.Context.App.OnAppNotify += App_OnAppNotify;

        RefreshPage();
    }

    public void OnPageClosing()
    {

        if (browserType != null)
        {
            var db = Services.Provider.GetService<IDatabase>();
            if (db == null)
            {
                return;
            }

            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Автоком",
                "settings",
                db.CurrentUser
            );

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            string file = Path.Combine(path, $"{browserType.Name}.json");
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            if (AllowColumnsConfigure())
            {
                UpdateSettingsColumn();
            }

            SaveSettings();

            string json = JsonSerializer.Serialize(Settings, Settings.GetType(), options);
            File.WriteAllText(file, json);
        }
    }

    private void LoadBrowserSettings()
    {
        if (browserType != null)
        {
            var db = Services.Provider.GetService<IDatabase>();
            if (db == null)
            {
                return;
            }

            var file = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Автоком",
                "settings",
                db.CurrentUser,
                $"{browserType.Name}.json"
            );

            if (File.Exists(file))
            {
                string json = File.ReadAllText(file);
                var options = new JsonSerializerOptions();
                options.Converters.Add(new JsonStringEnumConverter());

                settings = LoadSettings(json, options);
            }
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (AllowColumnsConfigure())
        {
            RefreshSettingsHiddenColumns();
            CustomizeColumns();
        }
    }

    protected virtual void SaveSettings() { }

    protected virtual BrowserSettings CreateBrowserSettings() => new();

    protected virtual BrowserSettings? LoadSettings(string json, JsonSerializerOptions options) => JsonSerializer.Deserialize<BrowserSettings>(json, options);

    protected virtual bool AllowColumnsConfigure() => true;

    protected virtual void DoBeforeRefreshPage() { }

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

        foreach (var column in visible)
        {
            bool? columnVisible = IsColumnVisible(column);
            if (columnVisible == null)
            {
                continue;
            }

            column.Visible = columnVisible.Value;
            var item = menuVisibleColumns.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(x => x.Tag == column);
            if (item != null)
            {
                item.Checked = columnVisible.Value;
            }
        }

        foreach (var column in visibility)
        {
            var item = menuVisibleColumns.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(x => x.Tag == column);
            if (item != null)
            {
                bool? columnVisible = IsAllowVisibilityColumn(column);
                if (columnVisible == null)
                {
                    continue;
                }

                item.Visible = columnVisible.Value;
            }
        }
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
            var gridColumn = new GridCheckBoxColumn()
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

    protected virtual (string MappingName, GridColumn[] Columns) GetColumnsAfter() => (string.Empty, Array.Empty<GridColumn>());

    protected void AddColumns(GridColumn[] columns)
    {
        var inserting = GetColumnsAfter();
        for (int i = 0; i < columns.Length; i++)
        {
            AddColumn(columns[i], hiddens.Contains(columns[i]));
            if (inserting.MappingName == columns[i].MappingName)
            {
                for (int j = 0; j < inserting.Columns.Length; j++)
                {
                    AddColumn(inserting.Columns[j], hiddens.Contains(inserting.Columns[j]));
                }
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

    protected void ChangeColumnsVisible(IEnumerable<GridColumn> columns) => visible.AddRange(columns);

    protected void ChangeColumnsVisible(params GridColumn[] columns) => visible.AddRange(columns);

    protected void AllowColumnVisibility(IEnumerable<GridColumn> columns) => visibility.AddRange(columns);

    protected void AllowColumnVisibility(params GridColumn[] columns) => visibility.AddRange(columns);

    protected virtual bool? IsColumnVisible(GridColumn column) => null;

    protected virtual bool? IsAllowVisibilityColumn(GridColumn column) => null;

    protected virtual void OnChangeParent() { }

    protected virtual void BrowserCellStyle(T document, string column, CellStyleInfo style) { }

    protected virtual void BrowserImageStyle(T document, string column, ImageCellStyle style) { }

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

    protected void ClearStackedRows()
    {
        gridContent.StackedHeaderRows.Clear();
    }

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

    protected virtual bool OnDrawPreviewRow(Graphics graphics, T row, Rectangle bounds, PreviewRowStyleInfo style) => false;

    protected virtual void OnActionPreviewDoubleClick(int x, int y) { }

    protected virtual bool CanExpandPreview(T row) => false;

    private void RefreshSettingsHiddenColumns()
    {
        if (Settings.Columns != null)
        {
            foreach (var item in Settings.Columns)
            {
                item.Hidden = hiddens.FirstOrDefault(x => x.MappingName == item.Name) != null;
            }
        }
    }

    private void UpdateSettingsColumn()
    {
        var list = new List<BrowserColumn>();
        foreach (var item in gridContent.Columns)
        {
            BrowserColumn column = new(item)
            {
                Hidden = hiddens.Contains(item)
            };

            list.Add(column);
        }

        Settings.Columns = list;
    }

    private void CustomizeColumns()
    {
        if (Settings.Columns == null)
        {
            return;
        }

        foreach (var item in Settings.Columns)
        {
            var column = gridContent.Columns.FirstOrDefault(x => x.MappingName == item.Name);
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
            column.Visible = item.Visible;

            bool? b = null;
            var c = visible.FirstOrDefault(x => x.MappingName == item.Name);
            if (c != null)
            {
                b = IsColumnVisible(c);
            }

            if (b != null)
            {
                column.Visible = column.Visible && b.Value;
            }

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

    private void ConfigureBrowser()
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

        Text = HeaderText;

        buttonCreateGroup.Visible = Navigator != null;
        menuCreateGroup.Visible = Navigator != null;
        menuCreateGroup2.Visible = Navigator != null;

        gridContent.TableControl.KeyUp += GridContentKeyUp;
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
            pageManager.ShowEditor(browserType, editableRow?.id, owner_id, parent_id, readOnly || (document?.deleted ?? false));
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
                Clipboard.SetText(value.ToString());
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
            group = (e.ContextMenuInfo as RowContextMenuInfo)?.Row as Group;
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

    private void ButtonRefresh_Click(object sender, EventArgs e) => RefreshView();

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
        var form = new BrowserCustomizationForm(Settings);
        if (form.ShowDialog() == DialogResult.OK && form.Columns != null)
        {
            CustomizeColumns();
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
        if (gridContent.DataSource is IList<T> list)
        {
            string dataName = typeof(T).Name.Underscore();

            FastReport.Report report = new();
            report.RegisterData(list, dataName);
            report.GetDataSource(dataName).Enabled = true;

            FastReport.ReportPage page1 = new()
            {
                Name = "Page1",
                LeftMargin = Settings.Page.Settings.LeftMargin,
                RightMargin = Settings.Page.Settings.RightMargin,
                TopMargin = Settings.Page.Settings.TopMargin,
                BottomMargin = Settings.Page.Settings.BottomMargin,
                PaperWidth = Settings.Page.Settings.PaperWidth,
                PaperHeight = Settings.Page.Settings.PaperHeight,
                MirrorMargins = Settings.Page.Settings.MirrorMargins,
                ReportTitle = new()
                {
                    Name = "ReportTitle1",
                    Height = Units.Centimeters * 1.0f
                }
            };

            int width = Settings.Page.Settings.PrintableWidth;

            TextObject textTitle = new()
            {
                Bounds = new RectangleF(0, 0, Units.Millimeters * width, Units.Centimeters * 1.0f),
                Font = Settings.Page.Fonts.Title.CreateFont(),
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
                    Font = Settings.Page.Fonts.Header.CreateFont(),
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
                    Font = Settings.Page.Fonts.Base.CreateFont(),
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
