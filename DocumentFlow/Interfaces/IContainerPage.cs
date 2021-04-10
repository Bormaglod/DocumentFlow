//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
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
        /// Возращает страницу идентификатор которой равен code.
        /// </summary>
        /// <param name="code">Идентификатор искомой страницы.</param>
        /// <returns>Страница, идентификатор которой равен code.</returns>
        IPage Get(string code);

        /// <summary>
        /// Возвращает список всех страниц реализующих интерфейс <see cref="IContentPage" />.
        /// </summary>
        /// <returns>Страницы, реализующие интерфейс IContentPage.</returns>
        IEnumerable<IContentPage> GetContentPages();

        ITwain Twain { get; }

        void Logout();
        void About();
    }
}
