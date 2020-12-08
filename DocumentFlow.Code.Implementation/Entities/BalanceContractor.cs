using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.BalanceContractorImp
{
    public class BalanceContractor : IDocument
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_code { get; protected set; }
        public string status_name { get; protected set; }
        public Guid document_id { get; set; }
        public string document_name { get; set; }
        public DateTime document_date { get; set; }
        public string document_number { get; set; }
        public decimal operation_summa { get; set; }
        public decimal? income { get; protected set; }
        public decimal? expense { get; protected set; }
        public string contractor_name { get; protected set; }
        public string doc_number { get; set; }
        public DateTime doc_date { get; set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class BalanceContractorBrowser : BrowserCodeBase<BalanceContractor>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                bc.id, 
                bc.status_id, 
                s.note as status_name, 
                bc.owner_id as document_id, 
                case 
                    when bc.owner_id is null then 'Начальный остаток' 
                    else bc.document_name 
                end as document_name, 
                bc.document_date, 
                bc.document_number, 
                case 
                    when bc.operation_summa > 0::money then bc.operation_summa 
                    else null 
                end as income, 
                case 
                    when bc.operation_summa < 0::money then (@bc.operation_summa::numeric)::money 
                    else null 
                end as expense, 
                c.name as contractor_name 
            from balance_contractor bc 
                join status s on (s.id = bc.status_id) 
                left join contractor c on (c.id = bc.reference_id) 
            where {0} 
            order by bc.document_date desc";

        public void Initialize(IBrowser browser)
        {
            browser.AllowSorting = false;
            browser.DataType = DataType.Document;
            browser.CommandBarVisible = false;
            browser.ToolBar.ButtonStyle = ButtonDisplayStyle.Image;
            browser.ToolBar.IconSize = ButtonIconSize.Small;

            browser.CreateStatusColumnRenderer();

            IColumnCollection columns = browser.Columns;

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

            columns.CreateText("document_name", "Документ")
                .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

            columns.CreateDate("document_date", "Дата")
                .SetHideable(false)
                .SetWidth(150);

            columns.CreateText("document_number", "Номер")
                .SetWidth(100);

            columns.CreateNumeric("income", "Приход", NumberFormatMode.Currency)
                .SetWidth(100)
                .SetHorizontalAlignment(HorizontalAlignmentText.Right);

            columns.CreateNumeric("expense", "Расход", NumberFormatMode.Currency)
                .SetWidth(100)
                .SetHorizontalAlignment(HorizontalAlignmentText.Right);

            ICommandAdded open_document = browser.Commands.Add(CommandMethod.UserDefined, "open-document", "toolbar");
            open_document.Click += OpenDocumentClick;
        }

        public IEditorCode CreateEditor()
        {
            return new BalanceContractorEditor();
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "bc.reference_id = :owner_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "bc.id = :id");
        }

        private void OpenDocumentClick(object sender, ExecuteEventArgs e)
        {
            BalanceContractor balance = e.Browser.CurrentRow as BalanceContractor;
            if (balance != null)
            {
                e.Browser.Commands.OpenDocument(balance.document_id);
            }
        }

        public override IEnumerable<BalanceContractor> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<BalanceContractor>(GetSelect(), new { owner_id = parameters.OwnerId });
        }
    }

    public class BalanceContractorEditor : EditorCodeBase<BalanceContractor>, IEditorCode
    {
        private const int labelWidth = 140;
        private const string select = @"
            select 
                bc.id, 
                s.code as status_code,
                bc.operation_summa, 
                bc.document_date, 
                c.name as contractor_name, 
                bc.document_name, 
                bc.document_number,
                case 
                    when bc.operation_summa > 0::money then bc.operation_summa 
                    else null 
                end as income, 
                case 
                    when bc.operation_summa < 0::money then (@bc.operation_summa::numeric)::money 
                    else null 
                end as expense
            from balance_contractor bc 
                join status s on (s.id = bc.status_id) 
                join contractor c on (c.id = bc.reference_id) 
            where 
                bc.id = :id";

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            BalanceContractor balance = editor.Entity as BalanceContractor;
            if (balance == null)
            {
                throw new Exception("Ожидался объект типа BalanceContractor.");
            }

            IControl contractor_name = editor.CreateTextBox("contractor_name", "Контрагент")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400)
                .SetEnabled(false);
            IControl document_name = editor.CreateTextBox("document_name", "Документ")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(250)
                .SetEnabled(false)
                .SetVisible(balance.status_code == "current balance");
            IControl document_number = editor.CreateTextBox("document_number", "Номер")
                .SetLabelWidth(labelWidth)
                .SetEnabled(false)
                .SetVisible(balance.status_code == "current balance");
            IControl document_date = editor.CreateDateTimePicker("document_date", "Дата")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(170);

            string op_name;
            string field;
            if (new string[] { "initial balance", "compiled" }.Contains(balance.status_code))
            {
                op_name = "Начальный остаток";
                field = "operation_summa";
            }
            else
            {
                op_name = balance.operation_summa < 0 ? "Расход" : "Приход";
                field = balance.operation_summa < 0 ? "expense" : "income";
            }

            IControl operation_summa = editor.CreateCurrency(field, op_name)
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);

            editor.Container.Add(new IControl[] {
                contractor_name,
                document_name,
                document_number,
                document_date,
                operation_summa
            });
        }

        protected override string GetSelect()
        {
            return select;
        }

        protected override string GetInsert()
        {
            return "insert into balance_contractor (reference_id) values (:owner_id) returning id";
        }

        protected override string GetUpdate(BalanceContractor balanceContractor)
        {
            return "update balance_contractor set operation_summa = :operation_summa, document_date = :document_date where id = :id";
        }

        public override TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            return connection.QuerySingle<TId>(GetInsert(), new { owner_id = parameters.OwnerId }, transaction);
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            if (field == "contractor_name")
                return false;

            return 
                new string[] { "document_date", "operation_summa" }.Contains(field) && 
                new string[] { "compiled" }.Contains(status_name);
        }

        public override bool GetVisibleValue(string field, string status_name)
        {
            if (new string[] { "document_name", "document_number" }.Contains(field))
                return new string[] { "current balance" }.Contains(status_name);

            return true;
        }
    }
}
