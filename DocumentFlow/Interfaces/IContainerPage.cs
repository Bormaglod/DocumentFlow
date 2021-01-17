//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2020
// Time: 22:22
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DocumentFlow.Interfaces
{
    public interface IContainerPage
    {
        /// <summary>
        /// Возвращает текущую активную страницу
        /// </summary>
        IPage Selected { get; set; }

        /// <summary>
        /// Добавляет новую страницу
        /// </summary>
        /// <param name="control"></param>
        void Add(IPage page);

        /// <summary>
        /// Возвращает true, если среди открытых страниц есть страница с <see cref="IPage"/>.Id = id
        /// </summary>
        /// <param name="id">Идентификатор искомой страницы</param>
        /// <returns>true, если среди открытых страниц есть страница с <see cref="IPage"/>.Id = id</returns>
        bool Contains<T>(Guid id);

        /// <summary>
        /// Возращает страницу идентификатор которой равен id при этом тип страницы должен быть T.
        /// </summary>
        /// <param name="id">Идентификатор искомой страницы.</param>
        /// <returns>Страница, идентификатор которой равен id.</returns>
        IPage Get<T>(Guid id);

        /// <summary>
        /// Возвращает список всех страниц идентификатор которых равен id.
        /// </summary>
        /// <param name="id">Идентификатор искомой страницы.</param>
        /// <returns>Страницы, идентификатор которой равен id.</returns>
        IEnumerable<IPage> Get(Guid id);

        IEnumerable<T> GetAll<T>();

        void Logout();
        void About();
    }
}
