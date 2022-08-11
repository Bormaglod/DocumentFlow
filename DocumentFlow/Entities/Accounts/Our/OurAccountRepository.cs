//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Accounts;

public class OurAccountRepository : OwnedRepository<Guid, OurAccount>, IOurAccountRepository
{
    public OurAccountRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("our_account.*")
            .Select("b.item_name as bank_name")
            .Select("c.item_name as company_name")
            .LeftJoin("bank as b", "b.id", "our_account.bank_id")
            .Join("company as c", "c.id", "our_account.owner_id");
    }
}
