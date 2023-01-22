//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.09.2020
//
// Версия 2022.12.3
//  - добавлено перечисление SelectOptions
//  - параметр includeDeleted в методах заменён на options
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Controls.Infrastructure в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Infrastructure.Controls;

public enum SummaryColumnFormat { None, Currency }

[Flags]
public enum SelectOptions
{
    None = 0,
    IncludeDeleted = 1,
    IncludeNotAccepted = 2,
    All = 3
}

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
