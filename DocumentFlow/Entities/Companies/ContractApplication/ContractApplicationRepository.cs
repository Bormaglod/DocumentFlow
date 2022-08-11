//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;
using Dapper;
using System.Data;

namespace DocumentFlow.Entities.Companies;

public class ContractApplicationRepository : OwnedRepository<Guid, ContractApplication>, IContractApplicationRepository
{
    public ContractApplicationRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("contract_application.*")
            .Select("c.item_name as contract_name")
            .LeftJoin("contract as c", "c.id", "contract_application.owner_id");
    }

    protected override void CopyChilds(ContractApplication from, ContractApplication to, IDbTransaction transaction)
    {
        base.CopyChilds(from, to, transaction);
        
        var sql = "insert into price_approval (owner_id, product_id, price) select :id_to, product_id, price from price_approval where owner_id = :id_from";

        var conn = transaction.Connection;
        conn.Execute(sql, new { id_to = to.id, id_from = from.id }, transaction: transaction);
    }
}
