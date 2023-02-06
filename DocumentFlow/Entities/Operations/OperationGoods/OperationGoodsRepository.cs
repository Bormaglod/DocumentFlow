//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Operations;

public class OperationGoodsRepository : OwnedRepository<long, OperationGoods>, IOperationGoodsRepository
{
    public OperationGoodsRepository(IDatabase database) : base(database) { }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("operation_goods.*")
            .Select("g.code as goods_code")
            .Select("g.item_name as goods_name")
            .Join("goods as g", "g.id", "operation_goods.goods_id");
    }
}
