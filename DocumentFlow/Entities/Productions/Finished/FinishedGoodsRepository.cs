//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Productions.Finished;

public class FinishedGoodsRepository : DocumentRepository<FinishedGoods>, IFinishedGoodsRepository
{
    public FinishedGoodsRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("finished_goods.*")
            .Select("o.item_name as organization_name")
            .Select("pl.document_number as lot_number")
            .Select("pl.document_date as lot_date")
            .Select("g.item_name as goods_name")
            .Join("organization as o", "o.id", "finished_goods.organization_id")
            .Join("production_lot as pl", "pl.id", "finished_goods.owner_id")
            .Join("goods as g", "g.id", "finished_goods.goods_id");
    }
}
