//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;

namespace DocumentFlow.Data.Interfaces.Filters;

public interface IDateRangeFilter : IFilter
{
    bool DateFromEnabled { get; set; }
    bool DateToEnabled { get; set; }
    DateTime DateFrom { get; set; }
    DateTime DateTo { get; set; }
    void SetDateRange(DateRange range);
}
