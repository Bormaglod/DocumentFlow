//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.03.2018
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Controls.Tools;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Properties;

using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Renderers;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.GridCommon.ScrollAxis;

namespace DocumentFlow.Controls.Renderers
{
    public class CustomRowHeaderCellRenderer : GridRowHeaderCellRenderer
    {
        private readonly SfDataGrid grid;
        private readonly IRowHeaderImage? headerImage;

        public CustomRowHeaderCellRenderer(SfDataGrid dataGrid, IRowHeaderImage? rowHeaderImage = null) => (grid, headerImage) = (dataGrid, rowHeaderImage);

        protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style, DataColumnBase column, RowColumnIndex rowColumnIndex)
        {
            paint.PaintCellBackground(cellRect, style);

            var entry = grid.GetRecordEntryAtRowIndex(rowColumnIndex.RowIndex);
            if (entry is not RecordEntry rowData)
            {
                return;
            }

            if (rowData.Data is not IDocumentInfo rowView)
            {
                return;
            }

            Image? image = headerImage?.Get(rowView); ;

            if (image == null)
            {
                if (rowData.Data is IDirectory dir && dir.IsFolder)
                {
                    image = rowView.Deleted ? Resources.icons8_folder_delete_16 : Resources.icons8_folder_16;
                }
                else
                {
                    if (rowView.Deleted)
                    {
                        image = Resources.icons8_document_delete_16;
                    }
                    else
                    {
                        image = (rowData.Data is IAccountingDocument doc && doc.CarriedOut)
                            ? (doc.ReCarriedOut ? Resources.icons8_document_warn_check_16 : Resources.icons8_document_check_16)
                            : Resources.icons8_document_16;
                    }
                }
            }

            int x = (cellRect.Width - image.Width) / 2 - 1;
            int y = (cellRect.Height - image.Height) / 2 - 1;
            paint.DrawImage(image, new Rectangle(x + cellRect.X, y + cellRect.Y, image.Width, image.Height));
        }
    }
}
