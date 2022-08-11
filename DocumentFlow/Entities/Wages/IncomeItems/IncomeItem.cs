﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.Wages.IncomeItems;

public class IncomeItem : Identifier<Guid>, IItem
{
    public string? code { get; set; }
    public string? item_name { get; set; }
}
