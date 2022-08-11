//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Waybills;

public class WaybillSaleBrowser : WaybillBrowser<WaybillSale>, IWaybillSaleBrowser
{
    public WaybillSaleBrowser(IWaybillSaleRepository repository, IPageManager pageManager, IDocumentFilter filter)
        : base(repository, pageManager, filter: filter)
    {
    }

    protected override string HeaderText => "Реализация";
}
