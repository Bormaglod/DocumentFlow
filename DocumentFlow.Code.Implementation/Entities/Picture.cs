using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.PictureImp
{
    public class Picture : IDirectory
    {
        public Guid id { get; protected set; }
        public Guid? parent_id { get; set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string size_small { get; set; }
        public string size_large { get; set; }
        public string img_name { get; set; }
        public string note { get; set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class PictureBrowser : BrowserCodeBase<Picture>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                p.id, 
                p.status_id, 
                s.note as status_name, 
                p.code, 
                p.name, 
                p.parent_id, 
                p.size_small, 
                p.size_large, 
                p.img_name, 
                p.note 
            from picture p 
                join status s on (s.id = p.status_id) 
            where {0}";

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
                .SetWidth(120);

            columns.CreateText("name", "Наименование")
                .SetHideable(false)
                .SetAutoSizeColumnsMode(SizeColumnsMode.LastColumnFill);

            columns.CreateImage("size_small", "Изображение")
                .SetWidth(100);

            columns.CreateSortedColumns()
                .Add("name", SortDirection.Ascending);
        }

        public IEditorCode CreateEditor()
        {
            return new PictureEditor();
        }

        public override IEnumerable<Picture> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Picture>(GetSelect(), new { parent_id = parameters.ParentId });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "p.parent_id is not distinct from :parent_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "p.id = :id");
        }
    }

    public class PictureEditor : EditorCodeBase<Picture>, IEditorCode
    {
        private const int labelWidth = 160;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            IControl code = editor.CreateTextBox("code", "Код")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);
            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);
            IControl img_name = editor.CreateTextBox("img_name", "Наименование (файл)")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);
            IControl note = editor.CreateTextBox("note", "Примечание")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(450);
            IControl size_small = editor.CreateImageBox("size_small", "Изображение 16х16")
                .SetLabelWidth(labelWidth);
            IControl size_large = editor.CreateImageBox("size_large", "Изображение 30х30")
                .SetLabelWidth(labelWidth);

            editor.Container.Add(new IControl[] {
                code,
                name,
                img_name,
                note,
                size_small,
                size_large
            });
        }

        protected override string GetSelect()
        {
            return "select id, code, name, parent_id, size_small, size_large, img_name, note from picture where id = :id";
        }

        protected override string GetUpdate(Picture picture)
        {
            return "update picture set code = :code, name = :name, parent_id = :parent_id, size_small = :size_small, size_large = :size_large, img_name = :img_name, note = :note where id = :id";
        }
    }
}