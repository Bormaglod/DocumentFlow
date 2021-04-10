//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2020
// Time: 22:55
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Core.Exceptions
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string message) : base(message) { }
    }
}
