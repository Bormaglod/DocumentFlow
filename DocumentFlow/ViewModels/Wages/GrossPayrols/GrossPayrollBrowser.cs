//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

namespace DocumentFlow.ViewModels;

public class GrossPayrollBrowser : BasePayrollBrowser<GrossPayroll>, IGrossPayrollBrowser
{
    public GrossPayrollBrowser(IServiceProvider services, IGrossPayrollRepository repository, IConfiguration configuration, IDocumentFilter filter)
        : base(services, repository, configuration, filter: filter)
    {
    }
}
