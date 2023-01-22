//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Data.Infrastructure в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using SqlKata;

namespace DocumentFlow.Infrastructure.Data;

public interface IOwnedRepository<Key, T> : IRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    IReadOnlyList<T> GetByOwner(Guid? owner_id, IFilter? filter = null, Func<Query, Query>? callback = null);
    IReadOnlyList<T> GetByOwner(Key? id, Guid? owner_id, IFilter? filter = null, Func<Query, Query>? callback = null, bool useBaseQuery = false);
}
