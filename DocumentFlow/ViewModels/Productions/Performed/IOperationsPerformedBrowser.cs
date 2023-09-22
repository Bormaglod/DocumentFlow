﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[MenuItem(MenuDestination.Document, "Выполненные работы", parent: "Производство", order: 30)]
[EntityEditor(typeof(IOperationsPerformedEditor))]
public interface IOperationsPerformedBrowser : IBrowser<OperationsPerformed>
{
}