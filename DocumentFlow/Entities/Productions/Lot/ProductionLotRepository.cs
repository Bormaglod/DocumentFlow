//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.04.2022
//
// Версия 2022.8.28
//  - добавлен метод SetState
//  - метод GetDefaultQuery изменен для получения прогресса выполнения
//    партии (свойство execute_percent)
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Core;
using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using Humanizer;

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
        var query = GetQuery(conn)
            .Select("co.*")
            .SelectRaw("production_lot.quantity * co.repeats as quantity_by_lot")
            .Join("calculation_operation as co", "co.owner_id", "production_lot.calculation_id")
            .Where("production_lot.id", lot_id);
        return query.Get<ProductionLotOperation>().ToList();
    }

    public void SetState(ProductionLot lot, LotState state)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            conn.Execute($"call set_production_lot_state(:document_id, '{state.ToString().Underscore()}'::lot_state)", new { document_id = lot.Id }, transaction);
            transaction.Commit();

            lot.State = state.ToString().Underscore();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            ExceptionHelper.Message(e);
        }
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        var goods_time = new Query("calculation_operation as co")
            .Select("co.owner_id as calc_id")
            .SelectRaw("sum(co.repeats * (3600.0 / o.production_rate)) as calc_time").Comment("время изготовления изделия")
            .Join("operation as o", "o.id", "co.item_id")
            .GroupBy("co.owner_id");

        var process = new Query("operations_performed as op")
            .Select("op.owner_id as lot_id")
            .SelectRaw("round(sum(op.quantity * (3600.0 / o.production_rate)) * 100 / (gt.calc_time * pl.quantity), 0) as execute_percent")
            .Join("production_lot as pl", "pl.id", "op.owner_id")
            .Join("calculation_operation as co", "co.id", "op.operation_id")
            .Join("operation as o", "o.id", "co.item_id")
            .Join("goods_time as gt", "gt.calc_id", "pl.calculation_id")
            .GroupBy("op.owner_id", "gt.calc_time", "pl.quantity");

        return query
            .With("goods_time", goods_time)
            .With("process", process)
            .Select("production_lot.*")
            .Select("o.item_name as organization_name")
            .Select("po.document_number as order_number")
            .Select("po.document_date as order_date")
            .Select("g.id as goods_id")
            .Select("g.item_name as goods_name")
            .Select("c.code as calculation_name")
            .Select("p.execute_percent")
            .Join("organization as o", "o.id", "production_lot.organization_id")
            .Join("production_order as po", "po.id", "production_lot.owner_id")
            .Join("calculation as c", "c.id", "production_lot.calculation_id")
            .Join("goods as g", "g.id", "c.owner_id")
            .LeftJoin("process as p", "p.lot_id", "production_lot.id");
    }
}
