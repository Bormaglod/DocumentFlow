//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2020
// Time: 21:42
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;
using DocumentFlow.Code.Controls;
using DocumentFlow.Code.System;
using DocumentFlow.Core;
using System.Linq;

namespace DocumentFlow.Code.Implementation
{
    public class GridColumnCollection : IColumnCollection
    {
        private SfDataGrid gridContent;
        private IList<IColumn> columns = new List<IColumn>();
        private ToolStripMenuItem menuItemVisibles;

        public GridColumnCollection(SfDataGrid dataGrid, ToolStripMenuItem menuItems)
        {
            gridContent = dataGrid;
            menuItemVisibles = menuItems;
        }

        public event EventHandler ChangeColumnVisible;

        IColumn IColumnCollection.this[int index] => columns[index];

        IColumn IColumnCollection.this[string dataField] => columns.FirstOrDefault(x => x.FieldName == dataField);

        public IEnumerator<IColumn> GetEnumerator() => columns.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => columns.GetEnumerator();

        IColumn IColumnCollection.CreateText(string dataField, string headerText)
        {
            GridColumn gridColumn = new GridTextColumn()
            {
                MappingName = dataField,
                HeaderText = headerText
            };

            return AddColumn(DataColumnType.Text, gridColumn);
        }

        IColumn IColumnCollection.CreateInteger(string dataField, string headerText, NumberFormatMode formatMode)
        {
            NumberFormatInfo numberFormat = Application.CurrentCulture.NumberFormat.Clone() as NumberFormatInfo;
            numberFormat.NumberDecimalDigits = 0;
            numberFormat.PercentDecimalDigits = 0;

            GridColumn gridColumn = new GridNumericColumn()
            {
                MappingName = dataField,
                HeaderText = headerText,
                FormatMode = EnumHelper.TransformEnum<FormatMode, NumberFormatMode>(formatMode),
                NumberFormatInfo = numberFormat
            };

            return AddColumn(DataColumnType.Integer, gridColumn);
        }

        IColumn IColumnCollection.CreateNumeric(string dataField, string headerText, NumberFormatMode formatMode, int decimalDigits)
        {
            NumberFormatInfo numberFormat = Application.CurrentCulture.NumberFormat.Clone() as NumberFormatInfo;
            numberFormat.NumberDecimalDigits = decimalDigits;
            numberFormat.PercentDecimalDigits = decimalDigits;

            GridColumn gridColumn = new GridDecimalColumn()
            {
                MappingName = dataField,
                HeaderText = headerText,
                FormatMode = EnumHelper.TransformEnum<FormatMode, NumberFormatMode>(formatMode),
                NumberFormatInfo = numberFormat
            };

            return AddColumn(DataColumnType.Numeric, gridColumn);
        }

        IColumn IColumnCollection.CreateBoolean(string dataField, string headerText)
        {
            GridColumn gridColumn = new GridCheckBoxColumn()
            {
                MappingName = dataField,
                HeaderText = headerText
            };

            return AddColumn(DataColumnType.Boolean, gridColumn);
        }

        IColumn IColumnCollection.CreateDate(string dataField, string headerText, string format)
        {
            GridColumn gridColumn = new GridDateTimeColumn()
            {
                MappingName = dataField,
                HeaderText = headerText,
                Pattern = DateTimePattern.Custom,
                Format = format
            };

            return AddColumn(DataColumnType.Date, gridColumn);
        }

        IColumn IColumnCollection.CreateImage(string dataField, string headerText)
        {
            GridImageColumn gridColumn = new GridImageColumn()
            {
                MappingName = dataField,
                HeaderText = headerText,
                ImageLayout = ImageLayout.Center
            };

            gridColumn.CellStyle.VerticalAlignment = VerticalAlignment.Center;
            gridColumn.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

            return AddColumn(DataColumnType.Image, gridColumn);
        }

        IColumn IColumnCollection.CreateProgress(string dataField, string headerText)
        {
            GridProgressBarColumn gridProgress = new GridProgressBarColumn()
            {
                MappingName = dataField,
                HeaderText = headerText,
                Maximum = 100,
                Minimum = 0,
                ValueMode = ProgressBarValueMode.Percentage
            };

            return AddColumn(DataColumnType.Progress, gridProgress);
        }

        ISorted IColumnCollection.CreateSortedColumns()
        {
            return new SortedColumnsData(gridContent);
        }

        IStackedColumn IColumnCollection.CreateStackedColumns()
        {
            return new StackedColumnData(gridContent);
        }

        ISummary IColumnCollection.CreateTableSummaryRow(GroupVerticalPosition position)
        {
            gridContent.TableSummaryRows.Clear();
            GridTableSummaryRow tableSummaryRow = new GridTableSummaryRow()
            {
                Name = "TableRowSummary",
                ShowSummaryInRow = false,
                Position = EnumHelper.TransformEnum<VerticalPosition, GroupVerticalPosition>(position)
            };

            gridContent.TableSummaryRows.Add(tableSummaryRow);

            gridContent.CellRenderers.Remove("TableSummary");
            gridContent.CellRenderers.Add("TableSummary", new CustomGridTableSummaryRenderer(columns));

            return new SummaryRowData(tableSummaryRow);
        }

        ISummary IColumnCollection.CreateGroupSummaryRow()
        {
            gridContent.GroupSummaryRows.Clear();
            GridSummaryRow groupSummaryRow = new GridSummaryRow()
            {
                Name = "GroupSummaryRow",
                ShowSummaryInRow = false
            };

            gridContent.GroupSummaryRows.Add(groupSummaryRow);

            gridContent.CellRenderers.Remove("GroupSummary");
            gridContent.CellRenderers.Add("GroupSummary", new CustomGridTableSummaryRenderer(columns));

            return new SummaryRowData(groupSummaryRow);
        }

        private IColumn AddColumn(DataColumnType type, GridColumn gridColumn)
        {
            gridContent.Columns.Add(gridColumn);

            ToolStripMenuItem item = new ToolStripMenuItem()
            {
                Text = gridColumn.HeaderText,
                CheckOnClick = true,
                Checked = gridColumn.Visible
            };

            IColumn column;
            if (gridColumn is GridNumericColumn numberColumn)
            {
                NumberFormatMode mode = EnumHelper.TransformEnum<NumberFormatMode, FormatMode>(numberColumn.FormatMode);
                column = new GridNumericColumnData(type, mode, numberColumn.NumberFormatInfo.NumberDecimalDigits, gridColumn, item);
            }
            else
            {
                column = new GridColumnData(type, gridColumn, item);
            }

            columns.Add(column);

            item.Click += OnChangeColumnVisible;
            if (menuItemVisibles != null)
            {
                menuItemVisibles.DropDownItems.Add(item);
            }

            return column;
        }

        private void OnChangeColumnVisible(object sender, EventArgs e)
        {
            ChangeColumnVisible?.Invoke(sender, EventArgs.Empty);
        }
    }
}
