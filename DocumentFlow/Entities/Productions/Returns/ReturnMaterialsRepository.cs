//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Productions.Returns;

public class ReturnMaterialsRepository : DocumentRepository<ReturnMaterials>, IReturnMaterialsRepository
{
    public ReturnMaterialsRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
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
