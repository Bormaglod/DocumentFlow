//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.11.2021
//
// Версия 2022.12.6
//  - дабавлены свойства contract_number, contract_date и contract_header
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Balances;

public class BalanceContractor : Balance
{
    public Guid contract_id { get; set; }
    public string? contract_name { get; protected set; }
    public string contract_number { get; protected set; } = string.Empty;
    public DateTime contract_date { get; protected set; }
    public decimal? contractor_debt { get; protected set; }
    public decimal? organization_debt { get; protected set; }
    public decimal debt { get; protected set; }

    public string contract_header => $"{contract_name} {contract_number} от {contract_date:d}";
}
