//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Tools;

using SqlKata.Execution;

namespace DocumentFlow.Data.Repository;

public abstract class DirectoryRepository<T> : OwnedRepository<Guid, T>, IDirectoryRepository<T>
    where T : IIdentifier<Guid>
{
    protected DirectoryRepository(IDatabase database) : base(database) { }

    public Guid GetRoot(Guid objectId)
    {
        string sql = @$"with recursive r as
                           (
	                            select id, parent_id from {GetTableName()} where id = :object_id
		                        union
	                            select p.id, p.parent_id from {GetTableName()} p join r on (r.parent_id = p.id)
                           )
                           select id from r where parent_id is null";

        using var conn = GetConnection();
        try
        {
            return conn.QuerySingle<Guid>(sql, new { object_id = objectId });
        }
        catch (Exception e)
        {
            throw new RepositoryException(ExceptionHelper.Message(e, CurrentDatabase), e);
        }
    }

    public IReadOnlyList<T> GetByParent(Guid? parent, IFilter? filter = null)
    {
        using var conn = GetConnection();
        var query = GetUserDefinedQuery(conn, filter);
        var table = GetTableName(query);

        return query
            .When(
                parent == null,
                q => q.WhereNull($"{table}.parent_id"),
                q => q.Where($"{table}.parent_id", parent)
            )
            .Get<T>()
            .ToList();
    }

    public IReadOnlyList<T> GetOnlyFolders()
    {
        using var conn = GetConnection();
        var query = GetQuery(conn);
        var table = GetTableName(query);

        return query
            .WhereTrue($"{table}.is_folder")
            .WhereFalse($"{table}.deleted")
            .OrderBy($"{table}.item_name")
            .Get<T>()
            .ToList();
    }

    public Guid AddFolder(Guid? parent, string code, string folderName)
    {
        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            string sql = $"insert into {GetTableName()} (is_folder, code, item_name, parent_id) values (true, :code, :item_name, :parent) returning id";
            var res = conn.QuerySingle<Guid>(sql, new { code, item_name = folderName, parent }, transaction);
            transaction.Commit();

            return res;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e, CurrentDatabase), e);
        }
    }

    public void WipeParented(Guid? owner, Guid? parent)
    {
        if (parent == null)
        {
            if (owner == null)
            {
                WipeAll();
            }
            else
            {
                WipeAll(owner.Value);
            }
        }
        else
        {
            using var conn = GetConnection();
            using var transaction = conn.BeginTransaction();
            try
            {
                string table = GetTableName();
                if (owner == null)
                {
                    conn.Execute($"delete from {table} where parent_id = :parent_id and deleted", new { parent_id = parent }, transaction: transaction);
                }
                else
                {
                    conn.Execute($"delete from {table} where owner_id = :owner_id and parent_id = :parent_id and deleted", new { owner_id = owner, parent_id = parent }, transaction: transaction);
                }

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new RepositoryException(ExceptionHelper.Message(e, CurrentDatabase), e);
            }
        }
    }
}
