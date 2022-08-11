//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceContractorRepository : DocumentRepository<InitialBalanceContractor>, IInitialBalanceContractorRepository
{
    public InitialBalanceContractorRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("initial_balance_contractor.*")
            .Select("c.item_name as contractor_name")
            .Select("contract.code as contract_number")
            .Select("contract.item_name as contract_name")
            .Select("contract.document_date as contract_date")
            .Join("contractor as c", "c.id", "initial_balance_contractor.reference_id")
            .Join("contract", "contract.id", "initial_balance_contractor.contract_id");
    }
}
