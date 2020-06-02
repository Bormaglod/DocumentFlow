//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.05.2020
// Time: 13:53
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Editor.Core
{
    using System;
    using System.Collections;
    using System.Linq;
    using NHibernate;
    using Flee.PublicTypes;
    using DocumentFlow.DataSchema;

    public class EditorContext
    {
        public EditorContext(ISession session, Guid id, DatasetEditor editor)
        {
            Session = session;
            Id = id;
            Editor = editor;
        }

        public Action<IEditorControl, IDictionary> ActionPopupate { get; set; }
        public VariableCollection Variables { get; set; }

        private ISession Session { get; }
        private Guid Id { get; }
        private DatasetEditor Editor { get; }

        public string GetRootCode(string entityName)
        {
            string sql =
                $@"with recursive r as (
                        select id, parent_id, code
                            from {entityName}
                            where id = :id
                        union
                        select e.id, e.parent_id, e.code
                            from {entityName} e
                                join r on e.id = r.parent_id
                    )
                    select code from r where r.parent_id is null";

            return Session.CreateSQLQuery(sql)
                .SetGuid("id", Id)
                .UniqueResult<string>();
        }

        public string PrevStatusName(int depth)
        {
            string sql =
                @"select s.code 
                        from history h 
                            join status s on (s.id = h.from_status_id) 
                        where 
                            h.reference_id = :id 
                        order by changed desc 
                        limit 1 
                        offset :depth";
            return Session.CreateSQLQuery(sql)
                .SetGuid("id", Id)
                .SetInt32("depth", depth - 1)
                .UniqueResult<string>();
        }

        public bool Populate(string fieldName)
        {
            if (ActionPopupate == null)
                return false;

            IEditorControl control = Editor.GetControls()
                .FirstOrDefault(x => x.Name == fieldName);
            if (control != null)
            {
                IDictionary row = new Hashtable();
                if (Variables != null)
                {
                    foreach (var item in Variables)
                    {
                        row.Add(item.Key, item.Value);
                    }
                }

                ActionPopupate(control, row);

                return true;
            }

            return false;
        }
    }
}
