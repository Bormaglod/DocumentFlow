//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[MenuItem(MenuDestination.Document, "Нач. остатки", parent: "Расчёты с контрагентами", order: 10)]
[EntityEditor(typeof(IInitialBalanceContractorEditor))]
public interface IInitialBalanceContractorBrowser : IBrowser<InitialBalanceContractor>
{
}
