//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.04.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using Humanizer;

using SqlKata;
using SqlKata.Execution;

using System.Diagnostics.Contracts;

namespace DocumentFlow.Data.Models;

public class ProductionLotRepository : DocumentRepository<ProductionLot>, IProductionLotRepository
{
    public ProductionLotRepository(IDatabase database) : base(database)
    {
    }

    public decimal GetFinishedGoods(ProductionLot lot)
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .From("finished_goods")
            .SelectRaw("sum(quantity)")
            .Where("owner_id", lot.Id)
            .WhereTrue("carried_out")
            .Get<decimal?>()
            .FirstOrDefault() ?? 0;
    }

    public IReadOnlyList<ProductionLotOperation> GetOperations(ProductionLot lot)
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .Select("co.*")
            .SelectRaw("production_lot.quantity * co.repeats as quantity_by_lot")
            .Join("calculation_operation as co", "co.owner_id", "production_lot.calculation_id")
            .Where("production_lot.id", lot.Id)
            .Get<ProductionLotOperation>()
            .ToList();
    }

    public IReadOnlyList<ProductionLot> GetActiveLots()
    {
        using var conn = GetConnection();

        var fg = new Query("finished_goods")
            .Select("goods_id")
            .Select("owner_id as lot_id")
            .SelectRaw("sum(quantity) as finished_quantity")
            .WhereTrue("carried_out")
            .GroupBy("owner_id", "goods_id");

        var query = GetQuery(conn)
            .Select("production_lot.*")
            .Select("c.owner_id as goods_id")
            .Select("g.item_name as goods_name")
            .SelectRaw("production_lot.quantity - coalesce(fg.finished_quantity, 0) as free_quantity")
            .Join("calculation as c", "c.id", "production_lot.calculation_id")
            .Join("goods as g", "g.id", "c.owner_id")
            .LeftJoin(fg.As("fg"), q => q.On("fg.lot_id", "production_lot.id"))
            .WhereFalse("g.is_service")
            .WhereTrue("production_lot.carried_out")
            .WhereRaw("production_lot.quantity - coalesce(fg.finished_quantity, 0) > 0");

        return query.Get<ProductionLot>().ToList();
    }

    public IReadOnlyList<ProductionLot> GetActiveLots(Contractor contractor) => GetActiveLots(contractor.Id);

    public IReadOnlyList<ProductionLot> GetActiveLots(Guid contractorId, Guid? goodsId = null)
    {
        using var conn = GetConnection();

        var fg = new Query("finished_goods")
            .Select("goods_id")
            .Select("owner_id as lot_id")
            .SelectRaw("sum(quantity) as finished_quantity")
            .WhereTrue("carried_out")
            .GroupBy("owner_id", "goods_id");

        var ls = new Query("lot_sale")
            .Select("lot_id")
            .SelectRaw("sum(quantity) as sale_quantity")
            .GroupBy("lot_id");

        var query = GetQuery(conn)
            .Select("production_lot.*")
            .Select("c.owner_id as goods_id")
            .Select("g.item_name as goods_name")
            .SelectRaw("fg.finished_quantity - coalesce(ls.sale_quantity, 0) as free_quantity")
            .Join("production_order as po", "po.id", "production_lot.owner_id")
            .Join("calculation as c", "c.id", "production_lot.calculation_id")
            .Join("goods as g", "g.id", "c.owner_id")
            .LeftJoin(fg.As("fg"), q => q.On("fg.lot_id", "production_lot.id"))
            .LeftJoin(ls.As("ls"), q => q.On("ls.lot_id", "production_lot.id"))
            .Where("po.contractor_id", contractorId)
            .When(goodsId != null, q => q.Where("c.owner_id", goodsId))
            .WhereFalse("g.is_service")
            .WhereNotNull("fg.finished_quantity")
            .WhereTrue("po.carried_out")
            .WhereTrue("production_lot.carried_out")
            .WhereRaw("(fg.finished_quantity - coalesce(ls.sale_quantity, 0) > 0)");

        return query.Get<ProductionLot>().ToList();
    }

    public void SetState(ProductionLot lot, LotState state)
    {
        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            var newState = state.ToString().Underscore();
            conn.Execute($"call set_production_lot_state(:document_id, '{newState}'::lot_state)", new { document_id = lot.Id }, transaction);
            transaction.Commit();

            lot.State = newState;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
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
