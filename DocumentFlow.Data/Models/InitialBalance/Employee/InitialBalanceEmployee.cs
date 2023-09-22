//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Нач. остаток")]
public class InitialBalanceEmployee : InitialBalance
{
    public InitialBalanceEmployee() => Amount = -1;

    public string? EmployeeName { get; protected set; }
    public decimal? OurDebt => Amount > 0 ? OperationSumma : null;
    public decimal? EmployeeDebt => Amount < 0 ? OperationSumma : null;

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