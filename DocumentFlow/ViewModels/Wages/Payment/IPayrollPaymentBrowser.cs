//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[MenuItem(MenuDestination.Document, "Выплата зар. платы", parent: "Зар. плата", order: 60 )]
[EntityEditor(typeof(IPayrollPaymentEditor))]
public interface IPayrollPaymentBrowser : IBrowser<PayrollPayment>
{
}
