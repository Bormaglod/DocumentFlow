//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.2.6
//  - метод GetList стал статическим
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data.Repositiry
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Data.Repositiry;

public abstract class OwnedRepository<Key, T> : Repository<Key, T>, IOwnedRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    protected OwnedRepository(IDatabase database) : base(database) { }

    public IReadOnlyList<T> GetByOwner(Guid? owner_id, IFilter? filter = null, Func<Query, Query>? callback = null)
    {
        if (owner_id == null)
        {
            return GetListUserDefined(filter, callback);
        }

        using var conn = Database.OpenConnection();
        Query query = GetQueryOwner(GetUserDefinedQuery(conn, filter), owner_id.Value);
        if (callback != null)
        {
            query = callback(query);
        }

        return query.Get<T>().ToList();
    }

    public IReadOnlyList<T> GetByOwner(Key? id, Guid? owner_id, IFilter? filter = null, Func<Query, Query>? callback = null, bool useBaseQuery = false)
    {
        using var conn = Database.OpenConnection();

        if (owner_id == null)
        {
            if (useBaseQuery)
            {
                return id == null ? GetList(GetQuery(conn), callback) : new[] { Get(id.Value) };
            }
            else
            {
                return id == null ? GetListUserDefined(filter) : new[] { Get(id.Value) };
            }
        }

        Query query = useBaseQuery ?
            GetQueryOwner(GetQuery(conn), owner_id.Value) :
            GetQueryOwner(GetUserDefinedQuery(conn, filter), owner_id.Value);

        return OwnedRepository<Key, T>.GetList(query, id, callback);
    }

    protected virtual Query GetQueryOwner(Query query, Guid owner_id) => query.Where($"{GetTableName(query)}.owner_id", owner_id);

    private static IReadOnlyList<T> GetList(Query query, Key? id, Func<Query, Query>? callback = null)
    {
        if (callback != null)
        {
            query = callback(query);
        }

        if (id != null)
        {
            query = query.OrWhere(q => q.Where($"{GetTableName(query)}.id", id));
        }

        return query
            .Get<T>()
            .ToList();
    }
}
