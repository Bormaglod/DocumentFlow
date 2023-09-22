//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repository;
using DocumentFlow.Data.Interfaces;

using SqlKata;
using SqlKata.Execution;
using DocumentFlow.Data.Interfaces.Filters;

namespace DocumentFlow.Data.Models;

public class OrganizationRepository : Repository<Guid, Organization>, IOrganizationRepository
{
    public OrganizationRepository(IDatabase database) : base(database)
    {
    }

    public Organization GetMain()
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .WhereTrue("default_org")
            .Get<Organization>()
            .First();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("organization.*")
            .Select("o.item_name as okopf_name")
            .LeftJoin("okopf as o", "o.id", "organization.okopf_id");
    }
}
