//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[MenuItem(MenuDestination.Document, "Начисление зар. платы", parent: "Зар. плата", order: 40 )]
[EntityEditor(typeof(IGrossPayrollEditor))]
public interface IGrossPayrollBrowser : IBrowser<GrossPayroll>
{
}
