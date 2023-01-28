//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Wages.Core;

namespace DocumentFlow.Entities.Wages;


[Description("Выплата заработной платы")]
public class PayrollPayment : AccountingDocument, IBilling
{
    public PayrollPayment() => billing_range = new(this);

#pragma warning disable IDE1006 // Стили именования
    public DateTime date_operation { get; set; } = DateTime.Now;
    public decimal transaction_amount { get; set; }
    public string? payment_number { get; set; }
    public int payroll_number { get; protected set; }
    public DateTime payroll_date { get; protected set; }
    public int billing_year { get; protected set; }
    public short billing_month { get; protected set; }
    public BillingRange billing_range { get; }
#pragma warning restore IDE1006 // Стили именования
}
