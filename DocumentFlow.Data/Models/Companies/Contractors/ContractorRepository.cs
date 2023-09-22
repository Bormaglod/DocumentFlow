//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using Humanizer;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Data.Models;

public class ContractorRepository : DirectoryRepository<Contractor>, IContractorRepository
{
    public ContractorRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<Contractor> GetSuppliers() => GetContractorByType(ContractorType.Seller);

    public IReadOnlyList<Contractor> GetCustomers() => GetContractorByType(ContractorType.Buyer);

    private IReadOnlyList<Contractor> GetContractorByType(ContractorType type)
    {
        using var conn = GetConnection();
        return GetUserDefinedQuery(conn)
            .Distinct()
            .LeftJoin("contract as c", "c.owner_id", "contractor.id")
            .WhereFalse("contractor.deleted")
            .Where(q => q
                .WhereTrue("contractor.is_folder")
                .OrWhereRaw($"c.c_type = '{type.ToString().Underscore()}'::contractor_type")
            )
            .Get<Contractor>()
            .ToList();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("contractor.*")
            .Select("o.item_name as okopf_name")
            .LeftJoin("okopf as o", "o.id", "contractor.okopf_id");
    }
}
