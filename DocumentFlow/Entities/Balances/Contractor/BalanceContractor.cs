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
    public Guid ContractId { get; set; }
    public string? ContractName { get; protected set; }
    public string ContractNumber { get; protected set; } = string.Empty;
    public DateTime ContractDate { get; protected set; }
    public decimal? ContractorDebt { get; protected set; }
    public decimal? OrganizationDebt { get; protected set; }
    public decimal Debt { get; protected set; }

    public string ContractHeader => $"{ContractName} {ContractNumber} от {ContractDate:d}";
}
