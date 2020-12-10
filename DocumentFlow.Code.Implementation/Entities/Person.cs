using System;
using System.ComponentModel;
using System.Linq;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.PersonImp
{
    public class Person : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class PersonBrowser : BrowserCodeBase<Person>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                p.id, 
                p.status_id, 
                s.note as status_name, 
                p.code, 
                p.name, 
                p.surname, 
                p.first_name, 
                p.middle_name, 
                p.phone, p.email 
            from person p 
                join status s on s.id = p.status_id";

        public void Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;

            browser.CreateStatusColumnRenderer();

            IColumnCollection columns = browser.Columns;

            columns.CreateText("id", "Id")
                .SetWidth(180)
                .SetVisible(false);

            columns.CreateInteger("status_id", "Код состояния")
                .SetWidth(80)
                .SetVisible(false);

            columns.CreateText("status_name", "Состояние")
                .SetWidth(110)
                .SetVisible(false);

            columns.CreateText("name", "Фамилия И.О.")
                .SetHideable(false)
                .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

            columns.CreateText("surname", "Фамилия")
                .SetWidth(150);

            columns.CreateText("first_name", "Имя")
                .SetWidth(100);

            columns.CreateText("middle_name", "Отчество")
                .SetWidth(150);

            columns.CreateText("phone", "Телефон")
                .SetWidth(150);

            columns.CreateText("email", "Эл. почта")
                .SetWidth(200);

            columns.CreateSortedColumns()
                .Add("name", ListSortDirection.Ascending);
        }

        public IEditorCode CreateEditor()
        {
            return new PersonEditor();
        }

        protected override string GetSelect()
        {
            return baseSelect;
        }

        protected override string GetSelectById()
        {
            return baseSelect + " where p.id = :id";
        }
    }

    public class PersonEditor : EditorCodeBase<Person>, IEditorCode
    {
        private const int labelWidth = 190;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            IControl name = editor.CreateTextBox("name", "Фамилия И.О.")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(250)
                .SetEnabled(false);
            IControl surname = editor.CreateTextBox("surname", "Фамилия")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(200);
            IControl first_name = editor.CreateTextBox("first_name", "Имя")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);
            IControl middle_name = editor.CreateTextBox("middle_name", "Отчество")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(200);
            IControl phone = editor.CreateTextBox("phone", "Телефон")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(140);
            IControl email = editor.CreateTextBox("email", "Эл. почта")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(300);

            editor.Container.Add(new IControl[] {
                name,
                surname,
                first_name,
                middle_name,
                phone,
                email
            });
        }

        protected override string GetSelect()
        {
            return "select id, name, surname, first_name, middle_name, phone, email from person where id = :id";
        }

        protected override string GetUpdate(Person person)
        {
            return "update person set name = :name, surname = :surname, first_name = :first_name, middle_name = :middle_name, phone = :phone, email = :email where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            if (field == "name")
                return false;

            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }
    }
}
