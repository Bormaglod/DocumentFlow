//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.03.2021
// Time: 09:55
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Data.Repositories
{
    public static class Commands
    {
        public static Command Get(Guid id)
        {
            using var conn = Db.OpenConnection();
            return conn.QuerySingle<Command>("select * from command where id = :id", new { id });
        }

        public static IEnumerable<Command> GetAll()
        {
            using var conn = Db.OpenConnection();
            string sql = "select * from command c left join picture p on (p.id = c.picture_id) left join entity_kind ek on (ek.id = c.entity_kind_id)";
            return conn.Query<Command, Picture, EntityKind, Command>(sql, (command, picture, entity) =>
            {
                command.Picture = picture;
                command.EntityKind = entity;
                return command;
            });
        }
    }
}
