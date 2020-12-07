using System;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.MeasurementImp
{
    public class Measurement : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string abbreviation { get; set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class MeasurementBrowser : BrowserCodeBase<Measurement>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                m.id, 
                m.status_id, 
                s.note as status_name, 
                m.code, 
                m.name, 
                m.abbreviation 
            from measurement m 
                join status s on s.id = m.status_id";

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

            columns.CreateText("abbreviation", "Сокр. наименование")
                .SetWidth(160);

            columns.CreateSortedColumns()
                .Add("name", SortDirection.Ascending);
        }

        public IEditorCode CreateEditor()
        {
            return new MeasurementEditor();
        }

        protected override string GetSelect()
        {
            return baseSelect;
        }

        protected override string GetSelectById()
        {
            return baseSelect + " where id = :id";
        }
    }

    public class MeasurementEditor : EditorCodeBase<Measurement>, IEditorCode
    {
        private const int labelWidth = 360;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            IControl code = editor.CreateTextBox("code", "Код")
                .SetLabelWidth(labelWidth);
            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(360);
            IControl abbreviation = editor.CreateTextBox("abbreviation", "Сокр. наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(360);

            editor.Container.Add(new IControl[] {
                code,
                name,
                abbreviation
            });
        }

        protected override string GetSelect()
        {
            return "select id, code, name, abbreviation from measurement where id = :id";
        }

        protected override string GetUpdate(Measurement measurement)
        {
            return "update measurement set code = :code, name = :name, abbreviation = :abbreviation where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return status_name == "compiled";
        }
    }
}
