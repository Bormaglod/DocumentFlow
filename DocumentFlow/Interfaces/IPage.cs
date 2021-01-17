//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2020
// Time: 22:22
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Interfaces
{
    public interface IPage
    {
        /// <summary>
        /// Идентификатор окна: идентификатор команды для списка, идентификатор записи - для редактора
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Дополнительный иднтификатор окна содержащий идентификатор текущей записи (окна содержащего таблицу) или идентификатор коианды - для окна редактора
        /// </summary>
        Guid InfoId { get; }

        IContainerPage Container { get; }

        void Close();

        void Rebuild();
    }
}
