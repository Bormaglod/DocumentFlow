//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

namespace DocumentFlow.Entities.Balances;

public class BalanceGoodsBrowser : BalanceProductBrowser<BalanceGoods>, IBalanceGoodsBrowser
{
    public BalanceGoodsBrowser(IBalanceGoodsRepository repository, IPageManager pageManager, IStandaloneSettings settings) : base(repository, pageManager, settings: settings) { }
}
