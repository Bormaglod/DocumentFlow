using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code.Implementation.BalanceGoodsImp
{
    public class BalanceGoods : Document
    {
        public string status_code { get; protected set; }
        public Guid document_id { get; set; }
        public DateTime document_date { get; set; }
        public string document_number { get; set; }
        public decimal operation_summa { get; set; }
        public decimal? income { get; protected set; }
        public decimal? expense { get; protected set; }
        public decimal amount { get; set; }
        public string goods_name { get; protected set; }
        public decimal remainder { get; protected set; }
    }

    public class BalanceGoodsBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelect = @"
            select 
                bg.id, 
                bg.status_id, 
                s.note as status_name, 
                bg.owner_id as document_id, 
                case 
                    when bg.status_id = 1110 then 'Начальный остаток' 
                    else ek.name 
                end as document_name, 
                bg.document_date, 
                bg.document_number, 
                bg.operation_summa, 
                case 
                    when bg.amount > 0 then bg.amount 
                    else null 
                end as income, 
                case 
                    when bg.amount < 0 then @amount 
                    else null 
                end as expense, 
                bg.amount,
                g.name as goods_name, 
		        sum(bg.amount) over (order by document_date, document_number) as remainder, 
                bg.doc_date, 
                bg.doc_number
            from balance_goods bg 
                join status s on (s.id = bg.status_id) 
                left join goods g on (g.id = bg.reference_id) 
                left join entity_kind ek on (ek.id = bg.document_kind)
            where 
                {0} 
            order by bg.document_date desc";

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.AllowSorting = false;
            browser.DataType = DataType.Document;
            browser.CommandBarVisible = false;
            browser.ToolBar.ButtonStyle = ToolStripItemDisplayStyle.Image;
            browser.ToolBar.IconSize = ButtonIconSize.Small;

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

                columns.CreateText("document_name", "Документ")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateDate("document_date", "Дата")
                    .SetHideable(false)
                    .SetWidth(150);

                columns.CreateText("document_number", "Номер")
                    .SetWidth(100);

				columns.CreateNumeric("operation_summa", "Сумма", NumberFormatMode.Currency)                    
                    .SetWidth(100)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("income", "Приход")
					.SetDecimalDigits(3)
                    .SetWidth(130)
				                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("expense", "Расход")
					.SetDecimalDigits(3)
                    .SetWidth(130)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("remainder", "Остаток")
                    .SetDecimalDigits(3)
                    .SetWidth(130)
                    .SetHorizontalAlignment(HorizontalAlignment.Right)
                    .SetBackgroundColor("#DAE5F5");
            });

            IUserAction open_document = browser.Commands.Add(CommandMethod.UserDefined, "open-document");
            open_document.Click += OpenDocumentClick;

            browser.ToolBar.AddCommand(open_document);
        }

        IEditorCode IDataEditor.CreateEditor() => new BalanceGoodsEditor();

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<BalanceGoods>(string.Format(baseSelect, "bg.reference_id = :owner_id"), new { owner_id = parameters.OwnerId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<BalanceGoods>(string.Format(baseSelect, "bg.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from balance_goods where id = :id", new { id }, transaction);
        }

        private void OpenDocumentClick(object sender, ExecuteEventArgs e)
        {
            if (e.Browser.CurrentRow is BalanceGoods balance)
            {
                e.Browser.Commands.OpenDocument(balance.document_id);
            }
        }
    }

    public class BalanceGoodsEditor : IEditorCode, IDataOperation, IControlEnabled, IControlVisible
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
                    when bg.operation_summa > 0 then bg.operation_summa 
                    else null 
                end as income, 
                case 
                    when bg.operation_summa < 0 then (@bg.operation_summa)
                    else null 
                end as expense 
            from balance_goods bg 
                join status s on (s.id = bg.status_id) 
                join goods g on (g.id = bg.reference_id) 
            where 
                bg.id = :id";

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            if (editor.Entity is BalanceGoods balance)
            {
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
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<BalanceGoods>(select, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into balance_goods (reference_id) values (:owner_id) returning id";
            return connection.QuerySingle<Guid>(sql, new { owner_id = parameters.OwnerId }, transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update balance_goods set operation_summa = :operation_summa, amount = :amount, document_date = :document_date where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            string sql = "delete from balance_goods where id = :id";
            return connection.Execute(sql, new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            if (dataName == "goods_name")
                return false;
            
            return 
                new string[] { "document_date", "operation_summa", "amount" }.Contains(dataName) && 
                new string[] { "compiled" }.Contains(info.StatusCode);
        }

        bool IControlVisible.Ability(object entity, string dataName, IInformation info)
        {
            if (new string[] { "document_name", "document_number" }.Contains(dataName))
            {
                return new string[] { "current balance", "inventory" }.Contains(info.StatusCode);
            }

            return true;
        }
    }
}
