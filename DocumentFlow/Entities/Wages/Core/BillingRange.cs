﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//-----------------------------------------------------------------------

using System.Globalization;

namespace DocumentFlow.Entities.Wages.Core;

public class BillingRange : IComparable
{
    private readonly BillingDocument billing;

    public BillingRange(BillingDocument billing) => this.billing = billing;

    public int CompareTo(object? obj)
    {
        if (obj is BillingRange range)
        {
            var compare = billing.billing_year.CompareTo(range.billing.billing_year);
            if (compare == 0)
            {
                compare = billing.billing_month.CompareTo(range.billing.billing_month);
            }

            return compare;
        }

        throw new ArgumentException("Object is not a BillingRange");
    }

    public override string ToString()
    {
        return $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(billing.billing_month)} {billing.billing_year}";
    }
}