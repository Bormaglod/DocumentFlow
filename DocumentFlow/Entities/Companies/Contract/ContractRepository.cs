//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Companies;

public class ContractRepository : OwnedRepository<Guid, Contract>, IContractRepository
{
    public ContractRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<Contract> GetSuppliers(Guid supplier)
    {
        return GetByOwner(supplier, callback: q => q.WhereRaw("contract.c_type = 'seller'::contractor_type"));
    }

    public IReadOnlyList<Contract> GetCustomers(Guid customer)
    {
        return GetByOwner(customer, callback: q => q.WhereRaw("contract.c_type = 'buyer'::contractor_type"));
    }

    public IReadOnlyList<Contract> Get(Contractor contractor) => GetByOwner(contractor.id, callback: q => q.WhereFalse("contract.deleted"));

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("contract.*")
            .Select("c.item_name as contractor_name")
            .Select("e.item_name as signatory_name")
            .SelectRaw("coalesce(o1.signatory_name, o1.item_name) as signatory_post")
            .Select("oe.item_name as org_signatory_name")
            .SelectRaw("coalesce(o2.signatory_name, o2.item_name) as org_signatory_post")
            .LeftJoin("contractor as c", "c.id", "contract.owner_id")
            .LeftJoin("employee as e", "e.id", "contract.signatory_id")
            .LeftJoin("okpdtr as o1", "o1.id", "e.post_id")
            .LeftJoin("our_employee as oe", "oe.id", "contract.org_signatory_id")
            .LeftJoin("okpdtr as o2", "o2.id", "oe.post_id");
    }
}
