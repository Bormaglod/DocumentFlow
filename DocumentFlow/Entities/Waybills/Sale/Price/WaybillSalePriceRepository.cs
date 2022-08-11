//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

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
            .SelectRaw("p.tableoid::regclass::varchar as [table_name]")
            .Join("product as p", "p.id", "waybill_sale_price.reference_id");
    }
}
