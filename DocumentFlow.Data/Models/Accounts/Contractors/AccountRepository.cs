//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class AccountRepository : OwnedRepository<Guid, Account>, IAccountRepository
{
    public AccountRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("account.*")
            .Select("b.item_name as bank_name")
            .Select("c.item_name as company_name")
            .LeftJoin("bank as b", "b.id", "account.bank_id")
            .Join("company as c", "c.id", "account.owner_id");
    }
}
