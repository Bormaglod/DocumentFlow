using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.System;
using DocumentFlow.Data.Base;

namespace DocumentFlow.Code.Implementation.ReportBalanceGoodsImp
{
    public class ReportBalanceGoods : NameDataItem
    {
        public decimal opening_balance { get; set; }
        public decimal income { get; set; }
        public decimal expense { get; set; }
        public decimal closing_balance { get; set; }
    }

    public class ReportBalanceGoodsBrowser : IBrowserCode, IBrowserOperation
    {
        private const string baseSelect = "select id, name, opening_balance, income, expense, closing_balance from balance_sheet_goods(:from_date, :to_date)";

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Report;

            browser.CreateColumns((columns) =>
            {
                columns.CreateText("id", "Id")
                    .SetWidth(180)
                    .SetVisible(false);

                columns.CreateText("name", "Материал")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateNumeric("opening_balance", "Остаток на начало")
                    .SetDecimalDigits(3)
                    .SetHorizontalAlignment(HorizontalAlignment.Right)
                    .SetWidth(150);

                columns.CreateNumeric("income", "Приход")
                    .SetDecimalDigits(3)
                    .SetHorizontalAlignment(HorizontalAlignment.Right)
                    .SetWidth(150);

                columns.CreateNumeric("expense", "Расход")
                    .SetDecimalDigits(3)
                    .SetHorizontalAlignment(HorizontalAlignment.Right)
                    .SetWidth(150);

                columns.CreateNumeric("closing_balance", "Остаток на конец")
                    .SetDecimalDigits(3)
                    .SetHorizontalAlignment(HorizontalAlignment.Right)
                    .SetWidth(150);

                columns.CreateSortedColumns()
                    .Add("name");
            });
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<ReportBalanceGoods>(baseSelect, new { from_date = parameters.DateFrom, to_date = parameters.DateTo }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<ReportBalanceGoods>(string.Format(baseSelect, " where id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            throw new InvalidOperationException();
        }
    }
}
