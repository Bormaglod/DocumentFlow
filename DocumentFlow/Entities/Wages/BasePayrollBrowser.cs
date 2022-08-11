//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Entities.Wages;

public abstract class BasePayrollBrowser<T> : BillingDocumentBrowser<T>
    where T : BasePayroll
{
    public BasePayrollBrowser(IDocumentRepository<T> repository, IPageManager pageManager, IDocumentFilter filter)
        : base(repository, pageManager, filter: filter)
    {
        AddColumns(new GridColumn[] 
        {
            CreateCurrency(x => x.wage, "Зар. плата", width: 100)
        });
    }
}
