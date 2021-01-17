//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.01.2021
// Time: 19:00
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Data.Repositories
{
    public static class ChangingStatuses
    {
        public static IEnumerable<ChangingStatus> Get(Transition transition)
        {
            using (var conn = Db.OpenConnection())
            {
                return Get(conn, transition);
            }
        }

        public static IEnumerable<ChangingStatus> Get(IDbConnection connection, Transition transition)
        {
            if (transition == null)
            {
                return new ChangingStatus[] { };
            }

            string sql = @"
                select * 
                    from changing_status cs 
                        join status s_from on (s_from.id = cs.from_status_id) 
                        join status s_to on (s_to.id = cs.to_status_id) 
                    where 
                        transition_id = :id 
                    order by cs.order_index";

            return connection.Query<ChangingStatus, Status, Status, ChangingStatus>(sql, (changing_status, status_from, status_to) =>
            {
                changing_status.FromStatus = status_from;
                changing_status.ToStatus = status_to;
                return changing_status;
            }, new { transition.id });
        }

        public static IEnumerable<ChangingStatus> Get(IDbConnection connection, EntityKind entityKind, Status fromFtatus)
        {
            if (entityKind == null)
            {
                return new ChangingStatus[] { };
            }

            string sql = @"
                select * 
                    from changing_status cs 
                        join status s_from on (s_from.id = cs.from_status_id)
                        join status s_to on (s_to.id = cs.to_status_id)
                        left join picture p on (cs.picture_id = p.id) 
                    where 
                        cs.transition_id = :transition_id and 
                        cs.from_status_id = :status_id and 
                        not cs.is_system";

            return connection.Query<ChangingStatus, Status, Status, Picture, ChangingStatus>(sql, (cs, status_from, status_to, picture) =>
            {
                cs.FromStatus = status_from;
                cs.ToStatus = status_to;
                cs.Picture = picture;
                return cs;
            }, new { entityKind.transition_id, status_id = fromFtatus.id }).ToList().OrderBy(x => x.order_index);
        }

        public static bool AccessAllowed(IDbConnection connection, Guid doc_id, ChangingStatus changingStatus)
        {
            return connection.QuerySingle<bool>("select access_changing_status(:id, :changing_status_id)", new { id = doc_id, changing_status_id = changingStatus.id });
        }
    }
}
