//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Models;

public interface IWage1cRepository : IDocumentRepository<Wage1c>
{
    IReadOnlyList<Wage1cEmployee> Get(BillingDocument wage);
    IReadOnlyList<Wage1cEmployee> Get(int year, short month);
}
