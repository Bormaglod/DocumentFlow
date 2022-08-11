//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Balances;

public class BalanceMaterialBrowser : BalanceProductBrowser<BalanceMaterial>, IBalanceMaterialBrowser
{
    public BalanceMaterialBrowser(IBalanceMaterialRepository repository, IPageManager pageManager) : base(repository, pageManager) { }
}
