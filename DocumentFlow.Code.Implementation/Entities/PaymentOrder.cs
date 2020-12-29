using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Core;
using DocumentFlow.Code.System;
using DocumentFlow.Data;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Implementation.PaymentOrderImp
{
    public class PaymentOrder : Document
    {
        public Guid? contractor_id { get; set; }
        public string contractor_name { get; set; }
        public DateTime? date_debited { get; set; }
        public decimal amount_debited { get; set; }
        public decimal? expense { get; set; }
        public decimal? income { get; set; }
        public string organization_name { get; protected set; }
        public Guid? purchase_id { get; set; }
        public Guid? invoice_receipt_id { get; set; }
    }

    public class PaymentOrderBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelect = @"
            select 
                id, 
                status_id, 
                status_name, 
                user_created, 
                doc_date, 
                doc_number, 
                contractor_name,
                date_debited, 
                expense, 
                income, 
                organization_name
            from payment_order_view 
            where {0}";

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

                columns.CreateDate("doc_date", "Дата/время", "dd.MM.yyyy")
                    .SetWidth(150)
                    .SetHideable(false);

                columns.CreateText("doc_number", "Номер")
                    .SetWidth(100);

                columns.CreateText("organization_name", "Организация")
                    .SetVisible(false)
                    .SetWidth(150)
                    .SetAllowGrouping(true);

                columns.CreateText("contractor_name", "Контрагент")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill)
                    .SetAllowGrouping(true);

                columns.CreateDate("date_debited", "Дата операции", "dd.MM.yyyy")
                    .SetWidth(150);

                columns.CreateNumeric("income", "Приход", NumberFormatMode.Currency)
                    .SetWidth(120)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("expense", "Расход", NumberFormatMode.Currency)
                    .SetWidth(120)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateStackedColumns()
                    .Add("income")
                    .Add("expense")
                    .Header("Сумма операции");

                columns.CreateSortedColumns()
                    .Add("doc_date");
            });

            browser.MoveToEnd();
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<PaymentOrder>(string.Format(baseSelect, "(doc_date between :from_date and :to_date and organization_id = :organization_id) or (status_id != 1002)"), new
            {
                from_date = parameters.DateFrom,
                to_date = parameters.DateTo,
                organization_id = parameters.OrganizationId
            }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<PaymentOrder>(string.Format(baseSelect, "id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from payment_order where id = :id", new { id }, transaction);
        }

        IEditorCode IDataEditor.CreateEditor()
        {
            return new PaymentOrderEditor();
        }
    }

    public class PaymentOrderEditor : IEditorCode, IDataOperation, IControlEnabled, IControlVisible
    {
        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const int labelWidth = 210;
            const string contractorSelect = "select id, status_id, name, parent_id from contractor where (status_id in (500, 1002)) or (id = :contractor_id) order by name";
            const string purchaseSelect1 = "select pr.id, 'Заявка №' || pr.doc_number || ' от ' || to_char(pr.doc_date, 'DD.MM.YYYY') || ' на сумму ' || sum(prd.cost_with_tax) as name from purchase_request pr join purchase_request_detail prd on (prd.owner_id = pr.id) where (pr.status_id = 3002 or pr.id = :purchase_id) and (pr.contractor_id = :contractor_id) group by pr.id, pr.doc_number, pr.doc_date";
            const string purchaseSelect2 = "select pr.id, 'Заявка №' || pr.doc_number || ' от ' || to_char(pr.doc_date, 'DD.MM.YYYY') || ' на сумму ' || sum(prd.cost_with_tax) as name from purchase_request pr join purchase_request_detail prd on (prd.owner_id = pr.id) where (pr.status_id = 3002 or pr.id = :purchase_id) group by pr.id, pr.doc_number, pr.doc_date";
            const string invoiceSelect1 = "select ir.id, 'Поступление №' || ir.doc_number || ' от ' || to_char(ir.doc_date, 'DD.MM.YYYY') || ' на сумму ' || sum(ird.cost_with_tax) as name from invoice_receipt ir join invoice_receipt_detail ird on (ird.owner_id = ir.id) where (ir.status_id in (3004, 3006) or ir.id = :invoice_receipt_id) and (ir.contractor_id = :contractor_id) group by ir.id, ir.doc_number, ir.doc_date";
            const string invoiceSelect2 = "select ir.id, 'Поступление №' || ir.doc_number || ' от ' || to_char(ir.doc_date, 'DD.MM.YYYY') || ' на сумму ' || sum(ird.cost_with_tax) as name from invoice_receipt ir join invoice_receipt_detail ird on (ird.owner_id = ir.id) where (ir.status_id in (3004, 3006) or ir.id = :invoice_receipt_id) group by ir.id, ir.doc_number, ir.doc_date";

			PaymentOrder order = editor.Entity as PaymentOrder;

            IControl contractor_id = editor.CreateSelectBox("contractor_id", "Контрагент", (e, c) =>
                {
                    PaymentOrder po = e.Entity as PaymentOrder;
                    return c.Query<GroupDataItem>(contractorSelect, new { po.contractor_id });
                })
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = database.CreateConnection())
                    {
                        editor.Populates["purchase_id"].Populate(conn, editor.Entity);
                        editor.Populates["invoice_receipt_id"].Populate(conn, editor.Entity);
                    }
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(300);

            IControl doc_date = editor.CreateDateTimePicker("doc_date", "Дата п/п", customFormat: "dd.MM.yyyy")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(140);

            IControl doc_number = editor.CreateTextBox("doc_number", "Номер п/п")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(140);

            IControl date_debited = editor.CreateDateTimePicker("date_debited", "Дата операции", customFormat: "dd.MM.yyyy")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(140);

            IControl amount_debited = editor.CreateCurrency("amount_debited", "Сумма")
                .SetLabelWidth(labelWidth);

            IControl purchase_id = editor.CreateSelectBox("purchase_id", "Заявка на расход", (e, c) =>
                {
                    PaymentOrder po = e.Entity as PaymentOrder;
                    if (po.contractor_id != null)
                        return c.Query<GroupDataItem>(purchaseSelect1, new { po.contractor_id, po.purchase_id });
                    else
                        return c.Query<GroupDataItem>(purchaseSelect2, new { po.purchase_id });
                })
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = database.CreateConnection())
                    {
                        Guid id = database.ExecuteSqlCommand<Guid>("select contractor_id from purchase_request where id = :purchase_id", new { purchase_id = e.Value });
						editor.Data["invoice_receipt_id"] = null;
                        editor.Data["contractor_id"] = id;                        
                    }
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400)
				.SetVisible(order.expense.HasValue);

            IControl invoice_receipt_id = editor.CreateSelectBox("invoice_receipt_id", "Поступление товаров/материалов", (e, c) =>
                {
                    PaymentOrder po = e.Entity as PaymentOrder;
                    if (po.contractor_id != null)
                        return c.Query<GroupDataItem>(invoiceSelect1, new { po.contractor_id, po.invoice_receipt_id });
                    else
                        return c.Query<GroupDataItem>(invoiceSelect2, new { po.invoice_receipt_id });
                })
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = database.CreateConnection())
                    {
                        Guid id = database.ExecuteSqlCommand<Guid>("select contractor_id from invoice_receipt where id = :invoice_receipt_id", new { invoice_receipt_id = e.Value });
						editor.Data["purchase_id"] = null;
                        editor.Data["contractor_id"] = id;
                    }
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400)
				.SetVisible(order.expense.HasValue);

            editor.Container.Add(new IControl[] {
                contractor_id,
                doc_date,
                doc_number,
                date_debited,
                amount_debited,
                purchase_id,
                invoice_receipt_id
            });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = @"
                select 
                    id, 
                    '№' || doc_number || ' от ' || to_char(doc_date, 'DD.MM.YYYY') as document_name, 
                    doc_date, 
                    doc_number, 
                    contractor_id, 
                    date_debited, 
                    amount_debited, 
                    purchase_id, 
                    invoice_receipt_id,
					iif(direction = 'expense'::document_direction, amount_debited, NULL::money) AS expense,
    				iif(direction = 'income'::document_direction, amount_debited, NULL::money) AS income
                from payment_order
                where id = :id";
            return connection.QuerySingleOrDefault<PaymentOrder>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into payment_order default values returning id";
            return connection.QuerySingle<Guid>(sql, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update payment_order set doc_date = :doc_date, doc_number = :doc_number, contractor_id = :contractor_id, date_debited = :date_debited, amount_debited = :amount_debited, purchase_id = :purchase_id, invoice_receipt_id = :invoice_receipt_id where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from payment_order where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
			if (new string[] { "purchase_id", "invoice_receipt_id" }.Contains(dataName))
            {
                return info.StatusCode == "expense" && (entity as PaymentOrder).expense.HasValue;
            }

            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }

        bool IControlVisible.Ability(object entity, string dataName, IInformation info)
        {
            if (new string[] { "purchase_id", "invoice_receipt_id" }.Contains(dataName))
            {
                return info.StatusCode == "expense" || (entity as PaymentOrder).expense.HasValue;
            }

            return true;
        }
    }
}
