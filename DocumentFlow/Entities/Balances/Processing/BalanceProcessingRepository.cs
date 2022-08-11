//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Balances;

public class BalanceProcessingRepository : OwnedRepository<Guid, BalanceProcessing>, IBalanceProcessingRepository
{
    public BalanceProcessingRepository(IDatabase database) : base(database) { }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
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
            .WhereIn("dt.code", new string[] {
                "waybill_processing",
                "operations_performed",
                "return_materials"
            });

        return query
            .From("cte")
            .With("mat", m)
            .With("cte", q)
            .Select("*")
            .SelectRaw("sum(amount) over (partition by reference_id order by document_date, document_number) as remainder");
    }

    protected override Query GetQueryOwner(Query query, Guid owner_id) => query.Where("contractor_id", owner_id);
}
