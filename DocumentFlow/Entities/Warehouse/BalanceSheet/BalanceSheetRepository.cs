//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.07.2022
//
// Версия 2022.9.3
//  - добавлена сортировка
// Версия 2022.11.13
//  - исправлена ошибка с некорректным отображением суммы начального 
//    остатка
//  - в таблицу больше не попадают строки в которых отсутствуют остатки
//    и движение
// Версия 2022.11.16
//  - исправлена ошибка в результате которой в выборку не попадали
//    некоторые записи
// Версия 2022.12.3
//  - в выборку добавлено поле product_code
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using Humanizer;

using SqlKata;

namespace DocumentFlow.Entities.Warehouse;

public class BalanceSheetRepository : Repository<Guid, BalanceSheet>, IBalanceSheetRepository
{
    public BalanceSheetRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        if (filter is IBalanceSheetFilter f)
        {
            string name = f.Content.ToString().Underscore();

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
                .With("init_balance", init_balance)
                .With("range_balance", range_balance)
                .Select("p.id")
                .Select("p.item_name as product_name")
                .Select("p.code as product_code")
                .Select("pp.item_name as group_name")
                .SelectRaw("ib.init_amount as opening_balance_amount")
                .SelectRaw("ib.init_summa as opening_balance_summa")
                .SelectRaw("rb.income_amount as income_amount")
                .SelectRaw("rb.income_summa as income_summa")
                .SelectRaw("rb.expense_amount as expense_amount")
                .SelectRaw("rb.expense_summa as expense_summa")
                .SelectRaw("coalesce(ib.init_amount, 0) + coalesce(rb.income_amount, 0) - coalesce(rb.expense_amount, 0) as closing_balance_amount")
                .SelectRaw("coalesce(ib.init_summa, 0) + coalesce(rb.income_summa, 0) - coalesce(rb.expense_summa, 0) as closing_balance_summa")
                .LeftJoin("init_balance as ib", "ib.id", "p.id")
                .LeftJoin("range_balance as rb", "rb.id", "p.id")
                .LeftJoin($"{name} as pp", "pp.id", "p.parent_id")
                .When(
                    f.Content == BalanceSheetContent.Goods, 
                    q => q.WhereFalse("p.is_service")
                 )
                .WhereFalse("p.is_folder")
                .Where(q => q
                    .WhereRaw("coalesce(ib.init_amount, 0) != 0").Or()
                    .WhereRaw("coalesce(ib.init_summa, 0) != 0").Or()
                    .WhereRaw("coalesce(rb.income_amount, 0) != 0").Or()
                    .WhereRaw("coalesce(rb.income_summa, 0) != 0").Or()
                    .WhereRaw("coalesce(rb.expense_amount, 0) != 0").Or()
                    .WhereRaw("coalesce(rb.expense_summa, 0) != 0"))
                .OrderBy("pp.item_name", "p.item_name");
        }

        return query;
    }
}
