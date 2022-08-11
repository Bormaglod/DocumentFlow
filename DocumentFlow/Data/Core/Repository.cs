﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Core.Exceptions;
using DocumentFlow.Core.Reflection;
using DocumentFlow.Data.Infrastructure;

using Humanizer;

using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;

using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace DocumentFlow.Data.Core;

public enum SystemOperation { Accept, Delete, DeleteChilds }

public abstract class Repository<Key, T> : IRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    private readonly List<string> add = new();
    private readonly List<string> update = new();
    private readonly List<string> copy = new();
    private readonly Dictionary<string, string> enums = new();

    private enum Function { Get, Other }

    protected Repository(IDatabase database)
    {
        Database = database;

        Type t = typeof(T);
        foreach (var prop in t.GetProperties().Where(p => p.SetMethod?.IsPublic ?? false))
        {
            var exclude = prop.GetCustomAttribute<ExcludeAttribute>();
            if (exclude != null)
            {
                continue;
            }

            var operation = prop.GetCustomAttribute<DataOperationAttribute>();
            if (operation == null)
            {
                add.Add(prop.Name);
                update.Add(prop.Name);
                copy.Add(prop.Name);
            }
            else
            {
                if (operation.Operation.HasFlag(DataOperation.Add))
                {
                    add.Add(prop.Name);
                }

                if (operation.Operation.HasFlag(DataOperation.Update))
                {
                    update.Add(prop.Name);
                }

                if (operation.Operation.HasFlag(DataOperation.Copy))
                {
                    copy.Add(prop.Name);
                }
            }

            var enumAttr = prop.GetCustomAttribute<EnumTypeAttribute>();
            if (enumAttr != null)
            {
                enums.Add(prop.Name, enumAttr.Name);
            }
        }
    }

    protected IDatabase Database { get; }

    public T GetById(Key id, bool fullInformation = true)
    {
        using var conn = Database.OpenConnection();
        return GetById(id, conn, fullInformation);
    }

    public T GetById(Key id, IDbConnection connection, bool fullInformation = true)
    {
        Query query = fullInformation ? GetDefaultQuery(connection) : GetBaseQuery(connection);

        T res = query.Where(q => q.Where($"{GetTableName(query)}.id", id)).FirstOrDefault<T>();
        if (res != null)
        {
            return res;
        }

        throw new RecordNotFoundException(id);
    }

    public T Get(Func<Query, Query>? callback = null)
    {
        using var conn = Database.OpenConnection();

        var query = GetBaseQuery(conn);
        if (callback != null)
        {
            query = callback(query);
        }

        return query.First<T>();
    }

    public IReadOnlyList<T> GetAll(Func<Query, Query>? callback = null)
    {
        using var conn = Database.OpenConnection();
        return Repository<Key, T>.Get(GetBaseQuery(conn), callback);
    }

    public IReadOnlyList<T> GetAllDefault(IFilter? filter = null, Func<Query, Query>? callback = null)
    {
        using var conn = Database.OpenConnection();
        return Repository<Key, T>.Get(GetQueryFilters(GetDefaultQuery(conn, filter), filter), callback);
    }

    public IReadOnlyList<T> GetAllValid(Func<Query, Query>? callback = null)
    {
        using var conn = Database.OpenConnection();

        var query = GetBaseQuery(conn);

        return Repository<Key, T>.Get(query.WhereFalse($"{GetTableName(query)}.deleted"), callback);
    }

    public T Add()
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            T entity = Add(transaction);
            transaction.Commit();
            return entity;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public T Add(IDbTransaction transaction)
    {
        var sql = $"insert into {Repository<Key, T>.GetTableName()} default values returning id";

        var conn = transaction.Connection;
        Key id = conn.QuerySingle<Key>(sql, transaction: transaction);
        return GetById(id);
    }

    public T Add(T entity)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            T res = Add(entity, transaction);
            transaction.Commit();
            return res;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public T Add(T entity, IDbTransaction transaction)
    {
        var sql = $"insert into {GetTableName(entity, Function.Other)} ({GetOperationFields(DataOperation.Add)}) values ({GetOperationValues(DataOperation.Add)}) returning id";

        var conn = transaction.Connection;
        if (conn != null)
        {
            Key id = conn.QuerySingle<Key>(sql, entity, transaction: transaction);

            var query = GetDefaultQuery(GetBaseQuery(conn));
            query = query.Where($"{GetTableName(query, entity, Function.Get)}.id", id);

            var factory = new QueryFactory(conn, new PostgresCompiler());
            var result = factory.Compiler.Compile(query);
            return conn.QuerySingle<T>(result.ToString(), transaction: transaction);
        }

        throw new NullReferenceException(nameof(conn));
    }

    public T CopyFrom(T original)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            T res = CopyFrom(original, transaction);
            transaction.Commit();
            return res;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public T CopyFrom(T original, IDbTransaction transaction)
    {
        var sql = $"insert into {GetTableName(original, Function.Other)} ({GetOperationFields(DataOperation.Copy)}) values ({GetOperationValues(DataOperation.Copy)}) returning id";

        var conn = transaction.Connection;
        if (conn != null)
        {
            Key id = conn.QuerySingle<Key>(sql, original, transaction: transaction);

            var query = GetDefaultQuery(GetBaseQuery(conn));
            query = query.Where($"{GetTableName(query, original, Function.Get)}.id", id);

            var factory = new QueryFactory(conn, new PostgresCompiler());
            var result = factory.Compiler.Compile(query);
            T copy = conn.QuerySingle<T>(result.ToString(), transaction: transaction);

            CopyChilds(original, copy, transaction);

            return copy;
        }

        throw new NullReferenceException(nameof(conn));
    }

    public void Update(T entity)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            Update(entity, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public void Update(T entity, IDbTransaction transaction)
    {
        var sql = $"update {GetTableName(entity, Function.Other)} set {GetOperationValues(DataOperation.Update)} where id = :id";

        var conn = transaction.Connection;
        conn.Execute(sql, entity, transaction: transaction);
    }

    public void Delete(Key id)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            Delete(id, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public void Delete(Key id, IDbTransaction transaction)
    {
        var conn = transaction.Connection;
        conn.Execute($"update {Repository<Key, T>.GetTableName()} set deleted = true where id = :id", new { id }, transaction);
    }

    public void Delete(T entity) => Delete(entity.id);

    public void Delete(T entity, IDbTransaction transaction) => Delete(entity.id, transaction);

    public void Undelete(Key id)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            Undelete(id, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public void Undelete(Key id, IDbTransaction transaction)
    {
        var conn = transaction.Connection;
        conn.Execute($"update {Repository<Key, T>.GetTableName()} set deleted = false where id = :id", new { id }, transaction);
    }

    public void Undelete(T entity) => Undelete(entity.id);

    public void Undelete(T entity, IDbTransaction transaction) => Undelete(entity.id, transaction);

    public void Wipe(Key id)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            Wipe(id, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public void Wipe(Key id, IDbTransaction transaction)
    {
        var conn = transaction.Connection;
        conn.Execute($"delete from {Repository<Key, T>.GetTableName()} where id = :id", new { id }, transaction);
    }

    public void Wipe(T entity) => Wipe(entity.id);

    public void Wipe(T entity, IDbTransaction transaction) => Wipe(entity.id, transaction);

    public void WipeAll()
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            WipeAll(transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public void WipeAll(IDbTransaction transaction)
    {
        var conn = transaction.Connection;
        conn.Execute($"delete from {Repository<Key, T>.GetTableName()} where deleted", null, transaction: transaction);
    }

    public void WipeAll(Guid? owner)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            WipeAll(owner, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public void WipeAll(Guid? owner, IDbTransaction transaction)
    {
        var conn = transaction.Connection;
        if (owner == null)
        {
            WipeAll(transaction);
        }
        else
        {
            conn.Execute($"delete from {Repository<Key, T>.GetTableName()} where owner_id = :owner_id and deleted", new { owner_id = owner }, transaction: transaction);
        }
    }

    public bool HasPrivilege(params Privilege[] privilege) => HasPrivilege(Repository<Key, T>.GetTableName(), privilege);

    public bool HasPrivilege(string table, params Privilege[] privilege)
    {
        using var conn = Database.OpenConnection();
        if (privilege.Length == 0)
        {
            return false;
        }

        var p = string.Join(',', privilege);

        return conn.ExecuteScalar<bool>("select has_table_privilege(:user, :table, :privilege)", new
        {
            user = Database.CurrentUser,
            table,
            privilege = p
        });
    }

    protected virtual void CopyChilds(T from, T to, IDbTransaction transaction) { }

    protected string GetOperationFields(DataOperation operation)
    {
        IEnumerable<string> list = operation switch
        {
            DataOperation.Add => add,
            DataOperation.Update => update,
            DataOperation.Copy => copy,
            _ => throw new NotImplementedException()
        };

        return string.Join(",", list);
    }

    protected string GetOperationValues(DataOperation operation)
    {
        IEnumerable<string> list = operation switch
        {
            DataOperation.Add => add,
            DataOperation.Update => update,
            DataOperation.Copy => copy,
            _ => throw new NotImplementedException()
        };

        if (operation == DataOperation.Update)
        {
            return string.Join(',', list.Select(x => $"{x} = :{x}{(enums.ContainsKey(x) ? "::" + enums[x] : "")}"));
        }
        else
        {
            return string.Join(',', list.Select(x => $":{x}{(enums.ContainsKey(x) ? "::" + enums[x] : "")}"));
        }
    }

    protected int ExecuteSystemOperation(Key id, SystemOperation operation, bool value, IDbTransaction transaction)
    {
        return transaction.Connection.Execute($"call execute_system_operation(:id, '{operation.ToString().Underscore()}'::system_operation, {value}, '{Repository<Key, T>.GetTableName()}')", new { id }, transaction);
    }

    protected void ExcludeField(Expression<Func<T, object?>> memberExpression)
    {
        var member = ReflectionHelper.GetMember(memberExpression);
        if (member != null)
        {
            add.Remove(member.Name);
            copy.Remove(member.Name);
            update.Remove(member.Name);
        }
    }

    protected void ExcludeField(Expression<Func<T, object?>> memberExpression, DataOperation operation)
    {
        var member = ReflectionHelper.GetMember(memberExpression);
        if (member != null)
        {
            IList<string> list = operation switch
            {
                DataOperation.Add => add,
                DataOperation.Update => update,
                DataOperation.Copy => copy,
                _ => throw new NotImplementedException()
            };

            list.Remove(member.Name);
        }
    }

    protected virtual Query GetDefaultQuery(Query query, IFilter? filter = null) => query;

    protected Query GetBaseQuery(IDbConnection conn, string? alias = null)
    {
        var factory = new QueryFactory(conn, new PostgresCompiler());
        return factory.Query(Repository<Key, T>.GetTableName() + (alias == null ?
            string.Empty :
            $" as {alias}"));
    }

    protected Query GetDefaultQuery(IDbConnection conn, IFilter? filter = null) =>
        GetQueryFilters(GetDefaultQuery(GetBaseQuery(conn), filter), filter);

    protected string GetTableName(Query query) => query.Clauses.OfType<FromClause>().FirstOrDefault()?.Alias ?? Repository<Key, T>.GetTableName();

    protected static IReadOnlyList<T> Get(Query query, Func<Query, Query>? callback = null)
    {
        if (callback != null)
        {
            query = callback(query);
        }

        return query.Get<T>().ToList();
    }

    private string GetTableName(Query? query, T? entity, Function func)
    {
        var table = query == null ? Repository<Key, T>.GetTableName() : GetTableName(query);
        if (entity is IDiscriminator discriminator)
        {
            if (entity == null)
            {
                throw new RepositoryException($"Таблица {table} является родительской для других таблиц и в неё нельзя добавить новую запись.");
            }
            else
            {
                if (discriminator.UseGetId || func != Function.Get)
                {
                    return $"{table}_{discriminator.TableName}";
                }
            }
        }

        return table;
    }

    private string GetTableName(T? entity, Function func) => GetTableName(null, entity, func);

    private static string GetTableName() => typeof(T).Name.Underscore();

    private static Query GetQueryFilters(Query query, IFilter? filter = null)
    {
        if (filter != null)
        {
            var queryFilter = filter.CreateQuery<T>();
            if (queryFilter != null)
            {
                query = query.Where(q => queryFilter);
            }
        }

        return query;
    }
}
