//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;

using System.Data;

namespace DocumentFlow.Data.Interfaces;

public interface IDatabase
{
    string CurrentUser { get; }
    string ConnectionName { get; set; }
    string ConnectionString { get; }
    IDbConnection OpenConnection();
    IDbConnection OpenConnection(string userName, string password);
    void Login(string userName, string password);
    bool HasPrivilege(string tableName, params Privilege[] privilege);
}
