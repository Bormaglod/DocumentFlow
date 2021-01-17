//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.01.2021
// Time: 19:56
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Data.Repositories
{
    public static class Histories
    {
        public static IEnumerable<History> Get(Guid doc_id)
        {
            using (var conn = Db.OpenConnection())
            {
                return Get(conn, doc_id);
            }
        }

        public static IEnumerable<History> Get(IDbConnection connection, Guid doc_id)
        {
            string sql = @"
                select 
                    ua.name 
                    as user_name, 
                    h.*, 
                    fs.*, 
                    ts.* 
                from history h 
                    join status fs on (fs.id = h.from_status_id) 
                    join status ts on (ts.id = h.to_status_id) 
                    join user_alias ua on (ua.id = h.user_id) 
                where 
                    reference_id = :id 
                order by changed desc";

            return connection.Query<History, Status, Status, History>(sql, (history, fromStatus, toStatus) => {
                history.FromStatus = fromStatus;
                history.ToStatus = toStatus;
                return history;
            }, new { id = doc_id });
        }
    }
}
