//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.01.2021
// Time: 21:07
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Data.Repositories
{
    public static class Pictures
    {
        public static Picture Default
        {
            get
            {
                using var conn = Db.OpenConnection();
                return conn.QuerySingleOrDefault<Picture>("select * from picture where code = :code", new { code = "file" });
            }
        }

        public static Picture Get(Guid id)
        {
            using var conn = Db.OpenConnection();
            return Get(conn, id);
        }

        public static Picture Get(IDbConnection connection, Guid id)
        {
            return connection.QuerySingle<Picture>("select * from picture where id = :id", new { id });
        }

        public static IEnumerable<Picture> GetChilds(IDbConnection connection, Guid id)
        {
            return connection.Query<Picture>("select * from picture where parent_id = :id", new { id });
        }
    }
}
