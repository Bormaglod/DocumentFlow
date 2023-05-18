//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//
// Версия 2022.8.19
//  - добавлен метод GetCurrents
// Версия 2022.8.25
//  - процедура CopyChilds заменена на процедуру CopyNestedRows
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;
using SqlKata.Execution;

using System.Data;

namespace DocumentFlow.Entities.Companies;

public class ContractApplicationRepository : OwnedRepository<Guid, ContractApplication>, IContractApplicationRepository
{
    public ContractApplicationRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<ContractApplication> GetCurrents(Contract contract)
    {
        using var conn = Database.OpenConnection();
        var query = GetUserDefinedQuery(conn)
            .Where("c.id", contract.Id)
            .WhereDate("contract_application.date_start", "<=", DateTime.Now)
            .Where(q => q
                .WhereNull("contract_application.date_end")
                .Or()
                .WhereDate("contract_application.date_end", ">=", DateTime.Now));
        return query.Get<ContractApplication>().ToList();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("contract_application.*")
            .Select("c.item_name as contract_name")
            .LeftJoin("contract as c", "c.id", "contract_application.owner_id");
    }

    protected override void CopyNestedRows(ContractApplication from, ContractApplication to, IDbTransaction transaction)
    {
        base.CopyNestedRows(from, to, transaction);
        
        var sql = "insert into price_approval (owner_id, product_id, price) select :id_to, product_id, price from price_approval where owner_id = :id_from";

        transaction.Connection?.Execute(sql, new { id_to = to.Id, id_from = from.Id }, transaction: transaction);
    }
}
