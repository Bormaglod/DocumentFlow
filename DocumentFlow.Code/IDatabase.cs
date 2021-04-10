//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2020
// Time: 19:46
//-----------------------------------------------------------------------

using System.Data;

namespace DocumentFlow.Code
{
    public interface IDatabase
    {
        IDbConnection CreateConnection();
        T ExecuteSqlCommand<T>(string sql, object param = null);
        int ExecuteCommand(string sql, object param = null);
        T ExecuteSqlCommand<T>(IDbConnection connection, string sql, object param = null);
    }
}
