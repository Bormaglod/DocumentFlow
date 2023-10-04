//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.07.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;
using SqlKata.Execution;

using System.Data;

namespace DocumentFlow.Data.Models;

public class WaybillProcessingRepository : DocumentRepository<WaybillProcessing>, IWaybillProcessingRepository
{
    public WaybillProcessingRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<ReturnMaterialsRows> GetReturnMaterials(ProductionOrder order) => GetReturnMaterials(order.Id);

    public IReadOnlyList<ReturnMaterialsRows> GetReturnMaterials(Guid orderId)
    {
        using var conn = GetConnection();

        var wpw = new Query("waybill_processing_writeoff")
            .Select("waybill_processing_id")
            .Select("material_id")
            .SelectRaw("sum(amount) as written_off")
            .GroupBy("waybill_processing_id", "material_id");

        return GetQuery(conn, "wp")
            .Select("wpp.reference_id as material_id")
            .Select("m.item_name as material_name")
            .SelectRaw("sum(wpp.amount - coalesce(wpw.written_off, 0)) as quantity")
            .Join("waybill_processing_price as wpp", "wp.id", "wpp.owner_id")
            .Join("material as m", "m.id", "wpp.reference_id")
            .LeftJoin(wpw.As("wpw"), j => j
                .On("wpw.waybill_processing_id", "wp.id")
                .On("wpw.material_id", "wpp.reference_id"))
            .WhereRaw("coalesce(wpw.written_off, 0) < wpp.amount")
            .GroupBy("wpp.reference_id", "m.item_name")
            .Where("wp.owner_id", orderId)
            .Get<ReturnMaterialsRows>()
            .ToList();
    }

    protected override bool CreatingAdjustedQuery() => true;

    protected override WaybillProcessing GetAdjustedQuery(Guid id, IDbConnection connection)
    {
        var wpDictionary = new Dictionary<Guid, WaybillProcessing>();

        string sql = @"
            select 
                wp.*, 
                wpp.*, 
                p.item_name as product_name, 
                p.code, 
                m.abbreviation as measurement_name
            from waybill_processing wp
                left join waybill_processing_price wpp on wpp.owner_id = wp.id 
                left join product p on p.id = wpp.reference_id 
                left join measurement m on m.id = p.measurement_id
            where wp.id = :id";

        return connection.Query<WaybillProcessing, WaybillProcessingPrice, WaybillProcessing>(
            sql,
            (waybill, price) =>
            {
                if (!wpDictionary.TryGetValue(waybill.Id, out var waybillEntry))
                {
                    waybillEntry = waybill;
                    wpDictionary.Add(waybill.Id, waybillEntry);
                }

                if (price != null)
                {
                    waybillEntry.Prices.Add(price);
                }

                return waybillEntry;
            },
            new { id })
            .First();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        var q = new Query("waybill_processing_price")
            .Select("owner_id")
            .SelectRaw("sum([product_cost]) as [product_cost]")
            .SelectRaw("sum([tax_value]) as [tax_value]")
            .SelectRaw("sum([full_cost]) as [full_cost]")
            .GroupBy("owner_id");

        return query
            .Select("waybill_processing.*")
            .Select("o.item_name as organization_name")
            .Select("c.item_name as contractor_name")
            .Select("contract.tax_payer")
            .SelectRaw("case [contract].[tax_payer] when true then 20 else 0 end as [tax]")
            .Select("contract.item_name as contract_name")
            .Select("po.document_date as order_date")
            .Select("po.document_number as order_number")
            .Select("q.product_cost")
            .Select("q.tax_value")
            .Select("q.full_cost")
            .Join("organization as o", "o.id", "waybill_processing.organization_id")
            .Join("contractor as c", "c.id", "waybill_processing.contractor_id")
            .Join("production_order as po", "po.id", "waybill_processing.owner_id")
            .LeftJoin("contract", "contract.id", "waybill_processing.contract_id")
            .LeftJoin(q.As("q"), j => j.On("q.owner_id", "waybill_processing.id"));
    }
}
