using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.BankImp
{
    public class Bank : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public decimal bik { get; set; }
        public decimal account { get; set; }
        public string bik_text 
        {
            get
			{
				return bik.ToString("00 00 00 000");
			}

            set 
			{
				bik = Convert.ToDecimal(value);
			}
        }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class BankBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelect = @"
            select 
                b.id, 
                b.status_id, 
                s.note as status_name, 
                b.code, 
                b.name, 
                b.bik, 
                b.account 
            from bank b 
                join status s on s.id = b.status_id";

        void IBrowserCode.Initialize(IBrowser browser)
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

                columns.CreateText("code", "Код")
                    .SetWidth(110);

                columns.CreateText("name", "Наименование")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateInteger("bik", "БИК")
                    .SetFormat("00 00 00 000")
                    .SetWidth(100);

                columns.CreateInteger("account", "Корр. счёт")
                    .SetFormat("000 00 000 0 00000000 000")
                    .SetWidth(180);

                columns.CreateSortedColumns()
                    .Add("name", ListSortDirection.Ascending);
            });
        }

        IEditorCode IDataEditor.CreateEditor()
        {
            return new BankEditor();
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Bank>(baseSelect).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Bank>(baseSelect + " where b.id = :id", new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from bank where id = :id", new { id }, transaction);
        }
    }

    public class BankEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 120;

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            IControl code = editor.CreateTextBox("code", "Код")
                .SetLabelWidth(labelWidth);

            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(360);

            IControl bik = editor.CreateMaskedText<string>("bik_text", "БИК", "## ## ## ###")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(100);

            IControl account = editor.CreateMaskedText<decimal>("account", "Корр. счёт", "### ## ### # ######## ###")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(180);

            editor.Container.Add(new IControl[] {
                code,
                name,
                bik,
                account
            });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select id, code, name, bik, account from bank where id = :id";
            return connection.QuerySingleOrDefault<Bank>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into bank default values returning id";
            return connection.QuerySingle<Guid>(sql, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update bank set code = :code, name = :name, bik = :bik, account = :account where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from bank where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }
    }
}
