﻿//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2020
// Time: 23:31
//-----------------------------------------------------------------------

namespace DocumentFlow.Code
{
    public interface IControlCollection<T>
    {
        T this[string name] { get; }
    }
}
