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

public class AccountRepository : OwnedRepository<Guid, Account>, IAccountRepository
{
    public AccountRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("account.*")
            .Select("b.item_name as bank_name")
            .Select("c.item_name as company_name")
            .LeftJoin("bank as b", "b.id", "account.bank_id")
            .Join("company as c", "c.id", "account.owner_id");
    }
}
