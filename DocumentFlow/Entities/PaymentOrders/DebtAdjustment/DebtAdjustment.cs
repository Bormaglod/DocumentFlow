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
    public Guid? ContractorId { get; set; }
    public string? ContractorName { get; protected set; }
    public Guid? DocumentDebtId { get; set; }
    public DateTime? DocumentDebtDate { get; protected set; }
    public int? DocumentDebtNumber { get; protected set; }
    public decimal DocumentDebtAmount { get; protected set; }
    public decimal DocumentDebtPayment { get; protected set; }
    public Guid? DocumentCreditId { get; set; }
    public DateTime? DocumentCreditDate { get; protected set; }
    public int? DocumentCreditNumber { get; protected set; }
    public decimal DocumentCreditAmount { get; protected set; }
    public decimal DocumentCreditPayment { get; protected set; }
    public decimal TransactionAmount { get; set; }
}
