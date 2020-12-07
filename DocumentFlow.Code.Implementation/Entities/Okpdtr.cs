using System;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.OkpdtrImp
{
    public class Okpdtr : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class OkpdtrBrowser : BrowserCodeBase<Okpdtr>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                o.id, 
                o.status_id, 
                s.note as status_name, 
                o.code, 
                o.name 
            from okpdtr o 
                join status s on s.id = o.status_id";

        public void Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;

            browser.CreateStatusColumnRenderer();

            IColumnCollection columns = browser.Columns;

            columns.CreateText("id", "Id")
                .SetWidth(100)
                .SetVisible(false);

            columns.CreateInteger("status_id", "Код состояния")
                .SetWidth(80)
                .SetVisible(false);

            columns.CreateText("status_name", "Состояние")
                .SetWidth(100)
                .SetVisible(false);

            columns.CreateText("code", "Код")
                .SetWidth(110);

            columns.CreateText("name", "Наименование")
                .SetHideable(false)
                .SetAutoSizeColumnsMode(SizeColumnsMode.LastColumnFill);

            columns.CreateSortedColumns()
                .Add("name", SortDirection.Ascending);
        }

        public IEditorCode CreateEditor()
        {
            return new OkpdtrEditor();
        }

        protected override string GetSelect()
        {
            return baseSelect;
        }

        protected override string GetSelectById()
        {
            return baseSelect + " where o.id = :id";
        }
    }

    public class OkpdtrEditor : EditorCodeBase<Okpdtr>, IEditorCode
    {
        private const int labelWidth = 120;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            IControl code = editor.CreateTextBox("code", "Код")
                .SetLabelWidth(labelWidth);
            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(360);

            editor.Container.Add(new IControl[] {
                code,
                name
            });
        }

        protected override string GetSelect()
        {
            return "select id, code, name from okpdtr where id = :id";
        }

        protected override string GetUpdate(Okpdtr okpdtr)
        {
            return "update okpdtr set code = :code, name = :name where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return status_name == "compiled";
        }
    }
}
