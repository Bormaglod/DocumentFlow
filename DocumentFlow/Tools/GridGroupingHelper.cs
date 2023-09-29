//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;

using Humanizer;

using System.Globalization;

namespace DocumentFlow.Tools;

public class GridGroupingHelper
{
    public static object DocumentByDay(string _, object obj)
    {
        if (obj is BaseDocument doc && doc.DocumentDate.HasValue)
        {
            return doc.DocumentDate.Value.ToShortDateString();
        }

        return "NONE";
    }

    public static object DocumentByMonth(string _, object obj)
    {
        if (obj is BaseDocument doc && doc.DocumentDate.HasValue)
        {
            var date = doc.DocumentDate.Value;
            return $"{CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[date.Month - 1].Titleize()} {date.Year} г.";
        }

        return "NONE";
    }
}
