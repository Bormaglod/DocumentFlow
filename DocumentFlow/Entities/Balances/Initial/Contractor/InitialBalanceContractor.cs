//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.06.2022
//
// Версия 2022.12.18
//  - добавлено поле paid
// Версия 2022.12.29
//  - класс теперь public
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Balances.Initial;

[Description("Нач. остаток")]
public class InitialBalanceContractor : InitialBalance
{
    public string? ContractorName { get; protected set; }
    public Guid ContractId { get; set; }
    public string ContractNumber { get; protected set; } = string.Empty;
    public string ContractName { get; protected set; } = string.Empty;
    public DateTime ContractDate { get; protected set; }
    public decimal? OurDebt => Amount > 0 ? OperationSumma : null;
    public decimal? ContractorDebt => Amount < 0 ? OperationSumma : null;
    public decimal Paid { get; protected set; }
}