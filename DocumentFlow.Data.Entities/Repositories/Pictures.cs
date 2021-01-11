//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.01.2021
// Time: 21:07
//-----------------------------------------------------------------------

using Dapper;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Data.Repositories
{
    public class Pictures
    {
        public static Picture Default
        {
            get
            {
                using (var conn = Db.OpenConnection())
                {
                    return conn.QuerySingleOrDefault<Picture>("select * from picture where code = :code", new { code = "file" });
                }
            }
        }
    }
}
