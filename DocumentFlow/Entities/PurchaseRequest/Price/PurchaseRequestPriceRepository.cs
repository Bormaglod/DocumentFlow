//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.02.2022
//
// Версия 2022.12.11
//  - в выборку добавлено поле m.code
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

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
            .Select("m.code as code")
            .Join("material as m", "m.id", "purchase_request_price.reference_id");
    }
}
