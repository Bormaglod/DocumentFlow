using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Core;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.PaymentOrderImp
{
    public class PaymentOrder : IDocument
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string document_name { get; protected set; }
        public string user_created { get; protected set; }
        public DateTime doc_date { get; set; }
        public long doc_number { get; set; }
        public string view_number { get; set; }
        public Guid? contractor_id { get; set; }
        public string contractor_name { get; set; }
        public DateTime date_debited { get; set; }
        public decimal? expense { get; set; }
        public decimal? income { get; set; }
        public string organization_name { get; protected set; }
		public decimal amount_debited { get; set; }
        public Guid? purchase_id { get; set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class PaymentOrderBrowser : BrowserCodeBase<PaymentOrder>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                id, 
                status_id, 
                status_name, 
                user_created, 
                doc_date, 
                view_number, 
                contractor_name,
                date_debited, 
                expense, 
                income, 
                organization_name 
            from payment_order_view 
            where {0}";

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

                columns.CreateDate("doc_date", "Дата/время", "dd.MM.yyyy")
                    .SetWidth(150)
                    .SetHideable(false);

                columns.CreateText("view_number", "Номер")
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
                    .SetHorizontalAlignment(HorizontalAlignmentText.Right);

                columns.CreateNumeric("expense", "Расход", NumberFormatMode.Currency)
                    .SetWidth(120)
                    .SetHorizontalAlignment(HorizontalAlignmentText.Right);

                columns.CreateStackedColumns()
                    .Add("income")
                    .Add("expense")
                    .Header("Сумма операции");

                columns.CreateSortedColumns()
                    .Add("doc_date");
            });
        }

        public override IEnumerable<PaymentOrder> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<PaymentOrder>(GetSelect(), new
            {
                from_date = parameters.DateFrom,
                to_date = parameters.DateTo,
                organization_id = parameters.OrganizationId
            });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "doc_date between :from_date and :to_date and organization_id = :organization_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "id = :id");
        }

        public IEditorCode CreateEditor()
        {
            return new PaymentOrderEditor();
        }
    }

    public class PaymentOrderEditor : EditorCodeBase<PaymentOrder>, IEditorCode
    {
        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const int labelWidth = 210;
            const string contractorSelect = "select id, status_id, name, parent_id from contractor where (status_id in (500, 1002)) or (id = :contractor_id) order by name";
            const string purchaseSelect = "select pr.id, ek.name || ' №' || view_number || ' от ' || to_char(doc_date, 'DD.MM.YYYY') || ' на сумму ' || sum(prd.cost_with_tax) as name from purchase_request pr join entity_kind ek on (ek.id = pr.entity_kind_id) join purchase_request_detail prd on (prd.owner_id = pr.id) where (pr.status_id in (3002, 3004) or pr.id = :purchase_id) and (pr.contractor_id = :contractor_id) group by pr.id, ek.name, view_number, doc_date";

            IControl contractor_id = editor.CreateSelectBox("contractor_id", "Контрагент", (e, c) =>
                {
                    PaymentOrder po = e.Entity as PaymentOrder;
                    return c.Query<GroupDataItem>(contractorSelect, new { po.contractor_id });
                })
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = editor.CreateConnection())
                    {
                        editor["purchase_id"].AsPopulateControl().Populate(conn, editor.Entity);
                    }
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(300);

            IControl doc_date = editor.CreateDateTimePicker("doc_date", "Дата п/п", customFormat: "dd.MM.yyyy")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(140);

            IControl doc_number = editor.CreateInteger("doc_number", "Номер п/п")
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
                    return c.Query<GroupDataItem>(purchaseSelect, new { po.contractor_id, po.purchase_id });
                })
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = editor.CreateConnection())
                    {
                        Guid id = editor.ExecuteSqlCommand<Guid>("select contractor_id from purchase_request where id = :purchase_id", new { purchase_id = e.Value });
                        editor["contractor_id"].Value = id;
                    }
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400);

            editor.Container.Add(new IControl[] {
                contractor_id,
                doc_date,
                doc_number,
                date_debited,
                amount_debited,
                purchase_id
            });
        }

        protected override string GetSelect()
        {
            return "select po.id, '№' || view_number || ' от ' || to_char(doc_date, 'DD.MM.YYYY') as name, doc_date, doc_number, contractor_id, date_debited, amount_debited, purchase_id, s.code as cur_status_name from payment_order po join status s on (s.id = status_id) where po.id = :id";
        }

        protected override string GetUpdate(PaymentOrder pay)
        {
            return "update payment_order set doc_date = :doc_date, doc_number = :doc_number, contractor_id = :contractor_id, date_debited = :date_debited, amount_debited = :amount_debited, purchase_id = :purchase_id where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }
    }
}
