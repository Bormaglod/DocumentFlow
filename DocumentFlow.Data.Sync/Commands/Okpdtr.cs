using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code.Implementation.OkpdtrImp
{
    public class Okpdtr : Directory
    {
    }

    public class OkpdtrBrowser : IBrowserCode, IBrowserOperation, IDataEditor
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

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;

            browser.CreateStatusColumnRenderer();

            browser.CreateColumns((columns) =>
            {
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
                    .Add("name", ListSortDirection.Ascending);
            });
        }

        IEditorCode IDataEditor.CreateEditor() => new OkpdtrEditor();

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Okpdtr>(baseSelect).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Okpdtr>(baseSelect + " where o.id = :id", new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from okpdtr where id = :id", new { id }, transaction);
        }
    }

    public class OkpdtrEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 120;

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
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

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select id, code, name from okpdtr where id = :id";
            return connection.QuerySingleOrDefault<Okpdtr>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into okpdtr default values returning id"; ;
            return connection.QuerySingle<Guid>(sql, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update okpdtr set code = :code, name = :name where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from okpdtr where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            return info.StatusCode == "compiled";
        }
    }
}
