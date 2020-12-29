using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Core;
using DocumentFlow.Code.System;
using DocumentFlow.Data;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Implementation.PurchaseRequestImp
{
    public class PurchaseRequest : Document
    {
        public Guid? contractor_id { get; set; }
        public string contractor_name { get; protected set; }
        public Guid? contract_id { get; set; }
        public string contract_name { get; protected set; }
        public string organization_name { get; protected set; }
        public int tax { get; protected set; }
        public decimal cost { get; protected set; }
        public decimal tax_value { get; protected set; }
        public decimal cost_with_tax { get; protected set; }
    }

    public class PurchaseRequestDetail : IDetail
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

    public class PurchaseRequestBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelect = @"
            select 
                pr.id, 
                pr.status_id, 
                s.note as status_name, 
                pr.doc_number, 
                ua.name as user_created, 
                pr.contractor_id,
                c.name as contractor_name,
                pr.contract_id,
                contract.name as contract_name,
                pr.doc_date, 
                pr.doc_number, 
                o.name as organization_name, 
                case 
                    when c.tax_payer then 20 
                    else 0 
                end as tax, 
                coalesce(sum(cost), 0::money) as cost, 
                coalesce(sum(tax_value), 0::money) as tax_value, 
                coalesce(sum(cost_with_tax), 0::money) as cost_with_tax 
            from purchase_request pr 
                join status s on (s.id  = pr.status_id) 
                join user_alias ua on (ua.id = pr.user_created_id) 
                join organization o on (o.id = pr.organization_id) 
                left join contract on (contract.id = pr.contract_id)
                left join purchase_request_detail prd on (prd.owner_id = pr.id) 
                left join contractor c on (c.id = pr.contractor_id) 
            where {0}
            group by pr.id, pr.status_id, ua.name, c.name, contract.name, pr.doc_date, pr.doc_number, s.note, o.name, c.tax_payer";

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

                columns.CreateText("contractor_name", "Контрагент")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill)
                    .SetAllowGrouping(true);

                columns.CreateText("contract_name", "Договор")
                    .SetWidth(150)
                    .SetVisible(false)
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

                columns.CreateSortedColumns()
                    .Add("doc_date")
                    .Add("doc_number");
            });

            browser.MoveToEnd();
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<PurchaseRequest>(string.Format(baseSelect, "(pr.doc_date between :from_date and :to_date and pr.organization_id = :organization_id) or (pr.status_id not in (1011, 3000))"), new
            {
                from_date = parameters.DateFrom,
                to_date = parameters.DateTo,
                organization_id = parameters.OrganizationId
            }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<PurchaseRequest>(string.Format(baseSelect, "pr.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from purchase_request where id = :id", new { id }, transaction);
        }

        IEditorCode IDataEditor.CreateEditor()
        {
            return new PurchaseRequestEditor();
        }
    }

    public class PurchaseRequestEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const string orgSelect = "select id, name from organization where status_id = 1002";
            const string contractorSelect = "select c.id, c.status_id, c.name, c.parent_id from contractor c left join contract on (contract.owner_id = c.id) where (c.status_id = 1002 and contract.contractor_type = 'seller'::contractor_type) or (c.status_id = 500) or (c.id = :contractor_id) order by c.name";
            const string gridSelect = "select prd.id, prd.owner_id, g.name as goods_name, prd.amount, prd.price, prd.cost, prd.tax, prd.tax_value, prd.cost_with_tax from purchase_request_detail prd join goods g on g.id = prd.goods_id where prd.owner_id = :oid";
            const string contractSelect = "select id, status_id, name, parent_id from contract where owner_id = :contractor_id and contractor_type = 'seller'::contractor_type";

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

            IControl organization_id = editor.CreateComboBox("organization_id", "Организация", (conn) => { return conn.Query<ComboBoxDataItem>(orgSelect); })
                .SetLabelWidth(100)
                .SetControlWidth(200)
                .SetLabelTextAlignment(ContentAlignment.TopRight);

            container.Add(new IControl[]
            {
                doc_number,
                doc_date,
                organization_id
            });

            IControl contractor_id = editor.CreateSelectBox("contractor_id", "Контрагент", (e, c) =>
                {
                    PurchaseRequest pr = e.Entity as PurchaseRequest;
                    return c.Query<GroupDataItem>(contractorSelect, new { pr.contractor_id });
                })
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = database.CreateConnection())
                    {
                        editor.Populates["contract_id"].Populate(conn, editor.Entity);
                        editor.Data["contract_id"] = database.ExecuteSqlCommand<Guid>("select id from contract where owner_id = :contractor_id and contract.contractor_type = 'seller'::contractor_type and contract.is_default", new { contractor_id = editor.Data["contractor_id"] });
                    }
                })
                .SetLabelWidth(100)
                .SetControlWidth(580);

            IControl contract_id = editor.CreateSelectBox("contract_id", "Договор", (e, c) =>
                {
                    PurchaseRequest pr = e.Entity as PurchaseRequest;
                    return c.Query<GroupDataItem>(contractSelect, new { pr.contractor_id });
                })
                .SetLabelWidth(100)
                .SetControlWidth(580);

            IControl datagrid = editor.CreateDataGrid("datagrid", (c) => { return c.Query<PurchaseRequestDetail>(gridSelect, new { editor.Entity.oid }).AsList(); })
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
                .SetEditor("Номенклатура", new PurchaseRequestDetailEditor())
                .SetHeight(350);

            editor.Container.Add(new IControl[] {
                container.AsControl(),
                contractor_id,
                contract_id,
                datagrid
            });

            editor.Commands.Add(CommandMethod.UserDefined, "open-document")
                .SetIcon("organization")
                .SetTitle("Контрагент")
                .AppendTo(editor.ToolBar)
                .ExecuteAction(OpenContractorClick);

            editor.Commands.Add(CommandMethod.UserDefined, "open-document")
                .SetIcon("stack-books")
                .SetTitle("Договор")
                .AppendTo(editor.ToolBar)
                .ExecuteAction(OpenContractClick);
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = @"
                select 
                    id, 
                    '№' || doc_number || ' от ' || to_char(doc_date, 'DD.MM.YYYY') as document_name, 
                    contractor_id, 
                    contract_id, 
                    doc_date, 
                    doc_number, 
                    organization_id
                from purchase_request
                where id = :id";
            return connection.QuerySingleOrDefault<PurchaseRequest>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into purchase_request default values returning id";
            return connection.QuerySingle<Guid>(sql, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update purchase_request set contractor_id = :contractor_id, contract_id = :contract_id, doc_date = :doc_date, doc_number = :doc_number where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from purchase_request where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }

        private void OpenContractorClick(object sender, ExecuteEventArgs e)
        {
#pragma warning disable IDE0019 // Используйте сопоставление шаблонов
            PurchaseRequest pr = e.Editor.Entity as PurchaseRequest;
#pragma warning restore IDE0019 // Используйте сопоставление шаблонов
            if (pr != null)
            {
                if (pr.contractor_id.HasValue)
                {
                    e.Editor.Commands.OpenDocument(pr.contractor_id.Value);
                }
            }
        }

        private void OpenContractClick(object sender, ExecuteEventArgs e)
        {
#pragma warning disable IDE0019 // Используйте сопоставление шаблонов
            PurchaseRequest pr = e.Editor.Entity as PurchaseRequest;
#pragma warning restore IDE0019 // Используйте сопоставление шаблонов
            if (pr != null)
            {
                if (pr.contract_id.HasValue)
                {
                    e.Editor.Commands.OpenDocument(pr.contract_id.Value);
                }
            }
        }
    }

    public class PurchaseRequestDetailEditor : IEditorCode, IDataOperation
    {
        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
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
            string sql = "select id, owner_id, goods_id, amount, price, cost, tax, tax_value, cost_with_tax from purchase_request_detail where id = :id";
            return connection.QuerySingleOrDefault<PurchaseRequestDetail>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into purchase_request_detail (owner_id, goods_id, amount, price, cost, tax, tax_value, cost_with_tax) values (:owner_id, :goods_id, :amount, :price, :cost, :tax, :tax_value, :cost_with_tax) returning id";
            PurchaseRequestDetail detail = editor.Entity as PurchaseRequestDetail;
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
            string sql = "update purchase_request_detail set goods_id = :goods_id, amount = :amount, price = :price, cost = :cost, tax = :tax, tax_value = :tax_value, cost_with_tax = :cost_with_tax where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from purchase_request_detail where id = :id", new { id = id.oid }, transaction);
        }
    }
}
