//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class InitialBalanceContractorRepository : DocumentRepository<InitialBalanceContractor>, IInitialBalanceContractorRepository
{
    public InitialBalanceContractorRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<InitialBalanceContractor> GetByContractor(Guid? contractorId, BalanceCategory category)
    {
        return GetListUserDefined(callback: q => q
            .Where("reference_id", contractorId)
            .When(
                category == BalanceCategory.Debet, 
                q => q.Where("amount", ">", 0), 
                q => q.Where("amount", "<", 0))
            );
    }
    
    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        var p = new Query("posting_payments_balance")
            .Select("document_id")
            .SelectRaw("sum([transaction_amount]) as [transaction_amount]")
            .WhereTrue("carried_out")
            .GroupBy("document_id");

        return query
            .Select("initial_balance_contractor.*")
            .Select("c.item_name as contractor_name")
            .Select("contract.code as contract_number")
            .Select("contract.item_name as contract_name")
            .Select("contract.document_date as contract_date")
            .Select("p.transaction_amount as paid")
            .Join("contractor as c", "c.id", "initial_balance_contractor.reference_id")
            .Join("contract", "contract.id", "initial_balance_contractor.contract_id")
            .LeftJoin(p.As("p"), j => j.On("p.document_id", "initial_balance_contractor.id"));
    }
}
