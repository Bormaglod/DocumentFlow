//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.03.2019
// Time: 21:00
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using System.Windows.Forms;
    using NHibernate;
    using NHibernate.Transform;
    using Syncfusion.Windows.Forms;
    using DocumentFlow.Core;
    using DocumentFlow.Data.Core;

    public enum CommandAction { Create, Edit }

    public partial class GroupEditor : MetroForm
    {
        private string table;
        private Guid? id;
        private Guid entityKindId;
        private CommandAction action;
        

        private class Group
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public GroupEditor(string tableName)
        {
            InitializeComponent();
            table = tableName;
        }

        public bool Create(Guid entityKind, Guid? parent)
        {
            action = CommandAction.Create;
            id = parent;
            entityKindId = entityKind;

            return ShowDialog() == DialogResult.OK;
        }

        public bool Edit(Guid groupId)
        {
            if (groupId == Guid.Empty)
                return false;

            action = CommandAction.Edit;
            id = groupId;
            using (var session = Db.OpenSession())
            {
                Group g = session.CreateSQLQuery($"select code, name from {table} where id = :id")
                    .SetGuid("id", groupId)
                    .SetResultTransformer(Transformers.AliasToBean<Group>())
                    .UniqueResult<Group>();

                if (g == null)
                    return false;

                textCode.Text = g.Code;
                textName.Text = g.Name;
            }

            return ShowDialog() == DialogResult.OK;
        }

        private void DoCommit()
        {
            using (var session = Db.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    IQuery query = null;
                    switch (action)
                    {
                        case CommandAction.Create:
                            query = session.CreateSQLQuery($"insert into {table} (status_id, entity_kind_id, code, name, parent_id) values (500, :kind, :code, :name, :parent)")
                                .SetGuid("kind", entityKindId)
                                .SetString("code", textCode.Text)
                                .SetString("name", textName.Text)
                                .SetParameter("parent", id);
                            break;
                        case CommandAction.Edit:
                            query = session.CreateSQLQuery($"update {table} set code = :code, name = :name where id = :id")
                                .SetString("code", textCode.Text)
                                .SetString("name", textName.Text)
                                .SetGuid("id", id.Value);
                            break;
                        default:
                            break;
                    }

                    try
                    {
                        query.ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        DialogResult = DialogResult.None;
                        ExceptionHelper.MesssageBox(e);
                    }
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DoCommit();
        }
    }
}
