﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[MenuItem(MenuDestination.Document, "Готовая продукция", parent: "Производство", order: 40)]
[EntityEditor(typeof(IFinishedGoodsEditor))]
public interface IFinishedGoodsBrowser : IBrowser<FinishedGoods>
{
}