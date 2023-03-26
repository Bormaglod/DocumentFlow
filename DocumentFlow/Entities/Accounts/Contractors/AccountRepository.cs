//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Accounts;

public class AccountRepository : OwnedRepository<Guid, Account>, IAccountRepository
{
    public AccountRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.ParentId);
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
