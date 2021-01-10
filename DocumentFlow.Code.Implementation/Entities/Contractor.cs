using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Code.System;
using DocumentFlow.Data;
using DocumentFlow.Data.Base;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Implementation.ContractorImp
{
    public class Contractor : Directory
    {
        public string short_name { get; set; }
        public string full_name { get; set; }
        public decimal? inn { get; set; }
        public decimal? kpp { get; set; }
        public decimal? ogrn { get; set; }
        public decimal? okpo { get; set; }
        public Guid? okopf_id { get; set; }
        public string okopf_name { get; set; }
        public Guid? account_id { get; set; }
    }

    public class ContractorBrowser : IBrowserCode, IBrowserOperation, IDataEditor
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
                o.name as okopf_name
            from contractor c 
                join status s on (s.id = c.status_id) 
                left join okopf o on (o.id = c.okopf_id) 
            where 
                {0}";

        void IBrowserCode.Initialize(IBrowser browser)
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

	            columns.CreateSortedColumns()
    	            .Add("name", ListSortDirection.Ascending);
			});

			browser.ChangeParent += Browser_ChangeParent;
        }

        IEditorCode IDataEditor.CreateEditor() => new ContractorEditor();

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Contractor>(string.Format(baseSelect, "c.parent_id is not distinct from :parent_id"), new { parent_id = parameters.ParentId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Contractor>(string.Format(baseSelect, "c.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from contractor where id = :id", new { id }, transaction);
        }

		private void Browser_ChangeParent(object sender, ChangeParentEventArgs e)
		{
            if (sender is IBrowser browser)
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

                foreach (string column in new string[] { "kpp", "okpo", "ogrn", "okopf_name" })
                {
                    browser.Columns[column].Visibility = root == "Юр";
                }

                if (!string.IsNullOrEmpty(root))
                {
                    string format = root == "Юр" ? "0000 00000 0" : "0000 000000 00";
                    if (browser.Columns["inn"] is INumericColumn numericColumn)
                        numericColumn.Format = format;
                }
            }
        }
    }

    public class ContractorEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 190;

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const string folderSelect = "select id, parent_id, name, status_id from contractor where status_id = 500 order by name";
            const string okopfSelect = "select id, name from okopf where status_id = 1001 order by name";
            const string accountSelect = "select id, name from account where (owner_id = :id and status_id = 1002) or (id = :account_id)";

			string root = string.Empty;
			if (editor.Parameters.ParentId.HasValue)
            {
                Contractor g = database.ExecuteSqlCommand<Contractor>("select * from contractor where id = :id", new { id = editor.Parameters.ParentId });
                if (g.parent_id.HasValue)
            		root = database.ExecuteSqlCommand<string>("select root_code_contractor(:id)", new { id = editor.Parameters.ParentId });
                else
                    root = g.code;
            }

			if (string.IsNullOrEmpty(root))
				throw new Exception("Для добавления контрагента необходимо выбрать соответствующую папку.");

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

			string format = root == "Юр" ? "#### ##### #" : "#### ###### ##";
            IControl inn = editor.CreateMaskedText<decimal>("inn", "ИНН", format)
				.AsNullable()
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);

			editor.Container.Add(new IControl[] {
                name,
                parent,
                short_name,
                full_name,
                inn
            });

			if (root == "Юр")
			{
	            IControl kpp = editor.CreateMaskedText<decimal>("kpp", "КПП", "#### ## ###")
					.AsNullable()
        	        .SetLabelWidth(labelWidth)
            	    .SetControlWidth(150);

		        IControl ogrn = editor.CreateMaskedText<decimal>("ogrn", "ОГРН", "# ## ## ## ##### #")
					.AsNullable()
            	    .SetLabelWidth(labelWidth)
                	.SetControlWidth(150);

	            IControl okpo = editor.CreateMaskedText<decimal>("okpo", "ОКПО", "## ##### #")
					.AsNullable()
        	        .SetLabelWidth(labelWidth)
            	    .SetControlWidth(150);

	            IControl okopf = editor.CreateComboBox("okopf_id", "ОКОПФ", (c) => { return c.Query<NameDataItem>(okopfSelect); })
    	            .SetLabelWidth(labelWidth)
        	        .SetControlWidth(330);

	            IControl account = editor.CreateComboBox("account_id", "Расчётный счёт", (e, c) => {
    	                Contractor contractor = e.Entity as Contractor;
        	            return c.Query<NameDataItem>(accountSelect, new { contractor.id, contractor.account_id }); 
            	    })
                	.SetLabelWidth(labelWidth)
	                .SetControlWidth(330);

				editor.Container.Add(new IControl[] {
	                kpp,
    	            ogrn,
        	        okpo,
            	    okopf,
                	account
            	});
			}

            dependentViewer.AddDependentViewers(new string[] { "view-contract", "view-account", "view-employee", "view-balance-contr" });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select id, name, parent_id, short_name, full_name, inn, kpp, ogrn, okpo, okopf_id, account_id from contractor where id = :id";
            return connection.QuerySingleOrDefault<Contractor>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into contractor default values returning id";
            return connection.QuerySingle<Guid>(sql, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update contractor set name = :name, parent_id = :parent_id, short_name = :short_name, full_name = :full_name, inn = :inn, kpp = :kpp, ogrn = :ogrn, okpo = :okpo, okopf_id = :okopf_id, account_id = :account_id where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from contractor where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }
    }
}