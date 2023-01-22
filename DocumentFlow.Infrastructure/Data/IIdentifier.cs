//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.10.2020
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Data.Infrastructure в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Data;

/// <summary>
/// Общий интерфейс для объектов имеющих первичный ключ id указанного типа
/// </summary>
public interface IIdentifier<T>
    where T : struct, IComparable
{
    T id { get; set; }
}
