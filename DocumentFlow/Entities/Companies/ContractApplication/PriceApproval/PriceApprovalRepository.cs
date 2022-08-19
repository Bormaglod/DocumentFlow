//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.01.2022
//
// Версия 2022.8.19
//  - добавлен метод GetPrice
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Products;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Entities.Companies;

public class PriceApprovalRepository : OwnedRepository<long, PriceApproval>, IPriceApprovalRepository
{
    public PriceApprovalRepository(IDatabase database) : base(database)
    {
    }

    public PriceApproval? GetPrice(ContractApplication contractApplication, Product product)
    {
        using var conn = Database.OpenConnection();
        var query = GetDefaultQuery(conn)
            .Where("price_approval.owner_id", contractApplication.id)
            .Where("price_approval.product_id", product.id);
        return query.FirstOrDefault<PriceApproval>();
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("price_approval.*")
            .Select("p.item_name as product_name")
            .Select("p.measurement_id")
            .Select("m.abbreviation as measurement_name")
            .LeftJoin("product as p", "p.id", "price_approval.product_id")
            .LeftJoin("measurement as m", "m.id", "p.measurement_id");
    }
}
