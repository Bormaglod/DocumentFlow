//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.Equipments;

[Menu(MenuDestination.Directory, "Оборудование", 212000)]
public interface IEquipmentBrowser : IBrowser<Equipment>
{
}
