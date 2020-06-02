//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.03.2018
// Time: 22:09
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Renderers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using NHibernate;
    using Syncfusion.Data;
    using Syncfusion.WinForms.DataGrid;
    using Syncfusion.WinForms.DataGrid.Renderers;
    using Syncfusion.WinForms.DataGrid.Styles;
    using Syncfusion.WinForms.GridCommon.ScrollAxis;
    using DocumentFlow.Data.Entities;

    public class CustomRowHeaderCellRenderer : GridRowHeaderCellRenderer
    {
        private SfDataGrid grid;
        private Dictionary<Guid, Image> stateImages;
        private Func<ISession> currentSesson;
        private IList<Status> states;
        private string statusField;

        public CustomRowHeaderCellRenderer(Func<ISession> session, SfDataGrid dataGrid, string statusFieldName)
        {
            grid = dataGrid;
            stateImages = new Dictionary<Guid, Image>();
            currentSesson = session;
            statusField = statusFieldName;
            RefreshStateImages();
        }

        protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style, DataColumnBase column, RowColumnIndex rowColumnIndex)
        {
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

                DataRowView rowView = rowData.Data as DataRowView;
                int statusValue = Convert.ToInt32(rowView[statusField]);
                Status status = states.Where(x => x.Id == statusValue).FirstOrDefault();

                if (status?.Picture != null)
                {
                    Image image = stateImages[status.Picture.Id];
                    int x = (cellRect.Width - image.Width) / 2 - 1;
                    int y = (cellRect.Height - image.Height) / 2 - 1;
                    paint.DrawImage(image, new Rectangle(x + cellRect.X, y + cellRect.Y, image.Width, image.Height));
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

            Guid root = currentSesson().QueryOver<Picture>()
                .Where(x => x.Code == "state")
                .Select(x => x.Id)
                .SingleOrDefault<Guid>();

            IList<Picture> pictures = currentSesson().QueryOver<Picture>()
                .Where(x => x.ParentId == root)
                .List();

            foreach (Picture image in pictures)
            {
                stateImages.Add(image.Id, image.GetImageSmall());
            }

            states = currentSesson().QueryOver<Status>().List();
        }
    }
}
