//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.11.2020
// Time: 11:42
//-----------------------------------------------------------------------

using System;
using System.Data;

namespace DocumentFlow.Code
{
    public interface IPopulate
    {
        event EventHandler AfterPopulation;

        /// <summary>
        /// Метод вызывается для заполнения элемента управления данными из привязанного к нему поля БД или набора данных БД.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data">Объект полученный из БД значение поля которого используется для заполнения элемента управления.</param>
        void Populate(IDbConnection connection, object data);

        /// <summary>
        /// Метод вызывается после заполнения данными всех элементов управления.
        /// </summary>
        void DoAfterPopulation();

        /// <summary>
        /// Устанавливает обработку вызываемую после заполнения всех элементов управления данными БД в методе <seealso cref="DoAfterPopulation"/>.
        /// </summary>
        /// <param name="afterPopulation"></param>
        /// <returns></returns>
        IControl AfterPopulationAction(EventHandler afterPopulation);
    }
}
