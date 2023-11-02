//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using Humanizer;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class BalanceSheetRepository : Repository<Guid, BalanceSheet>, IBalanceSheetRepository
{
    public BalanceSheetRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        if (filter is IBalanceSheetFilter f)
        {
            string name = f.Content.ToString().Underscore();

            Query? init_processing_receipt = null;
            Query? init_processing_writeoff = null;
            Query? init_processing_balance = null;
            Query? processing_receipt = null;
            Query? processing_writeoff = null;
            if (!f.ShowGivingMaterial)
            {
                init_processing_receipt = new Query("waybill_processing_price as wpp")
                    .Select("wpp.reference_id")
                    .SelectRaw("sum(wpp.amount) as amount")
                    .Join("waybill_processing as wp", "wp.id", "wpp.owner_id")
                    .Where("wp.document_date", "<", f.DateFrom)
                    .GroupBy("wpp.reference_id");

                init_processing_writeoff = new Query("waybill_processing_writeoff as wpw")
                    .Select("wpw.material_id")
                    .SelectRaw("sum(wpw.amount) as amount")
                    .Join("waybill_processing as wp", "wp.id", "wpw.waybill_processing_id")
                    .Where("wp.document_date", "<", f.DateFrom)
                    .GroupBy("wpw.material_id");

                init_processing_balance = new Query("init_processing_receipt as pr")
                    .Select("pr.reference_id")
                    .SelectRaw("coalesce(pr.amount, 0) - coalesce(pw.amount, 0) as amount")
                    .LeftJoin("init_processing_writeoff as pw", "pw.material_id", "pr.reference_id")
                    .WhereRaw("coalesce(pr.amount, 0) - coalesce(pw.amount, 0) != 0");

                processing_receipt = new Query("waybill_processing_price as wpp")
                    .Select("wpp.reference_id")
                    .SelectRaw("sum(wpp.amount) as amount")
                    .Join("waybill_processing as wp", "wp.id", "wpp.owner_id")
                    .WhereBetween("wp.document_date", f.DateFrom, f.DateTo)
                    .GroupBy("wpp.reference_id");

                processing_writeoff = new Query("waybill_processing_writeoff as wpw")
                    .Select("wpw.material_id")
                    .SelectRaw("sum(wpw.amount) as amount")
                    .Join("waybill_processing as wp", "wp.id", "wpw.waybill_processing_id")
                    .WhereBetween("wp.document_date", f.DateFrom, f.DateTo)
                    .GroupBy("wpw.material_id");
            }

            var init_balance = new Query($"balance_{name}")
                .Select("reference_id as id")
                .SelectRaw("sum(amount) as init_amount")
                .SelectRaw("sum(operation_summa * sign(amount)) as init_summa")
                .Where("document_date", "<", f.DateFrom)
                .GroupBy("reference_id");

            var range_balance = new Query($"balance_{name}")
                .Select("reference_id as id")
                .SelectRaw("sum(iif(amount > 0, amount, 0::numeric)) as income_amount")
                .SelectRaw("sum(iif(amount > 0, operation_summa, 0::numeric)) as income_summa")
                .SelectRaw("sum(iif(amount < 0, abs(amount), 0::numeric)) as expense_amount")
                .SelectRaw("sum(iif(amount < 0, operation_summa, 0::numeric)) as expense_summa")
                .WhereBetween("document_date", f.DateFrom, f.DateTo)
                .GroupBy("reference_id");

            query.From($"{name} as p")
                .When(!f.ShowGivingMaterial, q => q
                    .With("init_processing_receipt", init_processing_receipt)
                    .With("init_processing_writeoff", init_processing_writeoff)
                    .With("init_processing_balance", init_processing_balance)
                    .With("processing_receipt", processing_receipt)
                    .With("processing_writeoff", processing_writeoff)
                )
                .With("init_balance", init_balance)
                .With("range_balance", range_balance)
                .Select("p.id")
                .Select("p.item_name as product_name")
                .Select("p.code as product_code")
                .Select("pp.item_name as group_name")
                .SelectRaw($"'{name}' as bucket_name")
                .When(f.ShowGivingMaterial, 
                    q => q.Select("ib.init_amount as opening_balance_amount"),
                    q => q.SelectRaw("coalesce(ib.init_amount, 0) - coalesce(ipb.amount, 0) as opening_balance_amount")
                )
                .Select("ib.init_summa as opening_balance_summa")
                
                .When(f.ShowGivingMaterial,
                    q => q.Select("rb.income_amount as income_amount"),
                    q => q.SelectRaw("coalesce(rb.income_amount, 0) - coalesce(pr.amount, 0) as income_amount")
                )
                .Select("rb.income_summa as income_summa")
                .When(f.ShowGivingMaterial,
                    q => q.Select("rb.expense_amount as expense_amount"),
                    q => q.SelectRaw("coalesce(rb.expense_amount, 0) - coalesce(pw.amount, 0) as expense_amount")
                )
                .Select("rb.expense_summa as expense_summa")
                .When(f.ShowGivingMaterial,
                    q => q.SelectRaw("coalesce(ib.init_amount, 0) + coalesce(rb.income_amount, 0) - coalesce(rb.expense_amount, 0) as closing_balance_amount"),
                    q => q.SelectRaw("coalesce(ib.init_amount, 0) - coalesce(ipb.amount, 0) + coalesce(rb.income_amount, 0) - coalesce(pr.amount, 0) - coalesce(rb.expense_amount, 0) + coalesce(pw.amount, 0) as closing_balance_amount")
                )
                .SelectRaw("coalesce(ib.init_summa, 0) + coalesce(rb.income_summa, 0) - coalesce(rb.expense_summa, 0) as closing_balance_summa")
                .LeftJoin("init_balance as ib", "ib.id", "p.id")
                .LeftJoin("range_balance as rb", "rb.id", "p.id")
                .LeftJoin($"{name} as pp", "pp.id", "p.parent_id")
                .When(!f.ShowGivingMaterial, q => q
                    .LeftJoin("init_processing_balance as ipb", "ipb.reference_id", "p.id")
                    .LeftJoin("processing_receipt as pr", "pr.reference_id", "p.id")
                    .LeftJoin("processing_writeoff as pw", "pw.material_id", "p.id")
                )
                .When(
                    f.Content == BalanceSheetContent.Goods, 
                    q => q.WhereFalse("p.is_service")
                 )
                .WhereFalse("p.is_folder")
                .Where(q => q
                    .WhereRaw("coalesce(ib.init_summa, 0) != 0").Or()
                    .WhereRaw("coalesce(rb.income_summa, 0) != 0").Or()
                    .WhereRaw("coalesce(rb.expense_summa, 0) != 0").Or()
                    .When(f.ShowGivingMaterial, 
                        q => q
                            .WhereRaw("coalesce(ib.init_amount, 0) != 0").Or()
                            .WhereRaw("coalesce(rb.income_amount, 0) != 0").Or()
                            .WhereRaw("coalesce(rb.expense_amount, 0) != 0"),
                        q => q
                            .WhereRaw("coalesce(ib.init_amount, 0) - coalesce(ipb.amount, 0) != 0").Or()
                            .WhereRaw("coalesce(rb.income_amount, 0) - coalesce(pr.amount, 0) != 0").Or()
                            .WhereRaw("coalesce(rb.expense_amount, 0) - coalesce(pw.amount, 0) != 0")
                    )
                )
                .OrderBy("pp.item_name", "p.item_name");
        }

        return query;
    }
}
