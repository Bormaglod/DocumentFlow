//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Entities.Wages;

[Menu(MenuDestination.Document, "Платёжная ведомость", 108900, "Зар. плата")]
public interface IPayrollBrowser : IBrowser<Payroll>
{
}
