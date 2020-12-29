using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.System;
using DocumentFlow.Data;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Implementation.OperationImp
{
    public class Operation : Directory
    {
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
    }

    public class OperationBrowser : IBrowserCode, IBrowserOperation, IDataEditor
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
            });

            browser.ChangeParent += Browser_ChangeParent;
        }

        IEditorCode IDataEditor.CreateEditor()
        {
            return new OperationEditor();
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Operation>(string.Format(baseSelect, "o.parent_id is not distinct from :parent_id"), new { parent_id = parameters.ParentId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Operation>(string.Format(baseSelect, "o.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from operation where id = :id", new { id }, transaction);
        }

        private void Browser_ChangeParent(object sender, ChangeParentEventArgs e)
        {
#pragma warning disable IDE0019 // Используйте сопоставление шаблонов
            IBrowser browser = sender as IBrowser;
#pragma warning restore IDE0019 // Используйте сопоставление шаблонов
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

    public class OperationEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 200;

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const string typeSelect = "select id, name from operation_type where status_id = 1002 order by name";
            const string folderSelect = "select id, parent_id, name, status_id from operation where status_id = 500 order by name";
            const string measurementSelect = "select id, name from measurement where status_id = 1001 order by name";
			const string programSelect = "with all_programs as ( select generate_series(1, 99) as id ) select a.id, '' || a.id as name from all_programs a left join operation on (program = a.id and status_id = 1002) where program is null or program = :program order by a.id";

            List<IControl> controls = new List<IControl>
            {
                editor.CreateTextBox("code", "Код")
                    .SetLabelWidth(labelWidth),

                editor.CreateTextBox("name", "Наименование")
                    .SetLabelWidth(labelWidth)
                    .SetControlWidth(380),

                editor.CreateSelectBox("type_id", "Тип операции", (c) => { return c.Query<ComboBoxDataItem>(typeSelect); })
                    .SetLabelWidth(labelWidth)
                    .SetControlWidth(330),

                editor.CreateSelectBox("parent_id", "Группа", (c) => { return c.Query<GroupDataItem>(folderSelect); }, showOnlyFolder: true)
                    .SetLabelWidth(labelWidth)
                    .SetControlWidth(360),

                editor.CreateComboBox("measurement_id", "Еденица измерения", (conn) => { return conn.Query<ComboBoxDataItem>(measurementSelect); })
                    .SetLabelWidth(labelWidth)
                    .SetControlWidth(250),

                editor.CreateInteger("produced", "Выработка")
                    .SetLabelWidth(labelWidth),

                editor.CreateInteger("prod_time", "Время выработки, сек.")
                    .SetLabelWidth(labelWidth),

                editor.CreateInteger("production_rate", "Норма выработка, ед./час")
                    .SetLabelWidth(labelWidth),

                editor.CreateCurrency("salary", "Зарплата, руб.")
                    .SetLabelWidth(labelWidth)
            };

            string root = database.ExecuteSqlCommand<string>("select root_code_operation(:oid)", new { editor.Entity.oid });
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

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select id, code, name, parent_id, produced, prod_time, production_rate, type_id, salary, length, left_cleaning, left_sweep, right_cleaning, right_sweep, program, measurement_id from operation where id = :id";
            return connection.QuerySingleOrDefault<Operation>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into operation default values returning id";
            return connection.QuerySingle<Guid>(sql, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update operation set code = :code, name = :name, parent_id = :parent_id, produced = :produced, prod_time = :prod_time, production_rate = :production_rate, type_id = :type_id, salary = :salary, length = :length, left_cleaning = :left_cleaning, left_sweep = :left_sweep, right_cleaning = :right_cleaning, right_sweep = :right_sweep, program = :program, measurement_id = :measurement_id where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from operation where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }

    }
}
