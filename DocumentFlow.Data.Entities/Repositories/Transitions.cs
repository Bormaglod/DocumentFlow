using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Data.Repositories
{
    public static class Transitions
    {
        public static IEnumerable<Transition> GetAll(bool extend = false)
        {
            using var conn = Db.OpenConnection();
            if (extend)
            {
                string sql = "select * from transition t join status s_start on (s_start.id = t.starting_id) left join status s_finish on (s_finish.id = t.finishing_id)";
                return conn.Query<Transition, Status, Status, Transition>(sql, (transition, start, finish) =>
                {
                    transition.Starting = start;
                    transition.Finishing = finish;
                    return transition;
                });
            }
            else
            {
                return conn.Query<Transition>("select * from transition");
            }
        }

        public static Transition Get(Guid id, bool extend = false)
        {
            using var conn = Db.OpenConnection();
            if (extend)
            {
                string sql = "select * from transition t join status s_start on (s_start.id = t.starting_id) left join status s_finish on (s_finish.id = t.finishing_id) where t.id = :id";
                return conn.Query<Transition, Status, Status, Transition>(sql, (transition, start, finish) =>
                {
                    transition.Starting = start;
                    transition.Finishing = finish;
                    return transition;
                }).Single();
            }
            else
            {
                return conn.QuerySingle<Transition>("select * from transition where id = :id", new { id });
            }
        }


    }
}
