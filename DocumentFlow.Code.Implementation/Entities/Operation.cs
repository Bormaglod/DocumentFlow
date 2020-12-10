using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.OperationImp
{
    public class Operation : IDirectory
    {
        public Guid id { get; protected set; }
        public Guid? parent_id { get; set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int produced { get; set; }
        public int prod_time { get; set; }
        public int production_rate { get; set; }
        public Guid? type_id { get; set; }
        public string operation_type_name { get; protected set; }
        public decimal salary { get; set; }
        public int? program { get; set; }
        public Guid? measurement_id { get; set; }
        public string abbreviation { get; set; }
        public int length { get; set; }
        public decimal left_cleaning { get; set; }
        public int left_sweep { get; set; }
        public decimal right_cleaning { get; set; }
        public int right_sweep { get; set; }

        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class OperationBrowser : BrowserCodeBase<Operation>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                o.id, 
                o.status_id, 
                s.note as status_name, 
                o.code, 
                o.name, 
                o.parent_id, 
                o.produced, 
                o.prod_time, 
                o.production_rate, 
                ot.name as operation_type_name, 
                o.salary, 
                o.program, 
                m.abbreviation,
                o.length,
                o.left_cleaning,
                o.left_sweep,
                o.right_cleaning,
                o.right_sweep
            from operation o 
                join status s on (s.id = o.status_id) 
                left join operation_type ot on (ot.id = o.type_id) 
                left join measurement m on (m.id = o.measurement_id) 
            where 
                {0}";

        public void Initialize(IBrowser browser)
        {
            browser.AllowGrouping = true;
            browser.DataType = DataType.Directory;

            browser.CreateStatusColumnRenderer();

            IColumnCollection columns = browser.Columns;

            columns.CreateText("id", "Id")
                .SetWidth(180)
                .SetVisible(false);

            columns.CreateInteger("status_id", "Код состояния")
                .SetWidth(80)
                .SetVisible(false)
                .SetAllowGrouping(true);

            columns.CreateText("status_name", "Состояние")
                .SetWidth(110)
                .SetVisible(false)
                .SetAllowGrouping(true);

            columns.CreateText("code", "Код")
                .SetWidth(150)
                .SetVisible(false);

            columns.CreateText("name", "Наименование")
                .SetHideable(false)
                .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

            columns.CreateText("abbreviation", "Ед. изм.")
                .SetWidth(100)
                .SetAllowGrouping(true)
                .SetHorizontalAlignment(HorizontalAlignment.Center)
                .SetVisibility(false);

            columns.CreateInteger("produced", "Выработка")
                .SetWidth(100)
                .SetHorizontalAlignment(HorizontalAlignment.Right)
                .SetVisible(false)
                .SetVisibility(false);

            columns.CreateInteger("prod_time", "Время выработки, мин.")
                .SetWidth(100)
                .SetHorizontalAlignment(HorizontalAlignment.Right)
                .SetVisible(false)
                .SetVisibility(false);

            columns.CreateInteger("production_rate", "Норма выработки, ед./час")
                .SetWidth(100)
                .SetHorizontalAlignment(HorizontalAlignment.Right)
                .SetVisible(false)
                .SetVisibility(false);

            columns.CreateText("operation_type_name", "Тип операции")
                .SetWidth(250)
                .SetAllowGrouping(true)
                .SetVisibility(false);

            columns.CreateNumeric("salary", "Зар. плата, руб.", NumberFormatMode.Currency)
                .SetWidth(150)
                .SetVisibility(false)
                .SetHorizontalAlignment(HorizontalAlignment.Right);

            columns.CreateInteger("length", "Длина провода")
                .SetWidth(100)
                .SetHorizontalAlignment(HorizontalAlignment.Right)
                .SetVisible(false)
                .SetVisibility(false);

            columns.CreateNumeric("left_cleaning", "Длина зачистки слева")
                .SetDecimalDigits(1)
                .SetWidth(100)
                .SetHorizontalAlignment(HorizontalAlignment.Right)
                .SetVisible(false)
                .SetVisibility(false);

            columns.CreateInteger("left_sweep", "Окно слева")
                .SetWidth(100)
                .SetHorizontalAlignment(HorizontalAlignment.Right)
                .SetVisible(false)
                .SetVisibility(false);

            columns.CreateNumeric("right_cleaning", "Длина зачистки справа")
                .SetDecimalDigits(1)
                .SetWidth(100)
                .SetHorizontalAlignment(HorizontalAlignment.Right)
                .SetVisible(false)
                .SetVisibility(false);

            columns.CreateInteger("right_sweep", "Окно справа")
                .SetWidth(100)
                .SetHorizontalAlignment(HorizontalAlignment.Right)
                .SetVisible(false)
                .SetVisibility(false);

            columns.CreateInteger("program", "Программа")
                .SetWidth(100)
                .SetHorizontalAlignment(HorizontalAlignment.Right)
                .SetVisible(false)
                .SetVisibility(false);

            columns.CreateSortedColumns()
                .Add("code", ListSortDirection.Ascending);

            browser.ChangeParent += Browser_ChangeParent;
        }

        public IEditorCode CreateEditor()
        {
            return new OperationEditor();
        }

        public override IEnumerable<Operation> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Operation>(GetSelect(), new { parent_id = parameters.ParentId });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "o.parent_id is not distinct from :parent_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "o.id = :id");
        }

        private void Browser_ChangeParent(object sender, ChangeParentEventArgs e)
        {
            IBrowser browser = sender as IBrowser;
            if (browser != null)
            {
                string root = string.Empty;
                if (browser.Parameters.ParentId.HasValue)
                {
                    Operation op = browser.ExecuteSqlCommand<Operation>("select * from operation where id = :id", new { id = browser.Parameters.ParentId });
                    if (op.parent_id.HasValue)
                        root = browser.ExecuteSqlCommand<string>("select root_code_operation(:id)", new { id = browser.Parameters.ParentId });
                    else
                        root = op.code;
                }

                foreach (string column in new string[] { "program", "length", "left_cleaning", "left_sweep", "right_cleaning", "right_sweep" })
                {
                    browser.Columns[column].Visibility = root == "Резка";
                }

                foreach (string column in new string[] { "code", "abbreviation", "produced", "prod_time", "production_rate", "operation_type_name", "salary" })
                {
                    browser.Columns[column].Visibility = !string.IsNullOrEmpty(root);
                }

                browser.Columns["code"].Visible = !string.IsNullOrEmpty(root);
            }
        }
    }

    public class OperationEditor : EditorCodeBase<Operation>, IEditorCode
    {
        private const int labelWidth = 200;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const string typeSelect = "select id, name from operation_type where status_id = 1002 order by name";
            const string folderSelect = "select id, parent_id, name, status_id from operation where status_id = 500 order by name";
            const string measurementSelect = "select id, name from measurement where status_id = 1001 order by name";
			const string programSelect = "with all_programs as ( select generate_series(1, 99) as id ) select a.id, '' || a.id as name from all_programs a left join operation on (program = a.id and status_id = 1002) where program is null or program = :program order by a.id";

            List<IControl> controls = new List<IControl>();

            controls.Add(editor.CreateTextBox("code", "Код")
                .SetLabelWidth(labelWidth));
            controls.Add(editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(380));
            controls.Add(editor.CreateSelectBox("type_id", "Тип операции", (c) => { return c.Query<ComboBoxDataItem>(typeSelect); })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(330));
            controls.Add(editor.CreateSelectBox("parent_id", "Группа", (c) => { return c.Query<GroupDataItem>(folderSelect); }, showOnlyFolder: true)
                .SetLabelWidth(labelWidth)
                .SetControlWidth(360));
            controls.Add(editor.CreateComboBox("measurement_id", "Еденица измерения", (conn) => { return conn.Query<ComboBoxDataItem>(measurementSelect); })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(250));
            controls.Add(editor.CreateInteger("produced", "Выработка")
                .SetLabelWidth(labelWidth));
            controls.Add(editor.CreateInteger("prod_time", "Время выработки, сек.")
                .SetLabelWidth(labelWidth));
            controls.Add(editor.CreateInteger("production_rate", "Норма выработка, ед./час")
                .SetLabelWidth(labelWidth));
            controls.Add(editor.CreateCurrency("salary", "Зарплата, руб.")
                .SetLabelWidth(labelWidth));

            string root = editor.ExecuteSqlCommand<string>("select root_code_operation(:oid)", new { editor.Entity.oid });
            if (root == "Резка")
            {
				controls.Add(editor.CreateChoice("program", "Программа", (conn) => 
                	{
                    	Operation o = editor.Entity as Operation;
	                    return conn.Query<ChoiceDataItem>(programSelect, new { o.program }); 
    	            })
        	        .SetLabelWidth(labelWidth)
            	    .SetControlWidth(250));
                controls.Add(editor.CreateInteger("length", "Длина провода")
                    .SetLabelWidth(labelWidth));
                controls.Add(editor.CreateNumeric("left_cleaning", "Длина зачистки с начала провода", numberDecimalDigits: 1)
                    .SetLabelWidth(labelWidth));
                controls.Add(editor.CreateInteger("left_sweep", "Ширина окна на которое снимается изоляция в начале провода")
                    .SetLabelWidth(labelWidth));
                controls.Add(editor.CreateNumeric("right_cleaning", "Длина зачистки с конца провода", numberDecimalDigits: 1)
                    .SetLabelWidth(labelWidth));
                controls.Add(editor.CreateInteger("right_sweep", "Ширина окна на которое снимается изоляция в конце провода")
                    .SetLabelWidth(labelWidth));
            }

            editor.Container.Add(controls.ToArray());

            dependentViewer.AddDependentViewers(new string[] { "view-archive-price" });
        }

        protected override string GetSelect()
        {
            return "select id, code, name, parent_id, produced, prod_time, production_rate, type_id, salary, length, left_cleaning, left_sweep, right_cleaning, right_sweep, program, measurement_id from operation where id = :id";
        }

        protected override string GetUpdate(Operation operation)
        {
            return "update operation set code = :code, name = :name, parent_id = :parent_id, produced = :produced, prod_time = :prod_time, production_rate = :production_rate, type_id = :type_id, salary = :salary, length = :length, left_cleaning = :left_cleaning, left_sweep = :left_sweep, right_cleaning = :right_cleaning, right_sweep = :right_sweep, program = :program, measurement_id = :measurement_id where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }

    }
}
