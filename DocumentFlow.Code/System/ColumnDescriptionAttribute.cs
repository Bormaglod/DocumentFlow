﻿//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.10.2020
// Time: 12:04
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code.System
{
    public class ColumnDescriptionAttribute : Attribute
    {
        public ColumnDescriptionAttribute(string title) => Title = title;

        public string Title { get; }
    }
}
