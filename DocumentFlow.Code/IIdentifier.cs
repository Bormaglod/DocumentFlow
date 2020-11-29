//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.10.2020
// Time: 21:16
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code
{
    /// <summary>
    /// Общий интерфейс для объектов имеющих первичный ключ id типа UUID
    /// </summary>
    public interface IIdentifier
    {
        Guid id { get; }
    }
}
