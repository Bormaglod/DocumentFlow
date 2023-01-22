//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2021
//
// Версия 2022.12.6
//  - добавлено свойство OwnerIdentifier
// Версия 2022.12.17
//  - добавлен метод CreateQuery(string tableName);
// Версия 2023.1.8
//  - добавлены методы Configure и WriteConfigure
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Data.Infrastructure в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Settings;

using SqlKata;

namespace DocumentFlow.Infrastructure.Data;

public interface IFilter
{
    Guid? OwnerIdentifier { get; set; }
    Control Control { get; }
    Query? CreateQuery<T>();
    Query? CreateQuery(string tableName);
    void Configure(IAppSettings appSettings);
    void WriteConfigure(IAppSettings appSettings);
}
