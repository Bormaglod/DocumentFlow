//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Products;

public class MaterialRepository : ProductRepository<Material>, IMaterialRepository
{
    public MaterialRepository(IDatabase database) : base(database) { }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        if (HasPrivilege("materials", Privilege.Select))
        {
            return query.From("materials");
        }
        else
        {
            return query.From("materials_simple");
        }
    }

    public IReadOnlyList<Material> GetAllMaterials()
    {
        string sql = @"
            with recursive r as 
            (
                select * from material where parent_id is null and not deleted 
                union 
                select m.* from material m join r on r.id = m.parent_id and not m.deleted
            ) select * from r order by item_name";

        using var conn = Database.OpenConnection();
        try
        {
            return conn.Query<Material>(sql).ToList();
        }
        catch (Exception e)
        {
            ExceptionHelper.MesssageBox(e);
            return Array.Empty<Material>();
        }
    }

    public IReadOnlyList<Material> GetMaterials()
    {
        string sql = @"
            with recursive r as 
            (
                select * from material where parent_id is null and not deleted 
                union 
                select m.* from material m join r on r.id = m.parent_id and not m.deleted and r.id != '0525748e-e98c-4296-bd0e-dcacee7224f3'
            ) select * from r order by item_name";

        using var conn = Database.OpenConnection();
        try
        {
            return conn.Query<Material>(sql).ToList();
        }
        catch (Exception e)
        {
            ExceptionHelper.MesssageBox(e);
            return Array.Empty<Material>();
        }
    }

    public IReadOnlyList<Material> GetWires()
    {
        string sql = @"
            with recursive r as
            (
	            select * from material where id = '0525748e-e98c-4296-bd0e-dcacee7224f3' 
                union 
            	select m.* from material m join r on r.id = m.parent_id and not m.deleted
            ) select * from r order by item_name";

        using var conn = Database.OpenConnection();
        try
        {
            return conn.Query<Material>(sql).ToList();
        }
        catch (Exception e)
        {
            ExceptionHelper.MesssageBox(e);
            return Array.Empty<Material>();
        }
    }
}
