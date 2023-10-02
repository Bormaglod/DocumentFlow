//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.09.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data;

namespace DocumentFlow.Tools;

public class GridGroupingHelper
{
    public static object DocumentByDay(string _, object obj)
    {
        if (obj is BaseDocument doc && doc.DocumentDate.HasValue)
        {
            return DateOnly.FromDateTime(doc.DocumentDate.Value);
        }

        return DateOnly.MinValue;
    }

    public static object DocumentByMonth(string _, object obj)
    {
        if (obj is BaseDocument doc && doc.DocumentDate.HasValue)
        {
            return DateMonthOnly.FromDateTime(doc.DocumentDate.Value);
        }

        return DateMonthOnly.MinValue;
    }
}
