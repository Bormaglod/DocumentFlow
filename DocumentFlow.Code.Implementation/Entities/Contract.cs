using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.Core;
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
        public int contract_type_value { get; set; }
        public string contract_type_name { get; set; }
        public string contractor_name { get; protected set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class ContractBrowser : BrowserCodeBase<Contract>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                c.id, 
                c.status_id, 
                s.note as status_name, 
                c.name, 
                c.tax_payer,
                case c.contract_type 
                	when 'sale'::contract_type then 'С продавцом'
                	when 'purchase'::contract_type then 'С покупателем'
                end as contract_type_name
            from contract c 
                join status s on (s.id = c.status_id)
            where {0}";

        public void Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.ToolBar.ButtonStyle = ToolStripItemDisplayStyle.Image;
            browser.ToolBar.IconSize = ButtonIconSize.Small;
            browser.CommandBarVisible = false;

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

            columns.CreateText("name", "Наименование")
                .SetHideable(false)
                .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

            columns.CreateBoolean("tax_payer", "Плательщик НДС")
                .SetWidth(140);

            columns.CreateText("contract_type_name", "Тип договора")
                .SetWidth(200);

            columns.CreateSortedColumns()
                .Add("name", ListSortDirection.Ascending);
        }

        public IEditorCode CreateEditor()
        {
            return new ContractEditor();
        }

        public override IEnumerable<Contract> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Contract>(GetSelect(), new { owner_id = parameters.OwnerId });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "c.owner_id = :owner_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "c.id = :id");
        }
    }

    public class ContractEditor : EditorCodeBase<Contract>, IEditorCode
    {
        private const int labelWidth = 120;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
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
            IControl contract_type = editor.CreateChoice("contract_type_value", "Тип договора", new Dictionary<int, string>() { {1, "С покупателем" }, { 2, "С продавцом" } })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(330);

            editor.Container.Add(new IControl[] {
                contractor_name,
                name,
                tax_payer,
                contract_type
            });
        }

        public override TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            return connection.QuerySingle<TId>(GetInsert(), new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        protected override string GetSelect()
        {
            return "select co.id, co.name, co.tax_payer, co.contract_type, c.name as contractor_name, co.contract_type::integer as contract_type_value from contract co join contractor c on (c.id = co.owner_id) where co.id = :id";
        }

        protected override string GetInsert()
        {
            return "insert into contract (owner_id) values (:owner_id) returning id";
        }

        protected override string GetUpdate(Contract contract)
        {
            if (contract.contract_type_value == 1)
                return "update contract set name = :name, tax_payer = :tax_payer, contract_type = 'purchase'::contract_type where id = :id";
            else
                return "update contract set name = :name, tax_payer = :tax_payer, contract_type = 'sale'::contract_type where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            if (field == "contractor_name")
                return false;

            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }
    }
}