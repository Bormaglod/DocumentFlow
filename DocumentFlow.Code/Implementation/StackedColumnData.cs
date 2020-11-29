//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2020
// Time: 10:09
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Code.Implementation
{
    public class StackedColumnData : IStackedColumn
    {
        private List<string> fields = new List<string>();
        private SfDataGrid gridContent;

        public StackedColumnData(SfDataGrid grid)
        {
            gridContent = grid;
        }

        IStackedColumn IStackedColumn.Add(string dataField)
        {
            fields.Add(dataField);
            return this;
        }

        IStackedColumn IStackedColumn.Add(IColumn column)
        {
            fields.Add(column.FieldName);
            return this;
        }

        void IStackedColumn.Header(string header)
        {
            StackedHeaderRow stackedHeader;
            if (gridContent.StackedHeaderRows.Count == 0)
            {
                stackedHeader = new StackedHeaderRow();
                gridContent.StackedHeaderRows.Add(stackedHeader);
            }
            else
            {
                stackedHeader = gridContent.StackedHeaderRows[0];
            }

            stackedHeader.StackedColumns.Add(new StackedColumn() { ChildColumns = string.Join(",", fields), HeaderText = header });
        }
    }
}
