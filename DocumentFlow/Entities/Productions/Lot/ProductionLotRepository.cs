//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Entities.Productions.Lot;

public class ProductionLotRepository : DocumentRepository<ProductionLot>, IProductionLotRepository
{
    public ProductionLotRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<ProductionLotOperation> GetOperations(Guid lot_id)
    {
        using var conn = Database.OpenConnection();
        var query = GetBaseQuery(conn)
            .Select("co.*")
            .SelectRaw("production_lot.quantity * co.repeats as quantity_by_lot")
            .Join("calculation_operation as co", "co.owner_id", "production_lot.calculation_id")
            .Where("production_lot.id", lot_id);
        return query.Get<ProductionLotOperation>().ToList();
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("production_lot.*")
            .Select("o.item_name as organization_name")
            .Select("po.document_number as order_number")
            .Select("po.document_date as order_date")
            .Select("g.id as goods_id")
            .Select("g.item_name as goods_name")
            .Select("c.code as calculation_name")
            .Join("organization as o", "o.id", "production_lot.organization_id")
            .Join("production_order as po", "po.id", "production_lot.owner_id")
            .Join("calculation as c", "c.id", "production_lot.calculation_id")
            .Join("goods as g", "g.id", "c.owner_id");
    }
}
