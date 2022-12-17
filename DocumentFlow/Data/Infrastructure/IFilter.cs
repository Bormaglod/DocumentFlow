//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2021
//
// Версия 2022.12.6
//  - добавлено свойство OwnerIdentifier
// Версия 2022.12.17
//  - добавлен метод CreateQuery(string tableName);
//
//-----------------------------------------------------------------------

using SqlKata;

namespace DocumentFlow.Data.Infrastructure;

public interface IFilter
{
    Guid? OwnerIdentifier { get; set; }
    Control Control { get; }
    Query? CreateQuery<T>();
    Query? CreateQuery(string tableName);
}
