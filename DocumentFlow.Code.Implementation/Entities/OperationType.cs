using System;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.OperationTypeImp
{
    public class OperationType : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public decimal hourly_salary { get; set; }
    }

    public class OperationTypeBrowser : BrowserCodeBase<OperationType>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                ot.id, 
                ot.status_id, 
                s.note as status_name, 
                ot.name, 
                ot.hourly_salary 
            from operation_type ot 
                join status s on (s.id = ot.status_id)";

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

            columns.CreateText("name", "Наименование")
                .SetHideable(false)
                .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

            columns.CreateNumeric("hourly_salary", "Расценка, руб./час", NumberFormatMode.Currency, decimalDigits: 2)
                .SetHorizontalAlignment(HorizontalAlignmentText.Right)
                .SetWidth(200);

            columns.CreateSortedColumns()
                .Add("name", SortDirection.Ascending);
        }

        public IEditorCode CreateEditor()
        {
            return new OperationTypeEditor();
        }

        protected override string GetSelect()
        {
            return baseSelect;
        }

        protected override string GetSelectById()
        {
            return baseSelect + " where ot.id = :id";
        }
    }

    public class OperationTypeEditor : EditorCodeBase<OperationType>, IEditorCode
    {
        private const int labelWidth = 140;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(380);
            IControl hourly_salary = editor.CreateCurrency("hourly_salary", "Расценка, руб./час")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(200);

            editor.Container.Add(new IControl[] {
                name,
                hourly_salary
            });

            dependentViewer.AddDependentViewers(new string[] { "view-archive-price" });
        }

        protected override string GetSelect()
        {
            return "select id, name, hourly_salary from operation_type where id = :id";
        }

        protected override string GetUpdate(OperationType operationType)
        {
            return "update operation_type set name = :name, hourly_salary = :hourly_salary where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return status_name == "compiled";
        }
    }
}
