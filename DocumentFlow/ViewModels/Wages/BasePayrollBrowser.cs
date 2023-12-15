//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.ViewModels;

public abstract class BasePayrollBrowser<T> : BillingDocumentBrowser<T>
    where T : BasePayroll
{
    public BasePayrollBrowser(IServiceProvider services, IDocumentRepository<T> repository, IConfiguration configuration, IDocumentFilter? filter = null)
        : base(services, repository, configuration, filter: filter)
    {
        AddColumns(new GridColumn[] 
        {
            CreateCurrency(x => x.Wage, "Зар. плата", width: 100)
        });
    }
}
