using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code.Implementation.BalanceTollingImp
{
    public class BalanceTolling : Document
    {
        public string status_code { get; protected set; }
        public Guid document_id { get; set; }
        public DateTime document_date { get; set; }
        public string document_number { get; set; }
        public decimal operation_summa { get; set; }
        public decimal? income { get; protected set; }
        public decimal? expense { get; protected set; }
        public decimal amount { get; set; }
        public decimal remainder { get; protected set; }
        public Guid? contractor_id { get; set; }
        public string contractor_name { get; protected set; }
        public string goods_name { get; protected set; }
    }

    public class BalanceTollingBrowser : IBrowserCode, IBrowserOperation
    {
        private string kind;

        private const string baseSelect = @"
            select
            	bt.id,
	            bt.status_id,
	            s.note as status_name, 
                bt.owner_id as document_id,
                ek.name as document_name,
                bt.document_date, 
                bt.document_number, 
                bt.operation_summa,
                case 
                	when bt.amount > 0 then bt.amount 
                   	else null 
                end as income, 
                case 
    	            when bt.amount < 0 then @bt.amount 
                    else null 
                end as expense, 
                bt.amount,
	            sum(bt.amount) over (partition by bt.reference_id order by bt.document_date, bt.document_number) as remainder, 
                bt.doc_date, 
                bt.doc_number,
                bt.contractor_id,
                c.short_name as contractor_name,
                g.name as goods_name
            from balance_tolling bt 
	            join status s on (s.id = bt.status_id)
	            left join entity_kind ek on (ek.id = bt.document_kind)
	            left join contractor c on (c.id = bt.contractor_id)
                left join goods g on (g.id = bt.reference_id)
            where 
   	            {0}
            order by bt.document_date desc";

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.AllowSorting = false;
            browser.AllowGrouping = true;
            browser.DataType = DataType.Document;
            browser.CommandBarVisible = false;
            browser.ToolBar.ButtonStyle = ToolStripItemDisplayStyle.Image;
            browser.ToolBar.IconSize = ButtonIconSize.Small;

            browser.CreateStatusColumnRenderer();

            kind = browser.ExecuteSqlCommand<string>("select ek.code from directory d join entity_kind ek on (ek.id = d.entity_kind_id) where d.id = :owner_id", new { owner_id = browser.Parameters.OwnerId });

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

                columns.CreateText("contractor_name", "Контрагент")
                    .SetWidth(200)
                    .SetVisible(kind == "goods")
                    .SetAllowGrouping(true);

                columns.CreateText("goods_name", "Материал")
                    .SetWidth(200)
                    .SetVisible(kind == "contractor")
                    .SetAllowGrouping(true);

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

                if (kind == "contractor")
                {
                    browser.CreateGroups()
                        .Add("goods_name");
                }
            });

            IUserAction open_document = browser.Commands.Add(CommandMethod.UserDefined, "open-document")
                .AppendTo(browser.ToolBar)
                .ExecuteAction((s, e) =>
                {
                    if (e.Browser.CurrentRow is BalanceTolling balance)
                    {
                        e.Browser.Commands.OpenDocument(balance.document_id);
                    }
                });

            if (kind == "goods")
            {
                IUserAction open_contractor = browser.Commands.Add(CommandMethod.UserDefined, "open-document", "open-contractor")
                    .SetTitle("Контрагент")
                    .SetIcon("organization")
                    .AppendTo(browser.ToolBar)
                    .ExecuteAction((s, e) =>
                    {
                        if (e.Browser.CurrentRow is BalanceTolling balance && balance.contractor_id.HasValue)
                        {
                            e.Browser.Commands.OpenDocument(balance.contractor_id.Value);
                        }
                    });
            }
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            if (kind == "contractor")
            {
                return connection.Query<BalanceTolling>(string.Format(baseSelect, "bt.contractor_id = :owner_id"), new { owner_id = parameters.OwnerId }).AsList();
            }
            else if (kind == "goods")
            {
                return connection.Query<BalanceTolling>(string.Format(baseSelect, "bt.reference_id = :owner_id"), new { owner_id = parameters.OwnerId }).AsList();
            }
            else
            {
                return Array.Empty<BalanceTolling>();
            }
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<BalanceTolling>(string.Format(baseSelect, "bt.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from balance_tolling where id = :id", new { id }, transaction);
        }
    }
}
