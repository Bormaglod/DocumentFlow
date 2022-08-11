//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.06.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Infrastructure;

public enum DateRange
{
    CurrentDay,
    CurrentMonth,
    CurrentQuarter,
    CurrentYear
}

public interface IDateRangeFilter : IFilter
{
    bool DateFromEnabled { get; set; }
    bool DateToEnabled { get; set; }
    DateTime? DateFrom { get; set; }
    DateTime? DateTo { get; set; }
    void SetDateRange(DateRange range);
}
