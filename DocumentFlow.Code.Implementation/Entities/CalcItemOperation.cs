using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.CalcItemOperationImp
{
    public class CalcItemOperation : IDirectory
    {
#pragma warning disable IDE1006 // Стили именования
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string calculation_name { get; protected set; }
        public string code { get; set; }
        public string name { get; set; }
        public Guid item_id { get; set; }
        public decimal cost { get; set; }
        public decimal price { get; set; }

        /// <summary>
        /// Количество операций
        /// </summary>
        public decimal amount { get; set; }

        public decimal produced_time { get; set; }
#pragma warning restore IDE1006 // Стили именования

        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class CalcItemOperationBrowser : BrowserCodeBase<CalcItemOperation>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                cio.id, 
                cio.status_id, 
                s.note as status_name, 
                o.name, 
                cio.cost, 
                cio.price, 
                cio.amount, 
                round(3600.0 * cio.amount / o.production_rate, 1) as produced_time 
            from calc_item_operation cio 
                join status s on (s.id = cio.status_id)
                left join operation o on (o.id = cio.item_id) 
            where 
                {0}";

        public void Initialize(IBrowser browser)
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

                columns.CreateText("name", "Производственная операция")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateNumeric("amount", "Количество")
                    .SetWidth(150)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("produced_time", "Время, с")
                    .SetDecimalDigits(1)
                    .SetWidth(150)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("price", "Цена", NumberFormatMode.Currency)
                    .SetWidth(150)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("cost", "Стоимость", NumberFormatMode.Currency)
                    .SetWidth(150)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                    .AddColumn("name", RowSummaryType.CountAggregate, "Всего наименований: {Count}")
                    .AddColumn("amount", RowSummaryType.DoubleAggregate, "{Sum}")
                    .AddColumn("produced_time", RowSummaryType.DoubleAggregate, "{Sum}")
                    .AddColumn("cost", RowSummaryType.DoubleAggregate, "{Sum:c}");

                columns.CreateSortedColumns()
                    .Add("name", ListSortDirection.Ascending);
            });
        }

        public IEditorCode CreateEditor()
        {
            return new CalcItemOperationEditor();
        }

        public override IEnumerable<CalcItemOperation> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<CalcItemOperation>(GetSelect(), new { owner_id = parameters.OwnerId });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "cio.owner_id = :owner_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "cio.id = :id");
        }
    }

    public class UsedMaterial : IDetail
    {
        public long id { get; protected set; }
        public Guid calc_item_operation_id { get; set; }
        public Guid goods_id { get; set; }
        public string goods_name { get; protected set; }
        public decimal count_by_goods { get; set; }
        public decimal count_by_operation { get; set; }

        Guid IDetail.owner_id
        {
            get { return calc_item_operation_id; }
        }

        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class CalcItemOperationEditor : EditorCodeBase<CalcItemOperation>, IEditorCode
    {
        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const int labelWidth = 120;
            const string itemSelect = "with recursive r as (select id, status_id, parent_id, name, code from operation where parent_id is null and status_id in (500, 1002) union select o.id, o.status_id, o.parent_id, o.name, o.code from operation o join r on r.id = o.parent_id and o.status_id in (500, 1002)) select id, status_id, parent_id, name from r order by code";
            const string gridSelect = "select um.id, g.name as goods_name, um.count_by_goods, um.count_by_operation from used_material um join goods g on (g.id = um.goods_id) where um.calc_item_operation_id = :oid";

            IControl calculation_name = editor.CreateTextBox("calculation_name", "Калькуляция")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(300)
                .SetEnabled(false);
            IControl item_id = editor.CreateSelectBox("item_id", "Операция", (c) => { return c.Query<GroupDataItem>(itemSelect); })
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
            IControl grid = editor.CreateDataGrid("datagrid", (c) => { return c.Query<UsedMaterial>(gridSelect, new { editor.Entity.oid }).AsList(); })
                .CreateColumns((columns) =>
                {
                    columns.CreateText("goods_name", "Номенклатура")
                        .SetHideable(false)
                        .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);
                    columns.CreateNumeric("count_by_goods", "Количество на изд.")
                        .SetDecimalDigits(3)
                        .SetWidth(150)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignment.Right);
                    columns.CreateNumeric("count_by_operation", "Количество на опер.")
                        .SetDecimalDigits(3)
                        .SetWidth(150)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignment.Right);
                })
                .SetEditor("Номенклатура", new UsedMaterialEditor(), (e) =>
                {
                    UsedMaterial material = e as UsedMaterial;
                    if (material.count_by_goods <= 0)
                        throw new Exception("Количество материала на изделие должно быть больше 0.");

                    if (material.count_by_operation <= 0)
                        throw new Exception("Количество материала на операцию должно быть больше 0.");

                    if (material.goods_id == Guid.Empty)
                        throw new Exception("Не указан материал");
                })
                .SetHeight(300);

            editor.Container.Add(new IControl[] {
                calculation_name,
                item_id,
                amount,
                price,
                cost,
                grid
            });
        }

        public override TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            return connection.QuerySingle<TId>(GetInsert(), new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        protected override string GetSelect()
        {
            return "select cio.id, c.name as calculation_name, cio.item_id, cio.cost, cio.price, cio.amount from calc_item_operation cio left join calculation c on (c.id = cio.owner_id) where cio.id = :id";
        }

        protected override string GetInsert()
        {
            return "insert into calc_item_operation (owner_id) values (:owner_id) returning id";
        }

        protected override string GetUpdate(CalcItemOperation item)
        {
            return "update calc_item_operation set item_id = :item_id, amount = :amount, price = :price, cost = :cost where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            if (field == "calculation_name")
                return false;

            return status_name == "compiled";
        }
    }

    public class UsedMaterialEditor : EditorCodeBase<UsedMaterial>, IEditorCode
    {
        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const int labelWidth = 160;
            const string goodsSelect = "with recursive r as (select id, status_id, parent_id, name from goods where parent_id is null and not code in ('Прд', 'Усл') and status_id in (500, 1002) union select g.id, g.status_id, g.parent_id, g.name from goods g join r on r.id = g.parent_id and g.status_id in (500, 1002)) select * from r order by name";

            IControl goods_id = editor.CreateSelectBox("goods_id", "Номенклатура", (c) => { return c.Query<GroupDataItem>(goodsSelect); })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl count_by_goods = editor.CreateNumeric("count_by_goods", "Количество на изд.")
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl count_by_operation = editor.CreateNumeric("count_by_operation", "Количество на опер.")
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);

            editor.Container.Add(new IControl[] {
                goods_id,
                count_by_goods,
                count_by_operation
            });
        }

        public override TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            UsedMaterial material = editor.Entity as UsedMaterial;
            return connection.QuerySingle<TId>(GetInsert(),
                new
                {
                    owner_id = parameters.OwnerId,
                    material.goods_id,
                    material.count_by_goods,
                    material.count_by_operation
                },
                transaction: transaction);
        }

        protected override string GetSelect()
        {
            return "select id, goods_id, count_by_goods, count_by_operation from used_material where id = :id";
        }

        protected override string GetInsert()
        {
            return "insert into used_material (calc_item_operation_id, goods_id, count_by_goods, count_by_operation) values (:owner_id, :goods_id, :count_by_goods, :count_by_operation) returning id";
        }

        protected override string GetUpdate(UsedMaterial entity)
        {
            return "update used_material set goods_id = :goods_id, count_by_goods = :count_by_goods, count_by_operation = :count_by_operation where id = :id";
        }
    }
}