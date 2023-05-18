//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.08.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Wages;

public class Wage1cRepository : DocumentRepository<Wage1c>, IWage1cRepository
{
    public Wage1cRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.OwnerId);
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
