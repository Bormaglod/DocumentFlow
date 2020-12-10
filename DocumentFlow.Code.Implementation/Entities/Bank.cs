using System;
using System.ComponentModel;
using System.Linq;
using DocumentFlow.Code.Core;
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

    public class BankBrowser : BrowserCodeBase<Bank>, IBrowserCode
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
        }

        public IEditorCode CreateEditor()
        {
            return new BankEditor();
        }

        protected override string GetSelect()
        {
            return baseSelect;
        }

        protected override string GetSelectById()
        {
            return baseSelect + " where b.id = :id";
        }
    }

    public class BankEditor : EditorCodeBase<Bank>, IEditorCode
    {
        private const int labelWidth = 160;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
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

        protected override string GetSelect()
        {
            return "select id, code, name, bik, account from bank where id = :id";
        }

        protected override string GetUpdate(Bank bank)
        {
            return "update bank set code = :code, name = :name, bik = :bik, account = :account where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }
    }
}
