//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.Balances.Initial;

[Menu(MenuDestination.Document, "Нач. остатки", 104510, "Расчёты с контрагентами")]
internal interface IInitialBalanceContractorBrowser : IBrowser<InitialBalanceContractor>
{
}
