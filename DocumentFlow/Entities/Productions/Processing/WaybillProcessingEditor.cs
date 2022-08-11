//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Waybills;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Productions.Processing;

public class WaybillProcessingEditor : WaybillEditor<WaybillProcessing, WaybillProcessingPrice>, IWaybillProcessingEditor
{
    public WaybillProcessingEditor(IWaybillProcessingRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    { 
        
    }

    protected override IOwnedRepository<long, WaybillProcessingPrice> GetDetailsRepository()
    {
        return Services.Provider.GetService<IWaybillProcessingPriceRepository>()!;
    }
}
