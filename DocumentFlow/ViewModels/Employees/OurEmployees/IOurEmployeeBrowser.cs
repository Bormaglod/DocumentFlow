﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Tools;
using DocumentFlow.Data.Models;

namespace DocumentFlow.ViewModels;

[EntityEditor(typeof(IOurEmployeeEditor))]
public interface IOurEmployeeBrowser : IBrowser<OurEmployee>
{
}
