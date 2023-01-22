//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Productions.Order;

public class ProductionOrderPriceRepository : OwnedRepository<long, ProductionOrderPrice>, IProductionOrderPriceRepository
{
    public ProductionOrderPriceRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("production_order_price.*")
            .Select("g.item_name as product_name")
            .Select("c.code as calculation_name")
            .Join("goods as g", "g.id", "production_order_price.reference_id")
            .Join("calculation as c", "c.id", "production_order_price.calculation_id");
    }
}
