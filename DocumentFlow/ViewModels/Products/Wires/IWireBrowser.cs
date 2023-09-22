//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Tools;
using DocumentFlow.Data.Models;

namespace DocumentFlow.ViewModels;

[MenuItem(MenuDestination.Directory, "Типы проводов", parent: "Номенклатура", order: 10)]
[EntityEditor(typeof(IWireEditor))]
public interface IWireBrowser : IBrowser<Wire>
{
}
