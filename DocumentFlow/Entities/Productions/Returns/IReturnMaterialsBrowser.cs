//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.Productions.Returns;

[Menu(MenuDestination.Document, "Возврат материалов заказчику", 102050, "Производство")]
public interface IReturnMaterialsBrowser : IBrowser<ReturnMaterials>
{
}
