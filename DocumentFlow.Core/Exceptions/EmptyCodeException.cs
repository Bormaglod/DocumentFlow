//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2020
// Time: 22:55
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Core.Exceptions
{
    public class EmptyCodeException : Exception
    {
        public EmptyCodeException(string message) : base(message) { }
    }
}
