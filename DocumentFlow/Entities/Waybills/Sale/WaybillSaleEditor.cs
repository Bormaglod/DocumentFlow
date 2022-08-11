//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Waybills;

public class WaybillSaleEditor : WaybillEditor<WaybillSale, WaybillSalePrice>, IWaybillSaleEditor
{
    public WaybillSaleEditor(IWaybillSaleRepository repository, IPageManager pageManager) : base(repository, pageManager) { }

    protected override IOwnedRepository<long, WaybillSalePrice> GetDetailsRepository()
    {
        return Services.Provider.GetService<IWaybillSalePriceRepository>()!;
    }
}
