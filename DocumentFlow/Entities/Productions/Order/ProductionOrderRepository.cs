//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//
// Версия 2022.9.2
//  - доработан метод GetDefaultQuery - если поле item_name таблицы
//    conttactor содержит null, то будет возвращено значение поля code
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using SqlKata;

namespace DocumentFlow.Entities.Productions.Order;

public class ProductionOrderRepository : DocumentRepository<ProductionOrder>, IProductionOrderRepository
{
    public ProductionOrderRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.owner_id);
    }

    public IReadOnlyList<ProductionOrderPrice> GetList(ProductionOrder order)
    {
        var repo = Services.Provider.GetService<IProductionOrderPriceRepository>();
        return repo!.GetByOwner(order.id);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        var d = new Query("production_order_price")
            .Select("owner_id")
            .SelectRaw("sum([product_cost]) as [cost_order]")
            .SelectRaw("sum([tax_value]) as [tax_value]")
            .SelectRaw("sum([full_cost]) as [full_cost]")
            .GroupBy("owner_id");

        return query
            .Select("production_order.*")
            .Select("o.item_name as organization_name")
            .SelectRaw("case when c.item_name is null then c.code else c.item_name end as contractor_name")
            .Select("contract.tax_payer")
            .SelectRaw("case [contract].[tax_payer] when true then 20 else 0 end as [tax]")
            .Select("contract.item_name as contract_name")
            .Select("d.*")
            .Join("organization as o", "o.id", "production_order.organization_id")
            .Join("contractor as c", "c.id", "production_order.contractor_id")
            .LeftJoin("contract", "contract.id", "production_order.contract_id")
            .LeftJoin(d.As("d"), j => j.On("d.owner_id", "production_order.id"));
    }
}
