//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Entities.Wages;

[Menu(MenuDestination.Document, "Выплата зар. платы", parent: "Зар. плата", order: 60 )]
public interface IPayrollPaymentBrowser : IBrowser<PayrollPayment>
{
}
