//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Data.Models;

public class BalanceProcessingRepository : OwnedRepository<Guid, BalanceProcessing>, IBalanceProcessingRepository
{
    public BalanceProcessingRepository(IDatabase database) : base(database) { }

    public IReadOnlyList<BalanceProcessing> GetRemainders()
    {
        using var conn = GetConnection();

        var q_wpp = new Query("waybill_processing_price as wpp")
            .Select("wp.contractor_id")
            .Select("wpp.reference_id")
            .SelectRaw("sum(wpp.amount) as income_material")
            .Join("waybill_processing as wp", "wp.id", "wpp.owner_id")
            .GroupBy("wp.contractor_id", "wpp.reference_id");

        var q_wpw = new Query("waybill_processing_writeoff as wpw")
            .Select("wp.contractor_id")
            .Select("wpw.material_id")
            .SelectRaw("sum(wpw.amount) as expense_material")
            .Join("waybill_processing as wp", "wp.id", "wpw.waybill_processing_id")
            .GroupBy("wp.contractor_id", "wpw.material_id");

        return GetQuery(conn)
            .From("q_wpp as i")
            .With("q_wpp", q_wpp)
            .With("q_wpw", q_wpw)
            .Select("i.{contractor_id, reference_id}")
            .Select("m.item_name as material_name")
            .Select("c.item_name as contractor_name")
            .Select("i.income_material as income")
            .Select("e.expense_material as expense")
            .SelectRaw("i.income_material - coalesce(e.expense_material, 0) as remainder")
            .LeftJoin("q_wpw as e", q => q.On("e.contractor_id", "i.contractor_id").On("e.material_id", "i.reference_id"))
            .Join("material as m", "m.id", "i.reference_id")
            .Join("contractor as c", "c.id", "i.contractor_id")
            .WhereRaw("i.income_material - coalesce(e.expense_material, 0) > 0")
            .OrderBy("m.item_name")
            .Get<BalanceProcessing>()
            .ToList();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        Query m = new Query("waybill_processing_price as wpp")
            .Distinct()
            .Select("wp.contractor_id")
            .Select("wpp.reference_id")
            .Join("waybill_processing as wp", "wpp.owner_id", "wp.id")
            .WhereTrue("wp.carried_out");

        Query q = new Query("balance_material as bm")
            .Select("bm.*")
            .Select("m.item_name as material_name")
            .SelectRaw("case when bm.amount > 0 then bm.amount else null end as income")
            .SelectRaw("case when bm.amount < 0 then @bm.amount else null end as expense")
            .SelectRaw("coalesce(wp.contractor_id, po.contractor_id, rm.contractor_id) as contractor_id")
            .Select("dt.code as document_type_code")
            .Select("dt.document_name as document_type_name")
            .Join("mat as mp", "mp.reference_id", "bm.reference_id")
            .Join("material as m", "m.id", "mp.reference_id")
            .Join("document_type as dt", "dt.id", "bm.document_type_id")
            .LeftJoin("waybill_processing as wp", "wp.id", "bm.owner_id")
            .LeftJoin("operations_performed as op", "op.id", "bm.owner_id")
            .LeftJoin("production_lot as pl", "pl.id", "op.owner_id")
            .LeftJoin("production_order as po", "po.id", "pl.owner_id")
            .LeftJoin("return_materials as rm", "rm.id", "bm.owner_id")
            .LeftJoin("calculation as c", "c.id", "pl.calculation_id")
            .LeftJoin("calculation_material as cm", q => q.On("cm.owner_id", "c.id").On("cm.item_id", "mp.reference_id"))
            .WhereIn("dt.code", new string[] {
                "waybill_processing",
                "operations_performed",
                "return_materials"
            })
            .WhereRaw("cm.price_method = 'is_giving'::price_setting_method");

        return query
            .From("cte")
            .With("mat", m)
            .With("cte", q)
            .Select("*")
            .SelectRaw("sum(amount) over (partition by reference_id order by document_date, document_number) as remainder");
    }

    protected override Query GetQueryOwner(Query query, Guid owner_id) => query.Where("contractor_id", owner_id);
}
