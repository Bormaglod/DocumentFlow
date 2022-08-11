//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Balances.Initial;

[Description("Нач. остаток")]
internal class InitialBalanceEmployee : InitialBalance
{
    public string? employee_name { get; protected set; }
    public decimal? OurDebt => amount > 0 ? operation_summa : null;
    public decimal? EmployeeDebt => amount < 0 ? operation_summa : null;
}