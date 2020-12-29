//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.03.2018
// Time: 22:09
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Dapper;
using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Renderers;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.GridCommon.ScrollAxis;
using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Controls.Renderers
{
    public class CustomRowHeaderCellRenderer : GridRowHeaderCellRenderer
    {
        private readonly SfDataGrid grid;
        private readonly Dictionary<Guid, Image> stateImages;
        private IEnumerable<Status> states;

        public CustomRowHeaderCellRenderer(SfDataGrid dataGrid)
        {
            grid = dataGrid;
            stateImages = new Dictionary<Guid, Image>();
            RefreshStateImages();
        }

        protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style, DataColumnBase column, RowColumnIndex rowColumnIndex)
        {
            if (grid.View == null)
                return;

            bool topLevel = grid.View.TopLevelGroup == null || grid.View.TopLevelGroup.Groups.Count == 0;
            int cnt = (topLevel ? grid.View.Records.Count : grid.View.TopLevelGroup.DisplayElements.Count) + grid.StackedHeaderRows.Count;

            if (rowColumnIndex.RowIndex > grid.StackedHeaderRows.Count && rowColumnIndex.RowIndex <= cnt)
            {
                RecordEntry rowData;
                int row = rowColumnIndex.RowIndex - 1 - grid.StackedHeaderRows.Count;
                if (topLevel)
                {
                    rowData = grid.View.Records[row];
                }
                else
                {
                    rowData = grid.View.TopLevelGroup.DisplayElements[row] as RecordEntry;
                }

                if (rowData == null)
                    return;

                if (rowData.Data is IDocumentInfo rowView)
                {
                    Status status = states.Where(x => x.id == rowView.status_id).FirstOrDefault();

                    if (status.picture_id.HasValue)
                    {
                        Image image = stateImages[status.picture_id.Value];
                        int x = (cellRect.Width - image.Width) / 2 - 1;
                        int y = (cellRect.Height - image.Height) / 2 - 1;
                        paint.DrawImage(image, new Rectangle(x + cellRect.X, y + cellRect.Y, image.Width, image.Height));
                    }
                }
            }

            if (rowColumnIndex.RowIndex > cnt)
            {
                paint.FillRectangle(new SolidBrush(style.BackColor), cellRect);
            }
        }

        private void RefreshStateImages()
        {
            stateImages.Clear();

            using (var conn = Db.OpenConnection())
            {
                // список иконок находящихся в группе state (Состояния документов)
                IEnumerable<Picture> pictures = conn.Query<Picture>("select * from picture where parent_id = :parent", new { parent = new Guid("8d491ef2-a8de-418b-8b88-64238e550663") });

                foreach (Picture image in pictures)
                {
                    stateImages.Add(image.id, image.GetImageSmall());
                }

                states = conn.Query<Status>("select * from status");
            }
        }
    }
}
