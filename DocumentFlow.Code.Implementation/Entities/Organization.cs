using System;
using System.Linq;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.OrganizationImp
{
    public class Organization : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; protected set; }
        public string status_name { get; protected set; }
        public string code { get; set; }
        public string name { get; set; }
        public string short_name { get; set; }
        public string full_name { get; set; }
        public decimal inn { get; set; }
        public decimal kpp { get; set; }
        public decimal ogrn { get; set; }
        public decimal okpo { get; set; }
        public Guid? okopf_id { get; set; }
        public string okopf_name { get; protected set; }
        public Guid? account_id { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool default_org { get; set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class OrganizationBrowser : BrowserCodeBase<Organization>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                org.id, 
                org.status_id, 
                s.note as status_name, 
                org.name, 
                org.short_name, 
                org.full_name, 
                org.inn, 
                org.kpp, 
                org.ogrn, 
                org.okpo, 
                o.name as okopf_name, 
                org.address, 
                org.phone, 
                org.email, 
                org.default_org 
            from organization org 
                join status s on (s.id = org.status_id) 
                left join okopf o on (o.id = org.okopf_id)";

        public void Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;

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

                columns.CreateText("name", "Наименование")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill)
                    .SetHideable(false);

                columns.CreateText("short_name", "Краткое наименование")
                    .SetWidth(150)
                    .SetVisible(false);

                columns.CreateText("full_name", "Полное наименование")
                    .SetWidth(200)
                    .SetVisible(false);

                columns.CreateInteger("inn", "ИНН")
					.SetFormat("0000 00000 0")
                    .SetWidth(100);

                columns.CreateInteger("kpp", "КПП")
					.SetFormat("0000 00 000")
                    .SetWidth(100);

                columns.CreateInteger("okpo", "ОКПО")
					.SetFormat("00 00000 0")
                    .SetWidth(100)
                    .SetVisible(false);

                columns.CreateInteger("ogrn", "ОГРН")
					.SetFormat("0 00 00 00 00000 0")
                    .SetWidth(120);

                columns.CreateText("okopf_name", "ОКОПФ")
                    .SetWidth(300);

                columns.CreateText("address", "Адрес")
					.SetVisible(false)
                    .SetWidth(250);

                columns.CreateText("phone", "Телефон")
                    .SetWidth(150);

                columns.CreateText("email", "Эл. почта")
                    .SetWidth(200);

                columns.CreateBoolean("default_org", "Основная")
                    .SetWidth(70);

                columns.CreateSortedColumns()
                    .Add("name", SortDirection.Ascending);
            });
        }

        public IEditorCode CreateEditor()
        {
            return new OrganizationEditor();
        }

        protected override string GetSelect()
        {
            return baseSelect;
        }

        protected override string GetSelectById()
        {
            return baseSelect + " where org.id = :id";
        }
    }

    public class OrganizationEditor : EditorCodeBase<Organization>, IEditorCode
    {
        private const int labelWidth = 190;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const string okopfSelect = "select id, name from okopf where status_id = 1001 order by name";
            const string accountSelect = "select id, name from account where (owner_id = :id and status_id = 1002) or (id = :account_id)";

            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(360);
            IControl short_name = editor.CreateTextBox("short_name", "Короткое наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(450);
            IControl full_name = editor.CreateTextBox("full_name", "Полное наименование", true)
                .SetLabelWidth(labelWidth)
                .SetControlWidth(450)
                .SetHeight(75)
                .SetPadding(bottom: 7);
            IControl inn = editor.CreateMaskedText<decimal>("inn", "ИНН", "#### ##### #")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);
            IControl kpp = editor.CreateMaskedText<decimal>("kpp", "КПП", "#### ## ###")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);
            IControl ogrn = editor.CreateMaskedText<decimal>("ogrn", "ОГРН", "# ## ## ## ##### #")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);
            IControl okpo = editor.CreateMaskedText<decimal>("okpo", "ОКПО", "## ##### #")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);
            IControl okopf = editor.CreateComboBox("okopf_id", "ОКОПФ", (conn) => { return conn.Query<ComboBoxDataItem>(okopfSelect); })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(330);
            IControl account = editor.CreateComboBox("account_id", "Расчётный счёт", (e, c) => {
                    Organization org = e.Entity as Organization;
                    return c.Query<ComboBoxDataItem>(accountSelect, new { org.id, org.account_id }); 
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(330);
            IControl address = editor.CreateTextBox("address", "Адрес", true)
                .SetLabelWidth(labelWidth)
                .SetControlWidth(450)
                .SetHeight(75)
                .SetPadding(bottom: 7);
            IControl phone = editor.CreateTextBox("phone", "Телефон")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(190);
            IControl email = editor.CreateTextBox("email", "Эл. почта")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(190);
            IControl default_org = editor.CreateCheckBox("default_org", "Основная организация")
                .SetLabelWidth(labelWidth);

            editor.Container.Add(new IControl[] {
                name,
                short_name,
                full_name,
                inn,
                kpp,
                ogrn,
                okpo,
                okopf,
                account,
                address,
                phone,
                email,
                default_org
            });

            dependentViewer.AddDependentViewers(new string[] { "view-account", "view-employee" });
        }

        protected override string GetSelect()
        {
            return "select id, name, short_name, full_name, inn, kpp, ogrn, okpo, okopf_id, account_id, address, phone, email, default_org from organization where id = :id";
        }

        protected override string GetUpdate(Organization organization)
        {
            return "update organization set name = :name, short_name = :short_name, full_name = :full_name, inn = :inn, kpp = :kpp, ogrn = :ogrn, okpo = :okpo, okopf_id = :okopf_id, account_id = :account_id, address = :address, phone = :phone, email = :email, default_org = :default_org where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }
    }
}