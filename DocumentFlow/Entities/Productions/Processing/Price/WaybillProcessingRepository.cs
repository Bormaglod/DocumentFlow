//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.07.2022
//
// Версия 2022.12.7
//  - в выборку добавлено поле material.code
// Версия 2023.1.17
//  - запрос изменён с учётом переноса количества списываемого давальческого
//    материала из таблицы waybill_processing_price в waybill_processing_writeoff
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Productions.Processing;

public class WaybillProcessingPriceRepository : OwnedRepository<long, WaybillProcessingPrice>, IWaybillProcessingPriceRepository
{
    public WaybillProcessingPriceRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        var wpp = new Query("waybill_processing_writeoff")
            .Select("waybill_processing_id")
            .Select("material_id")
            .SelectRaw("sum(amount) as written_off")
            .GroupBy("waybill_processing_id", "material_id");

        return query
            .Select("waybill_processing_price.*")
            .Select("m.item_name as product_name")
            .Select("m.code")
            .Select("wpp.written_off")
            .Join("material as m", "m.id", "waybill_processing_price.reference_id")
            .LeftJoin(wpp.As("wpp"), j => j
                .On("wpp.waybill_processing_id", "waybill_processing_price.owner_id")
                .On("wpp.material_id", "waybill_processing_price.reference_id"));
    }
}
