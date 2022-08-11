//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.11.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Balances;

public class BalanceContractor : Balance
{
    public Guid contract_id { get; set; }
    public string? contract_name { get; protected set; }
    public decimal? contractor_debt { get; protected set; }
    public decimal? organization_debt { get; protected set; }
    public decimal debt { get; protected set; }
}
