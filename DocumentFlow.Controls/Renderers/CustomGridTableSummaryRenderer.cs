//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.04.2020
// Time: 17:05
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Renderers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;
    using Syncfusion.WinForms.DataGrid;
    using Syncfusion.WinForms.DataGrid.Renderers;
    using Syncfusion.WinForms.DataGrid.Styles;
    using Syncfusion.WinForms.GridCommon.ScrollAxis;
    using Syncfusion.WinForms.Input.Enums;
    using DocumentFlow.DataSchema;

    public class CustomGridTableSummaryRenderer : GridTableSummaryCellRenderer
    {
        private IList<DatasetColumn> columns;

        public CustomGridTableSummaryRenderer(IList<DatasetColumn> dataColumns)
        {
            columns = dataColumns;
        }

        protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style, DataColumnBase column, RowColumnIndex rowColumnIndex)
        {
            if (string.IsNullOrEmpty(cellValue))
                return;

            DatasetColumn c = columns.FirstOrDefault(x => x.DataField == column.GridColumn.MappingName);
            if (c == null)
                return;

            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalDigits = c.DecimalDigits;


            if (c.Type == DatasetColumnType.Integer || c.Type == DatasetColumnType.Numeric)
            {
                if (c.FormatMode == FormatMode.Percent)
                {
                    cellValue = Convert.ToDouble(decimal.Parse(cellValue)).ToString("N", format) + " %";
                }
            }

            StringFormat stringFormat = new StringFormat();
            stringFormat.LineAlignment = StringAlignment.Center;
            if (c.HorizontalAlignment == HorizontalAlignment.Left)
                stringFormat.Alignment = StringAlignment.Near;
            else if (c.HorizontalAlignment == HorizontalAlignment.Center)
                stringFormat.Alignment = StringAlignment.Center;
            else if (c.HorizontalAlignment == HorizontalAlignment.Right)
                stringFormat.Alignment = StringAlignment.Far;
            paint.DrawString(cellValue, style.Font.GetFont(), Brushes.Black, new RectangleF(cellRect.Left + 3, cellRect.Top, cellRect.Width - 6, cellRect.Height), stringFormat);
        }
    }
}
