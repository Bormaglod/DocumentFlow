using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.CalcItemGoodsImp
{
    public class CalcItemGoods : IDirectory
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
        /// Цена за единицу материала
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal cost { get; set; }

        /// <summary>
        /// Количество материала
        /// </summary>
        public decimal amount { get; set; }

        /// <summary>
        /// Процент использования в операциях
        /// </summary>
        public int percent_uses { get; set; }
#pragma warning restore IDE1006 // Стили именования

        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class CalcItemGoodsBrowser : BrowserCodeBase<CalcItemGoods>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                cg.id, 
                cg.status_id, 
                s.note as status_name, 
                g.name, 
                cg.cost, 
                cg.price, 
                cg.amount, 
                (case when coalesce(cg.amount, 0) = 0 then 0 else coalesce(cg.uses, 0) * 100 / coalesce(cg.amount, 0) end)::integer as percent_uses 
            from calc_item_goods cg 
                join status s on (s.id = cg.status_id)
                left join goods g on (g.id = cg.item_id) 
            where {0}";

        public void Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.ToolBar.ButtonStyle = ToolStripItemDisplayStyle.Image;
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

            columns.CreateText("name", "Номенклатура")
                .SetHideable(false)
                .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

            columns.CreateNumeric("amount", "Количество")
                .SetDecimalDigits(3)
                .SetWidth(150)
                .SetHorizontalAlignment(HorizontalAlignment.Right);

            columns.CreateNumeric("price", "Цена", NumberFormatMode.Currency)
                .SetWidth(150)
                .SetHorizontalAlignment(HorizontalAlignment.Right);

            columns.CreateNumeric("cost", "Стоимость", NumberFormatMode.Currency)
                .SetWidth(150)
                .SetHorizontalAlignment(HorizontalAlignment.Right);

            columns.CreateProgress("percent_uses", "Использовано, %")
                .SetWidth(150);

            columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                .AddColumn("name", RowSummaryType.CountAggregate, "Всего наименований: {Count}")
                .AddColumn("amount", RowSummaryType.DoubleAggregate, "{Sum}")
                .AddColumn("cost", RowSummaryType.DoubleAggregate, "{Sum:c}");

            columns.CreateSortedColumns()
                .Add("name", ListSortDirection.Ascending);
        }

        public IEditorCode CreateEditor()
        {
            return new CalcItemGoodsEditor();
        }

        public override IEnumerable<CalcItemGoods> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<CalcItemGoods>(GetSelect(), new { owner_id = parameters.OwnerId });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "cg.owner_id = :owner_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "cg.id = :id");
        }
    }

    public class CalcItemGoodsEditor : EditorCodeBase<CalcItemGoods>, IEditorCode
    {
        private const int labelWidth = 120;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const string itemSelect = "with recursive r as (select id, status_id, parent_id, name from goods where parent_id is null and not code in ('Прд', 'Усл') and status_id in (500, 1002) union select g.id, g.status_id, g.parent_id, g.name from goods g join r on r.id = g.parent_id and g.status_id in (500, 1002)) select * from r order by name";

            IControl calculation_name = editor.CreateTextBox("calculation_name", "Калькуляция")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(300)
                .SetEnabled(false);
            IControl item_id = editor.CreateSelectBox("item_id", "Материал", (c) => { return c.Query<GroupDataItem>(itemSelect); })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(450);
            IControl amount = editor.CreateNumeric("amount", "Количество")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);
            IControl price = editor.CreateCurrency("price", "Цена")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(200);
            IControl cost = editor.CreateCurrency("cost", "Стоимость")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(200);

            editor.Container.Add(new IControl[] {
                calculation_name,
                item_id,
                amount,
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
            return "select cg.id, c.name as calculation_name, cg.item_id, cg.amount, cg.price, cg.cost from calc_item_goods cg join calculation c on (c.id = cg.owner_id) where cg.id = :id";
        }

        protected override string GetInsert()
        {
            return "insert into calc_item_goods (owner_id) values (:owner_id) returning id";
        }

        protected override string GetUpdate(CalcItemGoods item)
        {
            return "update calc_item_goods set item_id = :item_id, amount = :amount, price = :price, cost = :cost where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            if (field == "calculation_name")
                return false;

            return status_name == "compiled";
        }
    }
}