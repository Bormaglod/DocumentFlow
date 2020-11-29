//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.09.2020
// Time: 17:44
//-----------------------------------------------------------------------

using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using DocumentFlow.Code.System;
using DocumentFlow.Core;

namespace DocumentFlow.Code.Implementation
{
    public class SummaryRowData : ISummary
    {
        private GridSummaryRow summaryRow;

        public SummaryRowData(GridSummaryRow row)
        {
            summaryRow = row;
        }

        ISummary ISummary.AddColumn(string fieldName, RowSummaryType type, string format)
        {
            GridSummaryColumn summaryColumn = new GridSummaryColumn
            {
                SummaryType = EnumHelper.TransformEnum<SummaryType, RowSummaryType>(type),
                Format = format,
                MappingName = fieldName,
                Name = fieldName
            };

            summaryRow.SummaryColumns.Add(summaryColumn);
            return this;
        }
    }
}
