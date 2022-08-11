//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.11.2020
//-----------------------------------------------------------------------

namespace DocumentFlow.Core.Exceptions;

public class SqlExecuteException : Exception
{
    public SqlExecuteException(string message) : base(message) { }

    public SqlExecuteException(string message, Exception innerException) : base(message, innerException) { }
}
