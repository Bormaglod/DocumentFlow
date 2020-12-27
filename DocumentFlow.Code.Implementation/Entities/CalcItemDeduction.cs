using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.CalcItemDeductionImp
{
    public class CalcItemDeduction : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string calculation_name { get; protected set; }
        public string code { get; set; }
        public string name { get; set; }

        /// <summary>
        /// Ссылка на материал (goods)
        /// </summary>

        public Guid item_id { get; set; }

        /// <summary>
        /// База для отчисления
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// Сумма отчисления
        /// </summary>
        public decimal cost { get; set; }

        /// <summary>
        /// Процент от базы
        /// </summary>
        public decimal percentage { get; set; }

        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class CalcItemDeductionBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelect = @"
            select 
                cid.id, 
                cid.status_id, 
                s.note as status_name, 
                d.name, 
                cid.cost, 
                cid.price, 
                cid.percentage 
            from calc_item_deduction cid 
                join status s on (s.id = cid.status_id) 
                left join deduction d on (d.id = cid.item_id) 
            where {0}";

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.ToolBar.ButtonStyle = ToolStripItemDisplayStyle.Image;
            browser.ToolBar.IconSize = ButtonIconSize.Small;
            browser.CommandBarVisible = false;

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

                columns.CreateText("name", "Отчисление")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateNumeric("percentage", "Процент", NumberFormatMode.Percent)
                    .SetDecimalDigits(2)
                    .SetWidth(150);

                columns.CreateNumeric("price", "База", NumberFormatMode.Currency)
                    .SetWidth(150)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("cost", "Сумма", NumberFormatMode.Currency)
                    .SetWidth(150)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                    .AddColumn("name", RowSummaryType.CountAggregate, "Всего наименований: {Count}")
                    .AddColumn("cost", RowSummaryType.DoubleAggregate, "{Sum:c}");

                columns.CreateSortedColumns()
                    .Add("name", ListSortDirection.Ascending);
            });
        }

        IEditorCode IDataEditor.CreateEditor()
        {
            return new CalcItemDeductionEditor();
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<CalcItemDeduction>(string.Format(baseSelect, "cid.owner_id = :owner_id"), new { owner_id = parameters.OwnerId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<CalcItemDeduction>(string.Format(baseSelect, "cid.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from calc_item_deduction where id = :id", new { id }, transaction);
        }
    }

    public class CalcItemDeductionEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 200;

        void IEditorCode.Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const string itemSelect = "select id, status_id, name, parent_id from deduction where status_id in (500, 1002) order by name";

            IControl calculation_name = editor.CreateTextBox("calculation_name", "Калькуляция")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(300)
                .SetEnabled(false);

            IControl item_id = editor.CreateSelectBox("item_id", "Отчисление", (c) => { return c.Query<GroupDataItem>(itemSelect); })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(450);

            IControl percentage = editor.CreateNumeric("percentage", "Процент", numberDecimalDigits: 2)
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);

            IControl price = editor.CreateCurrency("price", "База для отчисления")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(200);

            IControl cost = editor.CreateCurrency("cost", "Сумма отчисления")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(200);

            editor.Container.Add(new IControl[] {
                calculation_name,
                item_id,
                percentage,
                price,
                cost
            });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = @"
                select 
                    cid.id, 
                    c.name as calculation_name, 
                    cid.item_id, 
                    cid.cost, 
                    cid.price, 
                    cid.percentage 
                from calc_item_deduction cid 
                    left join calculation c on (c.id = cid.owner_id) 
                where cid.id = :id";
            return connection.QuerySingleOrDefault<CalcItemDeduction>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into calc_item_deduction (owner_id) values (:owner_id) returning id";
            return connection.QuerySingle<Guid>(sql, new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update calc_item_deduction set item_id = :item_id, percentage = :percentage, price = :price, cost = :cost where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from calc_item_deduction where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            if (dataName == "calculation_name")
                return false;

            return info.StatusCode == "compiled";
        }
    }
}