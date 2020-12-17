﻿//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.12.2020
// Time: 21:46
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace DocumentFlow
{
    public interface ICommandExecutor
    {
        void Execute();
        IDictionary<string, object> GetParameters();
    }
}
