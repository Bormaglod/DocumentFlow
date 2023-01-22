//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.06.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Entities.Balances.Initial;

[Menu(MenuDestination.Document, "Нач. остатки", 104510, "Расчёты с контрагентами")]
internal interface IInitialBalanceContractorBrowser : IBrowser<InitialBalanceContractor>
{
}
