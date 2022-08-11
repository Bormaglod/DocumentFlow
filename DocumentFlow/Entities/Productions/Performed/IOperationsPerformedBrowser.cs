//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.Productions.Performed;

[Menu(MenuDestination.Document, "Выполненные работы", 102030, "Производство")]
public interface IOperationsPerformedBrowser : IBrowser<OperationsPerformed>
{
}
