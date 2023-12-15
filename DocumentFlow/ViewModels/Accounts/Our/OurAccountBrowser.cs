//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

namespace DocumentFlow.ViewModels;

public class OurAccountBrowser : BaseAccountBrowser<OurAccount>, IOurAccountBrowser
{
    public OurAccountBrowser(IServiceProvider services, IOurAccountRepository repository, IConfiguration configuration) 
        : base(services, repository, configuration) 
    { 
    }
}
