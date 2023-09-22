//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.04.2020
// Time: 23:38
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Tools;
using DocumentFlow.Data.Interfaces;

using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Renderers;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.GridCommon.ScrollAxis;

namespace DocumentFlow.Controls.Renderers;

public class CustomUnboundCellRenderer : GridUnboundCellRenderer
{
    private readonly SfDataGrid grid;

    public CustomUnboundCellRenderer(SfDataGrid grid)
    {
        this.grid = grid;
    }

    protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style, DataColumnBase column, RowColumnIndex rowColumnIndex)
    {
        paint.PaintCellBackground(cellRect, style);

        var entry = grid.GetRecordEntryAtRowIndex(rowColumnIndex.RowIndex);
        if (entry is not RecordEntry rowData)
        {
            return;
        }

        if (rowData.Data is not IDocumentInfo doc)
        {
            return;
        }

        Image? image = null;

        if (doc.HasDocuments == null)
        {
            image = DocumentFlow.Properties.Resources.icons8_question_16;
        }
        else
        {
            if (doc.HasDocuments.Value)
            {
                image = DocumentFlow.Properties.Resources.icons8_attachment_small_16;
            }
        }

        if (image != null)
        {
            float x = cellRect.X + (cellRect.Width - image.Width) / 2;
            float y = cellRect.Y + (cellRect.Height - image.Height) / 2;
            paint.DrawImage(image, new RectangleF(x, y, image.Width, image.Height));
        }
    }
}
