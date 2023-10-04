//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

using System.Data;

namespace DocumentFlow.Data.Models;

public class ReturnMaterialsRepository : DocumentRepository<ReturnMaterials>, IReturnMaterialsRepository
{
    public ReturnMaterialsRepository(IDatabase database) : base(database)
    {
    }

    protected override bool CreatingAdjustedQuery() => true;

    protected override ReturnMaterials GetAdjustedQuery(Guid id, IDbConnection connection)
    {
        var rmDictionary = new Dictionary<Guid, ReturnMaterials>();

        string sql = @"
            select 
                rm.*, 
                rmr.*, 
                p.item_name as material_name, 
                p.code, 
                m.abbreviation as measurement_name
            from return_materials rm
                left join return_materials_rows rmr on rmr.owner_id = rm.id 
                left join material p on p.id = rmr.material_id 
                left join measurement m on m.id = p.measurement_id
            where rm.id = :id";

        return connection.Query<ReturnMaterials, ReturnMaterialsRows, ReturnMaterials>(
            sql,
            (returnMaterial, material) =>
            {
                if (!rmDictionary.TryGetValue(returnMaterial.Id, out var returnMaterialEntry))
                {
                    returnMaterialEntry = returnMaterial;
                    rmDictionary.Add(returnMaterial.Id, returnMaterialEntry);
                }

                if (material != null)
                {
                    returnMaterialEntry.Materials.Add(material);
                }

                return returnMaterialEntry;
            },
            new { id })
            .First();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("return_materials.*")
            .Select("o.item_name as organization_name")
            .Select("c.item_name as contractor_name")
            .Select("contract.item_name as contract_name")
            .Select("po.document_date as order_date")
            .Select("po.document_number as order_number")
            .Join("organization as o", "o.id", "return_materials.organization_id")
            .Join("contractor as c", "c.id", "return_materials.contractor_id")
            .Join("production_order as po", "po.id", "return_materials.owner_id")
            .LeftJoin("contract", "contract.id", "return_materials.contract_id");
    }
}
