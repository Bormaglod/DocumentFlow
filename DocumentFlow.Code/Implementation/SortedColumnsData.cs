//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.10.2020
// Time: 19:23
//-----------------------------------------------------------------------

using System.ComponentModel;
using Syncfusion.WinForms.DataGrid;
using DocumentFlow.Code.System;
using DocumentFlow.Core;

namespace DocumentFlow.Code.Implementation
{
    public class SortedColumnsData : ISorted
    {
        private SfDataGrid gridContent;

        public SortedColumnsData(SfDataGrid grid)
        {
            gridContent = grid;
            gridContent.SortColumnDescriptions.Clear();
        }

        ISorted ISorted.Add(string columnName, SortDirection direction)
        {
            SortColumnDescription sort = new SortColumnDescription()
            {
                ColumnName = columnName,
                SortDirection = EnumHelper.TransformEnum<ListSortDirection, SortDirection>(direction)
            };

            gridContent.SortColumnDescriptions.Add(sort);

            return this;
        }
    }
}
