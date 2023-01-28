//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//
// Версия 2023.1.28
//  - BillingDocument заменен на IBilling
//  - добавлено условие в метод ToString для предотвращения появления
//    ошибки при создании нового объекта IBilling у которого отсутствует
//    инициализация свойств billing_year и billing_month
//
//-----------------------------------------------------------------------

using System.Globalization;

namespace DocumentFlow.Entities.Wages.Core;

public class BillingRange : IComparable
{
    private readonly IBilling billing;

    public BillingRange(IBilling billing) => this.billing = billing;

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
        if (billing.billing_year == default || billing.billing_month == default)
        {
            return "Не установлено";
        }

        return $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(billing.billing_month)} {billing.billing_year}";
    }
}
