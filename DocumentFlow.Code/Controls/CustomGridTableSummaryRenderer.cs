//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.04.2020
// Time: 17:05
//-----------------------------------------------------------------------

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
using DocumentFlow.Code.Core;

namespace DocumentFlow.Code.Controls
{
    public class CustomGridTableSummaryRenderer : GridTableSummaryCellRenderer
    {
        private readonly IEnumerable<IColumn> columns;

        public CustomGridTableSummaryRenderer(IEnumerable<IColumn> dataColumns) => columns = dataColumns;

        protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style, DataColumnBase column, RowColumnIndex rowColumnIndex)
        {
            if (string.IsNullOrEmpty(cellValue))
                return;

            IColumn c = columns.FirstOrDefault(x => x.FieldName == column.GridColumn.MappingName);
            if (c == null)
                return;

            if (c is INumericColumn numericColumn)
            {
                NumberFormatInfo format = new()
                {
                    NumberDecimalDigits = numericColumn.DecimalDigits
                };

                if (numericColumn.FormatMode == NumberFormatMode.Percent)
                {
                    cellValue = Convert.ToDouble(decimal.Parse(cellValue)).ToString("N", format) + " %";
                }
            }

            StringFormat stringFormat = new()
            {
                LineAlignment = StringAlignment.Center
            };

            stringFormat.Alignment = c.HorizontalAlignment switch
            {
                HorizontalAlignment.Left => StringAlignment.Near,
                HorizontalAlignment.Center => StringAlignment.Center,
                HorizontalAlignment.Right => StringAlignment.Far,
                _ => throw new NotImplementedException()
            };
            paint.DrawString(cellValue, style.Font.GetFont(), Brushes.Black, new RectangleF(cellRect.Left + 3, cellRect.Top, cellRect.Width - 6, cellRect.Height), stringFormat);
        }
    }
}
