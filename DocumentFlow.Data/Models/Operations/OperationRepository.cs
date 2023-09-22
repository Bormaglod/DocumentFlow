//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2021
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

using System.Data;

namespace DocumentFlow.Data.Models;

public class OperationRepository : DirectoryRepository<Operation>, IOperationRepository
{
    public OperationRepository(IDatabase database) : base(database)
    {
    }

    protected override bool CreatingAdjustedQuery() => true;

    protected override Operation GetAdjustedQuery(Guid id, IDbConnection connection)
    {
        var operationDictionary = new Dictionary<Guid, Operation>();

        string sql = @$"
            select 
                o.*, og.*, g.code, g.item_name 
            from operations o
                left join operation_goods og on og.owner_id = o.id 
                left join goods g on g.id = og.goods_id 
            where o.id = :id";

        return connection.Query<Operation, OperationGoods, Operation>(
            sql,
            (operation, goods) =>
            {
                if (!operationDictionary.TryGetValue(operation.Id, out var operationEntry))
                {
                    operationEntry = operation;
                    operationDictionary.Add(operationEntry.Id, operationEntry);
                }

                if (goods != null)
                {
                    operationEntry.OperationGoods.Add(goods);
                }

                return operationEntry;
            },
            new { id })
            .First();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter) => query.From("operations");
}
