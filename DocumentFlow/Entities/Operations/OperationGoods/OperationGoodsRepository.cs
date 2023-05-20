//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2023
//
// Версия 2023.5.20
//  - изменены наименования полей code и item_name
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Operations;

public class OperationGoodsRepository : OwnedRepository<long, OperationGoods>, IOperationGoodsRepository
{
    public OperationGoodsRepository(IDatabase database) : base(database) { }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("operation_goods.*")
            .Select("g.code as code")
            .Select("g.item_name as name")
            .Join("goods as g", "g.id", "operation_goods.goods_id");
    }
}
