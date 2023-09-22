//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Interfaces.Repository;

using Humanizer;

using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;

using System.Data;

namespace DocumentFlow.Data.Repository;

public abstract class ReadOnlyRepository<Key, T> : IReadOnlyRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    private readonly IDatabase database;

    protected ReadOnlyRepository(IDatabase database)
    {
        this.database = database;
    }

    protected IDatabase CurrentDatabase => database;

    public T Get(Key id, bool userDefindedQuery = true, bool ignoreAdjustedDapper = false)
    {
        using var conn = database.OpenConnection();
        return Get(conn, id, userDefindedQuery, ignoreAdjustedDapper);
    }

    public T Get(IDbConnection connection, Key id, bool userDefindedQuery = true, bool ignoreAdjustedDapper = false)
    {
        T res;

        if (CreatingAdjustedQuery() && !ignoreAdjustedDapper)
        {
            res = GetAdjustedQuery(id, connection);
        }
        else
        {
            Query query = (userDefindedQuery ? GetUserDefinedQuery(connection) : GetQuery(connection));
            res = query.Where(q => q.Where($"{GetTableName(query)}.id", id)).FirstOrDefault<T>();
        }
        if (res == null)
        {
            throw new RecordNotFoundException(id);
        }

        return res;
    }

    public T Get(Func<Query, Query>? callback = null)
    {
        using var conn = database.OpenConnection();

        var query = GetQuery(conn);
        if (callback != null)
        {
            query = callback(query);
        }

        return query.First<T>();
    }

    public IReadOnlyList<T> GetList(Func<Query, Query>? callback = null)
    {
        using var conn = database.OpenConnection();
        return GetList(GetQuery(conn), callback);
    }

    public IReadOnlyList<T> GetListUserDefined(IFilter? filter = null, Func<Query, Query>? callback = null)
    {
        using var conn = database.OpenConnection();
        return GetList(GetUserDefinedQuery(conn, filter), callback);
    }

    public IReadOnlyList<T> GetListExisting(Func<Query, Query>? callback = null)
    {
        using var conn = database.OpenConnection();

        var query = GetQuery(conn);

        return GetList(query.WhereFalse($"{GetTableName(query)}.deleted"), callback);
    }

    public bool HasPrivilege(params Privilege[] privilege)
    {
        return database.HasPrivilege(GetTableName(), privilege);
    }

    protected IDbConnection GetConnection() => database.OpenConnection();

    protected static string GetTableName() => typeof(T).Name.Underscore();

    protected static string GetTableName(Query query) => query.Clauses.OfType<FromClause>().FirstOrDefault()?.Alias ?? GetTableName();

    protected virtual T GetAdjustedQuery(Key id, IDbConnection connection) => throw new NotImplementedException();

    protected virtual bool CreatingAdjustedQuery() => false;

    protected virtual Query GetUserDefinedQuery(Query query, IFilter? filter = null) => query;

    protected Query GetUserDefinedQuery(IDbConnection conn, IFilter? filter = null)
    {
        var query = ApplyFilters(GetUserDefinedQuery(GetQuery(conn), filter), filter);
        if (database.HasPrivilege("document_refs", Privilege.Select) && typeof(T).IsAssignableTo(typeof(DocumentInfo)))
        {
            string table = GetTableName(query);
            if (query.Clauses.FirstOrDefault(c => c.Component == "select") == null)
            {
                query = query.Select($"{table}.*");
            }

            query = query.SelectRaw($"exists(select 1 from document_refs dr where dr.owner_id = {table}.id) as has_documents");
        }

        return query;
    }

    protected virtual Query GetQuery(Query query) => query;

    protected Query GetQuery(IDbConnection conn, string? alias = null)
    {
        var factory = new QueryFactory(conn, new PostgresCompiler());
        return GetQuery(factory.Query(GetTableName() + (alias == null ?
            string.Empty :
            $" as {alias}")));
    }

    protected static IReadOnlyList<T> GetList(Query query, Func<Query, Query>? callback = null)
    {
        if (callback != null)
        {
            query = callback(query);
        }

        return query
            .Get<T>()
            .ToList();
    }

    private static Query ApplyFilters(Query query, IFilter? filter = null)
    {
        if (filter != null)
        {
            var queryFilter = filter.CreateQuery(GetTableName(query));
            if (queryFilter != null)
            {
                query = query.Where(q => queryFilter);
            }
        }

        return query;
    }
}
