//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.10.2020
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces;

/// <summary>
/// Общий интерфейс для объектов имеющих первичный ключ id указанного типа
/// </summary>
public interface IIdentifier<T>
    where T : struct, IComparable
{
    T Id { get; set; }
}
