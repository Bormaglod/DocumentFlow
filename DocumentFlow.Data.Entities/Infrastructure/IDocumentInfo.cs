//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.10.2020
// Time: 18:57
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data
{
    /// <summary>
    /// Интерфейс определяющий свойства объекта для всех типов документов и справочников
    /// </summary>
    public interface IDocumentInfo : IIdentifier<Guid>
    {
        /// <summary>
        /// Возвращает идентификатор состояния документа
        /// </summary>
        int status_id { get; }

        /// <summary>
        /// Возвращает наименование состояния документа
        /// </summary>
        string status_name { get; }
    }
}
