//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public class PurchaseRequestPriceRepository : OwnedRepository<long, PurchaseRequestPrice>, IPurchaseRequestPriceRepository
{
    public PurchaseRequestPriceRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("purchase_request_price.*")
            .Select("m.item_name as product_name")
            .Join("material as m", "m.id", "purchase_request_price.reference_id");
    }
}
