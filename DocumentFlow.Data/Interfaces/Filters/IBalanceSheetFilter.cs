//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;

namespace DocumentFlow.Data.Interfaces.Filters;

public interface IBalanceSheetFilter : IDateRangeFilter
{
    BalanceSheetContent Content { get; set; }
    bool AmountVisible { get; set; }
    bool SummaVisible { get; set; }
    bool ShowGivingMaterial { get; set; }
}
