//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;
using SqlKata.Execution;

using System.Data;

namespace DocumentFlow.Data.Models;

public class ContractApplicationRepository : OwnedRepository<Guid, ContractApplication>, IContractApplicationRepository
{
    public ContractApplicationRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<ContractApplication> GetCurrents(Contract contract)
    {
        using var conn = GetConnection();
        var query = GetUserDefinedQuery(conn)
            .Where("c.id", contract.Id)
            .WhereDate("contract_application.date_start", "<=", DateTime.Now)
            .Where(q => q
                .WhereNull("contract_application.date_end")
                .Or()
                .WhereDate("contract_application.date_end", ">=", DateTime.Now));
        return query.Get<ContractApplication>().ToList();
    }

    protected override bool CreatingAdjustedQuery() => true;

    protected override ContractApplication GetAdjustedQuery(Guid id, IDbConnection connection)
    {
        var appDictionary = new Dictionary<Guid, ContractApplication>();

        string sql = @$"
            select 
                ca.*, pa.*, p.item_name as product_name, m.abbreviation as measurement_name
            from contract_application ca
                left join price_approval pa on pa.owner_id = ca.id 
                left join product p on p.id = pa.product_id 
                left join measurement m on m.id = p.measurement_id
            where ca.id = :id";

        return connection.Query<ContractApplication, PriceApproval, ContractApplication>(
            sql,
            (app, price) =>
            {
                if (!appDictionary.TryGetValue(app.Id, out var appEntry))
                {
                    appEntry = app;
                    appDictionary.Add(appEntry.Id, appEntry);
                }

                if (price != null)
                {
                    appEntry.PriceApprovals.Add(price);
                }

                return appEntry;
            },
            new { id })
            .First();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("contract_application.*")
            .Select("c.item_name as contract_name")
            .LeftJoin("contract as c", "c.id", "contract_application.owner_id");
    }

    protected override void CopyNestedRows(IDbConnection connection, ContractApplication from, ContractApplication to, IDbTransaction? transaction = null)
    {
        base.CopyNestedRows(connection, from, to, transaction);
        
        var sql = "insert into price_approval (owner_id, product_id, price) select :id_to, product_id, price from price_approval where owner_id = :id_from";

        connection.Execute(sql, new { id_to = to.Id, id_from = from.Id }, transaction: transaction);
    }
}
