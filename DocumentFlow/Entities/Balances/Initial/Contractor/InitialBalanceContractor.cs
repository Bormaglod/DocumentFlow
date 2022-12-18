//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.06.2022
//
// Версия 2022.12.18
//  - добавлено поле paid
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Balances.Initial;

[Description("Нач. остаток")]
internal class InitialBalanceContractor : InitialBalance
{
    public string? contractor_name { get; protected set; }
    public Guid contract_id { get; set; }
    public string contract_number { get; protected set; } = string.Empty;
    public string contract_name { get; protected set; } = string.Empty;
    public DateTime contract_date { get; protected set; }
    public decimal? OurDebt => amount > 0 ? operation_summa : null;
    public decimal? ContractorDebt => amount < 0 ? operation_summa : null;
    public decimal paid { get; protected set; }
}