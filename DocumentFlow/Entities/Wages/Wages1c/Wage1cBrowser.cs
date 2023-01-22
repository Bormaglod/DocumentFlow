//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.08.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Settings;

namespace DocumentFlow.Entities.Wages;

public class Wage1cBrowser : BasePayrollBrowser<Wage1c>, IWage1cBrowser
{
    public Wage1cBrowser(IWage1cRepository repository, IPageManager pageManager, IDocumentFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
    }

    protected override string HeaderText => "Зар. плата 1С";
}
