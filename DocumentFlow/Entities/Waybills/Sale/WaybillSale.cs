//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//
// Версия 2023.1.19
//  - добавлено поле payment_exists
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Waybills;

[Description("Реализация")]
public class WaybillSale : Waybill
{
    public bool? payment_exists
    {
        get
        {
            if (paid == 0)
            {
                return false;
            }

            if (paid == full_cost)
            {
                return true;
            }

            return null;
        }
    }
}
