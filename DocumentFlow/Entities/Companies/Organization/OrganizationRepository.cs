//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Entities.Companies;

public class OrganizationRepository : Repository<Guid, Organization>, IOrganizationRepository
{
    public OrganizationRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.ParentId);
        ExcludeField(x => x.OwnerId);
    }

    public Organization GetMain()
    {
        using var conn = Database.OpenConnection();
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
