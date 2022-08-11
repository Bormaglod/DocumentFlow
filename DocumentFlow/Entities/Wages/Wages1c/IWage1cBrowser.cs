//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.Wages;

[Menu(MenuDestination.Document, "Зар. плата 1С", 108010, "Зар. плата")]
public interface IWage1cBrowser : IBrowser<Wage1c>
{
}
