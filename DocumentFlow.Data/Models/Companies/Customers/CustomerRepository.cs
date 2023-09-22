//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class CustomerRepository : OwnedRepository<Guid, Customer>, ICustomerRepository
{
    public CustomerRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetQueryOwner(Query query, Guid owner_id)
    {
        Query customer = new Query("contractor")
            .Select("contractor.*")
            .SelectRaw("row_number() over(partition by [contractor].[id] order by [ca].[date_start] desc) as [rn]")
            .Select("ca.code as doc_number", "ca.item_name as doc_name", "ca.date_start as date_start", "pa.price")
            .Select("c.id as contract_id")
            .Select("ca.id as application_id")
            .Join("contract as c", "c.owner_id", "contractor.id")
            .Join("contract_application as ca", "ca.owner_id", "c.id")
            .Join("price_approval as pa", "pa.owner_id", "ca.id")
            .WhereRaw("coalesce(c.date_end, ca.date_end) is null")
            .Where("pa.product_id", owner_id);

        return query
            .With("customer", customer)
            .Where("rn", 1);
    }
}
