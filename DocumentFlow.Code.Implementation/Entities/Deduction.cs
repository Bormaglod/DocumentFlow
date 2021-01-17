using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code.Implementation.DeductionImp
{
    public class Deduction : Directory
    {
        public int accrual_base { get; set; }
        public string accrual_base_name { get; protected set; }
        public decimal percentage { get; set; }
    }

    public class DeductionBrowser : IBrowserCode, IBrowserOperation, IDataEditor
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

        void IBrowserCode.Initialize(IBrowser browser)
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

        IEditorCode IDataEditor.CreateEditor() => new DeductionEditor();

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Deduction>(baseSelect).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Deduction>(baseSelect + " where d.id = :id", new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from deduction where id = :id", new { id }, transaction);
        }
    }

    public class DeductionEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 170;

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400);

            IControl accrual_base = editor.CreateChoice("accrual_base", "База для начисления", new Dictionary<int, string>() {
                [1] = "Материалы",
                [2] = "ФОТ"})
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

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select id, name, d.accrual_base, d.percentage from deduction d where id = :id";
            return connection.QuerySingleOrDefault<Deduction>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into deduction default values returning id";
            return connection.QuerySingle<Guid>(sql, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update deduction set name = :name, accrual_base = :accrual_base, percentage = :percentage where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from deduction where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }
    }
}
