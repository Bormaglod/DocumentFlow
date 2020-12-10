using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.DeductionImp
{
    public class Deduction : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int accrual_base { get; set; }
        public string accrual_base_name { get; protected set; }
        public decimal percentage { get; set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class DeductionBrowser : BrowserCodeBase<Deduction>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                d.id, 
                d.status_id, 
                s.note as status_name, 
                d.name, 
                d.accrual_base,
                case d.accrual_base 
                    when 1 then 'Материалы' 
                    when 2 then 'ФОТ' 
                    else 'не установлено' 
                end as accrual_base_name, 
                d.percentage 
            from deduction d 
                join status s on (s.id = d.status_id)";

        public void Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.AllowGrouping = true;

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

                columns.CreateText("accrual_base_name", "База для начисления")
                    .SetAllowGrouping(true)
                    .SetWidth(160);

                columns.CreateNumeric("percentage", "Процент", NumberFormatMode.Percent)
                    .SetDecimalDigits(2)
                    .SetWidth(100);

                columns.CreateSortedColumns()
                    .Add("name", ListSortDirection.Ascending);
            });
        }

        public IEditorCode CreateEditor()
        {
            return new DeductionEditor();
        }

        protected override string GetSelect()
        {
            return baseSelect;
        }

        protected override string GetSelectById()
        {
            return baseSelect + " where d.id = :id";
        }
    }

    public class DeductionEditor : EditorCodeBase<Deduction>, IEditorCode
    {
        private const int labelWidth = 170;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400);
            IControl accrual_base = editor.CreateChoice("accrual_base", "База для начисления", new Dictionary<int, string>() { { 1, "Материалы" }, { 2, "ФОТ" } })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(180);
            IControl percentage = editor.CreatePercent("percentage", "Процент", percentDecimalDigits: 2)
                .SetLabelWidth(labelWidth)
                .SetControlWidth(70);

            editor.Container.Add(new IControl[] {
                name,
                accrual_base,
                percentage
            });
        }

        protected override string GetSelect()
        {
            return "select id, name, d.accrual_base, d.percentage from deduction d where id = :id";
        }

        protected override string GetUpdate(Deduction deduction)
        {
            return "update deduction set name = :name, accrual_base = :accrual_base, percentage = :percentage where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }
    }
}
