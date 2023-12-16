//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Tools;

using SqlKata;
using SqlKata.Execution;

using System.Data;

namespace DocumentFlow.Data.Models;

public class MaterialRepository : ProductRepository<Material>, IMaterialRepository
{
    public MaterialRepository(IDatabase database) : base(database) { }

    public IReadOnlyList<Material> GetMaterials()
    {
        string sql = @$"with recursive r as
                           (
	                            select * from material where material_kind != 'wire'
		                        union
	                            select p.* from material p join r on (r.parent_id = p.id)
                           )
                           select * from r where not deleted order by item_name";

        using var conn = GetConnection();
        try
        {
            return conn.Query<Material>(sql).ToList();
        }
        catch (Exception e)
        {
            throw new RepositoryException(ExceptionHelper.Message(e, CurrentDatabase), e);
        }
    }

    public IReadOnlyList<Material> GetWires()
    {
        string sql = @$"with recursive r as
                           (
	                            select * from material where material_kind = 'wire'
		                        union
	                            select p.* from material p join r on (r.parent_id = p.id)
                           )
                           select * from r where not deleted order by item_name";

        using var conn = GetConnection();
        try
        {
            return conn.Query<Material>(sql).ToList();
        }
        catch (Exception e)
        {
            throw new RepositoryException(ExceptionHelper.Message(e, CurrentDatabase), e);
        }
    }

    public IReadOnlyList<Material> GetCrossMaterials(Guid exceptMaterial)
    {
        return GetListExisting(callback: query => query
            .OrderBy("material.item_name")
            .WhereNull("material.owner_id")
            .When(exceptMaterial != Guid.Empty, q => q
                .WhereNot("material.id", exceptMaterial)));
    }

    protected override bool CreatingAdjustedQuery() => true;

    protected override Material GetAdjustedQuery(Guid id, IDbConnection connection)
    {
        string table;
        if (CurrentDatabase.HasPrivilege("materials", Privilege.Select))
        {
            table = "materials";
        }
        else
        {
            table = "materials_simple";
        }

        var materialDictionary = new Dictionary<Guid, Material>();

        string sql = @$"
            select 
                m.*, cp.*, material.code, material.item_name
            from {table} m 
                left join compatible_part cp on cp.owner_id = m.id 
                left join material on material.id = cp.compatible_id 
            where m.id = :id";

        return connection.Query<Material, CompatiblePart, Material>(
            sql,
            (material, part) =>
            {
                if (!materialDictionary.TryGetValue(material.Id, out var materialEntry))
                {
                    materialEntry = material;
                    materialDictionary.Add(materialEntry.Id, materialEntry);
                }

                if (part != null)
                {
                    materialEntry.CompatibleParts.Add(part);
                }

                return materialEntry;
            },
            new { id })
            .First();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        query.Clauses.Clear();
        if (CurrentDatabase.HasPrivilege("materials", Privilege.Select))
        {
            return query.From("materials");
        }
        else
        {
            return query.From("materials_simple");
        }
    }

    protected override Query GetQuery(Query query)
    {
        return query
            .Select("material.*")
            .Select("m.abbreviation as measurement_name")
            .LeftJoin("measurement as m", "m.id", "material.measurement_id");
    }
}
