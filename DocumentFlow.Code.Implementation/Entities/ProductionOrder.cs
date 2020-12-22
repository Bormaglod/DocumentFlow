using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;
using DocumentFlow.Core;

namespace DocumentFlow.Code.Implementation.ProductionOrderImp
{
    public class ProductionOrder : IDocument
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string document_name { get; protected set; }
        public string user_created { get; protected set; }
        public Guid? contractor_id { get; set; }
        public string contractor_name { get; protected set; }
        public DateTime doc_date { get; set; }
        public string doc_number { get; set; }
        public Guid organization_id { get; set; }
        public string organization_name { get; protected set; }
        public int tax { get; protected set; }
        public decimal cost { get; protected set; }
        public decimal tax_value { get; protected set; }
        public decimal cost_with_tax { get; protected set; }
        public int complete_status { get; protected set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class ProductionOrderBrowser : BrowserCodeBase<ProductionOrder>, IBrowserCode
    {
        private const string baseSelect = @"
            with cte as 
            (
                select 
                    op.owner_id, 
                    round(avg(op.completed * 100 / op.amount), 0) as complete_status 
                from production_operation op 
                group by op.owner_id
            ) 
            select 
                po.id, 
                po.status_id, 
                s.note as status_name, 
                ua.name as user_created, 
                c.name as contractor_name, 
                po.doc_date, 
                po.doc_number, 
                o.name as organization_name, 
                case 
                    when c.tax_payer then 20 
                    else 0 
                end as tax, 
                coalesce(sum(pod.cost), 0::money) as cost, 
                coalesce(sum(pod.tax_value), 0::money) as tax_value, 
                coalesce(sum(pod.cost_with_tax), 0::money) as cost_with_tax, 
                coalesce(cte.complete_status, 0) as complete_status 
            from production_order po 
                join status s on (s.id  = po.status_id) 
                join user_alias ua on (ua.id = po.user_created_id) 
                join organization o on (o.id = po.organization_id) 
                left join production_order_detail pod on (pod.owner_id = po.id) 
                left join contractor c on (c.id = po.contractor_id) 
                left join cte on (cte.owner_id = po.id) 
            where {0}
            group by po.id, po.status_id, ua.name, c.name, po.doc_date, po.doc_number, s.note, o.name, c.tax_payer, cte.complete_status";

        public void Initialize(IBrowser browser)
        {
            browser.AllowGrouping = true;
            browser.DataType = DataType.Document;
            browser.FromDate = DateRanges.FirstYearDay;
            browser.ToDate = DateRanges.LastYearDay;

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

                columns.CreateDate("doc_date", "Дата")
                    .SetWidth(150)
                    .SetHideable(false);

                columns.CreateText("doc_number", "Номер")
                    .SetWidth(100);

                columns.CreateText("organization_name", "Организация")
                    .SetWidth(150)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateText("contractor_name", "Контрагент")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill)
                    .SetAllowGrouping(true);

                columns.CreateNumeric("cost", "Сумма", NumberFormatMode.Currency)
                    .SetWidth(120)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateInteger("tax", "НДС%", NumberFormatMode.Percent)
                    .SetWidth(80)
                    .SetHorizontalAlignment(HorizontalAlignment.Center)
                    .SetAllowGrouping(true);

                columns.CreateNumeric("tax_value", "НДС", NumberFormatMode.Currency)
                    .SetWidth(120)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("cost_with_tax", "Всего с НДС", NumberFormatMode.Currency)
                    .SetWidth(120)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateProgress("complete_status", "Готовность")
                    .SetWidth(100);

                columns.CreateSortedColumns()
                    .Add("doc_date")
                    .Add("doc_number");
            });
        }

        public override IEnumerable<ProductionOrder> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<ProductionOrder>(GetSelect(), new
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
            return new ProductionOrderEditor();
        }
    }

    public class ProductionOrderDetail : IDetail
    {
        public long id { get; protected set; }
        public Guid owner_id { get; set; }
        public Guid goods_id { get; set; }
        public string goods_name { get; protected set; }
        public decimal amount { get; set; }
        public decimal price { get; set; }
        public decimal cost { get; set; }
        public int tax { get; set; }
        public decimal tax_value { get; set; }
        public decimal cost_with_tax { get; set; }
        public int complete_status { get; set; }
        public Guid calculation_id { get; set; }
        public string calculation_code { get; protected set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class ProductionOrderEditor : EditorCodeBase<ProductionOrder>, IEditorCode
    {
        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const string orgSelect = "select id, name from organization where status_id = 1002";
            const string contractorSelect = "select id, status_id, name, parent_id from contractor where (status_id = 1002 and buyer) or (status_id = 500) or (id = :contractor_id) order by name";
            const string gridSelect = "select pod.id, pod.owner_id, g.name as goods_name, pod.amount, pod.price, pod.cost, pod.tax, pod.tax_value, pod.cost_with_tax, pod.complete_status, c.code as calculation_code from production_order_detail pod join goods g on (g.id = pod.goods_id) join calculation c on (c.id = pod.calculation_id) where pod.owner_id = :oid";

            IContainer container = editor.CreateContainer(32);

            IControl doc_number = editor.CreateTextBox("doc_number", "Номер")
                .SetControlWidth(110)
                .SetLabelAutoSize(true)
                .SetWidth(165)
                .SetDock(DockStyle.Left);

            IControl doc_date = editor.CreateDateTimePicker("doc_date", "от")
                .SetLabelWidth(40)
                .SetControlWidth(170)
                .SetLabelTextAlignment(ContentAlignment.TopCenter)
                .SetWidth(210)
                .SetDock(DockStyle.Left);

            IControl organization_id = editor.CreateComboBox("organization_id", "Организация", (conn) => { return conn.Query<ComboBoxDataItem>(orgSelect); })
                .SetLabelWidth(150)
                .SetControlWidth(300)
                .SetLabelTextAlignment(ContentAlignment.TopRight);

            container.Add(new IControl[]
            {
                doc_number,
                doc_date,
                organization_id
            });

            IControl contractor_id = editor.CreateSelectBox("contractor_id", "Контрагент", (e, c) =>
                {
                    ProductionOrder pr = e.Entity as ProductionOrder;
                    return c.Query<GroupDataItem>(contractorSelect, new { pr.contractor_id });
                })
                .SetLabelWidth(120)
                .SetControlWidth(700);

            IControl datagrid = editor.CreateDataGrid("datagrid", (c) => { return c.Query<ProductionOrderDetail>(gridSelect, new { editor.Entity.oid }).AsList(); })
                .CreateColumns((columns) =>
                {
                    columns.CreateText("goods_name", "Номенклатура")
                        .SetHideable(false)
                        .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);
                    columns.CreateText("calculation_code", "Калькуляция")
                        .SetHideable(false)
                        .SetWidth(150);
                    columns.CreateNumeric("amount", "Количество")
                        .SetDecimalDigits(3)
                        .SetWidth(100)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignment.Right);
                    columns.CreateNumeric("price", "Цена", NumberFormatMode.Currency)
                        .SetWidth(100)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignment.Right);
                    columns.CreateNumeric("cost", "Сумма", NumberFormatMode.Currency)
                        .SetWidth(140)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignment.Right);
                    columns.CreateNumeric("tax", "%НДС", NumberFormatMode.Percent)
                        .SetWidth(80)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignment.Center);
                    columns.CreateNumeric("tax_value", "НДС", NumberFormatMode.Currency)
                        .SetWidth(140)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignment.Right);
                    columns.CreateNumeric("cost_with_tax", "Всего", NumberFormatMode.Currency)
                        .SetWidth(140)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignment.Right);
                    columns.CreateProgress("complete_status", "Выполнено")
                        .SetWidth(100)
                        .SetHideable(false);
                    columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                        .AddColumn("cost", RowSummaryType.DoubleAggregate, "{Sum:c}")
                        .AddColumn("tax_value", RowSummaryType.DoubleAggregate, "{Sum:c}")
                        .AddColumn("cost_with_tax", RowSummaryType.DoubleAggregate, "{Sum:c}");
                })
                .SetEditor("Номенклатура", new ProductionOrderDetailEditor())
                .SetHeight(350);

            editor.Container.Add(new IControl[] {
                container.AsControl(),
                contractor_id,
                datagrid
            });

			editor.Commands.Add(CommandMethod.UserDefined, "open-document")
                .SetIcon("organization")
                .SetTitle("Контрагент")
                .AppendTo(editor.ToolBar)
                .ExecuteAction(OpenContractorClick);

            dependentViewer.AddDependentViewers(new string[] { "view-prod-oper", "view-ready-goods" });
        }

        protected override string GetSelect()
        {
            return "select id, '№' || doc_number || ' от ' || to_char(doc_date, 'DD.MM.YYYY') as document_name, contractor_id, doc_date, doc_number, organization_id from production_order where id = :id";
        }

        protected override string GetUpdate(ProductionOrder entity)
        {
            return "update production_order set contractor_id = :contractor_id, doc_date = :doc_date, doc_number = :doc_number where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }

		private void OpenContractorClick(object sender, ExecuteEventArgs e)
        {
            ProductionOrder po = e.Editor.Entity as ProductionOrder;
            if (po != null && po.contractor_id.HasValue)
            {
                e.Editor.Commands.OpenDocument(po.contractor_id.Value);
            }
        }
    }

    public class ProductionOrderDetailEditor : EditorCodeBase<ProductionOrderDetail>, IEditorCode
    {
        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const int labelWidth = 120;
            const string goodsSelect = "with recursive gp as (select id, status_id, name, parent_id from goods where status_id in (500, 1002) and id = '4da429d1-cd8f-4757-bea8-49c99adc48d8' union all select gc.id, gc.status_id, gc.name, gc.parent_id from goods gc join gp on (gc.parent_id = gp.id) where gc.status_id in (500, 1002)) select * from gp order by name";
            const string goodsCalculation = "select id, status_id, name from calculation where owner_id = :goods_id and status_id in (1001, 1002)";

            IControl goods_id = editor.CreateSelectBox("goods_id", "Номенклатура", (c) => { return c.Query<GroupDataItem>(goodsSelect); })
                .ValueChangedAction((s, e) =>
                {
                    decimal goods_price = editor.ExecuteSqlCommand<decimal>("select price from goods where id = :goods_id", new { goods_id = e.Value });
                    editor.Data["price"] = goods_price;

                    using (IDbConnection conn = editor.CreateConnection())
                    {
                        editor.Populates["calculation_id"].Populate(conn, editor.Entity);
                    }
                })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl calculation_id = editor.CreateSelectBox("calculation_id", "Калькуляция", (c) => { return c.Query<GroupDataItem>(goodsCalculation, new { goods_id = editor.Data["goods_id"] }); })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl amount = editor.CreateNumeric("amount", "Количество")
                .ValueChangedAction((s, e) =>
                {
                    editor.Data["cost"] = Convert.ToDecimal(e.Value) * Convert.ToDecimal(editor.Data["price"]);
                })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl price = editor.CreateCurrency("price", "Цена")
                .ValueChangedAction((s, e) =>
                {
                    editor.Data["cost"] = Convert.ToDecimal(editor.Data["amount"]) * Convert.ToDecimal(e.Value);
                })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl cost = editor.CreateCurrency("cost", "Сумма")
                .ValueChangedAction((s, e) =>
                {
                    decimal tax_price = Convert.ToDecimal(e.Value) * Convert.ToDecimal(editor.Data["tax"]) / 100;
                    editor.Data["tax_value"] = tax_price;
                    editor.Data["cost_with_tax"] = Convert.ToDecimal(e.Value) + tax_price;
                })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl tax = editor.CreateChoice("tax", "НДС%", new Dictionary<int, string>() { { 0, "Без НДС" }, { 10, "10%" }, { 20, "20%" } })
                .ValueChangedAction((s, e) =>
                {
                    int tax_percent = Convert.ToInt32(e.Value);
                    editor.Data["tax_value"] = Convert.ToDecimal(editor.Data["cost"]) * tax_percent / 100;
                    editor["tax_value"].Enabled = tax_percent != 0;
                })
                .SetLabelWidth(labelWidth)
                .AsPopulate().AfterPopulationAction((s, e) =>
                {
                    int tax_percent = Convert.ToInt32(editor.Data["tax"]);
                    editor["tax_value"].Enabled = tax_percent != 0;
                })
                .SetFitToSize(true);
            IControl tax_value = editor.CreateCurrency("tax_value", "НДС")
                .ValueChangedAction((s, e) =>
                {
                    editor.Data["cost_with_tax"] = Convert.ToDecimal(editor.Data["cost"]) + Convert.ToDecimal(e.Value);
                })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl cost_with_tax = editor.CreateCurrency("cost_with_tax", "Всего с НДС")
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);

            editor.Container.Add(new IControl[] {
                goods_id,
                calculation_id,
                amount,
                price,
                cost,
                tax,
                tax_value,
                cost_with_tax
            });
        }

        public override TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            ProductionOrderDetail detail = editor.Entity as ProductionOrderDetail;
            return connection.QuerySingle<TId>(GetInsert(),
                new
                {
                    owner_id = parameters.OwnerId,
                    detail.goods_id,
                    detail.amount,
                    detail.price,
                    detail.cost,
                    detail.tax,
                    detail.tax_value,
                    detail.cost_with_tax,
                    detail.calculation_id
                },
                transaction: transaction);
        }

        protected override string GetSelect()
        {
            return "select id, owner_id, goods_id, amount, price, cost, tax, tax_value, cost_with_tax, calculation_id from production_order_detail where id = :id";
        }

        protected override string GetInsert()
        {
            return "insert into production_order_detail (owner_id, goods_id, amount, price, cost, tax, tax_value, cost_with_tax, calculation_id) values (:owner_id, :goods_id, :amount, :price, :cost, :tax, :tax_value, :cost_with_tax, :calculation_id) returning id";
        }

        protected override string GetUpdate(ProductionOrderDetail entity)
        {
            return "update production_order_detail set goods_id = :goods_id, amount = :amount, price = :price, cost = :cost, tax = :tax, tax_value = :tax_value, cost_with_tax = :cost_with_tax, calculation_id = :calculation_id where id = :id";
        }
    }
}
