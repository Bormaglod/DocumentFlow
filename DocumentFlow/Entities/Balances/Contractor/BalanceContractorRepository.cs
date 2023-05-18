﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.12.6
//  - в выборку дабавлены поля contract.item_name, contract_code и
//    contract.document_date
// Версия 2022.12.30
//  - добавлен метод GetCustomersDebt
// Версия 2022.12.30
//  - добавлен метод GetSuppliersDebt
// Версия 2023.1.5
//  - в методе GetSuppliersDebt отрицательные значения изменены на
//    положительные
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Entities.Balances;

public class BalanceContractorRepository : OwnedRepository<Guid, BalanceContractor>, IBalanceContractorRepository
{
    public BalanceContractorRepository(IDatabase database) : base(database) { }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
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

    public IReadOnlyList<ContractorDebt> GetCustomersDebt(int limit = 0)
    {
        using var conn = Database.OpenConnection();

        var query = GetQuery(conn)
            .Select("c.id")
            .SelectRaw("c.code as contractor_name")
            .SelectRaw("sum(operation_summa * amount) as debt")
            .Join("contractor as c", "c.id", "reference_id")
            .GroupBy("c.id")
            .HavingRaw("sum(operation_summa * amount) > 0")
            .OrderByRaw("3 desc")
            .When(limit > 0, q => q.Limit(limit));

        return query.Get<ContractorDebt>().ToList();
    }

    public IReadOnlyList<ContractorDebt> GetSuppliersDebt(int limit = 0)
    {
        using var conn = Database.OpenConnection();

        var query = GetQuery(conn)
            .Select("c.id")
            .SelectRaw("c.code as contractor_name")
            .SelectRaw("abs(sum(operation_summa * amount)) as debt")
            .Join("contractor as c", "c.id", "reference_id")
            .GroupBy("c.id")
            .HavingRaw("sum(operation_summa * amount) < 0")
            .OrderByRaw("3 desc")
            .When(limit > 0, q => q.Limit(limit));

        return query.Get<ContractorDebt>().ToList();
    }

    protected override Query GetQueryOwner(Query query, Guid owner_id) => query.Where($"balance_contractor.reference_id", owner_id);
}
