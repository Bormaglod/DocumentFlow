﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Data.Core;

public abstract class OwnedRepository<Key, T> : Repository<Key, T>, IOwnedRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    protected OwnedRepository(IDatabase database) : base(database) { }

    public IReadOnlyList<T> GetByOwner(Guid? owner_id, IFilter? filter = null, Func<Query, Query>? callback = null)
    {
        if (owner_id == null)
        {
            return GetAllDefault(filter, callback);
        }

        using var conn = Database.OpenConnection();
        Query query = GetQueryOwner(GetDefaultQuery(conn, filter), owner_id.Value);
        if (callback != null)
        {
            query = callback(query);
        }

        var res1 = query.Get<T>();
        var res2 = res1.ToList();

        return res2;

    }

    public IReadOnlyList<T> GetByOwner(Key? id, Guid? owner_id, IFilter? filter = null, Func<Query, Query>? callback = null, bool useBaseQuery = false)
    {
        using var conn = Database.OpenConnection();

        if (owner_id == null)
        {
            if (useBaseQuery)
            {
                return id == null ? Get(GetBaseQuery(conn), callback) : new[] { GetById(id.Value) };
            }
            else
            {
                return id == null ? GetAllDefault(filter) : new[] { GetById(id.Value) };
            }
        }

        Query query = useBaseQuery ?
            GetQueryOwner(GetBaseQuery(conn), owner_id.Value) :
            GetQueryOwner(GetDefaultQuery(conn, filter), owner_id.Value);

        return GetList(query, id, callback);
    }

    protected virtual Query GetQueryOwner(Query query, Guid owner_id) => query.Where($"{GetTableName(query)}.owner_id", owner_id);

    private IReadOnlyList<T> GetList(Query query, Key? id, Func<Query, Query>? callback = null)
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