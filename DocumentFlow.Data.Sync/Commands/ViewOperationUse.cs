using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code.Implementation.ViewOperationUseImp
{
    public class ViewOperationUse : DocumentInfo
    {
        public string calculation_name { get; set; }
        public Guid goods_id { get; set; }
        public string goods_code { get; set; }
        public string goods_name { get; set; }
    }

    public class ViewOperationUseBrowser : IBrowserCode, IBrowserOperation
    {
        private const string baseSelect = @"
            select 
                c.id,
                c.name as calculation_name,
                c.status_id,
                s.note as status_name,
                g.id as goods_id, 
                g.code as goods_code, 
                g.name as goods_name
	        from calc_item_operation cio
		        join calculation c on (c.id = cio.owner_id)
		        join goods g on (g.id = c.owner_id)
                join status s on (s.id = c.status_id)
	        where item_id = :operation_id";

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.ToolBar.ButtonStyle = ToolStripItemDisplayStyle.Image;
            browser.ToolBar.IconSize = ButtonIconSize.Small;
            browser.CommandBarVisible = false;
            browser.AllowGrouping = true;

            browser.CreateStatusColumnRenderer();

            browser.CreateColumns((columns) =>
            {
                columns.CreateText("id", "Id")
                    .SetWidth(180)
                    .SetVisible(false);

                columns.CreateInteger("status_id", "Код состояния")
                    .SetWidth(80)
                    .SetVisible(false);

                columns.CreateText("status_name", "Состояние")
                    .SetWidth(110)
                    .SetVisible(false);

                columns.CreateText("goods_id", "Id калькуляции")
                    .SetWidth(180)
                    .SetVisible(false);

                columns.CreateText("goods_code", "Артикул")
                    .SetWidth(140)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateText("goods_name", "Наименование")
                    .SetHideable(false)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateText("calculation_name", "Калькуляция")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateSortedColumns()
                    .Add("goods_name")
                    .Add("calculation_name");
            });

            browser.CreateGroups()
                .Add("goods_name");

            browser.Commands.Get("add-record").SetVisible(false);
            browser.Commands.Get("edit-record").SetVisible(false);
            browser.Commands.Get("delete-record").SetVisible(false);
            browser.Commands.Get("copy-record").SetVisible(false);
            browser.Commands.Get("create-group").SetVisible(false);

            browser.Commands.Add(CommandMethod.UserDefined, "open-document")
                .SetIcon("товар")
                .SetTitle("Изделие")
                .AppendTo(browser.ToolBar)
                .AppendTo(browser.ContextMenuRecord)
                .ExecuteAction(OpenGoodsClick);

            browser.Commands.Add(CommandMethod.UserDefined, "open-document")
                .SetIcon("гроссбух")
                .SetTitle("Калькуляция")
                .AppendTo(browser.ToolBar)
                .AppendTo(browser.ContextMenuRecord)
                .ExecuteAction(OpenCalculationClick);
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<ViewOperationUse>(baseSelect, new { operation_id = parameters.OwnerId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<ViewOperationUse>(baseSelect + " and id = :id", new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            throw new InvalidOperationException();
        }

        private void OpenGoodsClick(object sender, ExecuteEventArgs e)
        {
            if (e.Browser.CurrentRow is ViewOperationUse vou)
            {
                e.Browser.Commands.OpenDocument(vou.goods_id);
            }
        }

        private void OpenCalculationClick(object sender, ExecuteEventArgs e)
        {
            if (e.Browser.CurrentRow is ViewOperationUse vou)
            {
                e.Browser.Commands.OpenDocument(vou.id);
            }
        }
    }
}
