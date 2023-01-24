//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.04.2020
//-----------------------------------------------------------------------

namespace DocumentFlow.Core.Exceptions;

public class RecordNotFoundException : Exception
{
    public RecordNotFoundException(object id) : base($"Запись с идентификатором '{id}' не нйадена.") { }
}
