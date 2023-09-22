//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repository;
using DocumentFlow.Data.Interfaces;

using SqlKata;
using DocumentFlow.Data.Interfaces.Filters;

namespace DocumentFlow.Data.Models;

public class EmployeeRepository : OwnedRepository<Guid, Employee>, IEmployeeRepository
{
    public EmployeeRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .FromRaw("only employee")
            .Select("employee.*")
            .Select("o.item_name as post_name")
            .Select("c.item_name as owner_name")
            .Join("contractor as c", "c.id", "employee.owner_id")
            .LeftJoin("okpdtr as o", "o.id", "employee.post_id");
    }
}
