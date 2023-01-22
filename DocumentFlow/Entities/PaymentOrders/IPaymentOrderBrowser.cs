//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//
// Версия 2022.9.8
//  - пункт меню переехал в "Документы/Расчёты с контрагентами"
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Entities.PaymentOrders;

[Menu(MenuDestination.Document, "Платежи", 104520, "Расчёты с контрагентами")]
public interface IPaymentOrderBrowser : IBrowser<PaymentOrder>
{
}
