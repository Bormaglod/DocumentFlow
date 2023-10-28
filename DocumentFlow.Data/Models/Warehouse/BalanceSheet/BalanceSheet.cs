//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Data.Models;

public class BalanceSheet : Identifier<Guid>, IBucketInfo
{
    public string ProductName { get; protected set; } = string.Empty;
    public string ProductCode { get; protected set; } = string.Empty;
    public string GroupName { get; protected set; } = string.Empty;
    public decimal OpeningBalanceAmount { get; protected set; }
    public decimal OpeningBalanceSumma { get; protected set; }
    public decimal IncomeAmount { get; protected set; }
    public decimal IncomeSumma { get; protected set; }
    public decimal ExpenseAmount { get; protected set; }
    public decimal ExpenseSumma { get; protected set; }
    public decimal ClosingBalanceAmount { get; protected set; }
    public decimal ClosingBalanceSumma { get; protected set; }
    public string BucketName { get; protected set; } = string.Empty;
}
