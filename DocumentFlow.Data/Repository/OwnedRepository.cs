//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Interfaces.Repository;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Data.Repository;

public abstract class OwnedRepository<Key, T> : Repository<Key, T>, IOwnedRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    protected OwnedRepository(IDatabase database) : base(database) { }

    public IReadOnlyList<T> GetByOwner(Guid? owner, IFilter? filter = null, Func<Query, Query>? callback = null)
    {
        if (owner == null)
        {
            return GetListUserDefined(filter, callback);
        }

        using var conn = GetConnection();
        Query query = GetQueryOwner(GetUserDefinedQuery(conn, filter), owner.Value);
        if (callback != null)
        {
            query = callback(query);
        }

        return query.Get<T>().ToList();
    }

    public IReadOnlyList<T> GetByOwner(Key? id, Guid? owner, IFilter? filter = null, Func<Query, Query>? callback = null, bool userDefindedQuery = true)
    {
        using var conn = GetConnection();

        if (owner == null || owner.Value == Guid.Empty)
        {
            if (userDefindedQuery)
            {
                return id == null ? GetListUserDefined(filter) : new[] { Get(id.Value) };
            }
            else
            {
                return id == null ? GetList(GetQuery(conn), callback) : new[] { Get(id.Value) };
            }
        }

        Query query = userDefindedQuery ?
            GetQueryOwner(GetUserDefinedQuery(conn, filter), owner.Value) :
            GetQueryOwner(GetQuery(conn), owner.Value);

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

    protected virtual Query GetQueryOwner(Query query, Guid owner_id) => query.Where($"{GetTableName(query)}.owner_id", owner_id);
}
