//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Wages.Core;

namespace DocumentFlow.Entities.Wages;


[Description("Выплата заработной платы")]
public class PayrollPayment : AccountingDocument, IBilling
{
    public PayrollPayment() => BillingRange = new(this);
    public DateTime DateOperation { get; set; } = DateTime.Now;
    public decimal TransactionAmount { get; set; }
    public string? PaymentNumber { get; set; }
    public int PayrollNumber { get; protected set; }
    public DateTime PayrollDate { get; protected set; }
    public int BillingYear { get; protected set; }
    public short BillingMonth { get; protected set; }
    public BillingRange BillingRange { get; }
}
