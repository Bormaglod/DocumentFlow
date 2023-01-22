//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.12.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Entities.PaymentOrders;

[Menu(MenuDestination.Document, "Корректировка долга", 104530, "Расчёты с контрагентами")]
public interface IDebtAdjustmentBrowser : IBrowser<DebtAdjustment>
{
}
