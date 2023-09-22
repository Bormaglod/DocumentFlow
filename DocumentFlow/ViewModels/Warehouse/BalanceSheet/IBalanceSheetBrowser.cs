//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[MenuItem(MenuDestination.Document, "Материальный отчёт", parent: "Склад", order: 40 )]
public interface IBalanceSheetBrowser : IBrowser<BalanceSheet>
{
}
