//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.PaymentOrders;

[Menu(MenuDestination.Document, "Корректировка долга", 104530, "Расчёты с контрагентами")]
public interface IDebtAdjustmentBrowser : IBrowser<DebtAdjustment>
{
}
