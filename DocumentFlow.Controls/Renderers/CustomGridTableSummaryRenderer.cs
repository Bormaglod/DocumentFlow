using System;
using System.Drawing;
using System.Globalization;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.DataGrid.Renderers;
using Syncfusion.WinForms.GridCommon.ScrollAxis;
using Syncfusion.WinForms.Input.Enums;
using DocumentFlow.Code;

namespace DocumentFlow.Controls.Renderers
{
    public class CustomGridTableSummaryRenderer : GridSummaryCellRendererBase
    {
        private readonly IColumnCollection columns;

        public CustomGridTableSummaryRenderer(IColumnCollection columns)
        {
            this.columns = columns;
        }

        protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue,
            CellStyleInfo style, DataColumnBase column, RowColumnIndex rowColumnIndex)
        {
            if (string.IsNullOrEmpty(cellValue))
                return;

            IColumn c = columns[column.GridColumn.MappingName];
            if (c == null)
                return;

            NumberFormatInfo format = new NumberFormatInfo();

            if (column.GridColumn is GridNumericColumn numericColumn)
            {
                format.NumberDecimalDigits = numericColumn.NumberFormatInfo.NumberDecimalDigits;
                format.PercentDecimalDigits = numericColumn.NumberFormatInfo.PercentDecimalDigits;
                format.NumberDecimalSeparator = numericColumn.NumberFormatInfo.NumberDecimalSeparator;
                format.NumberGroupSeparator = numericColumn.NumberFormatInfo.NumberGroupSeparator;

                if (numericColumn.FormatMode == FormatMode.Numeric)
                {
                    cellValue = Convert.ToDouble(double.Parse(cellValue, NumberStyles.Number)).ToString("N", format);
                }

                StringFormat stringFormat = new StringFormat();
                stringFormat.LineAlignment = StringAlignment.Center;

                switch (numericColumn.CellStyle.HorizontalAlignment)
                {
                    case System.Windows.Forms.HorizontalAlignment.Left:
                        stringFormat.Alignment = StringAlignment.Near;
                        break;
                    case System.Windows.Forms.HorizontalAlignment.Right:
                        stringFormat.Alignment = StringAlignment.Far;
                        break;
                    case System.Windows.Forms.HorizontalAlignment.Center:
                        stringFormat.Alignment = StringAlignment.Center;
                        break;
                }

                paint.DrawString(cellValue, style.Font.GetFont(), Brushes.Black, cellRect, stringFormat);
            }
        }
    }
}
