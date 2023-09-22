//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

[Description("Выполненные работы")]
[EntityEditor(typeof(IOperationsPerformedEditor))]
public interface IOperationsPerformedNestedBrowser : IBrowser<OperationsPerformed>
{
}