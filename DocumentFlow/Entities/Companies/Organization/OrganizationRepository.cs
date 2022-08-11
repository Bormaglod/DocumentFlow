//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Entities.Companies;

public class OrganizationRepository : Repository<Guid, Organization>, IOrganizationRepository
{
    public OrganizationRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
        ExcludeField(x => x.owner_id);
    }

    public Organization GetMain()
    {
        using var conn = Database.OpenConnection();
        return GetBaseQuery(conn)
            .WhereTrue("default_org")
            .Get<Organization>()
            .First();
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("organization.*")
            .Select("o.item_name as okopf_name")
            .LeftJoin("okopf as o", "o.id", "organization.okopf_id");
    }
}
