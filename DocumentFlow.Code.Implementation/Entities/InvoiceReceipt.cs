using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Dapper;
using DocumentFlow.Core;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.InvoiceReceiptImp
{
    public class InvoiceReceipt : IDocument
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string document_name { get; protected set; }
        public string user_created { get; protected set; }
        public Guid? owner_id { get; set; }
        public Guid? contractor_id { get; set; }
        public string contractor_name { get; set; }
        public DateTime doc_date { get; set; }
        public long doc_number { get; set; }
        public Guid organization_id { get; set; }
        public string organization_name { get; protected set; }
        public DateTime? invoice_date { get; set; }
        public string invoice_number { get; set; }
        public DateTime? purchase_date { get; set; }
        public string purchase_number { get; set; }
        public int tax { get; protected set; }
        public decimal cost { get; protected set; }
        public decimal tax_value { get; protected set; }
        public decimal cost_with_tax { get; protected set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class InvoiceReceiptBrowser : BrowserCodeBase<InvoiceReceipt>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                ir.id, 
                ir.status_id, 
                s.note as status_name, 
                ua.name as user_created, 
                c.name as contractor_name, 
                ir.doc_date, 
                ir.doc_number, 
                o.name as organization_name, 
                ir.invoice_date, 
                ir.invoice_number, 
                pr.view_number as purchase_number, 
                pr.doc_date as purchase_date, 
                case 
                    when c.tax_payer then 20 
                    else 0 
                end as tax,     
                coalesce(sum(cost), 0::money) as cost, 
                coalesce(sum(tax_value), 0::money) as tax_value, 
                coalesce(sum(cost_with_tax), 0::money) as cost_with_tax 
            from invoice_receipt ir 
                join status s on (s.id = ir.status_id) 
                join user_alias ua on (ua.id = ir.user_created_id) 
                join organization o on (o.id = ir.organization_id) 
                left join invoice_receipt_detail ird on (ird.owner_id = ir.id) 
                left join contractor c on (c.id = ir.contractor_id) 
                left join purchase_request pr on (pr.id = ir.owner_id) 
            where {0} 
            group by ir.id, ir.status_id, ua.name, c.name, ir.doc_date, ir.doc_number, s.note, o.name, c.tax_payer, ir.invoice_date, ir.invoice_number, pr.view_number, pr.doc_date";

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

                columns.CreateInteger("doc_number", "Номер")
                    .SetWidth(100);

                columns.CreateText("organization_name", "Организация")
                    .SetWidth(150)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateDate("invoice_date", "Дата", "dd.MM.yyyy")
                    .SetWidth(150)
                    .SetHideable(false);

                columns.CreateInteger("invoice_number", "Номер")
                    .SetWidth(100);

                columns.CreateText("contractor_name", "Контрагент")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill)
                    .SetAllowGrouping(true);

                columns.CreateDate("purchase_date", "Дата")
                    .SetWidth(150)
                    .SetHideable(false);

                columns.CreateInteger("purchase_number", "Номер")
                    .SetWidth(100);

                columns.CreateNumeric("cost", "Сумма", NumberFormatMode.Currency)
                    .SetWidth(120)
                    .SetHorizontalAlignment(HorizontalAlignmentText.Right);

                columns.CreateInteger("tax", "НДС%", NumberFormatMode.Percent)
                    .SetWidth(80)
                    .SetHorizontalAlignment(HorizontalAlignmentText.Center)
                    .SetAllowGrouping(true);

                columns.CreateNumeric("tax_value", "НДС", NumberFormatMode.Currency)
                    .SetWidth(120)
                    .SetHorizontalAlignment(HorizontalAlignmentText.Right);

                columns.CreateNumeric("cost_with_tax", "Всего с НДС", NumberFormatMode.Currency)
                    .SetWidth(120)
                    .SetHorizontalAlignment(HorizontalAlignmentText.Right);

                columns.CreateSortedColumns()
                    .Add("doc_date")
                    .Add("doc_number");

                columns.CreateStackedColumns()
                    .Add("invoice_date")
                    .Add("invoice_number")
                    .Header("Счёт-фактура");

                columns.CreateStackedColumns()
                    .Add("purchase_date")
                    .Add("purchase_number")
                    .Header("Заявка");
            });
        }

        public override IEnumerable<InvoiceReceipt> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<InvoiceReceipt>(GetSelect(), new
            {
                from_date = parameters.DateFrom,
                to_date = parameters.DateTo,
                organization_id = parameters.OrganizationId
            });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "ir.doc_date between :from_date and :to_date and ir.organization_id = :organization_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "ir.id = :id");
        }

        public IEditorCode CreateEditor()
        {
            return new InvoiceReceiptEditor();
        }
    }

    public class InvoiceReceiptDetail : IDetail
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
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class InvoiceReceiptEditor : EditorCodeBase<InvoiceReceipt>, IEditorCode
    {
        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const string orgSelect = "select id, name from organization where status_id = 1002";
            const string contractorSelect = "select id, status_id, name, parent_id from contractor where (status_id = 1002 and supplier) or (status_id = 500) or (id = :contractor_id) order by name";
            const string ownerSelect = "select pr.id, ek.name || ' №' || view_number || ' от ' || to_char(doc_date, 'DD.MM.YYYY') || ' на сумму ' || sum(prd.cost_with_tax) || ' (' || c.name || ')' as name from purchase_request pr join entity_kind ek on (ek.id = pr.entity_kind_id) join purchase_request_detail prd on (prd.owner_id = pr.id) join contractor c on (c.id = pr.contractor_id) where (pr.status_id in (1001, 3001, 3002, 3003) or (pr.id = :owner_id)) and (pr.contractor_id = :contractor_id or :contractor_id is null) group by pr.id, ek.name, view_number, doc_date, c.name";
            const string gridSelect = "select ird.id, ird.owner_id, g.name as goods_name, ird.amount, ird.price, ird.cost, ird.tax, ird.tax_value, ird.cost_with_tax from invoice_receipt_detail ird join goods g on (g.id = ird.goods_id) where ird.owner_id = :oid";

            IContainer container = editor.CreateContainer(32);

            IControl doc_number = editor.CreateInteger("doc_number", "Номер")
                .SetControlWidth(110)
                .SetLabelAutoSize(true)
                .SetWidth(165)
                .SetDock(DockStyleControl.Left);

            IControl doc_date = editor.CreateDateTimePicker("doc_date", "от")
                .SetLabelWidth(40)
                .SetControlWidth(170)
                .SetLabelTextAlignment(ContentAlignment.TopCenter)
                .SetWidth(210)
                .SetDock(DockStyleControl.Left);

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
                    InvoiceReceipt ir = e.Entity as InvoiceReceipt;
                    return c.Query<GroupDataItem>(contractorSelect, new { ir.contractor_id });
                })
                .SetLabelWidth(90)
                .SetControlWidth(700);

            IControl owner_id = editor.CreateSelectBox("owner_id", "Заказ", (e, c) =>
                {
                    InvoiceReceipt ir = e.Entity as InvoiceReceipt;
                    return c.Query<GroupDataItem>(ownerSelect, new { ir.contractor_id, ir.owner_id });
                })
                .SetLabelWidth(90)
                .SetControlWidth(450);

            IControl datagrid = editor.CreateDataGrid("datagrid", (c) => { return c.Query<InvoiceReceiptDetail>(gridSelect, new { editor.Entity.oid }).AsList(); })
                .CreateColumns((columns) =>
                {
                    columns.CreateText("goods_name", "Номенклатура")
                        .SetHideable(false)
                        .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);
                    columns.CreateNumeric("amount", "Количество")
                        .SetDecimalDigits(3)
                        .SetWidth(100)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignmentText.Right);
                    columns.CreateNumeric("price", "Цена", NumberFormatMode.Currency)
                        .SetWidth(100)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignmentText.Right);
                    columns.CreateNumeric("cost", "Сумма", NumberFormatMode.Currency)
                        .SetWidth(140)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignmentText.Right);
                    columns.CreateNumeric("tax", "%НДС", NumberFormatMode.Percent)
                        .SetWidth(80)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignmentText.Center);
                    columns.CreateNumeric("tax_value", "НДС", NumberFormatMode.Currency)
                        .SetWidth(140)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignmentText.Right);
                    columns.CreateNumeric("cost_with_tax", "Всего", NumberFormatMode.Currency)
                        .SetWidth(140)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignmentText.Right);
                    columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                        .AddColumn("cost", RowSummaryType.DoubleAggregate, "{Sum:c}")
                        .AddColumn("tax_value", RowSummaryType.DoubleAggregate, "{Sum:c}")
                        .AddColumn("cost_with_tax", RowSummaryType.DoubleAggregate, "{Sum:c}");
                })
                .SetEditor("Номенклатура", new InvoiceReceiptDetailEditor())
                .SetHeight(350);

            IContainer invoice_panel = editor.CreateContainer(42);
            invoice_panel.AsControl()
                .SetPadding(top: 10);

            IControl invoice_number = editor.CreateTextBox("invoice_number", "Счёт фактура №")
                .SetLabelAutoSize(true)
                .SetWidth(225)
                .SetDock(DockStyleControl.Left);

            IControl invoice_date = editor.CreateDateTimePicker("invoice_date", "от", customFormat: "dd.MM.yyyy")
                .SetLabelWidth(40)
                .SetLabelTextAlignment(ContentAlignment.TopCenter)
                .SetDock(DockStyleControl.Left)
                .SetWidth(210);

            editor.Container.Add(new IControl[] {
                container.AsControl(),
                contractor_id,
                owner_id,
                datagrid,
                invoice_panel.AsControl()
            });
        }

        protected override string GetSelect()
        {
            return "select id, '№' || view_number || ' от ' || to_char(doc_date, 'DD.MM.YYYY') as document_name, owner_id, contractor_id, doc_date, doc_number, organization_id, invoice_date, invoice_number from invoice_receipt where id = :id";
        }

        protected override string GetUpdate(InvoiceReceipt entity)
        {
            return "update invoice_receipt set contractor_id = :contractor_id, doc_date = :doc_date, doc_number = :doc_number, invoice_date = :invoice_date, invoice_number = :invoice_number, owner_id = :owner_id where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }
    }

    public class InvoiceReceiptDetailEditor : EditorCodeBase<InvoiceReceiptDetail>, IEditorCode
    {
        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const int labelWidth = 120;
            const string goodsSelect = "select id, status_id, name, parent_id from goods where status_id in (500, 1002) order by name";

            IControl goods_id = editor.CreateSelectBox("goods_id", "Номенклатура", (c) => { return c.Query<GroupDataItem>(goodsSelect); })
                .ValueChangedAction((s, e) =>
                {
                    decimal goods_price = editor.ExecuteSqlCommand<decimal>("select price from goods where id = :goods_id", new { goods_id = e.Value });
                    editor["price"].Value = goods_price;
                })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl amount = editor.CreateNumeric("amount", "Количество")
                .ValueChangedAction((s, e) =>
                {
                    editor["cost"].Value = Convert.ToDecimal(e.Value) * Convert.ToDecimal(editor["price"].Value);
                })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl price = editor.CreateCurrency("price", "Цена")
                .ValueChangedAction((s, e) =>
                {
                    editor["cost"].Value = Convert.ToDecimal(editor["amount"].Value) * Convert.ToDecimal(e.Value);
                })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl cost = editor.CreateCurrency("cost", "Сумма")
                .ValueChangedAction((s, e) =>
                {
                    decimal tax_price = Convert.ToDecimal(e.Value) * Convert.ToDecimal(editor["tax"].Value) / 100;
                    editor["tax_value"].Value = tax_price;
                    editor["cost_with_tax"].Value = Convert.ToDecimal(e.Value) + tax_price;
                })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl tax = editor.CreateChoice("tax", "НДС%", new Dictionary<int, string>() { { 0, "Без НДС" }, { 10, "10%" }, { 20, "20%" } })
                .ValueChangedAction((s, e) =>
                {
                    int tax_percent = Convert.ToInt32(e.Value);
                    editor["tax_value"].Value = Convert.ToDecimal(editor["cost"].Value) * tax_percent / 100;
                    editor["tax_value"].Enabled = tax_percent != 0;
                })
                .SetLabelWidth(labelWidth)
                .AsPopulateControl().AfterPopulationAction((s, e) =>
                {
                    int tax_percent = Convert.ToInt32(editor["tax"].Value);
                    editor["tax_value"].Enabled = tax_percent != 0;
                })
                .SetFitToSize(true);
            IControl tax_value = editor.CreateCurrency("tax_value", "НДС")
                .ValueChangedAction((s, e) =>
                {
                    editor["cost_with_tax"].Value = Convert.ToDecimal(editor["cost"].Value) + Convert.ToDecimal(e.Value);
                })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);
            IControl cost_with_tax = editor.CreateCurrency("cost_with_tax", "Всего с НДС")
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);

            editor.Container.Add(new IControl[] {
                goods_id,
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
            InvoiceReceiptDetail detail = editor.Entity as InvoiceReceiptDetail;
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
                    detail.cost_with_tax
                },
                transaction: transaction);
        }

        protected override string GetSelect()
        {
            return "select id, owner_id, goods_id, amount, price, cost, tax, tax_value, cost_with_tax from invoice_receipt_detail where id = :id";
        }

        protected override string GetInsert()
        {
            return "insert into invoice_receipt_detail (owner_id, goods_id, amount, price, cost, tax, tax_value, cost_with_tax) values (:owner_id, :goods_id, :amount, :price, :cost, :tax, :tax_value, :cost_with_tax) returning id";
        }

        protected override string GetUpdate(InvoiceReceiptDetail entity)
        {
            return "update invoice_receipt_detail set goods_id = :goods_id, amount = :amount, price = :price, cost = :cost, tax = :tax, tax_value = :tax_value, cost_with_tax = :cost_with_tax where id = :id";
        }
    }
}
