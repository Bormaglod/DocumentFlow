//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[MenuItem(MenuDestination.Document, "Корректировка остатков", parent: "Склад", order: 30 )]
[EntityEditor(typeof(IAdjustingBalancesEditor))]
public interface IAdjustingBalancesBrowser : IBrowser<AdjustingBalances>
{
}
