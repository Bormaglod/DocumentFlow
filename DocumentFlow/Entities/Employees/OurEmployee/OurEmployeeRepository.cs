//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Employees;

public class OurEmployeeRepository : Repository<Guid, OurEmployee>, IOurEmployeeRepository
{
    public OurEmployeeRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
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
