//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Filters;
using SqlKata;

namespace DocumentFlow.Data.Interfaces.Repository;

public interface IOwnedRepository<Key, T> : IRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    IReadOnlyList<T> GetByOwner(Guid? owner, IFilter? filter = null, Func<Query, Query>? callback = null);
    IReadOnlyList<T> GetByOwner(Key? id, Guid? owner, IFilter? filter = null, Func<Query, Query>? callback = null, bool userDefindedQuery = true);
}
