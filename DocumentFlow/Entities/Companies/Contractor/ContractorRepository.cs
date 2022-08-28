//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2022.8.28
//  - параметр в процедуре GetContractorByType заменен на тип ContractorType
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using Humanizer;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Entities.Companies;

public class ContractorRepository : DirectoryRepository<Contractor>, IContractorRepository
{
    public ContractorRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.owner_id);
    }

    public IReadOnlyList<Contractor> GetSuppliers() => GetContractorByType(ContractorType.Seller);

    public IReadOnlyList<Contractor> GetCustomers() => GetContractorByType(ContractorType.Buyer);

    private IReadOnlyList<Contractor> GetContractorByType(ContractorType type)
    {
        using var conn = Database.OpenConnection();
        return GetDefaultQuery(conn)
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

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("contractor.*")
            .Select("o.item_name as okopf_name")
            .LeftJoin("okopf as o", "o.id", "contractor.okopf_id");
    }
}
