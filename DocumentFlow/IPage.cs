//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2020
// Time: 22:22
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;

    public interface IPage
    {
        /// <summary>
        /// Для окна содержащего список записей Id - идентификатор типа сущности (таблица entity_kind), для окна редактора - идентификатор записи
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Для окна содержащего список записей Id - идентификатор текущей записи, для окна редактора - идентификатор записи (то-же самое, что и Id)
        /// </summary>
        Guid ContentId { get; }

        void OnClosed();
    }
}
