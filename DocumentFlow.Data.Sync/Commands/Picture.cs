﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code.Implementation.PictureImp
{
    public class Picture : Directory
    {
        public string size_small { get; set; }
        public string size_large { get; set; }
        public string img_name { get; set; }
        public string note { get; set; }
    }

    public class PictureBrowser : IBrowserCode, IBrowserOperation, IDataEditor
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

        void IBrowserCode.Initialize(IBrowser browser)
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
                    .Add("name", ListSortDirection.Ascending);
            });
        }

        IEditorCode IDataEditor.CreateEditor() => new PictureEditor();

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Picture>(string.Format(baseSelect, "p.parent_id is not distinct from :parent_id"), new { parent_id = parameters.ParentId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Picture>(string.Format(baseSelect, "p.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from picture where id = :id", new { id }, transaction);
        }
    }

    public class PictureEditor : IEditorCode, IDataOperation
    {
        private const int labelWidth = 160;

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
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

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select id, code, name, parent_id, size_small, size_large, img_name, note from picture where id = :id";
            return connection.QuerySingleOrDefault<Picture>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into picture (parent_id) values (:parent_id) returning id";
            return connection.QuerySingle<Guid>(sql, new { parent_id = parameters.ParentId }, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update picture set code = :code, name = :name, parent_id = :parent_id, size_small = :size_small, size_large = :size_large, img_name = :img_name, note = :note where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from picture where id = :id", new { id = id.oid }, transaction);
        }
    }
}