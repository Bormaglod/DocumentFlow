//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.03.2018
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data.Infrastructure;
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
            if (grid.View == null)
                return;

            bool topLevel = grid.View.TopLevelGroup == null || grid.View.TopLevelGroup.Groups.Count == 0;
            int cnt = (topLevel ? grid.View.Records.Count : grid.View.TopLevelGroup?.DisplayElements.Count ?? 0) + grid.StackedHeaderRows.Count;

            int rowIndex = grid.ShowPreviewRow && rowColumnIndex.RowIndex > 0 ? 
                (rowColumnIndex.RowIndex - 1) / 2 + 1 : 
                rowColumnIndex.RowIndex;

            if (rowIndex > grid.StackedHeaderRows.Count && rowIndex <= cnt)
            {
                RecordEntry? rowData;
                
                int row = rowIndex - 1 - grid.StackedHeaderRows.Count;
                if (topLevel)
                {
                    rowData = grid.View.Records[row];
                }
                else
                {
                    rowData = grid.View.TopLevelGroup?.DisplayElements[row] as RecordEntry;
                }

                if (rowData == null)
                    return;

                if (rowData.Data is IDocumentInfo rowView)
                {
                    Image? image = null;

                    if (headerImage != null)
                    {
                        image = headerImage.Get(rowView);
                    }

                    if (image == null)
                    {
                        if (rowData.Data is IDirectory dir && dir.is_folder)
                        {
                            image = rowView.deleted ? Resources.icons8_folder_delete_16 : Resources.icons8_folder_16;
                        }
                        else
                        {
                            if (rowView.deleted)
                            {
                                image = Resources.icons8_document_delete_16;
                            }
                            else
                            {
                                image = (rowData.Data is IAccountingDocument doc && doc.carried_out) 
                                    ? (doc.re_carried_out ? Resources.icons8_document_warn_check_16 : Resources.icons8_document_check_16)
                                    : Resources.icons8_document_16;
                            }
                        }
                    }

                    int x = (cellRect.Width - image.Width) / 2 - 1;
                    int y = (cellRect.Height - image.Height) / 2 - 1;
                    paint.DrawImage(image, new Rectangle(x + cellRect.X, y + cellRect.Y, image.Width, image.Height));
                }
            }

            if (rowIndex > cnt)
            {
                paint.FillRectangle(new SolidBrush(style.BackColor), cellRect);
            }
        }
    }
}
