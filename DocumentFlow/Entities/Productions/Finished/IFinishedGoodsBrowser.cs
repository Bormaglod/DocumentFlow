//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.Productions.Finished;

[Menu(MenuDestination.Document, "Готовая продукция", 102035, "Производство")]
public interface IFinishedGoodsBrowser : IBrowser<FinishedGoods>
{
}
