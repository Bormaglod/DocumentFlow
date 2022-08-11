//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

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
