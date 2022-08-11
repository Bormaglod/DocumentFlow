﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Balances;

public class BalanceEmployee : Balance
{
    public decimal? employee_debt { get; protected set; }
    public decimal? organization_debt { get; protected set; }
    public decimal debt { get; protected set; }
}
