using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Core;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.PerformOperationImp
{
    public class PerformOperation : IDocument
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string document_name { get; protected set; }
        public string user_created { get; protected set; }
        public DateTime doc_date { get; set; }
        public Guid? order_id { get; set; }
        public string order_name { get; protected set; }
        public Guid? goods_id { get; set; }
        public string goods_name { get; protected set; }
        public Guid? operation_id { get; set; }
        public string operation_name { get; protected set; }
        public Guid? employee_id { get; set; }
        public string employee_name { get; protected set; }
        public Guid? using_goods_id { get; set; }
        public string using_goods_name { get; protected set; }
        public Guid? replacing_goods_id { get; set; }
        public string replacing_goods_name { get; protected set; }
        public int amount { get; set; }
        public decimal salary { get; set; }
    }

    public class PerformOperationBrowser : BrowserCodeBase<PerformOperation>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                po.id, 
                po.status_id, 
                s.note as status_name, 
                ua.name as user_created, 
                po.doc_date, 
                '№' || p_ord.doc_number || ' от ' || to_char(p_ord.doc_date, 'DD.MM.YYYY') as order_name, 
                g.name as goods_name, 
                o.name as operation_name, 
                p.name as employee_name, 
                ug.name as using_goods_name, 
                case 
                    when ur.name is null then ug.name 
                    else ur.name 
                end as replacing_goods_name, 
                po.amount, 
                po.salary 
            from perform_operation po 
                join status s on (s.id = po.status_id) 
                join user_alias ua on (ua.id = po.user_created_id) 
                left join production_order p_ord on (p_ord.id = po.order_id) 
                left join goods g on (g.id = po.goods_id) 
                left join operation o on (o.id = po.operation_id) 
                left join employee e on (e.id = po.employee_id) 
                left join person p on (p.id = e.person_id) 
                left join goods ug on (ug.id = po.using_goods_id) 
                left join goods ur on (ur.id = po.replacing_goods_id) 
            where {0}";

        public void Initialize(IBrowser browser)
        {
            browser.AllowGrouping = true;
            browser.AllowSorting = true;
            browser.DataType = DataType.Document;
            browser.FromDate = DateRanges.CurrentDay;
            browser.ToDate = DateRanges.CurrentDay;

            browser.CreateStatusColumnRenderer();

            browser.CreateColumns((columns) =>
            {
                columns.CreateText("id", "Id")
                    .SetWidth(180)
                    .SetVisible(false);

                columns.CreateInteger("status_id", "Код состояния")
                    .SetWidth(80)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateText("status_name", "Состояние")
                    .SetWidth(110)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateText("user_created", "Автор")
                    .SetWidth(110)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateDate("doc_date", "Дата/время")
                    .SetWidth(150)
                    .SetHideable(false);

                columns.CreateText("order_name", "Заказ")
                    .SetWidth(200)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateText("goods_name", "Изделие")
                    .SetWidth(200)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateText("operation_name", "Операция")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill)
                    .SetAllowGrouping(true);

                columns.CreateText("employee_name", "Исполнитель")
                    .SetWidth(200)
                    .SetAllowGrouping(true);

                columns.CreateText("replacing_goods_name", "Использованный материал")
                    .SetWidth(270)
                    .SetAllowGrouping(true);

                columns.CreateInteger("amount", "Количество")
                    .SetWidth(140)
                    .SetHorizontalAlignment(HorizontalAlignmentText.Right);

                columns.CreateNumeric("salary", "Зарплата", NumberFormatMode.Currency, decimalDigits: 2)
                    .SetWidth(120)
                    .SetHorizontalAlignment(HorizontalAlignmentText.Right);

                columns.CreateGroupSummaryRow()
                    .AddColumn("salary", RowSummaryType.DoubleAggregate, "{Sum:c}");

                columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                    .AddColumn("salary", RowSummaryType.DoubleAggregate, "{Sum:c}");

                columns.CreateSortedColumns()
                    .Add("doc_date", SortDirection.Descending);
            });

            browser.CreateGroups()
                .Add("order_name")
                .Add("goods_name");
        }

        public override IEnumerable<PerformOperation> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<PerformOperation>(GetSelect(), new
            {
                from_date = parameters.DateFrom,
                to_date = parameters.DateTo,
                organization_id = parameters.OrganizationId
            });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "po.doc_date between :from_date and :to_date and po.organization_id = :organization_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "po.id = :id");
        }

        public IEditorCode CreateEditor()
        {
            return new PerformOperationEditor();
        }
    }

    public class PerformOperationEditor : EditorCodeBase<PerformOperation>, IEditorCode
    {
        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const int labelWidth = 210;
            const string orderSelect = "select po.id, po.status_id, '№' || po.view_number || ' от ' || to_char(po.doc_date, 'DD.MM.YYYY') || ' на сумму ' || sum(pod.cost_with_tax) || ' (' || c.name || ')' as name from production_order po join production_order_detail pod on (pod.owner_id = po.id) join contractor c on (c.id = po.contractor_id) where po.status_id = status_code('in production') or po.id = :order_id group by po.id, c.name order by po.doc_date, po.doc_number";
            const string goodsSelect = "select g.id, g.status_id, g.name, g.parent_id from goods g left join production_order_detail pod on (pod.goods_id = g.id) where (pod.owner_id = :order_id and pod.complete_status < 100) or g.status_id = 500 or g.id = :goods_id";
            const string operationSelect = "select o.id, o.status_id, o.name, o.parent_id from operation o left join calc_item_operation cio on (cio.item_id = o.id) left join calculation c on (cio.owner_id = c.id) left join production_order_detail pod on (c.id = pod.calculation_id) where (pod.owner_id = :order_id and c.owner_id = :goods_id and (o.status_id = 1002 or o.id = :operation_id)) or o.status_id = 500 order by o.status_id, o.name";
            const string usingGoodsSelect = "select gu.id, gu.status_id, gu.name, gu.parent_id from goods gp join production_order_detail pod on (pod.goods_id = gp.id) join calc_item_operation cio on (cio.owner_id = pod.calculation_id) join used_material um on (um.calc_item_operation_id = cio.id) join goods gu on (gu.id = um.goods_id) where (pod.owner_id = :order_id and cio.item_id = :operation_id and gp.id = :goods_id) or (gp.id = :using_goods_id) union select g.id, g.status_id, g.name, g.parent_id from goods g where g.status_id = 500 order by name";
            const string replacingGoodsSelect = "select g.id, g.status_id, g.name, g.parent_id from goods g where g.status_id in (500, 1002) order by g.name";
            const string empSelect = "select e.id, e.status_id, p.name from perform_operation po join organization org on (org.id = po.organization_id) join employee e on (e.owner_id = org.id) join person p on (p.id = e.person_id) where po.id = :id and (e.status_id = 1002 or e.id = :employee_id)";

            IControl order_id = editor.CreateSelectBox("order_id", "Заказ", (e, c) =>
                {
                    PerformOperation po = e.Entity as PerformOperation;
                    return c.Query<GroupDataItem>(orderSelect, new { po.order_id });
                })
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = editor.CreateConnection())
                    {
                        editor["goods_id"].AsPopulateControl().Populate(conn, editor.Entity);
                    }
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);

            IControl goods_id = editor.CreateSelectBox("goods_id", "Изделие", (e, c) =>
                {
                    PerformOperation po = e.Entity as PerformOperation;
                    return c.Query<GroupDataItem>(goodsSelect, new { po.order_id, po.goods_id });
                })
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = editor.CreateConnection())
                    {
                        editor["operation_id"].AsPopulateControl().Populate(conn, editor.Entity);
                    }
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);

            IControl operation_id = editor.CreateSelectBox("operation_id", "Операция", (e, c) =>
                {
                    PerformOperation po = e.Entity as PerformOperation;
                    return c.Query<GroupDataItem>(operationSelect, new { po.order_id, po.goods_id, po.operation_id });
                })
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = editor.CreateConnection())
                    {
                        editor["using_goods_id"].AsPopulateControl().Populate(conn, editor.Entity);
                    }
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);

            IControl amount = editor.CreateInteger("amount", "Количество")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);

            IControl doc_date = editor.CreateDateTimePicker("doc_date", "Дата/время")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(170);

            IControl using_goods_id = editor.CreateSelectBox("using_goods_id", "Материал (по спецификации)", (e, c) =>
                {
                    PerformOperation po = e.Entity as PerformOperation;
                    return c.Query<GroupDataItem>(usingGoodsSelect, new { po.order_id, po.goods_id, po.operation_id, po.using_goods_id });
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);

            IControl replacing_goods_id = editor.CreateSelectBox("replacing_goods_id", "Использованный материал", (e, c) => { return c.Query<GroupDataItem>(replacingGoodsSelect); })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);

            IControl employee_id = editor.CreateSelectBox("employee_id", "Исполнитель", (e, c) => 
                {
                    PerformOperation po = e.Entity as PerformOperation;
                    return c.Query<GroupDataItem>(empSelect, new { po.id, po.employee_id }); 
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);

            IControl salary = editor.CreateCurrency("salary", "Зарплата")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);

            editor.Container.Add(new IControl[] {
                order_id,
                goods_id,
                operation_id,
                amount,
                doc_date,
                using_goods_id,
                replacing_goods_id,
                employee_id,
                salary
            });
        }

        protected override string GetSelect()
        {
            return "select id, doc_date, order_id, goods_id, operation_id, amount, using_goods_id, replacing_goods_id, employee_id, salary from perform_operation where id = :id";
        }

        protected override string GetUpdate(PerformOperation performOperation)
        {
            return "update perform_operation set doc_date = :doc_date, order_id = :order_id, goods_id = :goods_id, operation_id = :operation_id, amount = :amount, using_goods_id = :using_goods_id, replacing_goods_id = :replacing_goods_id, employee_id = :employee_id, salary = :salary where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return new string[] { "compiled", "is changing", "corrected" }.Contains(status_name);
        }
    }
}
