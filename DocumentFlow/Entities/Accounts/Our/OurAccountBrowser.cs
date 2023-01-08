//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

namespace DocumentFlow.Entities.Accounts;

public class OurAccountBrowser : BaseAccountBrowser<OurAccount>, IOurAccountBrowser
{
    public OurAccountBrowser(IOurAccountRepository repository, IPageManager pageManager, IStandaloneSettings settings) : base(repository, pageManager, settings: settings) { }

    protected override string HeaderText => "Расчётные счёта";
}
