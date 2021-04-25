using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code.Implementation.CalcItemGoodsImp
{
    public class CalcItemGoods : Directory
    {
        public string calculation_name { get; protected set; }

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

		/// <summary>
        /// Давальческий материал
        /// </summary>
		public bool is_tolling { get; set; }

        /// <summary>
        /// Вес материала использованного в изделии
        /// </summary>
        public decimal weight { get; set; }
    }

    public class CalcItemGoodsBrowser : IBrowserCode, IBrowserOperation, IDataEditor
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
				cg.is_tolling,
                round(g.weight * cg.amount, 3) as weight,
                (case when coalesce(cg.amount, 0) = 0 then 0 else coalesce(cg.uses, 0) * 100 / coalesce(cg.amount, 0) end)::integer as percent_uses 
            from calc_item_goods cg 
                join status s on (s.id = cg.status_id)
                left join goods g on (g.id = cg.item_id) 
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

                columns.CreateNumeric("weight", "Вес, г")
                    .SetDecimalDigits(3)
                    .SetWidth(100)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateBoolean("is_tolling", "Давальческий");

                columns.CreateProgress("percent_uses", "Использовано, %")
                    .SetWidth(150);

                columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                    .AddColumn("name", RowSummaryType.CountAggregate, "Всего наименований: {Count}")
                    .AddColumn("amount", RowSummaryType.DoubleAggregate, "{Sum}")
                    .AddColumn("cost", RowSummaryType.DoubleAggregate, "{Sum:c}")
                    .AddColumn("weight", RowSummaryType.DoubleAggregate, "{Sum}");

                columns.CreateSortedColumns()
                    .Add("name", ListSortDirection.Ascending);
            });
        }

        IEditorCode IDataEditor.CreateEditor() => new CalcItemGoodsEditor();

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<CalcItemGoods>(string.Format(baseSelect, "cg.owner_id = :owner_id"), new { owner_id = parameters.OwnerId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<CalcItemGoods>(string.Format(baseSelect, "cg.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from calc_item_goods where id = :id", new { id }, transaction);
        }
    }

    public class CalcItemGoodsEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 120;

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
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

			IControl is_tolling = editor.CreateCheckBox("is_tolling", "Давальческий материал")
				.SetLabelWidth(labelWidth);

            editor.Container.Add(new IControl[] {
                calculation_name,
                item_id,
                amount,
                price,
                cost,
				is_tolling
            });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select cg.id, c.name as calculation_name, cg.item_id, cg.amount, cg.price, cg.cost, cg.is_tolling from calc_item_goods cg join calculation c on (c.id = cg.owner_id) where cg.id = :id";
            return connection.QuerySingleOrDefault<CalcItemGoods>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into calc_item_goods (owner_id) values (:owner_id) returning id";
            return connection.QuerySingle<Guid>(sql, new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update calc_item_goods set item_id = :item_id, amount = :amount, price = :price, cost = :cost, is_tolling = :is_tolling where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from calc_item_goods where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            if (dataName == "calculation_name")
                return false;

            return info.StatusCode == "compiled";
        }
    }
}