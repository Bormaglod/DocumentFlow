//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.10.2020
// Time: 21:16
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Core
{
    /// <summary>
    /// Общий интерфейс для объектов имеющих первичный ключ id
    /// </summary>
    public interface IIdentifier 
    {
        object oid { get; }
    }

    /// <summary>
    /// Общий интерфейс для объектов имеющих первичный ключ id указанного типа
    /// </summary>
    public interface IIdentifier<T> : IIdentifier where T: IComparable<T>
    {
        T id { get; }
    }
}
