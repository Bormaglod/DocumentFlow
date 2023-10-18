//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Controls.Renderers;
using DocumentFlow.Controls.Tools;
using DocumentFlow.Data;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;
using DocumentFlow.Dialogs.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.Input.Enums;

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

using SE = Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Controls.Editors;

public delegate void DataGridSummaryRow<T>(IDataGridSummary<T> summary) where T : IEntity<long>;

[ToolboxItem(true)]
public partial class DfDataGrid : DfControl, IAccess
{
    private Type? contentType;
    private bool enabledEditor = true;
    private IDataGridDialog? dialog;
    private MethodInfo? createMethod;
    private MethodInfo? editMethod;

    private readonly List<(ToolStripDropDownButton, ToolStripMenuItem)> commands = new();

    public DfDataGrid()
    {
        InitializeComponent();
        SetNestedControl(gridMain);

        gridMain.CellRenderers.Remove("TableSummary");
        gridMain.CellRenderers.Add("TableSummary", new CustomGridTableSummaryRenderer());
    }

    public event EventHandler<DialogParametersEventArgs>? DialogParameters;
    public event EventHandler<DependentEntitySelectEventArgs>? CreateRow;
    public event EventHandler<DependentEntitySelectEventArgs>? EditRow;
    public event EventHandler<ConfirmGeneratingColumnArgs>? ConfirmGeneratingColumn;

