//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using SqlKata;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace DocumentFlow.Data.Models;

public class GoodsRepository : ProductRepository<Goods>, IGoodsRepository
{
    private readonly IServiceProvider services;

    public GoodsRepository(IDatabase database, IServiceProvider services) : base(database) 
    { 
        this.services = services;
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        query = query
            .SelectRaw("(exists (select 1 from document_refs dr where ((dr.owner_id = goods.id) and (dr.thumbnail IS not null)))) AS thumbnails");

        if (CurrentDatabase.HasPrivilege("calculation", Privilege.Select))
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

    protected override Query GetQuery(Query query)
    {
        return query
            .Select("goods.*")
            .Select("m.abbreviation as measurement_name")
            .LeftJoin("measurement as m", "m.id", "goods.measurement_id");
    }

    protected override void CopyNestedRows(IDbConnection connection, Goods from, Goods to, IDbTransaction? transaction = null)
    {
        base.CopyNestedRows(connection, from, to, transaction);

        if (from.CalculationId != null)
        {
            var calcRepo = services.GetRequiredService<ICalculationRepository>();

            var calc = calcRepo.Get(connection, from.CalculationId.Value, false);
            calc.OwnerId = to.Id;
            calc.CalculationState = CalculationState.Prepare;

            calcRepo.CopyFrom(connection, calc, transaction);

            to.CalculationId = null;
            Update(connection, to, transaction);
        }
    }
}
