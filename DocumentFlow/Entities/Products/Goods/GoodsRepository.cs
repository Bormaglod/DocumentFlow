//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2022.8.21
//  - поскольку свойство code не допускает значения null, присвивание
//    этому поля данного значения в не имеет смысла
// Версия 2022.8.25
//  - процедура CopyChilds заменена на процедуру CopyNestedRows
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Calculations;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using SqlKata;

using System.Data;

namespace DocumentFlow.Entities.Products;

public class GoodsRepository : ProductRepository<Goods>, IGoodsRepository
{
    public GoodsRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.OwnerId);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        query = query
            .Select("goods.*")
            .Select("m.abbreviation as measurement_name")
            .SelectRaw("(exists (select 1 from document_refs dr where ((dr.owner_id = goods.id) and (dr.thumbnail IS not null)))) AS thumbnails")
            .LeftJoin("measurement as m", "goods.measurement_id", "m.id");

        if (HasPrivilege("calculation", Privilege.Select))
        {
            Query pb = new Query("balance_product")
                .Select("reference_id")
                .SelectRaw("sum(amount) as product_balance")
                .GroupBy("reference_id");

            query = query
                .Select("c.cost_price")
                .Select("c.profit_percent")
                .Select("c.profit_value")
                .Select("pb.product_balance")
                .Select("c.date_approval")
                .LeftJoin("calculation as c", "goods.calculation_id", "c.id")
                .LeftJoin(
                    pb.As("pb"),
                    q => q.On("pb.reference_id", "goods.id"));
        }

        return query;
    }

    protected override void CopyNestedRows(Goods from, Goods to, IDbTransaction transaction)
    {
        base.CopyNestedRows(from, to, transaction);

        if (transaction.Connection == null)
        {
            return;
        }

        if (from.CalculationId != null)
        {
            var calcRepo = Services.Provider.GetService<ICalculationRepository>();
            if (calcRepo == null)
            {
                return;
            }

            var calc = calcRepo.GetById(from.CalculationId.Value, transaction.Connection, false);
            calc.OwnerId = to.Id;
            calc.CalculationState = CalculationState.Prepare;

            calcRepo.CopyFrom(calc, transaction);

            to.CalculationId = null;
            Update(to, transaction);
        }
    }
}
