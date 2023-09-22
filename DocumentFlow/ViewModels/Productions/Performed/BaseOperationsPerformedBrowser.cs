//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

namespace DocumentFlow.ViewModels;

public abstract class BaseOperationsPerformedBrowser : BrowserPage<OperationsPerformed>
{
    public BaseOperationsPerformedBrowser(IServiceProvider services, IPageManager pageManager, IOperationsPerformedRepository repository, IConfiguration configuration, IDocumentFilter? filter = null)
        : base(services, pageManager, repository, configuration, filter: filter)
    {
    }
}
