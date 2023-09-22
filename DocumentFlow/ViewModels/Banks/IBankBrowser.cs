//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Tools;
using DocumentFlow.Data.Models;

namespace DocumentFlow.ViewModels;

[MenuItem(MenuDestination.Directory, "Банки", order: 40)]
[EntityEditor(typeof(IBankEditor))]
public interface IBankBrowser : IBrowser<Bank>
{
}
