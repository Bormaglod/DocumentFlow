//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.07.2022
//
// Версия 2022.12.7
//  - в выборку добавлено поле material.code
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Productions.Processing;

public class WaybillProcessingPriceRepository : OwnedRepository<long, WaybillProcessingPrice>, IWaybillProcessingPriceRepository
{
    public WaybillProcessingPriceRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("waybill_processing_price.*")
            .Select("m.item_name as product_name")
            .Select("m.code")
            .Join("material as m", "m.id", "waybill_processing_price.reference_id");
    }
}
