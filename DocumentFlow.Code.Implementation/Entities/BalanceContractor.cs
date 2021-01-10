using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.System;
using DocumentFlow.Data;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Implementation.BalanceContractorImp
{
    public class BalanceContractor : Document
    {
        public string status_code { get; protected set; }
        public Guid document_id { get; set; }
        public DateTime document_date { get; set; }
        public string document_number { get; set; }
        public decimal operation_summa { get; set; }
        public decimal? income { get; protected set; }
        public decimal? expense { get; protected set; }
        public string contractor_name { get; protected set; }
    }

    public class BalanceContractorBrowser : IBrowserCode, IBrowserOperation, IDataEditor
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

                columns.CreateNumeric("income", "Приход", NumberFormatMode.Currency)
                    .SetWidth(100)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("expense", "Расход", NumberFormatMode.Currency)
                    .SetWidth(100)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);
            });

            ICommand open_document = browser.Commands.Add(CommandMethod.UserDefined, "open-document");
            open_document.Click += OpenDocumentClick;

            browser.ToolBar.AddCommand(open_document);
        }

        IEditorCode IDataEditor.CreateEditor() => new BalanceContractorEditor();

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<BalanceContractor>(string.Format(baseSelect, "bc.reference_id = :owner_id"), new { owner_id = parameters.OwnerId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<BalanceContractor>(string.Format(baseSelect, "bc.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from balance_contractor where id = :id", new { id }, transaction);
        }

        private void OpenDocumentClick(object sender, ExecuteEventArgs e)
        {
            if (e.Browser.CurrentRow is BalanceContractor balance)
            {
                e.Browser.Commands.OpenDocument(balance.document_id);
            }
        }
    }

    public class BalanceContractorEditor : IEditorCode, IDataOperation, IControlEnabled, IControlVisible
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

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            if (editor.Entity is BalanceContractor balance)
            {
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
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<BalanceContractor>(select, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into balance_contractor (reference_id) values (:owner_id) returning id";
            return connection.QuerySingle<Guid>(sql, new { owner_id = parameters.OwnerId }, transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update balance_contractor set operation_summa = :operation_summa, document_date = :document_date where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            string sql = "delete from balance_contractor where id = :id";
            return connection.Execute(sql, new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            if (dataName == "contractor_name")
                return false;

            return 
                new string[] { "document_date", "operation_summa" }.Contains(dataName) && 
                new string[] { "compiled" }.Contains(info.StatusCode);
        }

        bool IControlVisible.Ability(object entity, string dataName, IInformation info)
        {
            if (new string[] { "document_name", "document_number" }.Contains(dataName))
            {
                return new string[] { "current balance" }.Contains(info.StatusCode);
            }

            return true;
        }
    }
}
