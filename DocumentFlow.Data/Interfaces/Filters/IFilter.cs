//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2021
//-----------------------------------------------------------------------

using SqlKata;

namespace DocumentFlow.Data.Interfaces.Filters;

public interface IFilter
{
    object? Settings { get; }
    Guid? OwnerId { get; set; }
    Query? CreateQuery<T>();
    Query? CreateQuery(string tableName);
    void SettingsLoaded();
}
