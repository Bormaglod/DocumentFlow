//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.11.2020
// Time: 18:25
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Code.Implementation
{
    public class GroupColumnCollection : IGroupColumnCollection
    {
        private readonly SfDataGrid grid;

        public GroupColumnCollection(SfDataGrid grid) => this.grid = grid;

        IGroupColumnCollection IGroupColumnCollection.Add(string columnName)
        {
            if (grid.AllowGrouping)
            {
                var groupColumn = new GroupColumnDescription()
                {
                    ColumnName = columnName,
                    SortGroupRecords = false
                };

                grid.GroupColumnDescriptions.Add(groupColumn);
            }

            return this;
        }
    }
}
