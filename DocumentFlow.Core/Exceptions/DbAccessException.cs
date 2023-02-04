//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.12.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Core.Exceptions;

public class DbAccessException : Exception
{
    public DbAccessException(string message) : base(message) { }
}
