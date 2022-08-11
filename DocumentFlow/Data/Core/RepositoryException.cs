//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

public class RepositoryException : Exception
{
    public RepositoryException(string message) : base(message) { }
    public RepositoryException(string message, Exception innerException) : base(message, innerException) { }
}
