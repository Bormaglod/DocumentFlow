//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//
// Версия 2022.11.26
//  - добавлены поля purchase_request_number и purchase_request_date
// Версия 2022.12.18
//  - добавлено поле payment_exists
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Waybills;

[Description("Поступление")]
public class WaybillReceipt : Waybill
{
    public string? purchase_request_number { get; protected set; }
    public DateTime? purchase_request_date { get; protected set; }
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
