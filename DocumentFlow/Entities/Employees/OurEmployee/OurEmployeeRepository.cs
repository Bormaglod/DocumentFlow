//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.2.4
//  - добавлен метод GetWages
//
//-----------------------------------------------------------------------

using DocumentFlow.Core;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Employees;

public class OurEmployeeRepository : Repository<Guid, OurEmployee>, IOurEmployeeRepository
{
    public OurEmployeeRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
    }

    public IReadOnlyList<Wages> GetWages()
    {
        if (HasPrivilege("employee_wages", Privilege.Select))
        {
            return GetViewList<Wages>("employee_wages");
        }

        throw new DbAccessException($"Отсутствует доступ к таблице {"employee_wages"}");
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("our_employee.*")
            .Select("o.item_name as post_name")
            .Select("org.item_name as owner_name")
            .Join("organization as org", "org.id", "our_employee.owner_id")
            .LeftJoin("okpdtr as o", "o.id", "our_employee.post_id");
    }
}
