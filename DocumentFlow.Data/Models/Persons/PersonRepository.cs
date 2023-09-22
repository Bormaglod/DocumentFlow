//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Repository;

namespace DocumentFlow.Data.Models;

public class PersonRepository : Repository<Guid, Person>, IPersonRepository
{
    public PersonRepository(IDatabase database) : base(database) { }
}
