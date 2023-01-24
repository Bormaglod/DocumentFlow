//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Core.Exceptions;

public class BrowserException : Exception
{
    public BrowserException(string message) : base(message) { }
}
