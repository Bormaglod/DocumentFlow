//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2022.8.28
//  - параметр в процедуре GetContractorByType заменен на тип ContractorType
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using Humanizer;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Entities.Companies;

public class ContractorRepository : DirectoryRepository<Contractor>, IContractorRepository
{
    public ContractorRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.OwnerId);
    }

    public IReadOnlyList<Contractor> GetSuppliers() => GetContractorByType(ContractorType.Seller);

    public IReadOnlyList<Contractor> GetCustomers() => GetContractorByType(ContractorType.Buyer);

    private IReadOnlyList<Contractor> GetContractorByType(ContractorType type)
    {
        using var conn = Database.OpenConnection();
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
