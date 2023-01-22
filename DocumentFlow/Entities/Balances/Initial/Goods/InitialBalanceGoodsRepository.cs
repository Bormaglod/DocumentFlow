//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.07.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceGoodsRepository : DocumentRepository<InitialBalanceGoods>, IInitialBalanceGoodsRepository
{
    public InitialBalanceGoodsRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("initial_balance_goods.*")
            .Select("g.code as goods_code")
            .Select("g.item_name as goods_name")
            .Join("goods as g", "g.id", "initial_balance_goods.reference_id")
            .OrderBy("g.code");
    }
}
