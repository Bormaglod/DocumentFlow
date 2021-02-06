using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code.Implementation.ProductionOperationImp
{
    public class ProductionOperation : Document
    {
        public string goods_name { get; protected set; }
        public string operation_name { get; protected set; }
        public int amount { get; set; }
        public int completed { get; set; }
        public int complete_status { get; set; }
    }

    public class ProductionOperationBrowser : IBrowserCode, IBrowserOperation
    {
        private const string baseSelect = @"
            select 
                po.id, 
                po.status_id, 
                s.note as status_name, 
                po.date_updated, 
                g.name as goods_name, 
                o.name as operation_name, 
                po.amount, 
                po.completed, 
                round(po.completed * 100 / po.amount) as complete_status 
            from production_operation po 
                join status s on (s.id = po.status_id) 
                join goods g on (g.id = po.goods_id) 
                join operation o on (o.id = po.operation_id) 
            where {0}";

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.AllowGrouping = true;
            browser.DataType = DataType.Document;
            browser.ToolBar.ButtonStyle = ToolStripItemDisplayStyle.Image;
            browser.ToolBar.IconSize = ButtonIconSize.Small;
            browser.CommandBarVisible = false;

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

                columns.CreateDate("date_updated", "Дата")
                    .SetWidth(150)
                    .SetHideable(false);

                columns.CreateText("goods_name", "Изделие")
                    .SetWidth(150)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateText("operation_name", "Операция")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill)
                    .SetHideable(false)
                    .SetAllowGrouping(true);

                columns.CreateInteger("amount", "Количество")
                    .SetWidth(150)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateInteger("completed", "Выполнено")
                    .SetWidth(150)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateProgress("complete_status", "Выполнено, %")
                    .SetWidth(150);

                columns.CreateSortedColumns()
                    .Add("date_updated", ListSortDirection.Descending);

                columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                    .AddColumn("operation_name", RowSummaryType.CountAggregate, "Всего наименований: {Count}");
            });

            browser.CreateGroups()
                .Add("goods_name");
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<ProductionOperation>(string.Format(baseSelect, "po.owner_id = :owner_id"), new { owner_id = parameters.OwnerId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<ProductionOperation>(string.Format(baseSelect, "po.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from production_operation where id = :id", new { id }, transaction);
        }
    }
}
