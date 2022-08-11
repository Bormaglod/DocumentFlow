//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Productions.Lot;

public class BaseProductionLotBrowser : Browser<ProductionLot>
{
    public BaseProductionLotBrowser(IProductionLotRepository repository, IPageManager pageManager, IDocumentFilter? filter = null) 
        : base(repository, pageManager, filter: filter)
    {
    }

    protected override string HeaderText => "Партия";
}