    public bool EnabledEditor
    {
        get => enabledEditor;
        set
        {
            if (enabledEditor != value)
            {
                enabledEditor = value;
                toolStrip1.Enabled = value;

                menuCreate.Enabled = value;
                menuEdit.Enabled = value;
                menuDelete.Enabled = value;
                menuCopy.Enabled = value;
            }
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object DataSource
    {
        get => gridMain.DataSource;
        set
        {
            if (value != null)
            {
                var type = value.GetType();
                contentType = type.GenericTypeArguments.FirstOrDefault();

                ArgumentNullException.ThrowIfNull(contentType);
            }

            gridMain.DataSource = value;
        }
    }

    [Browsable(false)]
    public object SelectedItem => gridMain.SelectedItem;

    public void GridSummaryRow<T>(VerticalPosition position, DataGridSummaryRow<T> summaryRow) where T : Entity<long>
    {
        gridMain.TableSummaryRows.Clear();
        var tableSummaryRow = new GridTableSummaryRow()
        {
            Name = "TableRowSummary",
            ShowSummaryInRow = false,
            Position = position
        };

        gridMain.TableSummaryRows.Add(tableSummaryRow);

        var summary = new DataGridSummary<T>(tableSummaryRow);
        summaryRow(summary);
    }

    public void AddCommand(string text, Image image, Action action)
    {
        toolStripButtonSeparatorCustom.Visible = true;
        toolStripMenuSeparatorCustom.Visible = true;

        ToolStripButton button = new(text, image, (sender, e) => action())
        {
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            TextImageRelation = TextImageRelation.ImageBeforeText
        };

        toolStrip1.Items.Add(button);

        ToolStripMenuItem item = new(text, image, (sender, e) => action());
        contextMenuStripEx1.Items.Add(item);
    }

    public int AddCommands(string text, Image image)
    {
        toolStripButtonSeparatorCustom.Visible = true;
        toolStripMenuSeparatorCustom.Visible = true;

        ToolStripDropDownButton button = new(text, image)
        {
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            TextImageRelation = TextImageRelation.ImageBeforeText
        };

        toolStrip1.Items.Add(button);

        ToolStripMenuItem item = new(text, image);
        contextMenuStripEx1.Items.Add(item);

        var cmds = (button, item);
        commands.Add(cmds);

        return commands.IndexOf(cmds);
    }

    public void AddCommand(int commandIndex, string text, object data, Action<object> action)
    {
        var (button, item) = commands[commandIndex];

        button.DropDownItems.Add(new ToolStripMenuItem(text, null, (sender, e) => { if (sender is ToolStripMenuItem tool) action(tool.Tag); })
        {
            Tag = data
        });

        item.DropDownItems.Add(new ToolStripMenuItem(text, null, (sender, e) => { if (sender is ToolStripMenuItem tool) action(tool.Tag); })
        {
            Tag = data
        });
    }

    public void ClearCommands(int commandIndex)
    {
        var (button, item) = commands[commandIndex];
        button.DropDownItems.Clear();
        item.DropDownItems.Clear();
    }

    public void RegisterDialog<D, T>()
        where D : IDataGridDialog
        where T : new()
    {
        dialog = CurrentApplicationContext.GetServices().GetRequiredService<D>();

        var dialogType = dialog.GetType();
        
        var methodType = dialogType.GetMethod("Create") ?? throw new Exception($"Класс {dialogType.Name} должен модержать метод Create.");
        createMethod = methodType.MakeGenericMethod(typeof(T));

        methodType = dialogType.GetMethod("Edit") ?? throw new Exception($"Класс {dialogType.Name} должен модержать метод Edit.");
        editMethod = methodType.MakeGenericMethod(typeof(T));
    }

    private void Edit()
    {
        if (DataSource is IList list && gridMain.SelectedItem is IDependentEntity row)
        {
            if (dialog != null && editMethod != null)
            {
                DialogParameters?.Invoke(this, new DialogParametersEventArgs(dialog));

                var res = (bool)editMethod.Invoke(dialog, new object[] { row })!;
                if (res)
                {
                    list[list.IndexOf(row)] = row;
                }
            }
            else
            {
                if (EditRow != null)
                {
                    var args = new DependentEntitySelectEventArgs(row);
                    EditRow(this, args);

                    if (args.Accept)
                    {
                        list[list.IndexOf(args.DependentEntity)] = args.DependentEntity;
                    }
                }
            }
        }
    }

    private void ButtonCreate_Click(object sender, EventArgs e)
    {
        if (DataSource is IList list)
        {
            if (dialog != null && createMethod != null)
            {
                DialogParameters?.Invoke(this, new DialogParametersEventArgs(dialog));

                var parameters = new object?[] { null };
                var res = (bool)createMethod.Invoke(dialog, parameters)!;
                if (res && parameters[0] is IDependentEntity entity)
                {
                    list.Add(entity);
                }
            }
            else
            {
                if (CreateRow != null)
                {
                    var args = new DependentEntitySelectEventArgs();
                    CreateRow(this, args);

                    if (args.Accept)
                    {
                        list.Add(args.DependentEntity);
                    }
                }
            }
        }
    }

    private void ButtonEdit_Click(object sender, EventArgs e) => Edit();

    private void ButtonDelete_Click(object sender, EventArgs e)
    {
        if (DataSource is IList list && gridMain.SelectedItem is IDependentEntity row)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            bool last = list.IndexOf(row) == list.Count - 1;
            list.Remove(row);

            // FIX: Если в таблицу добавлены GridSummaryRow, то удаление последней строки приводит к Exception
            if (last)
            {
                gridMain.DataSource = null;
                gridMain.DataSource = list;
            }
        }
    }

    private void ButtonCopy_Click(object sender, EventArgs e)
    {
        if (DataSource is IList list && gridMain.SelectedItem is ICopyable item)
        {
            var copy = item.Copy();
            if (copy is IDependentEntity entity)
            {
                if (dialog != null && editMethod != null)
                {
                    DialogParameters?.Invoke(this, new DialogParametersEventArgs(dialog));

                    var res = (bool)editMethod.Invoke(dialog, new object[] { entity })!;
                    if (res)
                    {
                        list.Add(entity);
                    }
                }
                else
                {
                    if (EditRow != null)
                    {
                        var args = new DependentEntitySelectEventArgs(entity);
                        EditRow(this, args);
                        if (args.Accept)
                        {
                            list.Add(args.DependentEntity);
                        }
                    }
                    else
                    {
                        list.Add(entity);
                    }
                }
            }
        }
    }

    private void MenuCopyToClipboard_Click(object sender, EventArgs e)
    {
        if (gridMain.CurrentCell == null || gridMain.SelectedItem == null || contentType == null)
        {
            return;
        }

        PropertyInfo? prop = contentType.GetProperty(gridMain.CurrentCell.Column.MappingName);
        if (prop != null)
        {
            object? value = prop.GetValue(gridMain.SelectedItem);
            if (value != null)
            {
                Clipboard.SetText(value.ToString() ?? string.Empty);
            }
        }
    }

    private void GridMain_CellDoubleClick(object sender, CellClickEventArgs e) => Edit();

    private void GridMain_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
    {
        if (contentType == null)
        {
            return;
        }

        var prop = contentType.GetProperties().FirstOrDefault(p => p.Name == e.Column.MappingName);
        if (prop == null)
        {
            return;
        }

        var attr = prop.GetCustomAttribute<ColumnModeAttribute>();
        if (attr == null)
        {
            return;
        }

        if (attr.AutoSizeColumnsMode != Data.Enums.AutoSizeColumnsMode.None)
        {
            e.Column.AutoSizeColumnsMode = (SE.AutoSizeColumnsMode)(int)attr.AutoSizeColumnsMode;
        }

        if (attr.Width > 0)
        {
            e.Column.Width = attr.Width;
        }

        e.Column.CellStyle.HorizontalAlignment = (System.Windows.Forms.HorizontalAlignment)(int)attr.Alignment;

        switch (attr.Format)
        {
            case ColumnFormat.Currency:
                if (e.Column is GridNumericColumn c)
                {
                    c.FormatMode = FormatMode.Currency;
                }

                e.Column.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;

                break;
            case ColumnFormat.Progress:
                var disp = prop.GetCustomAttribute<DisplayAttribute>();
                var header = disp?.Name ?? e.Column.MappingName;
                e.Column = new GridProgressBarColumn()
                {
                    MappingName = e.Column.MappingName,
                    HeaderText = header,
                    Maximum = 100,
                    Minimum = 0,
                    ValueMode = ProgressBarValueMode.Percentage
                };

                break;
            default:
                break;
        }

        if (e.Column is GridNumericColumn numericColumn && attr.DecimalDigits != -1)
        {
            NumberFormatInfo numericFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
            numericFormat.NumberDecimalDigits = attr.DecimalDigits;

            numericColumn.NumberFormatInfo = numericFormat;
        }

        var args = new ConfirmGeneratingColumnArgs(e.Column.MappingName, false);
        ConfirmGeneratingColumn?.Invoke(this, args);

        e.Cancel = args.Cancel;
    }
}
