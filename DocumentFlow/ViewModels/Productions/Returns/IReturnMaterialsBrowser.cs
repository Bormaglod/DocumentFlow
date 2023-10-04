//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[MenuItem(MenuDestination.Document, "Возврат материалов заказчику", parent: "Производство", order: 60 )]
[EntityEditor(typeof(IReturnMaterialsEditor))]
public interface IReturnMaterialsBrowser : IBrowser<ReturnMaterials>
{
}
