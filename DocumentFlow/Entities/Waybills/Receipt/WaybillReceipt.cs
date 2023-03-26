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
    public string? PurchaseRequestNumber { get; protected set; }
    public DateTime? PurchaseRequestDate { get; protected set; }
    public bool? PaymentExists
    {
        get
        {
            if (Paid == 0)
            {
                return false;
            }

            if (Paid == FullCost)
            {
                return true;
            }

            return null;
        }
    }
}
