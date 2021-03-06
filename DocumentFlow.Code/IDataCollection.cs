﻿//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2020
// Time: 23:10
//-----------------------------------------------------------------------

namespace DocumentFlow.Code
{
    public interface IDataCollection
    {
        object this[string name] { get; set; }
    }
}
