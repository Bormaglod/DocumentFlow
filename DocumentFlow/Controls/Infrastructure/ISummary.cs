//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.09.2020
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Controls.Infrastructure;

public enum SummaryColumnFormat { None, Currency }

public interface ISummary
{
    ISummary AsCount(GridColumn column, string format, bool includeDeleted = false);
    ISummary AsCount(string columnName, string format, bool includeDeleted = false);
    ISummary AsCount(GridColumn column, SummaryColumnFormat format = SummaryColumnFormat.None, bool includeDeleted = false);
    ISummary AsCount(string columnName, SummaryColumnFormat format = SummaryColumnFormat.None, bool includeDeleted = false);
    ISummary AsSummary(GridColumn column, string format, bool includeDeleted = false);
    ISummary AsSummary(string columnName, string format, bool includeDeleted = false);
    ISummary AsSummary(GridColumn column, SummaryColumnFormat format = SummaryColumnFormat.None, bool includeDeleted = false);
    ISummary AsSummary(string columnName, SummaryColumnFormat format = SummaryColumnFormat.None, bool includeDeleted = false);
}
