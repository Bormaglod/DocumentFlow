//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.04.2020
// Time: 20:06
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    using System;

    public class ParameterNotFoundException : Exception
    {
        public ParameterNotFoundException(string parameterName) : base($"В запросе используется неизвестный параметр '{parameterName}'.") { }
    }
}
