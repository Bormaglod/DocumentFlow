using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.CalcItemDeductionImp
{
    public class CalcItemDeduction : IDirectory
    {
#pragma warning disable IDE1006 // Стили именования
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
#pragma warning restore IDE1006 // Стили именования

        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class CalcItemDeductionBrowser : BrowserCodeBase<CalcItemDeduction>, IBrowserCode
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

        public void Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.ToolBar.ButtonStyle = ButtonDisplayStyle.Image;
            browser.ToolBar.IconSize = ButtonIconSize.Small;
            browser.CommandBarVisible = false;

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

            columns.CreateText("name", "Отчисление")
                .SetHideable(false)
                .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

            columns.CreateNumeric("percentage", "Процент", NumberFormatMode.Percent)
                .SetDecimalDigits(2)
                .SetWidth(150);

            columns.CreateNumeric("price", "База", NumberFormatMode.Currency)
                .SetWidth(150)
                .SetHorizontalAlignment(HorizontalAlignmentText.Right);

            columns.CreateNumeric("cost", "Сумма", NumberFormatMode.Currency)
                .SetWidth(150)
                .SetHorizontalAlignment(HorizontalAlignmentText.Right);

            columns.CreateGroupSummaryRow()
                .AddColumn("name", RowSummaryType.CountAggregate, "Всего наименований: {Count}")
                .AddColumn("cost", RowSummaryType.DoubleAggregate, "{Sum:c}");

            columns.CreateSortedColumns()
                .Add("name", SortDirection.Ascending);
        }

        public IEditorCode CreateEditor()
        {
            return new CalcItemDeductionEditor();
        }

        public override IEnumerable<CalcItemDeduction> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<CalcItemDeduction>(GetSelect(), new { owner_id = parameters.OwnerId });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "cid.owner_id = :owner_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "cid.id = :id");
        }
    }

    public class CalcItemDeductionEditor : EditorCodeBase<CalcItemDeduction>, IEditorCode
    {
        private const int labelWidth = 200;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
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

        public override TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            return connection.QuerySingle<TId>(GetInsert(), new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        protected override string GetSelect()
        {
            return "select cid.id, c.name as calculation_name, cid.item_id, cid.cost, cid.price, cid.percentage from calc_item_deduction cid left join calculation c on (c.id = cid.owner_id) where cid.id = :id";
        }

        protected override string GetInsert()
        {
            return "insert into calc_item_deduction (owner_id) values (:owner_id) returning id";
        }

        protected override string GetUpdate(CalcItemDeduction item)
        {
            return "update calc_item_deduction set item_id = :item_id, percentage = :percentage, price = :price, cost = :cost where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            if (field == "calculation_name")
                return false;

            return status_name == "compiled";
        }
    }
}