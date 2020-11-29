//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2020
// Time: 16:51
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Core.Exceptions
{
    public class MissingImpException : Exception
    {
        public MissingImpException(string message) : base(message) { }
    }
}
