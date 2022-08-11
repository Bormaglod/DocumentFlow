//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.Balances.Initial;

[Menu(MenuDestination.Document, "Нач. остатки (продукция)", 101511, "Склад")]
internal interface IInitialBalanceGoodsBrowser : IBrowser<InitialBalanceGoods>
{
}