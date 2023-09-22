//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class InitialBalanceGoodsRepository : DocumentRepository<InitialBalanceGoods>, IInitialBalanceGoodsRepository
{
    public InitialBalanceGoodsRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("initial_balance_goods.*")
            .Select("g.code as goods_code")
            .Select("g.item_name as goods_name")
            .Join("goods as g", "g.id", "initial_balance_goods.reference_id")
            .OrderBy("g.code");
    }
}
