//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

namespace DocumentFlow.ViewModels;

public class OrgEmployeeBrowser : BaseEmployeeBrowser<OurEmployee>, IOrgEmployeeBrowser
{
    public OrgEmployeeBrowser(IServiceProvider services, IOurEmployeeRepository repository, IConfiguration configuration) 
        : base(services, repository, configuration) 
    {
        ToolBar.SmallIcons();
    }
}
