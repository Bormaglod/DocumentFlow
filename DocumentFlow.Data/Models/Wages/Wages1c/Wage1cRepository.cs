//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Data.Models;

public class Wage1cRepository : DocumentRepository<Wage1c>, IWage1cRepository
{
    public Wage1cRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<Wage1cEmployee> Get(BillingDocument wage) => Get(wage.BillingYear, wage.BillingMonth);

    public IReadOnlyList<Wage1cEmployee> Get(int year, short month)
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .Select("wce.employee_id")
            .SelectRaw("sum(wce.wage) as wage")
            .Join("wage1c_employee as wce", "wce.owner_id", "wage1c.id")
            .Where("billing_year", year)
            .Where("billing_month", month)
            .WhereTrue("wage1c.carried_out")
            .GroupBy("wce.employee_id")
            .Get<Wage1cEmployee>()
            .ToList();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        var q = new Query("wage1c_employee as we")
            .Select("we.owner_id")
            .SelectRaw("sum(we.wage) as wage")
            .SelectRaw("array_agg(oe.item_name) as employee_names")
            .Join("our_employee as oe", "oe.id", "we.employee_id")
            .GroupBy("we.owner_id");

        return query
            .Select("wage1c.*")
            .Select("o.item_name as organization_name")
            .Select("q.wage")
            .Select("q.employee_names")
            .Join("organization as o", "o.id", "wage1c.organization_id")
            .LeftJoin(q.As("q"), j => j.On("q.owner_id", "wage1c.id"));
    }
}
