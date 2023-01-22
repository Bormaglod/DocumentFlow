//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.02.2022
//
// Версия 2022.8.17
//  - в выборку добавлено поле m.code
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Waybills;

public class WaybillSalePriceRepository : OwnedRepository<long, WaybillSalePrice>, IWaybillSalePriceRepository
{
    public WaybillSalePriceRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("waybill_sale_price.*")
            .Select("p.item_name as product_name")
            .Select("p.code as code")
            .SelectRaw("p.tableoid::regclass::varchar as [table_name]")
            .Join("product as p", "p.id", "waybill_sale_price.reference_id");
    }
}
