//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.04.2020
// Time: 16:19
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Extensions
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;
    using Syncfusion.Data;
    using Syncfusion.WinForms.DataGrid;
    using Syncfusion.WinForms.DataGrid.Enums;
    using Syncfusion.WinForms.Input.Enums;
    using DocumentFlow.DataSchema;
    using DocumentFlow.Controls.Renderers;

    public static class DataGridExtension
    {
        public static GridColumn CreateColumn(this SfDataGrid grid, DatasetColumn c)
        {
            GridColumn column = null;

            NumberFormatInfo numberFormat = Application.CurrentCulture.NumberFormat.Clone() as NumberFormatInfo;
            numberFormat.NumberDecimalDigits = c.DecimalDigits;
            numberFormat.PercentDecimalDigits = c.DecimalDigits;

            switch (c.Type)
            {
                case DatasetColumnType.Integer:
                    column = new GridNumericColumn()
                    {
                        FormatMode = c.FormatMode,
                        NumberFormatInfo = numberFormat
                    };

                    break;
                case DatasetColumnType.Numeric:
                    column = new GridDecimalColumn()
                    {
                        FormatMode = c.FormatMode,
                        NumberFormatInfo = numberFormat
                    };

                    break;
                case DatasetColumnType.Text:
                    column = new GridTextColumn();
                    break;
                case DatasetColumnType.Memo:
                    column = new GridTextColumn()
                    {
                        AllowMultiline = true,
                        AllowVerticalScrollbar = true
                    };

                    break;
                case DatasetColumnType.Image:
                    column = new GridImageColumn()
                    {
                        ImageLayout = ImageLayout.Center
                    };

                    break;
                case DatasetColumnType.Boolean:
                    column = new GridCheckBoxColumn();
                    break;
                case DatasetColumnType.Date:
                    column = new GridDateTimeColumn()
                    {
                        Pattern = DateTimePattern.Custom,
                        Format = c.DateFormat
                    };

                    break;
                case DatasetColumnType.Enums:
                    column = new GridTextColumn
                    {
                        FormatProvider = new EnumFormatter(c.ColumnEnums),
                        Format = "*"
                    };

                    break;
                case DatasetColumnType.Progress:
                    column = new GridProgressBarColumn()
                    {
                        Maximum = 100,
                        Minimum = 0,
                        ValueMode = ProgressBarValueMode.Percentage
                    };

                    break;
                default:
                    break;
            }

            column.MappingName = c.DataField;
            column.HeaderText = c.Text;
            column.Width = c.AutoSize ? double.NaN : c.Width;
            column.AutoSizeColumnsMode = c.AutoSize ? AutoSizeColumnsMode.LastColumnFill : AutoSizeColumnsMode.AllCells;
            column.Visible = c.Visible;
            column.AllowSorting = c.Sortable;
            column.AllowResizing = c.Resizable;
            column.AllowGrouping = c.AllowGrouping;
            column.CellStyle.HorizontalAlignment = c.HorizontalAlignment;

            if (c.Type == DatasetColumnType.Image)
            {
                (column as GridImageColumn).CellStyle.VerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
                (column as GridImageColumn).CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
            }

            grid.Columns.Add(column);

            return column;
        }

        public static void CreateSummaryRow(this SfDataGrid grid, IList<DatasetColumn> columns)
        {
            grid.TableSummaryRows.Clear();

            if (columns == null)
                return;

            GridTableSummaryRow row = null;
            foreach (DatasetColumn c in columns.Where(x => x.Aggregate != Aggregate.None))
            {
                if (row == null)
                {
                    row = new GridTableSummaryRow();
                    row.Name = "TableRowSummary";
                    row.ShowSummaryInRow = false;
                    row.Position = VerticalPosition.Bottom;
                }

                GridSummaryColumn column = new GridSummaryColumn();
                column.SummaryType = SummaryType.DoubleAggregate;
                column.Format = c.AggregateTitle + "{" + c.SummaryFormat + "}";
                column.MappingName = c.DataField;
                column.Name = c.DataField;
                row.SummaryColumns.Add(column);
            }

            if (row != null)
            {
                grid.TableSummaryRows.Add(row);

                grid.CellRenderers.Remove("TableSummary");
                grid.CellRenderers.Add("TableSummary", new CustomGridTableSummaryRenderer(columns));
            }
        }

        public static void CreateStackedColumns(this SfDataGrid grid, IList<StackedColumnData> stackedColumns)
        {
            grid.StackedHeaderRows.Clear();

            if (stackedColumns == null || stackedColumns.Count == 0)
                return;

            var stackedHeaderRow = new StackedHeaderRow();
            foreach (StackedColumnData c in stackedColumns)
            {
                stackedHeaderRow.StackedColumns.Add(new StackedColumn() { ChildColumns = string.Join(",", c.Childs), HeaderText = c.Header });
            }

            grid.StackedHeaderRows.Add(stackedHeaderRow);
        }
    }
}
