//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Data.Models;

public class OurEmployeeRepository : Repository<Guid, OurEmployee>, IOurEmployeeRepository
{
    public OurEmployeeRepository(IDatabase database) : base(database)
    {
        
    }

    public IReadOnlyList<EmployeeWageBalance> GetWages()
    {
        if (CurrentDatabase.HasPrivilege("employee_wages", Privilege.Select))
        {
            using var conn = GetConnection();
            return GetQuery(conn)
                .From("employee_wages")
                .Get<EmployeeWageBalance>()
                .ToList();
        }

        throw new RepositoryException("Отсутствует доступ к таблице employee_wages");
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("our_employee.*")
            .Select("o.item_name as post_name")
            .Select("org.item_name as owner_name")
            .Join("organization as org", "org.id", "our_employee.owner_id")
            .LeftJoin("okpdtr as o", "o.id", "our_employee.post_id");
    }
}
