//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.09.2020
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Enums;

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Controls.Interfaces;

public interface ISummary
{
    ISummary AsCount(GridColumn column, string format, SelectOptions options = SelectOptions.None);
    ISummary AsCount(string columnName, string format, SelectOptions options = SelectOptions.None);
    ISummary AsCount(GridColumn column, SummaryColumnFormat format = SummaryColumnFormat.None, SelectOptions options = SelectOptions.None);
    ISummary AsCount(string columnName, SummaryColumnFormat format = SummaryColumnFormat.None, SelectOptions options = SelectOptions.None);
    ISummary AsSummary(GridColumn column, string format, SelectOptions options = SelectOptions.None);
    ISummary AsSummary(string columnName, string format, SelectOptions options = SelectOptions.None);
    ISummary AsSummary(GridColumn column, SummaryColumnFormat format = SummaryColumnFormat.None, SelectOptions options = SelectOptions.None);
    ISummary AsSummary(string columnName, SummaryColumnFormat format = SummaryColumnFormat.None, SelectOptions options = SelectOptions.None);
}
