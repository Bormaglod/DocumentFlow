using System;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.OkopfImp
{
    public class Okopf : IDirectory
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

    public class OkopfBrowser : BrowserCodeBase<Okopf>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                o.id, 
                o.status_id, 
                s.note as status_name, 
                o.code, 
                o.name 
            from okopf o 
                join status s on s.id = o.status_id";

        public void Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;

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
                    .SetWidth(100)
                    .SetVisible(false);

                columns.CreateText("code", "Код")
                    .SetWidth(110);

                columns.CreateText("name", "Наименование")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.LastColumnFill);

                columns.CreateSortedColumns()
                    .Add("name", SortDirection.Ascending);
            });
        }

        protected override string GetSelect()
        {
            return baseSelect;
        }

        protected override string GetSelectById()
        {
            return baseSelect + " where o.id = :id";
        }

        public IEditorCode CreateEditor()
        {
            return new OkopfEditor();
        }
    }

    public class OkopfEditor : EditorCodeBase<Okopf>, IEditorCode
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
            return "select id, code, name from okopf where id = :id";
        }

        protected override string GetUpdate(Okopf okopf)
        {
            return "update okopf set code = :code, name = :name where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return status_name == "compiled";
        }
    }
}