//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Employees;

public class EmployeeRepository : OwnedRepository<Guid, Employee>, IEmployeeRepository
{
    public EmployeeRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
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
