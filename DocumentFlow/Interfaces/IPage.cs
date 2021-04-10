//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
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
        /// Идентификатор окна
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Иднтификатор текущей записи (окна содержащего таблицу) или идентификатор команды - для окна редактора
        /// </summary>
        Guid Id { get; }

        string Header { get; }

        IContainerPage Container { get; }

        void Close();

        void Rebuild();
    }
}
