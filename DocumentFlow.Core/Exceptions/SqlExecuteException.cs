//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.11.2020
// Time: 20:22
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Core.Exceptions
{
    public class SqlExecuteException : Exception
    {
        public SqlExecuteException(string message) : base(message) { }

        public SqlExecuteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
