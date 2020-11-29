using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.BalanceGoodsImp
{
    public class BalanceGoods : IDocument
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
        public decimal amount { get; set; }
        public string goods_name { get; protected set; }
        public decimal remainder { get; protected set; }
        public int doc_number { get; set; }
        public DateTime doc_date { get; set; }
        public string view_number { get; protected set; }
    }

    public class BalanceGoodsBrowser : BrowserCodeBase<BalanceGoods>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                bg.id, 
                bg.status_id, 
                s.note as status_name, 
                bg.owner_id as document_id, 
                case 
                    when bg.owner_id is null then 'Начальный остаток' 
                    else bg.document_name 
                end as document_name, 
                bg.document_date, 
                bg.document_number, 
                bg.operation_summa, 
                case 
                    when bg.operation_summa > 0::money then bg.operation_summa 
                    else null 
                end as income, 
                case 
                    when bg.operation_summa < 0::money then (@bg.operation_summa::numeric)::money 
                    else null 
                end as expense, 
                bg.amount, 
                g.name as goods_name, 
                sum(bg.amount * sign(bg.operation_summa::numeric)) over (order by document_date, document_number) as remainder, 
                bg.doc_date, 
                bg.doc_number, 
                bg.view_number 
            from balance_goods bg 
                join status s on (s.id = bg.status_id) 
                left join goods g on (g.id = bg.reference_id) 
            where 
                {0} 
            order by bg.document_date desc";

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

            columns.CreateNumeric("amount", "Количество", decimalDigits: 3)
                .SetWidth(130)
                .SetHorizontalAlignment(HorizontalAlignmentText.Right);

            columns.CreateNumeric("remainder", "Остаток", decimalDigits: 3)
                .SetWidth(130)
                .SetHorizontalAlignment(HorizontalAlignmentText.Right)
                .SetBackgroundColor("#DAE5F5");

            ICommandAdded open_document = browser.Commands.Add(CommandMethod.UserDefined, "open-document", "toolbar");
            open_document.Click += OpenDocumentClick;
        }

        public IEditorCode CreateEditor()
        {
            return new BalanceGoodsEditor();
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "bg.reference_id = :owner_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "bg.id = :id");
        }

        private void OpenDocumentClick(object sender, ExecuteEventArgs e)
        {
            BalanceGoods balance = e.Browser.CurrentRow as BalanceGoods;
            if (balance != null)
            {
                e.Browser.Commands.OpenDocument(balance.document_id);
            }
        }

        public override IEnumerable<BalanceGoods> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<BalanceGoods>(GetSelect(), new { owner_id = parameters.OwnerId });
        }
    }

    public class BalanceGoodsEditor : EditorCodeBase<BalanceGoods>, IEditorCode
    {
        private const int labelWidth = 140;
        private const string select = @"
            select 
                bg.id, 
                s.code as status_code, 
                bg.operation_summa, 
                bg.amount, 
                bg.document_date, 
                g.name as goods_name, 
                bg.document_name, 
                bg.document_number, 
                case 
                    when bg.operation_summa > 0::money then bg.operation_summa 
                    else null 
                end as income, 
                case 
                    when bg.operation_summa < 0::money then (@bg.operation_summa::numeric)::money 
                    else null 
                end as expense 
            from balance_goods bg 
                join status s on (s.id = bg.status_id) 
                join goods g on (g.id = bg.reference_id) 
            where 
                bg.id = :id";

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            BalanceGoods balance = editor.Entity as BalanceGoods;
            if (balance == null)
            {
                throw new Exception("Ожидался объект типа BalanceGoods.");
            }

            IControl goods_name = editor.CreateTextBox("goods_name", "Материал")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400)
                .SetEnabled(false);
            IControl document_name = editor.CreateTextBox("document_name", "Документ")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(250)
                .SetEnabled(false)
                .SetVisible(new string[] { "current balance", "inventory" }.Contains(balance.status_code));
            IControl document_number = editor.CreateTextBox("document_number", "Номер")
                .SetLabelWidth(labelWidth)
                .SetEnabled(false)
                .SetVisible(new string[] { "current balance", "inventory" }.Contains(balance.status_code));
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
            IControl amount = editor.CreateNumeric("amount", "Количество")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);

            editor.Container.Add(new IControl[] {
                goods_name,
                document_name,
                document_number,
                document_date,
                operation_summa,
                amount
            });
        }

        protected override string GetSelect()
        {
            return select;
        }

        protected override string GetInsert()
        {
            return "insert into balance_goods (reference_id) values (:owner_id) returning id";
        }

        protected override string GetUpdate(BalanceGoods balanceGoods)
        {
            return "update balance_goods set operation_summa = :operation_summa, amount = :amount, document_date = :document_date where id = :id";
        }

        public override TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            return connection.QuerySingle<TId>(GetInsert(), new { owner_id = parameters.OwnerId }, transaction);
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            if (field == "goods_name")
                return false;
            
            return 
                new string[] { "document_date", "operation_summa", "amount" }.Contains(field) && 
                new string[] { "compiled" }.Contains(status_name);
        }

        public override bool GetVisibleValue(string field, string status_name)
        {
            if (new string[] { "document_name", "document_number" }.Contains(field))
                return new string[] { "current balance", "inventory" }.Contains(status_name);

            return true;
        }
    }
}
