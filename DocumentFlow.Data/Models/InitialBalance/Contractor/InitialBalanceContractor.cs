//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Нач. остаток")]
public class InitialBalanceContractor : InitialBalance
{
    private Guid contractId;


    public InitialBalanceContractor() => Amount = -1;

    public Guid ContractId 
    { 
        get => contractId;
        set => SetProperty(ref contractId, value);
    }

    public string? ContractorName { get; protected set; }
    public string ContractNumber { get; protected set; } = string.Empty;
    public string ContractName { get; protected set; } = string.Empty;
    public DateTime ContractDate { get; protected set; }
    public decimal? OurDebt => Amount > 0 ? OperationSumma : null;
    public decimal? ContractorDebt => Amount < 0 ? OperationSumma : null;
    public decimal Paid { get; protected set; }

    [Write(false)]
    public DebtType DebtType
    {
        get
        {
            return Amount switch
            {
                < 0 => DebtType.Organization,
                > 0 => DebtType.Contractor,
                _ => throw new InvalidOperationException()
            };
        }

        set
        {
            Amount = value switch
            {
                DebtType.Organization => -1,
                DebtType.Contractor => 1,
                _ => throw new InvalidOperationException()
            };
        }
    }
}