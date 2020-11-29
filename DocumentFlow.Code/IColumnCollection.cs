//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2020
// Time: 21:42
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code
{
    public interface IColumnCollection : IEnumerable, IEnumerable<IColumn>
    {
        IColumn this[int index] { get; }
        IColumn this[string dataField] { get; }
        IColumn CreateText(string dataField, string headerText);
        IColumn CreateInteger(string dataField, string headerText, NumberFormatMode formatMode = NumberFormatMode.Numeric);
        IColumn CreateNumeric(string dataField, string headerText, NumberFormatMode formatMode = NumberFormatMode.Numeric, int decimalDigits = 0);
        IColumn CreateBoolean(string dataField, string headerText);
        IColumn CreateDate(string dataField, string headerText, string format = "dd.MM.yyyy HH:mm:ss");
        IColumn CreateImage(string dataField, string headerText);
        IColumn CreateProgress(string dataField, string headerText);
        ISorted CreateSortedColumns();
        IStackedColumn CreateStackedColumns();
        ISummary CreateTableSummaryRow(GroupVerticalPosition position);
        ISummary CreateGroupSummaryRow();
    }
}
