//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Tools;
using DocumentFlow.Data.Models;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

[Description("Виды операций")]
[MenuItem(MenuDestination.Directory, "Виды производственных операций", order: 100)]
[EntityEditor(typeof(IOperationTypeEditor))]
public interface IOperationTypeBrowser : IBrowser<OperationType>
{
}
