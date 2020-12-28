using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.ContractImp
{
    public class Contract : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public bool tax_payer { get; set; }
		public bool is_default { get; set; }
        public int contractor_type_value { get; set; }
        public string contractor_type_name { get; set; }
        public string contractor_name { get; protected set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class ContractBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelect = @"
            select 
                c.id, 
                c.status_id, 
                s.note as status_name, 
                c.name, 
                c.tax_payer,
                c.is_default,
                case c.contractor_type 
                	when 'seller'::contractor_type then 'С продавцом'
                	when 'buyer'::contractor_type then 'С покупателем'
                end as contractor_type_name
            from contract c 
                join status s on (s.id = c.status_id)
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
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateBoolean("tax_payer", "Плательщик НДС")
                    .SetWidth(140);

                columns.CreateText("contractor_type_name", "Тип договора")
                    .SetWidth(200);

                columns.CreateBoolean("is_default", "Основной")
                    .SetWidth(100);

                columns.CreateSortedColumns()
                    .Add("name", ListSortDirection.Ascending);
            });
        }

        IEditorCode IDataEditor.CreateEditor()
        {
            return new ContractEditor();
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Contract>(string.Format(baseSelect, "c.owner_id = :owner_id"), new { owner_id = parameters.OwnerId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Contract>(string.Format(baseSelect, "c.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from contract where id = :id", new { id }, transaction);
        }
    }

    public class ContractEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 120;

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            IControl contractor_name = editor.CreateTextBox("contractor_name", "Контрагент")
                .SetLabelWidth(labelWidth)
                .SetEnabled(false);

            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400);

            IControl tax_payer = editor.CreateCheckBox("tax_payer", "Плателшик НДС")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(250);

            IControl contract_type = editor.CreateChoice("contractor_type_value", "Тип договора", new Dictionary<int, string>() { {1, "С продавцом" }, { 2, "С покупателем" } })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(330);

            editor.Container.Add(new IControl[] {
                contractor_name,
                name,
                tax_payer,
                contract_type
            });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select co.id, co.name, co.tax_payer, co.contractor_type, c.name as contractor_name, co.contractor_type::integer as contractor_type_value from contract co join contractor c on (c.id = co.owner_id) where co.id = :id";
            return connection.QuerySingleOrDefault<Contract>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            return connection.QuerySingle<Guid>("insert into contract (owner_id) values (:owner_id) returning id", new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            Contract contract = editor.Entity as Contract;
            string sql = "update contract set name = :name, tax_payer = :tax_payer, contractor_type = '{0}'::contractor_type where id = :id";
            return connection.Execute(string.Format(sql, contract.contractor_type_value == 1 ? "buyer" : "seller"), editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from contract where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            if (dataName == "contractor_name")
                return false;

            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }
    }
}