//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.11.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Balances;

public class BalanceProduct : Balance
{
    public decimal? income { get; protected set; }
    public decimal? expense { get; protected set; }
    public decimal remainder { get; protected set; }
    public decimal monetary_balance { get; protected set; }
}
