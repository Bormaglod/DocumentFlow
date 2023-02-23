//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.2.23
//  - запрос GetDefaultQuery доработан с целью получения значения
//    свойства measurement_name
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

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
            .Select("m.abbreviation as measurement_name")
            .Join("organization as o", "o.id", "finished_goods.organization_id")
            .Join("production_lot as pl", "pl.id", "finished_goods.owner_id")
            .Join("goods as g", "g.id", "finished_goods.goods_id")
            .LeftJoin("measurement as m", "m.id", "g.measurement_id");
    }
}
