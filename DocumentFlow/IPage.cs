//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2020
// Time: 22:22
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow
{
    public interface IPage
    {
        /// <summary>
        /// Идентификатор команды (таблица command)
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Для окна содержащего список записей Id - идентификатор текущей записи, для окна редактора - идентификатор записи
        /// </summary>
        Guid ContentId { get; }

        IContainerPage Container { get; }

        void Close();

        void Rebuild();
    }
}
