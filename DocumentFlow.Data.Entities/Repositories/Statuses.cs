//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.01.2021
// Time: 18:51
//-----------------------------------------------------------------------

using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Data.Repositories
{
    public static class Statuses
    {
        public static Status Get(IDbConnection connection, int id)
        {
            return connection.Query<Status, Picture, Status>("select * from status s left join picture p on (p.id = s.picture_id) where s.id = :id", (status, picture) =>
            {
                status.Picture = picture;
                return status;
            }, new { id }).SingleOrDefault();
        }
    }
}
