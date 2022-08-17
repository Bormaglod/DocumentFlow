//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.02.2022
//
// Версия 2022.8.17
//  - в выборку добавлено поле m.code
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Waybills;

public class WaybillReceiptPriceRepository : OwnedRepository<long, WaybillReceiptPrice>, IWaybillReceiptPriceRepository
{
    public WaybillReceiptPriceRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("waybill_receipt_price.*")
            .Select("m.item_name as product_name")
            .Select("m.code as code")
            .Join("material as m", "m.id", "waybill_receipt_price.reference_id");
    }
}
