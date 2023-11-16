//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.11.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Models;

public class BalanceContractor : Balance
{
    private Guid contractId;

    /// <summary>
    /// Возвращает или устанавливает идентификатор договора для которого формируется данный остаток.
    /// </summary>
    public Guid ContractId 
    { 
        get => contractId;
        set => SetProperty(ref contractId, value);
    }

    public string? ContractName { get; protected set; }
    public string ContractNumber { get; protected set; } = string.Empty;
    public DateTime ContractDate { get; protected set; }
    public decimal? ContractorDebt { get; protected set; }
    public decimal? OrganizationDebt { get; protected set; }
    public decimal Debt { get; protected set; }

    public string ContractHeader => $"{ContractName} {ContractNumber} от {ContractDate:d}";
}
