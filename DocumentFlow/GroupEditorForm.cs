//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.03.2019
// Time: 21:00
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Data;

namespace DocumentFlow
{
    public enum CommandAction { Create, Edit }

    public partial class GroupEditorForm : Form
    {
        private readonly string table;
        private Guid? id;
        private Guid entityKindId;
        private CommandAction action;
        
        private class Group
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public GroupEditorForm(string tableName)
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
            using (var conn = Db.OpenConnection())
            {
                string sql = $"select code, name from {table} where id =:id";
                Group g = conn.Query<Group>(sql, new { id = groupId }).SingleOrDefault();

                if (g == null)
                    return false;

                textCode.Text = g.Code;
                textName.Text = g.Name;
            }

            return ShowDialog() == DialogResult.OK;
        }

        private void DoCommit()
        {
            using var conn = Db.OpenConnection();
            using var transaction = conn.BeginTransaction();
            string sql = null;
            DynamicParameters parameters = null;

            switch (action)
            {
                case CommandAction.Create:
                    sql = $"insert into {table} (status_id, entity_kind_id, code, name, parent_id) values (500, :kind, :code, :name, :parent)";
                    parameters = new DynamicParameters(new { kind = entityKindId, code = textCode.Text, name = textName.Text, parent = id });
                    break;
                case CommandAction.Edit:
                    sql = $"update {table} set code = :code, name = :name where id = :id";
                    parameters = new DynamicParameters(new { code = textCode.Text, name = textName.Text, id });
                    break;
                default:
                    break;
            }

            try
            {
                conn.Execute(sql, parameters, transaction);
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                DialogResult = DialogResult.None;
                ExceptionHelper.MesssageBox(e);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e) => DoCommit();
    }
}
