//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.06.2022
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Data.Infrastructure в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Data;

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
