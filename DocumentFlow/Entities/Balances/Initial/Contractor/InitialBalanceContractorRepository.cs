//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.06.2022
//
// Версия 2022.12.18
//  - добавлен метод GetByContractor
//  - метод GetDefaultQuery изменен для получения значения поля paid
//
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

    public IReadOnlyList<InitialBalanceContractor> GetByContractor(Guid? contractorId) => GetAllDefault(callback: q => q.Where("reference_id", contractorId));
    
    protected override Query GetDefaultQuery(Query query, IFilter? filter)
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
