﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

[Description("Удержания")]
[EntityEditor(typeof(ICalculationDeductionEditor))]
public interface ICalculationDeductionBrowser : IBrowser<CalculationDeduction>
{
}