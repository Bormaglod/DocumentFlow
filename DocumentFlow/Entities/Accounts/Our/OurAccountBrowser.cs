//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Accounts;

public class OurAccountBrowser : BaseAccountBrowser<OurAccount>, IOurAccountBrowser
{
    public OurAccountBrowser(IOurAccountRepository repository, IPageManager pageManager) : base(repository, pageManager) { }

    protected override string HeaderText => "Расчётные счёта";
}
