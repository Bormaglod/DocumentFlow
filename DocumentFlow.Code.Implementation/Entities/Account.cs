using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.System;
using DocumentFlow.Data;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Implementation.AccountImp
{
    public class Account : Directory
    {
        public decimal account_value { get; set; }
        public Guid? bank_id { get; set; }
        public string bank_name { get; set; }
        public string company_name { get; set; }
    }

    public class AccountBrowser : IBrowserCode, IBrowserOperation, IDataEditor
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

                columns.CreateText("name", "Наименование")
                    .SetHideable(false)
                    .SetWidth(200);

                columns.CreateNumeric("account_value", "Расчётный счёт2")
					.SetFormat("000 00 000 0 0000 0000000")
                    .SetWidth(200);

                columns.CreateText("bank_name", "Банк")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateSortedColumns()
                    .Add("name", ListSortDirection.Ascending);
            });
        }

        IEditorCode IDataEditor.CreateEditor()
        {
            return new AccountEditor();
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Account>(string.Format(baseSelect, "a.owner_id = :owner_id"), new { owner_id = parameters.OwnerId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Account>(string.Format(baseSelect, "a.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from account where id = :id", new { id }, transaction);
        }
    }

    public class AccountEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 120;

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const string bankSelect = "select id, name from bank where status_id = 1002 order by name";

            IControl company_name = editor.CreateTextBox("company_name", "Контрагент")
                .SetLabelWidth(labelWidth)
                .SetEnabled(false);

            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400);

            IControl account_value = editor.CreateMaskedText<decimal>("account_value", "Номер счета", "### ## ### # #### #######")
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

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = @"
                select 
                    a.id, 
                    a.name, 
                    a.account_value, 
                    a.bank_id, 
                    c.name as company_name
                from account a 
                    join company c on (c.id = a.owner_id) 
                where a.id = :id";
            return connection.QuerySingleOrDefault<Account>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into account (owner_id) values (:owner_id) returning id";
            return connection.QuerySingle<Guid>(sql, new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update account set name = :name, account_value = :account_value, bank_id = :bank_id where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            string sql = "delete from account where id = :id";
            return connection.Execute(sql, new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            if (dataName == "company_name")
                return false;

            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }
    }
}