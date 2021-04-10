using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Core;
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code.Implementation.InvoiceSalesImp
{
    public class InvoiceSales : Document
    {
        public Guid? contractor_id { get; set; }
        public string contractor_name { get; set; }
		public Guid? contract_id { get; set; }
        public string contract_name { get; protected set; }
        public string organization_name { get; protected set; }
        public DateTime? invoice_date { get; set; }
        public string invoice_number { get; set; }
        public DateTime? order_date { get; set; }
        public string order_number { get; set; }
        public int tax { get; protected set; }
        public decimal cost { get; protected set; }
        public decimal tax_value { get; protected set; }
        public decimal cost_with_tax { get; protected set; }
        public bool tax_payer { get; protected set; }
    }

    public class InvoiceSalesDetail : DetailEntity
    {
        public Guid goods_id { get; set; }
        public string goods_name { get; protected set; }
        public decimal amount { get; set; }
        public decimal price { get; set; }
        public decimal cost { get; set; }
        public int tax { get; set; }
        public decimal tax_value { get; set; }
        public decimal cost_with_tax { get; set; }
    }

    public class InvoiceSalesBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelect = @"
            select 
                i_s.id, 
                i_s.status_id, 
                s.note as status_name, 
                ua.name as user_created, 
                c.name as contractor_name, 
                i_s.doc_date, 
                i_s.doc_number, 
                o.name as organization_name, 
                i_s.invoice_date, 
                i_s.invoice_number, 
                po.doc_number as order_number, 
                po.doc_date as order_date, 
				i_s.contract_id,
				contract.name as contract_name,
                case 
                    when contract.tax_payer then 20 
                    else 0 
                end as tax, 
                coalesce(sum(isd.cost), 0) as cost, 
                coalesce(sum(isd.tax_value), 0) as tax_value, 
                coalesce(sum(isd.cost_with_tax), 0) as cost_with_tax 
            from invoice_sales i_s 
                join status s on (s.id  = i_s.status_id) 
                join user_alias ua on (ua.id = i_s.user_created_id) 
                join organization o on (o.id = i_s.organization_id) 
                left join invoice_sales_detail isd on (isd.owner_id = i_s.id) 
                left join contractor c on (c.id = i_s.contractor_id) 
                left join production_order po on (po.id = i_s.owner_id) 
				left join contract on (contract.id = i_s.contract_id)
            where {0} 
            group by i_s.id, i_s.status_id, ua.name, c.name, i_s.doc_date, i_s.doc_number, s.note, o.name, contract.tax_payer, i_s.invoice_date, i_s.invoice_number, po.doc_number, po.doc_date, contract.name";

        void IBrowserCode.Initialize(IBrowser browser)
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

                columns.CreateDate("invoice_date", "Дата", "dd.MM.yyyy")
                    .SetWidth(150)
                    .SetHideable(false);

                columns.CreateInteger("invoice_number", "Номер")
                    .SetWidth(100);

                columns.CreateText("contractor_name", "Контрагент")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill)
                    .SetAllowGrouping(true);

				columns.CreateText("contract_name", "Договор")
                    .SetWidth(150)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateDate("order_date", "Дата")
                    .SetWidth(150)
                    .SetHideable(false);

                columns.CreateInteger("order_number", "Номер")
                    .SetWidth(100);

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

                columns.CreateSortedColumns()
                    .Add("doc_date")
                    .Add("doc_number");

                columns.CreateStackedColumns()
                    .Add("invoice_date")
                    .Add("invoice_number")
                    .Header("Счёт-фактура");

                columns.CreateStackedColumns()
                    .Add("order_date")
                    .Add("order_number")
                    .Header("Заказ");

				columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                    .AddColumn("cost", RowSummaryType.DoubleAggregate, "{Sum:c}")
					.AddColumn("tax_value", RowSummaryType.DoubleAggregate, "{Sum:c}")
					.AddColumn("cost_with_tax", RowSummaryType.DoubleAggregate, "{Sum:c}");

				columns.CreateGroupSummaryRow()
                    .AddColumn("cost", RowSummaryType.DoubleAggregate, "{Sum:c}")
					.AddColumn("tax_value", RowSummaryType.DoubleAggregate, "{Sum:c}")
					.AddColumn("cost_with_tax", RowSummaryType.DoubleAggregate, "{Sum:c}");
            });

			browser.MoveToEnd();
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<InvoiceSales>(string.Format(baseSelect, "(i_s.doc_date <= :to_date and (i_s.status_id not in (1011, 3000) or i_s.doc_date >= :from_date) and i_s.organization_id = :organization_id)"), new
            {
                from_date = parameters.DateFrom,
                to_date = parameters.DateTo,
                organization_id = parameters.OrganizationId
            }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<InvoiceSales>(string.Format(baseSelect, "i_s.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from invoice_sales where id = :id", new { id }, transaction);
        }

        IEditorCode IDataEditor.CreateEditor() => new InvoiceSalesEditor();
    }

    public class InvoiceSalesEditor : IEditorCode, IDataOperation, IControlEnabled, IControlVisible
    {
		private class ProductionOrderData
        {
            public Guid contractor_id { get; set; }
            public Guid contract_id { get; set; }
        }

        public void Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const string orgSelect = "select id, name from organization where status_id = 1002";
            const string contractorSelect = "select distinct c.id, c.status_id, c.name, c.parent_id from contractor c left join contract on (contract.owner_id = c.id) where (c.status_id = 1002 and contract.contractor_type = 'buyer'::contractor_type) or (c.status_id = 500) or (c.id = :contractor_id) order by c.name";
            const string ownerSelect = "select po.id, ek.name || ' №' || po.doc_number || ' от ' || to_char(po.doc_date, 'DD.MM.YYYY') || ' на сумму ' || sum(pod.cost_with_tax) || ' (' || c.name || ')' as name from production_order po join entity_kind ek on (ek.id = po.entity_kind_id) join production_order_detail pod on (pod.owner_id = po.id) join contractor c on (c.id = po.contractor_id) where (po.status_id = 3100 or (po.id = :owner_id)) and (po.contractor_id = :contractor_id or :contractor_id is null) group by po.id, ek.name, po.doc_number, po.doc_date, c.name";
            const string gridSelect = "select isd.id, isd.owner_id, g.name as goods_name, isd.amount, isd.price, isd.cost, isd.tax, isd.tax_value, isd.cost_with_tax from invoice_sales_detail isd left join goods g on (g.id = isd.goods_id) where isd.owner_id = :oid";
			const string contractSelect = "select id, status_id, name, parent_id from contract where owner_id = :contractor_id and contractor_type = 'buyer'::contractor_type";

            IContainer container = editor.CreateContainer(32);

            IControl doc_number = editor.CreateTextBox("doc_number", "Номер")
                .SetControlWidth(110)
                .SetLabelAutoSize(true)
                .SetWidth(170)
                .SetDock(DockStyle.Left);

            IControl doc_date = editor.CreateDateTimePicker("doc_date", "от")
                .SetLabelWidth(40)
                .SetControlWidth(170)
                .SetLabelTextAlignment(ContentAlignment.TopCenter)
                .SetWidth(210)
                .SetDock(DockStyle.Left);

            IControl organization_id = editor.CreateComboBox("organization_id", "Организация", (conn) => { return conn.Query<NameDataItem>(orgSelect); })
                .SetLabelWidth(100)
                .SetControlWidth(200)
                .SetLabelTextAlignment(ContentAlignment.TopRight);

            container.Add(new IControl[]
            {
                doc_number,
                doc_date,
                organization_id
            });

            IControl contractor_id = editor.CreateSelectBox<InvoiceSales>("contractor_id", "Контрагент", (e, c) =>
                {
                    return c.Query<GroupDataItem>(contractorSelect, new { e.contractor_id });
                })
				.ValueChangedAction((s, e) =>
                {
                    using (var conn = database.CreateConnection())
                    {
                        editor.Populates["contract_id"].Populate(conn, editor.Entity);
                        editor.Data["contract_id"] = database.ExecuteSqlCommand<Guid>("select id from contract where owner_id = :contractor_id and contract.contractor_type = 'buyer'::contractor_type and contract.is_default", new { contractor_id = editor.Data["contractor_id"] });
                        editor.Populates["owner_id"].Populate(conn, editor.Entity);
                    }
                })
                .SetLabelWidth(100)
                .SetControlWidth(580);

			IControl contract_id = editor.CreateSelectBox<InvoiceSales>("contract_id", "Договор", (e, c) =>
                {
                    return c.Query<GroupDataItem>(contractSelect, new { e.contractor_id });
                })
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = database.CreateConnection())
                    {
                        if (e.Value is Guid cid)
                        {
                            editor.Container["InvoicePanel"].Visible = conn.QuerySingle<bool>("select tax_payer from contract where id = :id", new { id = cid });
                        }
                        else
                        {
                            editor.Container["InvoicePanel"].Visible = false;
                        }
                    }
                })
                .SetLabelWidth(100)
                .SetControlWidth(580);

            IControl owner_id = editor.CreateSelectBox<InvoiceSales>("owner_id", "Заказ", (e, c) =>
                {
                    return c.Query<GroupDataItem>(ownerSelect, new { e.contractor_id, e.owner_id });
                })
				.ValueChangedAction((s, e) =>
                {
                    using (var conn = database.CreateConnection())
                    {
                        var cc = conn.QuerySingle<ProductionOrderData>("select contractor_id, contract_id from production_order where id = :id", new { id = e.Value });
                        editor.Data["contractor_id"] = cc.contractor_id;
                        editor.Data["contract_id"] = cc.contract_id;

                        if (editor.Entity is InvoiceSales i_s)
                        {
                            conn.Execute("call fill_invoice_details('production_order', :purchase_id, :invoice_id)", new { invoice_id = i_s.id, purchase_id = e.Value });
                            editor.Populates["datagrid"].Populate(conn, editor.Entity);
                        }
                    }
                })
                .SetLabelWidth(100)
                .SetControlWidth(450);

            IControl datagrid = editor.CreateDataGrid("datagrid", (c) => { return c.Query<InvoiceSalesDetail>(gridSelect, new { editor.Entity.oid }).AsList(); })
                .CreateColumns((columns) =>
                {
                    columns.CreateText("goods_name", "Номенклатура")
                        .SetHideable(false)
                        .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

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

                    columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                        .AddColumn("cost", RowSummaryType.DoubleAggregate, "{Sum:c}")
                        .AddColumn("tax_value", RowSummaryType.DoubleAggregate, "{Sum:c}")
                        .AddColumn("cost_with_tax", RowSummaryType.DoubleAggregate, "{Sum:c}");
                })
                .SetEditor("Номенклатура", new InvoiceSalesDetailEditor())
                .SetHeight(350);

            IContainer invoice_panel = editor.CreateContainer("InvoicePanel", 42);
            invoice_panel.AsControl()
                .SetPadding(top: 10);

            InvoiceSales invoice = editor.Entity as InvoiceSales;
            IControl invoice_number = editor.CreateTextBox("invoice_number", "Счёт фактура №")
                .SetLabelAutoSize(true)
                .SetWidth(225)
                .SetDock(DockStyle.Left);

            IControl invoice_date = editor.CreateDateTimePicker("invoice_date", "от", customFormat: "dd.MM.yyyy")
                .SetLabelWidth(40)
                .SetLabelTextAlignment(ContentAlignment.TopCenter)
                .SetDock(DockStyle.Left)
                .SetWidth(210);

			invoice_panel.Add(new IControl[]
            {
                invoice_number,
                invoice_date
            });

            editor.Container.Add(new IControl[] {
                container.AsControl(),
                contractor_id,
				contract_id,
                owner_id,
                datagrid,
                invoice_panel.AsControl()
            });

            editor.Reports.Add(
                ReportForm.Create("invoice-sales", true)
                    .Parameter("InvoiceId", editor.Entity.oid)
                );
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = @"
                select 
                    i.id, 
                    '№' || i.doc_number || ' от ' || to_char(i.doc_date, 'DD.MM.YYYY') as document_name, 
                    i.owner_id, 
                    i.contractor_id, 
                    i.contract_id, 
                    i.doc_date, 
                    i.doc_number, 
                    i.organization_id, 
                    i.invoice_date, 
                    i.invoice_number,
                    c.tax_payer
                from 
                    invoice_sales i
                    left join contract c on (c.id = i.contract_id)
                where i.id = :id";
            return connection.QuerySingleOrDefault<InvoiceSales>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into invoice_sales default values returning id";
            return connection.QuerySingle<Guid>(sql, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update invoice_sales set contractor_id = :contractor_id, contract_id = :contract_id, doc_date = :doc_date, doc_number = :doc_number, invoice_date = :invoice_date, invoice_number = :invoice_number, owner_id = :owner_id where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from invoice_sales where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }

        bool IControlVisible.Ability(object entity, string dataName, IInformation info)
        {
            InvoiceSales i_s = entity as InvoiceSales;
            if (dataName == "InvoicePanel")
                return i_s.tax_payer;

            return true;
        }
    }

    public class InvoiceSalesDetailEditor : IEditorCode, IDataOperation
    {
        public void Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const int labelWidth = 120;
            const string goodsSelect = "select id, status_id, name, parent_id from goods where status_id in (500, 1002) order by name";

            IControl goods_id = editor.CreateSelectBox("goods_id", "Номенклатура", (c) => { return c.Query<GroupDataItem>(goodsSelect); })
                .ValueChangedAction((s, e) =>
                {
                    decimal goods_price = database.ExecuteSqlCommand<decimal>("select price from goods where id = :goods_id", new { goods_id = e.Value });
                    editor.Data["price"] = goods_price;
                })
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

            IControl tax = editor.CreateChoice("tax", "НДС%", new Dictionary<int, string>() {
                [0] = "Без НДС",
                [10] = "10%",
                [20] = "20%" })
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
                amount,
                price,
                cost,
                tax,
                tax_value,
                cost_with_tax
            });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select id, owner_id, goods_id, amount, price, cost, tax, tax_value, cost_with_tax from invoice_sales_detail where id = :id";
            return connection.QuerySingleOrDefault<InvoiceSalesDetail>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into invoice_sales_detail (owner_id, goods_id, amount, price, cost, tax, tax_value, cost_with_tax) values (:owner_id, :goods_id, :amount, :price, :cost, :tax, :tax_value, :cost_with_tax) returning id";
            InvoiceSalesDetail detail = editor.Entity as InvoiceSalesDetail;
            return connection.QuerySingle<long>(sql,
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

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update invoice_sales_detail set goods_id = :goods_id, amount = :amount, price = :price, cost = :cost, tax = :tax, tax_value = :tax_value, cost_with_tax = :cost_with_tax where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from invoice_sales_detail where id = :id", new { id = id.oid }, transaction);
        }
    }
}
