//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[EntityEditor(typeof(IInitialBalanceEmployeeBrowser))]
public interface IInitialBalanceEmployeeBrowser : IBrowser<InitialBalanceEmployee>
{
}
