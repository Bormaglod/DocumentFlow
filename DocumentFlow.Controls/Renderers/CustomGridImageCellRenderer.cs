//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.04.2020
// Time: 23:38
//-----------------------------------------------------------------------

using System.Drawing;
using Syncfusion.ComponentModel;
using Syncfusion.WinForms.Core;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.DataGrid.Renderers;
using Syncfusion.WinForms.GridCommon.ScrollAxis;
using DocumentFlow.Core;

namespace DocumentFlow.Controls.Renderers
{
    public class CustomGridImageCellRenderer : GridImageCellRenderer
    {
        public CustomGridImageCellRenderer(SfDataGrid grid)
        {
            DataGrid = grid;
        }

        private SfDataGrid DataGrid { get; }

        protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style, DataColumnBase column, RowColumnIndex rowColumnIndex)
        {
            PaintCellBackground(paint, cellRect, style);

            Image image = null;
            if (!string.IsNullOrEmpty(cellValue))
            {
                image = ImageHelper.Base64ToImage(cellValue);
            }

            RectangleF clipBounds = paint.ClipBounds;
            DrawImage(paint, DataGrid, column, style, cellRect, image, cellValue);
            paint.SetClip(clipBounds);
        }

        private void PaintCellBackground(Graphics graphics, Rectangle cellRect, CellStyleInfo style)
        {
            if (style.HasInterior)
            {
                BrushPainter.FillRectangle(graphics, cellRect, style.Interior);
                return;
            }

            Brush brush = new SolidBrush(style.BackColor);
            graphics.FillRectangle(brush, cellRect);
            DisposeHelper.Dispose(ref brush);
        }
    }
}
