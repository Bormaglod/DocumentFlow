//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2020
// Time: 19:53
//-----------------------------------------------------------------------

using System;
using System.Data;
using Dapper;
using DocumentFlow.Code;
using DocumentFlow.Data;

namespace DocumentFlow.Controls.Editor.Code
{
    public class Database : IDatabase
    {
        IDbConnection IDatabase.CreateConnection() => Db.OpenConnection();

        T IDatabase.ExecuteSqlCommand<T>(string sql, object param)
        {
            using (var conn = Db.OpenConnection())
            {
                return conn.QuerySingleOrDefault<T>(sql, param);
            }
        }

        int IDatabase.ExecuteCommand(string sql, object param)
        {
            using (var conn = Db.OpenConnection())
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int res = conn.Execute(sql, param, transaction);
                        transaction.Commit();
                        return res;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ExceptionHelper.MesssageBox(ex);
                    }
                }
            }

            return 0;
        }

        T IDatabase.ExecuteSqlCommand<T>(IDbConnection connection, string sql, object param) => connection.QuerySingleOrDefault<T>(sql, param);
    }
}
