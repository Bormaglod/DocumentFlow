using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.ContractorImp
{
    public class Contractor : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public Guid? parent_id { get; set; }
        public string short_name { get; set; }
        public string full_name { get; set; }
        public decimal? inn { get; set; }
        public decimal? kpp { get; set; }
        public decimal? ogrn { get; set; }
        public decimal? okpo { get; set; }
        public Guid? okopf_id { get; set; }
        public string okopf_name { get; set; }
        public Guid? account_id { get; set; }
        public bool tax_payer { get; set; }
        public bool supplier { get; set; }
        public bool buyer { get; set; }
    }

    public class ContractorBrowser : BrowserCodeBase<Contractor>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                c.id, 
                c.status_id, 
                s.note as status_name, 
                c.name, 
                c.parent_id, 
                c.short_name, 
                c.full_name, 
                c.inn, 
                c.kpp, 
                c.ogrn, 
                c.okpo,     
                o.name as okopf_name, 
                c.tax_payer, 
                c.supplier, 
                c.buyer 
            from contractor c 
                join status s on (s.id = c.status_id) 
                left join okopf o on (o.id = c.okopf_id) 
            where 
                {0}";

        public void Initialize(IBrowser browser)
        {
            browser.AllowGrouping = true;
            browser.DataType = DataType.Directory;

            browser.CreateStatusColumnRenderer();

            browser.CreateColumns((columns) =>
			{
	            columns.CreateText("id", "Id")
    	            .SetWidth(180)
        	        .SetVisible(false);

	            columns.CreateInteger("status_id", "Код состояния")
    	            .SetWidth(80)
        	        .SetAllowGrouping(true)
            	    .SetVisible(false);

	            columns.CreateText("status_name", "Состояние")
    	            .SetWidth(110)
        	        .SetAllowGrouping(true)
            	    .SetVisible(false);

	            columns.CreateText("name", "Наименование")
    	            .SetAutoSizeColumnsMode(SizeColumnsMode.Fill)
        	        .SetHideable(false);

	            columns.CreateText("short_name", "Краткое наименование")
    	            .SetWidth(150)
					.SetVisible(false)
        	        .SetVisibility(false);

            	columns.CreateText("full_name", "Полное наименование")
                	.SetWidth(200)
					.SetVisible(false)
	                .SetVisibility(false);

    	        columns.CreateInteger("inn", "ИНН")
        	        .SetWidth(100)
					.SetVisibility(false);

	            columns.CreateInteger("kpp", "КПП")
					.SetFormat("0000 00 000")
        	        .SetWidth(100)
					.SetVisibility(false);

            	columns.CreateInteger("okpo", "ОКПО")
					.SetFormat("00 00000 0")
	                .SetWidth(100)
    	            .SetVisibility(false);

            	columns.CreateInteger("ogrn", "ОГРН")
					.SetFormat("0 00 00 00 00000 0")
	                .SetWidth(120)
					.SetVisibility(false);

	            columns.CreateText("okopf_name", "ОКОПФ")
    	            .SetWidth(300)
        	        .SetAllowGrouping(true)
					.SetVisibility(false);

	            columns.CreateBoolean("tax_payer", "Плательщик НДС")
					.SetWidth(150)
    	            .SetAllowGrouping(true)
        	        .SetVisibility(false);

	            columns.CreateSortedColumns()
    	            .Add("name", SortDirection.Ascending);
			});

			browser.ChangeParent += Browser_ChangeParent;
        }

        public IEditorCode CreateEditor()
        {
            return new ContractorEditor();
        }

        public override IEnumerable<Contractor> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Contractor>(GetSelect(), new { parent_id = parameters.ParentId });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "c.parent_id is not distinct from :parent_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "c.id = :id");
        }

		private void Browser_ChangeParent(object sender, ChangeParentEventArgs e)
		{
			IBrowser browser = sender as IBrowser;
            if (browser != null)
            {
				string root = string.Empty;
				if (browser.Parameters.ParentId.HasValue)
                {
                    Contractor g = browser.ExecuteSqlCommand<Contractor>("select * from contractor where id = :id", new { id = browser.Parameters.ParentId });
                    if (g.parent_id.HasValue)
                        root = browser.ExecuteSqlCommand<string>("select root_code_contractor(:id)", new { id = browser.Parameters.ParentId });
                    else
                        root = g.code;
                }

				foreach (string column in new string[] { "short_name", "full_name", "inn" })
                {
                    browser.Columns[column].Visibility = !string.IsNullOrEmpty(root);
                }

				foreach (string column in new string[] { "kpp", "okpo", "ogrn", "okopf_name", "tax_payer" })
                {
                    browser.Columns[column].Visibility = root == "Юр";
                }

				if (!string.IsNullOrEmpty(root))
				{
					string format = root == "Юр" ? "0000 00000 0" : "0000 000000 00";
					((INumericColumn)browser.Columns["inn"]).Format = format;
				}
			}
		}
    }

    public class ContractorEditor : EditorCodeBase<Contractor>, IEditorCode
    {
        private const int labelWidth = 190;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const string folderSelect = "select id, parent_id, name, status_id from contractor where status_id = 500 order by name";
            const string okopfSelect = "select id, name from okopf where status_id = 1001 order by name";
            const string accountSelect = "select id, name from account where (owner_id = :id and status_id = 1002) or (id = :account_id)";

            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(360);
            IControl parent = editor.CreateSelectBox("parent_id", "Группа", (c) => { return c.Query<GroupDataItem>(folderSelect); }, showOnlyFolder: true)
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
            IControl inn = editor.CreateNumeric("inn", "ИНН", numberDecimalDigits: 0)
				.AsNullable()
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);
            IControl kpp = editor.CreateNumeric("kpp", "КПП", numberDecimalDigits: 0)
				.AsNullable()
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);
            IControl ogrn = editor.CreateNumeric("ogrn", "ОГРН", numberDecimalDigits: 0)
				.AsNullable()
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);
            IControl okpo = editor.CreateNumeric("okpo", "ОКПО", numberDecimalDigits: 0)
				.AsNullable()
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);
            IControl okopf = editor.CreateComboBox("okopf_id", "ОКОПФ", (c) => { return c.Query<ComboBoxDataItem>(okopfSelect); })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(330);
            IControl account = editor.CreateComboBox("account_id", "Расчётный счёт", (e, c) => {
                    Contractor contractor = e.Entity as Contractor;
                    return c.Query<ComboBoxDataItem>(accountSelect, new { contractor.id, contractor.account_id }); 
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(330);
            IControl tax_payer = editor.CreateCheckBox("tax_payer", "Плательщик НДС")
                .SetLabelWidth(labelWidth);
            IControl supplier = editor.CreateCheckBox("supplier", "Поставщик")
                .SetLabelWidth(labelWidth);
            IControl buyer = editor.CreateCheckBox("buyer", "Покупатель")
                .SetLabelWidth(labelWidth);

            editor.Container.Add(new IControl[] {
                name,
                parent,
                short_name,
                full_name,
                inn,
                kpp,
                ogrn,
                okpo,
                okopf,
                account,
                tax_payer,
                supplier,
                buyer
            });

            dependentViewer.AddDependentViewers(new string[] { "view-contract", "view-account", "view-employee", "view-balance-contr" });
        }

        protected override string GetSelect()
        {
            return "select id, name, parent_id, short_name, full_name, inn, kpp, ogrn, okpo, okopf_id, account_id, tax_payer, supplier, buyer from contractor where id = :id";
        }

        protected override string GetUpdate(Contractor contractor)
        {
            return "update contractor set name = :name, parent_id = :parent_id, short_name = :short_name, full_name = :full_name, inn = :inn, kpp = :kpp, ogrn = :ogrn, okpo = :okpo, okopf_id = :okopf_id, account_id = :account_id, tax_payer = :tax_payer, supplier = :supplier, buyer = :buyer where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }
    }
}