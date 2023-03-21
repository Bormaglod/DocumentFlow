//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.01.2022
//
// Версия 2022.8.19
//  - добавлен метод GetPrice
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Data;

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
            .Where("price_approval.owner_id", contractApplication.Id)
            .Where("price_approval.product_id", product.Id);
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
