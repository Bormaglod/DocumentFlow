//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Infrastructure.Data;

using Humanizer;

using SqlKata.Execution;

using System.Data;

namespace DocumentFlow.Data.Core;

public abstract class DirectoryRepository<T> : OwnedRepository<Guid, T>, IDirectoryRepository<T>
    where T : IIdentifier<Guid>
{
    protected DirectoryRepository(IDatabase database) : base(database) { }

    public IReadOnlyList<T> GetByParent(Guid? parent_id, IFilter? filter = null)
    {
        using var conn = Database.OpenConnection();
        var query = GetDefaultQuery(conn, filter);
        var table = GetTableName(query);

        return query
            .When(
                parent_id == null,
                q => q.WhereNull($"{table}.parent_id"),
                q => q.Where($"{table}.parent_id", parent_id)
            )
            .Get<T>()
            .ToList();
    }

    public IReadOnlyList<T> GetOnlyFolders()
    {
        using var conn = Database.OpenConnection();
        return GetBaseQuery(conn)
            .WhereTrue("is_folder")
            .WhereFalse("deleted")
            .OrderBy("item_name")
            .Get<T>()
            .ToList();
    }

    public Guid AddFolder(Guid? parent_id, string code, string folder_name)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            string table = typeof(T).Name.Underscore();
            string sql = $"insert into {table} (is_folder, code, item_name, parent_id) values (true, :code, :item_name, :parent_id) returning id";
            var res = conn.QuerySingle<Guid>(sql, new { code, item_name = folder_name, parent_id }, transaction);
            transaction.Commit();

            return res;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public Guid? GetRoot(Guid object_id)
    {
        string table = typeof(T).Name.Underscore();
        string sql = @$"with recursive r as
                           (
	                            select id, parent_id from {table} where id = :object_id
		                        union
	                            select p.id, p.parent_id from {table} p join r on (r.parent_id = p.id)
                           )
                           select id from r where parent_id is null";

        using var conn = Database.OpenConnection();
        try
        {
            return conn.QuerySingle<Guid?>(sql, new { object_id });
        }
        catch (Exception e)
        {
            ExceptionHelper.MesssageBox(e);
            return null;
        }
    }

    public IReadOnlyList<T> GetParentFolders(Guid object_id)
    {
        string table = typeof(T).Name.Underscore();
        string sql = @$"with recursive r as
                           (
	                            select * from {table} where id = :object_id
		                        union
	                            select p.* from {table} p join r on (r.parent_id = p.id)
                           )
                           select * from r where is_folder";

        using var conn = Database.OpenConnection();
        try
        {
            return conn.Query<T>(sql, new { object_id }).ToList();
        }
        catch (Exception e)
        {
            ExceptionHelper.MesssageBox(e);
            return new List<T>();
        }
    }

    public void WipeParented(Guid? owner, Guid? parent)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            WipeParented(owner, parent, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public void WipeParented(Guid? owner, Guid? parent, IDbTransaction transaction)
    {
        var conn = transaction.Connection;
        if (parent == null)
        {
            WipeAll(owner, transaction);
        }
        else
        {
            string table = typeof(T).Name.Underscore();
            if (owner == null)
            {
                conn.Execute($"delete from {table} where parent_id = :parent_id and deleted", new { parent_id = parent }, transaction: transaction);
            }
            else
            {
                conn.Execute($"delete from {table} where owner_id = :owner_id and parent_id = :parent_id and deleted", new { owner_id = owner, parent_id = parent }, transaction: transaction);
            }
        }
    }
}
