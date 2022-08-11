//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.Balances.Initial;

[Menu(MenuDestination.Document, "Нач. остатки (материалы)", 101510, "Склад")]
internal interface IInitialBalanceMaterialBrowser : IBrowser<InitialBalanceMaterial>
{
}
