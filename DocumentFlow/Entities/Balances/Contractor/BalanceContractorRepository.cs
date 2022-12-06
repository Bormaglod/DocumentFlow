//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.12.6
//  - в выборку дабавлены поля contract.item_name, contract_code и
//    contract.document_date
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Balances;

public class BalanceContractorRepository : OwnedRepository<Guid, BalanceContractor>, IBalanceContractorRepository
{
    public BalanceContractorRepository(IDatabase database) : base(database) { }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("balance_contractor.*")
            .SelectRaw("case when [amount] > 0 then [operation_summa] else null end as [contractor_debt]")
            .SelectRaw("case when [amount] < 0 then [operation_summa] else null end as [organization_debt]")
            .SelectRaw("sum([operation_summa] * sign([amount])) over (order by [balance_contractor].[document_date], [balance_contractor].[document_number]) as [debt]")
            .Select("dt.code as document_type_code")
            .Select("dt.document_name as document_type_name")
            .Select("c.item_name as contract_name")
            .Select("c.code as contract_number")
            .Select("c.document_date as contract_date")
            .Join("document_type as dt", "dt.id", "document_type_id")
            .LeftJoin("contract as c", "c.id", "balance_contractor.contract_id");
    }

    protected override Query GetQueryOwner(Query query, Guid owner_id) => query.Where($"balance_contractor.reference_id", owner_id);
}
