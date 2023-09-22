//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Поступление")]
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

    [WritableCollection]
    public IList<WaybillReceiptPrice> Prices { get; protected set; } = null!;
}
