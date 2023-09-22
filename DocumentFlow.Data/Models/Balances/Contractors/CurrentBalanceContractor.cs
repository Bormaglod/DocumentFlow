//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Models;

public class CurrentBalanceContractor : Identifier<Guid>
{
    public string ContractorName { get; protected set; } = string.Empty;
    public decimal Debt { get; protected set; }
}
