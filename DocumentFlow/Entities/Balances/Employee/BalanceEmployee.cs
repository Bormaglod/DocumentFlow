//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Balances;

public class BalanceEmployee : Balance
{
    public decimal? EmployeeDebt { get; protected set; }
    public decimal? OrganizationDebt { get; protected set; }
    public decimal Debt { get; protected set; }
}
