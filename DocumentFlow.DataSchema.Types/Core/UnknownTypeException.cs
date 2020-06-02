//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.05.2020
// Time: 18:41
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types.Core
{
    using System;

    public class UnknownTypeException : Exception
    {
        public UnknownTypeException(string type) : base($"Обнаружен неизвестный тип данных {type}. Обработка прервана.") { }
    }
}
