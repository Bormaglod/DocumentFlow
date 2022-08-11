//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Companies;

public class PriceApprovalRepository : OwnedRepository<long, PriceApproval>, IPriceApprovalRepository
{
    public PriceApprovalRepository(IDatabase database) : base(database)
    {
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
