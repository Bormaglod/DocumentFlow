using System;
using System.Collections.Generic;
using System.Data;

using Dapper;

using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.ProductionOperationImp
{
    public class ProductionOperation : IDocument
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string document_name { get; protected set; }
        public DateTime date_updated { get; set; }
        public string goods_name { get; protected set; }
        public string operation_name { get; protected set; }
        public int amount { get; set; }
        public int completed { get; set; }
        public int complete_status { get; set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class ProductionOperationBrowser : BrowserCodeBase<ProductionOperation>, IBrowserCode
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

        public void Initialize(IBrowser browser)
        {
            browser.AllowGrouping = true;
            browser.DataType = DataType.Document;
            browser.ToolBar.ButtonStyle = ButtonDisplayStyle.Image;
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
                    .SetHorizontalAlignment(HorizontalAlignmentText.Right);

                columns.CreateInteger("completed", "Выполнено")
                    .SetWidth(150)
                    .SetHorizontalAlignment(HorizontalAlignmentText.Right);

                columns.CreateProgress("complete_status", "Выполнено, %")
                    .SetWidth(150);

                columns.CreateSortedColumns()
                    .Add("date_updated", SortDirection.Descending);

                columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                    .AddColumn("operation_name", RowSummaryType.CountAggregate, "Всего наименований: {Count}");
            });

            browser.CreateGroups()
                .Add("goods_name");
        }

        public override IEnumerable<ProductionOperation> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<ProductionOperation>(GetSelect(), new { owner_id = parameters.OwnerId });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "po.owner_id = :owner_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "po.id = :id");
        }

        public IEditorCode CreateEditor()
        {
            return null;
        }
    }
}
