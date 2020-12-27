using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Dapper;

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
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class OperationTypeBrowser : IBrowserCode, IBrowserOperation, IDataEditor
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

                columns.CreateText("name", "Наименование")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateNumeric("hourly_salary", "Расценка, руб./час", NumberFormatMode.Currency)
                    .SetHorizontalAlignment(HorizontalAlignment.Right)
                    .SetWidth(200);

                columns.CreateSortedColumns()
                    .Add("name", ListSortDirection.Ascending);
            });
        }

        IEditorCode IDataEditor.CreateEditor()
        {
            return new OperationTypeEditor();
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<OperationType>(baseSelect).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<OperationType>(baseSelect + " where ot.id = :id", new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from operation_type where id = :id", new { id }, transaction);
        }
    }

    public class OperationTypeEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 140;

        void IEditorCode.Initialize(IEditor editor, IDependentViewer dependentViewer)
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

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select id, name, hourly_salaryfrom operation_type where id = :id";
            return connection.QuerySingleOrDefault<OperationType>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into operation_type default values returning id";
            return connection.QuerySingle<Guid>(sql, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update operation_type set name = :name, hourly_salary = :hourly_salary where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from operation_type where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            return info.StatusCode == "compiled";
        }
    }
}
