//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.10.2020
// Time: 19:47
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Core
{
    /// <summary>
    /// Интерфейс определяющий свойства справочника
    /// </summary>
    public interface IDirectory : IDocumentInfo
    {
        /// <summary>
        /// Возвращает код объекта
        /// </summary>
        string code { get; }

        /// <summary>
        /// Возвращает наименование объекта
        /// </summary>
        string name { get; }

        /// <summary>
        /// Возвращает идентификатор вышестоящей записи 
        /// </summary>
        Guid? parent_id { get; }
    }
}
