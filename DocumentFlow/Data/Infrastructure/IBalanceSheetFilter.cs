//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.07.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Infrastructure;

public enum BalanceSheetContent { Material, Goods }

public interface IBalanceSheetFilter : IDateRangeFilter
{
    BalanceSheetContent Content { get; set; }
    bool AmountVisible { get; set; }
    bool SummaVisible { get; set; }
}
