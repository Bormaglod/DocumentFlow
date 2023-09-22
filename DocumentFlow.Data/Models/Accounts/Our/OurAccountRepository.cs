//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repository;
using DocumentFlow.Data.Interfaces;

using SqlKata;
using DocumentFlow.Data.Interfaces.Filters;

namespace DocumentFlow.Data.Models;

public class OurAccountRepository : OwnedRepository<Guid, OurAccount>, IOurAccountRepository
{
    public OurAccountRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("our_account.*")
            .Select("b.item_name as bank_name")
            .Select("c.item_name as company_name")
            .LeftJoin("bank as b", "b.id", "our_account.bank_id")
            .Join("company as c", "c.id", "our_account.owner_id");
    }
}
