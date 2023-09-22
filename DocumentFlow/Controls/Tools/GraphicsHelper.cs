//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.08.2023
//-----------------------------------------------------------------------

using Syncfusion.ComponentModel;
using Syncfusion.WinForms.Core;
using Syncfusion.WinForms.DataGrid.Styles;

namespace DocumentFlow.Controls.Tools;

public static class GraphicsHelper
{
    public static void PaintCellBackground(this Graphics graphics, Rectangle cellRect, CellStyleInfo style)
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
