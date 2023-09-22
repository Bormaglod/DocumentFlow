//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

using System.Globalization;

namespace DocumentFlow.Data.Models;

public class BillingRange : IComparable
{
    private readonly IBilling billing;

    public BillingRange(IBilling billing) => this.billing = billing;

    public int CompareTo(object? obj)
    {
        if (obj is BillingRange range)
        {
            var compare = billing.BillingYear.CompareTo(range.billing.BillingYear);
            if (compare == 0)
            {
                compare = billing.BillingMonth.CompareTo(range.billing.BillingMonth);
            }

            return compare;
        }

        throw new ArgumentException("Object is not a BillingRange");
    }

    public override string ToString()
    {
        if (billing.BillingYear == default || billing.BillingMonth == default)
        {
            return "Не установлено";
        }

        return $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(billing.BillingMonth)} {billing.BillingYear}";
    }
}
