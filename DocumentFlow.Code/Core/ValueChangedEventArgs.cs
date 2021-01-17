//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.10.2020
// Time: 13:43
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code.Core
{
    public class ValueChangedEventArgs : EventArgs
    {
        public ValueChangedEventArgs(object value) => Value = value;

        public object Value { get; }
    }
}
