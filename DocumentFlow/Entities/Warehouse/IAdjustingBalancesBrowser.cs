//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.Warehouse;

[Menu(MenuDestination.Document, "Корректировка остатков", 101520, "Склад")]
public interface IAdjustingBalancesBrowser : IBrowser<AdjustingBalances>
{
}
