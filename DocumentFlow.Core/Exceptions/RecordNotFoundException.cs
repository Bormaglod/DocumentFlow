//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.04.2020
// Time: 05:04
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    using System;

    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(Guid id) : base($"Запись с идентификатором '{id}' не нйадена.") { }
    }
}
