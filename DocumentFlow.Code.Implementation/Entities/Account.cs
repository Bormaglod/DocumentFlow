using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.AccountImp
{
    public class Account : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public decimal account_value { get; set; }
        public Guid? bank_id { get; set; }
        public string bank_name { get; set; }
        public string company_name { get; set; }
    }

    public class AccountBrowser : BrowserCodeBase<Account>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                a.id, 
                a.status_id, 
                s.note as status_name, 
                a.name, 
                a.account_value, 
                b.name as bank_name 
            from account a 
                join status s on (s.id = a.status_id) 
                left join bank b on (b.id = a.bank_id) 
            where {0}";

        public void Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.ToolBar.ButtonStyle = ButtonDisplayStyle.Image;
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

                columns.CreateText("name", "Наименование")
                    .SetHideable(false)
                    .SetWidth(200);

                columns.CreateNumeric("account_value", "Расчётный счёт2")
					.SetFormat("000 00 000 0 0000 0000000")
                    .SetWidth(200);

                columns.CreateText("bank_name", "Банк")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateSortedColumns()
                    .Add("name", SortDirection.Ascending);
            });
        }

        public IEditorCode CreateEditor()
        {
            return new AccountEditor();
        }

        public override IEnumerable<Account> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Account>(GetSelect(), new { owner_id = parameters.OwnerId });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "a.owner_id = :owner_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "a.id = :id");
        }
    }

    public class AccountEditor : EditorCodeBase<Account>, IEditorCode
    {
        private const int labelWidth = 120;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const string bankSelect = "select id, name from bank where status_id = 1002 order by name";

            IControl company_name = editor.CreateTextBox("company_name", "Контрагент")
                .SetLabelWidth(labelWidth)
                .SetEnabled(false);
            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400);
            IControl account_value = editor.CreateNumeric("account_value", "Номер счета", numberDecimalDigits: 0)
                .SetLabelWidth(labelWidth)
                .SetControlWidth(250);
            IControl bank = editor.CreateComboBox("bank_id", "Банк", (conn) => { return conn.Query<ComboBoxDataItem>(bankSelect); })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(330);

            editor.Container.Add(new IControl[] {
                company_name,
                name,
                account_value,
                bank
            });
        }

        public override TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            return connection.QuerySingle<TId>(GetInsert(), new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        protected override string GetSelect()
        {
            return "select a.id, a.name, a.account_value, a.bank_id, c.name as company_name from account a join company c on (c.id = a.owner_id) where a.id = :id";
        }

        protected override string GetInsert()
        {
            return "insert into account (owner_id) values (:owner_id) returning id";
        }

        protected override string GetUpdate(Account account)
        {
            return "update account set name = :name, account_value = :account_value, bank_id = :bank_id where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            if (field == "company_name")
                return false;

            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }
    }
}