//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;
using SqlKata.Execution;

using System.Data;

namespace DocumentFlow.Data.Models;

public class ProductionOrderRepository : DocumentRepository<ProductionOrder>, IProductionOrderRepository
{
    public ProductionOrderRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<ProductionOrder> GetWithReturnMaterial(Contract contract)
    {
        using var conn = GetConnection();

        var wpw = new Query("waybill_processing_writeoff")
            .Select("waybill_processing_id")
            .Select("material_id")
            .SelectRaw("sum(amount) as written_off")
            .GroupBy("waybill_processing_id", "material_id");

        var rm = new Query("waybill_processing_price as wpp")
            .Distinct()
            .Select("wpp.owner_id as waybill_processing_id")
            .Join(wpw.As("wpw"), j => j.On("wpw.waybill_processing_id", "wpp.owner_id").On("wpw.material_id", "wpp.reference_id"))
            .WhereColumns("wpp.amount", "!=", "wpw.written_off");

        var d = new Query("production_order_price")
            .Select("owner_id")
            .SelectRaw("sum(product_cost) as product_cost")
            .SelectRaw("sum(tax_value) as tax_value")
            .SelectRaw("sum(full_cost) as full_cost")
            .GroupBy("owner_id");

        return GetQuery(conn, "po")
            .With("rm_waybills", rm)
            .Select("po.*")
            .Select("c.item_name as contractor_name")
            .Select("d.*")
            .Select("wp.contractor_id as wp_contractor_id")
            .Select("wp.contract_id as wp_contract_id")
            .Join("contractor as c", "c.id", "po.contractor_id")
            .LeftJoin(d.As("d"), j => j.On("d.owner_id", "po.id"))
            .Join("waybill_processing as wp", "wp.owner_id", "po.id")
            .Join("rm_waybills as rm", "rm.waybill_processing_id", "wp.id")
            .WhereFalse("po.deleted")
            .WhereTrue("po.carried_out")
            .Where("wp.contractor_id", contract.OwnerId)
            .Where("wp.contract_id", contract.Id)
            .Get<ProductionOrder>()
            .ToList();
    }

    public IReadOnlyList<T> GetOnlyGivingMaterials<T>(ProductionOrder order) where T : ProductPrice
    {
        using var conn = GetConnection();
        return GetQuery(conn, "po")
            .Select("cm.item_id as reference_id")
            .Select("m.code")
            .Select("m.item_name as product_name")
            .SelectRaw("sum(pop.amount * cm.amount) as amount")
            .Join("production_order_price as pop", "pop.owner_id", "po.id")
            .Join("calculation as c", "c.id", "pop.calculation_id")
            .Join("calculation_material as cm", q => q.On("cm.owner_id", "c.id").WhereTrue("cm.is_giving"))
            .Join("material as m", "m.id", "cm.item_id")
            .Where("po.id", order.Id)
            .GroupBy("cm.item_id", "m.code", "m.item_name")
            .OrderBy("m.code")
            .Get<T>()
            .ToList();
    }

    public IReadOnlyList<ProductionOrder> GetActiveOrders()
    {
        using var conn = GetConnection();
        return GetUserDefinedQuery(conn)
            .WhereFalse("production_order.deleted")
            .WhereTrue("production_order.carried_out")
            .WhereFalse("production_order.closed")
            .Get<ProductionOrder>()
            .OrderBy(x => x.DocumentDate)
            .ThenBy(x => x.DocumentNumber)
            .ToList();
    }

    public IReadOnlyList<Goods> GetGoods(ProductionOrder order)
    {
        using var conn = GetConnection();
        return GetQuery(conn, "po")
            .Distinct()
            .Select("g.*")
            .Join("production_order_price as pop", "po.id", "pop.owner_id")
            .Join("goods as g", "g.id", "pop.reference_id")
            .Where("po.id", order.Id)
            .Get<Goods>()
            .ToList();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        var d = new Query("production_order_price")
            .Select("owner_id")
            .SelectRaw("sum([product_cost]) as [cost_order]")
            .SelectRaw("sum([tax_value]) as [tax_value]")
            .SelectRaw("sum([full_cost]) as [full_cost]")
            .GroupBy("owner_id");

        return query
            .Select("production_order.*")
            .Select("o.item_name as organization_name")
            .SelectRaw("case when c.item_name is null then c.code else c.item_name end as contractor_name")
            .Select("contract.tax_payer")
            .SelectRaw("case [contract].[tax_payer] when true then 20 else 0 end as [tax]")
            .Select("contract.item_name as contract_name")
            .Select("d.*")
            .Join("organization as o", "o.id", "production_order.organization_id")
            .Join("contractor as c", "c.id", "production_order.contractor_id")
            .LeftJoin("contract", "contract.id", "production_order.contract_id")
            .LeftJoin(d.As("d"), j => j.On("d.owner_id", "production_order.id"));
    }

    protected override bool CreatingAdjustedQuery() => true;

    protected override ProductionOrder GetAdjustedQuery(Guid id, IDbConnection connection)
    {
        var orderDictionary = new Dictionary<Guid, ProductionOrder>();

        string sql = @"
            select 
                po.*, 
                pop.*, 
                g.item_name as product_name, 
                g.code, 
                m.abbreviation as measurement_name,
                c.code as calculation_name
            from production_order po
                left join production_order_price pop on pop.owner_id = po.id 
                left join goods g on g.id = pop.reference_id 
                left join measurement m on m.id = g.measurement_id
                join calculation c on c.id = pop.calculation_id
            where po.id = :id";

        return connection.Query<ProductionOrder, ProductionOrderPrice, ProductionOrder>(
            sql,
            (order, price) =>
            {
                if (!orderDictionary.TryGetValue(order.Id, out var orderEntry))
                {
                    orderEntry = order;
                    orderDictionary.Add(order.Id, orderEntry);
                }

                if (price != null)
                {
                    orderEntry.Prices.Add(price);
                }

                return orderEntry;
            },
            new { id })
            .First();
    }
}
