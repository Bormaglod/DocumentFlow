//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Warehouse;

public class BalanceSheet : Identifier<Guid>
{
    public string product_name { get; protected set; } = string.Empty;
    public string group_name { get; protected set; } = string.Empty;
    public decimal opening_balance_amount { get; protected set; }
    public decimal opening_balance_summa { get; protected set; }
    public decimal income_amount { get; protected set; }
    public decimal income_summa { get; protected set; }
    public decimal expense_amount { get; protected set; }
    public decimal expense_summa { get; protected set; }
    public decimal closing_balance_amount { get; protected set; }
    public decimal closing_balance_summa { get; protected set; }
}
