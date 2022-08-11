//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//-----------------------------------------------------------------------

using SqlKata;

namespace DocumentFlow.Data.Infrastructure;

public interface IOwnedRepository<Key, T> : IRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    IReadOnlyList<T> GetByOwner(Guid? owner_id, IFilter? filter = null, Func<Query, Query>? callback = null);
    IReadOnlyList<T> GetByOwner(Key? id, Guid? owner_id, IFilter? filter = null, Func<Query, Query>? callback = null, bool useBaseQuery = false);
}
