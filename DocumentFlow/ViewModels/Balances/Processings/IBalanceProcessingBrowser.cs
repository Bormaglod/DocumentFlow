﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

[Description("Остатки дав. сырья")]
public interface IBalanceProcessingBrowser : IBrowser<BalanceProcessing>
{
}