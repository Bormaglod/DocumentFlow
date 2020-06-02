//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.03.2020
// Time: 23:08
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    using System;

    public class FieldNotFoundException : Exception
    {
        public FieldNotFoundException(string fieldName) : base($"Строка данных не содержит поле '{fieldName}'") { }
        public FieldNotFoundException(string sql, string fieldName) : base($"Запрос '{sql}' не содержит поле '{fieldName}'") { }
    }
}
