//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Entities.Wages;

public abstract class BasePayrollBrowser<T> : BillingDocumentBrowser<T>
    where T : BasePayroll
{
    public BasePayrollBrowser(IDocumentRepository<T> repository, IPageManager pageManager, IDocumentFilter? filter = null, IStandaloneSettings? settings = null)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        AddColumns(new GridColumn[] 
        {
            CreateCurrency(x => x.wage, "Зар. плата", width: 100)
        });
    }
}
