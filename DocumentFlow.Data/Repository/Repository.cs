//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Extension;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Tools;

using Humanizer;

using System.Data;

namespace DocumentFlow.Data.Repository;

public abstract class Repository<Key, T> : ReadOnlyRepository<Key, T>, IRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    private readonly IDatabase database;

    protected Repository(IDatabase database) : base(database)
    {
        this.database = database;
    }

    public T Add(T entity)
    {
        using var conn = database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            conn.Insert(entity, transaction);
            if (entity is DocumentInfo entityInfo)
            {
                entityInfo.SaveChanges(conn, transaction);
            }

            transaction.Commit();

            return Get(entity.Id);
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e, database), e);
        }
    }

    public T CopyFrom(T original)
    {
        using var conn = database.OpenConnection();
        using var transaction = conn.BeginTransaction();

        return CopyFrom(conn, original, transaction);
    }

    public T CopyFrom(IDbConnection connection, T original, IDbTransaction? transaction = null)
    {
        try
        {
            if (connection.Copy(original, out var copy, transaction) && copy is T t)
            {
                t = Get(connection, t.Id);
                CopyNestedRows(connection, original, t, transaction);

                transaction?.Commit();

                return t;
            }

            throw new RepositoryException($"Не удалость сделать копию документа с id = {{{original.Id}}}");
        }
        catch (Exception e)
        {
            transaction?.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e, database), e);
        }
    }

    public void Update(T entity)
    {
        using var conn = database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        
        Update(conn, entity, transaction);
    }

    public void Update(IDbConnection connection, T entity, IDbTransaction? transaction = null)
    {
        try
        {
            connection.Update(entity, transaction);
            if (entity is DocumentInfo entityInfo)
            {
                entityInfo.SaveChanges(connection, transaction);
            }

            transaction?.Commit();
        }
        catch (Exception e)
        {
            transaction?.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e, database), e);
        }
    }

    public void Delete(T entity) => ExecuteCommand(SQLCommand.Delete, entity);

    public void Undelete(T entity) => ExecuteCommand(SQLCommand.Undelete, entity);

    public void Wipe(T entity) => ExecuteCommand(SQLCommand.Wipe, entity);

    public void WipeAll() => ExecuteCommand($"delete from {GetTableName()} where deleted");

    public void WipeAll(IDbConnection connection, IDbTransaction? transaction = null)
    {
        connection.Execute($"delete from {GetTableName()} where deleted", null, transaction: transaction);
    }

    public void WipeAll(Guid owner)
    {
        var sql = $"delete from {GetTableName()} where owner_id = :owner_id and deleted";
        var param = new { owner_id = owner };
        ExecuteCommand(sql, param);
    }

    protected virtual void CopyNestedRows(IDbConnection connection, T from, T to, IDbTransaction? transaction = null) { }

    protected void ApplySystemOperation(Key id, SystemOperation operation, string? table = null) => ExecuteSystemOperation(id, operation, true, table);

    protected int ExecuteSystemOperation(Key id, SystemOperation operation, bool value, string? table = null)
    {
        using var conn = database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            var res = conn.Execute(
                $"call execute_system_operation(:id, '{operation.ToString().Underscore()}'::system_operation, {value}, '{table ?? GetTableName()}')",
                new { id },
                transaction);
            transaction.Commit();

            return res;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e, database), e);
        }
    }

    private void ExecuteCommand(SQLCommand command, T entity)
    {
        using var conn = database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            conn.ExecuteCommand(command, entity, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e, database), e);
        }
    }

    private void ExecuteCommand(string sql, object? param = null)
    {
        using var conn = database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, param, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e, database), e);
        }
    }
}
