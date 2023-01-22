//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.11.2020
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Controls.Core;

public class GroupColumnCollection : IGroupColumnCollection
{
    private readonly SfDataGrid grid;

    public GroupColumnCollection(SfDataGrid grid) => this.grid = grid;

    public IGroupColumnCollection Add(GridColumn column)
    {
        if (grid.AllowGrouping)
        {
            var groupColumn = new GroupColumnDescription()
            {
                ColumnName = column.MappingName,
                SortGroupRecords = false
            };

            grid.GroupColumnDescriptions.Add(groupColumn);
        }

        return this;
    }
}
