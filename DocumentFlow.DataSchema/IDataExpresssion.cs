//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.04.2020
// Time: 23:21
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    public interface IDataExpresssion
    {
        /// <summary>
        /// Наименование поля которое будет изменено в результате вычисления выражения <see cref="Expression"/>
        /// </summary>
        string Destination { get; set; }

        string Expression { get; set; }

        /// <summary>
        /// SQL-выражение состоящие из команды SELECT и возвращающее одну строку с одним полем, которое и является значением выражения
        /// </summary>
        string SQLExpression { get; set; }
    }
}
