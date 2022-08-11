//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Wages;

public class Wage1cRepository : DocumentRepository<Wage1c>, IWage1cRepository
{
    public Wage1cRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.owner_id);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
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
