//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.04.2020
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Renderers;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.GridCommon.ScrollAxis;
using Syncfusion.WinForms.Input.Enums;

using System.Globalization;

namespace DocumentFlow.Controls.Renderers;

public class CustomGridTableSummaryRenderer : GridTableSummaryCellRenderer
{
    protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style, DataColumnBase column, RowColumnIndex rowColumnIndex)
    {
        if (string.IsNullOrEmpty(cellValue))
            return;

        if (column.GridColumn is GridNumericColumn numericColumn)
        {
            if (numericColumn.FormatMode != FormatMode.Currency)
            {
                int digits = 0;
                if (numericColumn.NumberFormatInfo != null)
                {
                    digits = numericColumn.FormatMode == FormatMode.Numeric
                        ? numericColumn.NumberFormatInfo.NumberDecimalDigits
                        : numericColumn.NumberFormatInfo.PercentDecimalDigits;
                }

                NumberFormatInfo format = new()
                {
                    NumberDecimalDigits = digits,
                    NumberDecimalSeparator = ",",
                    NumberGroupSeparator = " "
                };

                decimal value = decimal.Parse(cellValue);
                cellValue = numericColumn.FormatMode == FormatMode.Numeric
                    ? value.ToString("N", format)
                    : value.ToString("N", format) + " %";
            }
        }

        StringFormat stringFormat = new()
        {
            LineAlignment = StringAlignment.Center,
            Alignment = column.GridColumn.CellStyle.HorizontalAlignment switch
            {
                HorizontalAlignment.Left => StringAlignment.Near,
                HorizontalAlignment.Center => StringAlignment.Center,
                HorizontalAlignment.Right => StringAlignment.Far,
                _ => throw new NotImplementedException()
            }
        };

        paint.DrawString(cellValue, style.Font.GetFont(), Brushes.Black, new RectangleF(cellRect.Left + 3, cellRect.Top, cellRect.Width - 6, cellRect.Height), stringFormat);
    }
}
