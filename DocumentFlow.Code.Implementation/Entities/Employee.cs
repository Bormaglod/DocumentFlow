using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.System;
using DocumentFlow.Data;
using DocumentFlow.Data.Base;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Implementation.EmployeeImp
{
    public class Employee : Directory
    {
        public string company_name { get; set; }
        public Guid? person_id { get; set; }
        public string person_name { get; set; }
        public Guid? post_id { get; set; }
        public string post_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int post_role { get; set; }
        public string post_role_text { get; set; }
    }

    public class EmployeeBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelect = @"
            select 
                e.id, 
                e.status_id, 
                s.note as status_name, 
                p.name as person_name, 
                okpdtr.name as post_name, 
                e.phone, 
                e.email, 
                case e.post_role 
                    when 0 then 'Не определена' 
                    when 1 then 'Руководитель' 
                    when 2 then 'Гл. бухгалтер' 
                    when 3 then 'Служащий' 
                    when 4 then 'Рабочий' 
                end as post_role_text
            from employee e 
                join status s on (s.id = e.status_id) 
                join person p on (p.id = e.person_id) 
                left join okpdtr on (okpdtr.id = e.post_id) 
            where {0}";

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.ToolBar.ButtonStyle = ToolStripItemDisplayStyle.Image;
            browser.ToolBar.IconSize = ButtonIconSize.Small;
            browser.CommandBarVisible = false;

            browser.CreateStatusColumnRenderer();

            browser.CreateColumns((columns) =>
            {
                columns.CreateText("id", "Id")
                    .SetWidth(180)
                    .SetVisible(false);

                columns.CreateInteger("status_id", "Код состояния")
                    .SetWidth(80)
                    .SetVisible(false);

                columns.CreateText("status_name", "Состояние")
                    .SetWidth(110)
                    .SetVisible(false);

                columns.CreateText("person_name", "Сотрудник")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateText("post_name", "Должность")
                    .SetWidth(300);

                columns.CreateText("email", "Эл. почта")
                    .SetWidth(250);

                columns.CreateText("post_role", "Роль")
                    .SetWidth(150)
                    .SetVisible(false);

                columns.CreateSortedColumns()
                    .Add("person_name", ListSortDirection.Ascending);
            });
        }

        IEditorCode IDataEditor.CreateEditor() => new EmployeeEditor();

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Employee>(string.Format(baseSelect, "e.owner_id = :owner_id"), new { owner_id = parameters.OwnerId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Employee>(string.Format(baseSelect, "e.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from employee where id = :id", new { id }, transaction);
        }
    }

    public class EmployeeEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 120;

        public void Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const string personSelect = "select id, name from person where status_id = 1002 order by name";
            const string okpdtrSelect = "select id, name from okpdtr where status_id = 1001 order by name";

            IControl company_name = editor.CreateTextBox("company_name", "Организация")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400)
                .SetEnabled(false);

            IControl person_id = editor.CreateComboBox("person_id", "Сотрудник", (conn) => { return conn.Query<GroupDataItem>(personSelect); })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(200);

            IControl post_id = editor.CreateComboBox("post_id", "Должность", (conn) => { return conn.Query<NameDataItem>(okpdtrSelect); })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(300);

            IControl phone = editor.CreateTextBox("phone", "Телефон")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(190);

            IControl email = editor.CreateTextBox("email", "Эл. почта")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(250);

            IControl post_role = editor.CreateChoice("post_role", "Роль", new Dictionary<int, string>() {
                [0] = "Не определена",
                [1] = "Руководитель",
                [2] = "Гл. бухгалтер",
                [3] = "Служащий",
                [4] = "Рабочий" })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);

            editor.Container.Add(new IControl[] {
                company_name,
                person_id,
                post_id,
                phone,
                email,
                post_role
            });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select e.id, c.name as company_name, e.person_id, e.post_id, e.phone, e.email, e.post_role from employee e join company c on (c.id = e.owner_id) left join person p on (p.id = e.person_id) where e.id = :id";
            return connection.QuerySingleOrDefault<Employee>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into employee (owner_id) values (:owner_id) returning id";
            return connection.QuerySingle<Guid>(sql, new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update employee set person_id = :person_id, post_id = :post_id, phone = :phone, email = :email, post_role = :post_role where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from employee where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            if (dataName == "company_name")
                return false;

            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }
    }
}
