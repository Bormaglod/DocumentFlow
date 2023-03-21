//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.PaymentOrders;


[Description("Корректировка долга")]
public class DebtAdjustment : AccountingDocument
{
    public Guid? contractor_id { get; set; }
    public string? contractor_name { get; protected set; }
    public Guid? document_debt_id { get; set; }
    public DateTime? document_debt_date { get; protected set; }
    public int? document_debt_number { get; protected set; }
    public decimal document_debt_amount { get; protected set; }
    public decimal document_debt_payment { get; protected set; }
    public Guid? document_credit_id { get; set; }
    public DateTime? document_credit_date { get; protected set; }
    public int? document_credit_number { get; protected set; }
    public decimal document_credit_amount { get; protected set; }
    public decimal document_credit_payment { get; protected set; }
    public decimal transaction_amount { get; set; }
}
