//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Data.Models;

public class PriceApprovalRepository : OwnedRepository<long, PriceApproval>, IPriceApprovalRepository
{
    public PriceApprovalRepository(IDatabase database) : base(database)
    {
    }

    public PriceApproval? GetPrice(ContractApplication contractApplication, Product product)
    {
        using var conn = GetConnection();
        var query = GetUserDefinedQuery(conn)
            .Where("price_approval.owner_id", contractApplication.Id)
            .Where("price_approval.product_id", product.Id);
        return query.FirstOrDefault<PriceApproval>();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
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
