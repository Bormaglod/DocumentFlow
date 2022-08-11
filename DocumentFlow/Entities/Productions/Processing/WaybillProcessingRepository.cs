//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Entities.Productions.Returns;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Entities.Productions.Processing;

public class WaybillProcessingRepository : DocumentRepository<WaybillProcessing>, IWaybillProcessingRepository
{
    public WaybillProcessingRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<ReturnMaterialsRows> GetReturnMaterials(ProductionOrder order)
    {
        using var conn = Database.OpenConnection();
        return GetBaseQuery(conn, "wp")
            .Select("wpp.reference_id as material_id")
            .Select("m.item_name as material_name")
            .SelectRaw("sum(wpp.amount - wpp.written_off) as quantity")
            .Join("waybill_processing_price as wpp", "wp.id", "wpp.owner_id")
            .Join("material as m", "m.id", "wpp.reference_id")
            .WhereColumns("wpp.written_off", "<", "wpp.amount")
            .GroupBy("wpp.reference_id", "m.item_name")
            .Where("wp.owner_id", order.id)
            .Get<ReturnMaterialsRows>()
            .ToList();
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
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
