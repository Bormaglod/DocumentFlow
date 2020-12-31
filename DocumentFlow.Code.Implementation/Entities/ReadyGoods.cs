using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.System;
using DocumentFlow.Core;
using DocumentFlow.Data;
using DocumentFlow.Data.Base;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Implementation.ReadyGoodsImp
{
    public class ReadyGoods : Document
    {
        public DateTime order_date { get; protected set; }
        public string order_number { get; protected set; }
        public string organization_name { get; protected set; }
        public Guid? goods_id { get; set; }
        public string goods_name { get; protected set; }
        public decimal amount { get; set; }
        public decimal price { get; set; }
        public decimal cost { get; set; }
        public string contractor_name { get; protected set; }
    }

    public class ReadyGoodsBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelectChild = @"
            select 
                rg.id, 
                rg.status_id, 
                s.note as status_name, 
                rg.doc_date, 
                rg.doc_number, 
                o.name as organization_name, 
                g.name as goods_name, 
                rg.amount, 
                rg.price, 
                rg.cost 
            from ready_goods rg 
                join status s on (s.id = rg.status_id) 
                join organization o on (o.id = rg.organization_id) 
                left join goods g on (g.id = rg.goods_id) 
            where {0}";
        private const string baseSelectMain = @"
            select 
                rg.id, 
                rg.status_id, 
                s.note as status_name, 
                ua.name as user_created, 
                po.doc_date as order_date, 
                po.doc_number as order_number, 
                rg.doc_date, 
                rg.doc_number, 
                o.name as organization_name, 
                g.name as goods_name, 
                rg.amount, 
                rg.price, 
                rg.cost, 
                c.name as contractor_name 
            from ready_goods rg 
                join status s on (s.id = rg.status_id) 
                join user_alias ua on (ua.id = rg.user_created_id) 
                join organization o on(o.id = rg.organization_id) 
                left join goods g on(g.id = rg.goods_id) 
                left join production_order po on(po.id = rg.owner_id) 
                left join contractor c on(c.id = po.contractor_id) 
            where {0}";

        private BrowserMode mode;

        void IBrowserCode.Initialize(IBrowser browser)
        {
            mode = browser.Mode;

            browser.AllowGrouping = true;
            browser.DataType = DataType.Document;
            if (browser.Mode == BrowserMode.Dependent)
            {
                browser.ToolBar.ButtonStyle = ToolStripItemDisplayStyle.Image;
                browser.ToolBar.IconSize = ButtonIconSize.Small;
                browser.CommandBarVisible = false;
            }
            else
            {
                browser.FromDate = DateRanges.FirstYearDay;
                browser.ToDate = DateRanges.LastYearDay;
            }

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

                if (browser.Mode == BrowserMode.Main)
                {
                    columns.CreateText("user_created", "Автор")
                        .SetAllowGrouping(true)
                        .SetVisible(false)
                        .SetWidth(110);
                }

                columns.CreateDate("doc_date", "Дата")
                    .SetWidth(150)
                    .SetHideable(false);

                columns.CreateText("doc_number", "Номер")
                    .SetWidth(100);

                columns.CreateText("organization_name", "Организация")
                    .SetAllowGrouping(true)
                    .SetVisible(false)
                    .SetWidth(150);

                if (browser.Mode == BrowserMode.Main)
                {
                    columns.CreateDate("order_date", "Дата")
                        .SetWidth(150);

                    columns.CreateText("order_number", "Номер")
                        .SetWidth(100);

                    columns.CreateText("contractor_name", "Контрагент")
                        .SetAllowGrouping(true)
                        .SetWidth(150);
                }

                columns.CreateText("goods_name", "Изделие")
                    .SetAllowGrouping(true)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateNumeric("amount", "Количество")
                    .SetDecimalDigits(3)
                    .SetHorizontalAlignment(HorizontalAlignment.Right)
                    .SetWidth(120);

                columns.CreateNumeric("price", "Цена", NumberFormatMode.Currency)
                    .SetWidth(100)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("cost", "Стоимость", NumberFormatMode.Currency)
                    .SetWidth(100)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                if (browser.Mode == BrowserMode.Main)
                {
                    columns.CreateStackedColumns()
                        .Add("order_date")
                        .Add("order_number")
                        .Add("contractor_name")
                        .Header("Заказ");
                }

                columns.CreateSortedColumns()
                    .Add("doc_date", ListSortDirection.Descending);

                columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                    .AddColumn("amount", RowSummaryType.DoubleAggregate, "{Sum}")
                    .AddColumn("cost", RowSummaryType.DoubleAggregate, "{Sum:c}");

                columns.CreateGroupSummaryRow()
                    .AddColumn("amount", RowSummaryType.DoubleAggregate, "{Sum}")
                    .AddColumn("cost", RowSummaryType.DoubleAggregate, "{Sum:c}");
            });

            if (browser.Mode == BrowserMode.Dependent)
            {
                browser.CreateGroups()
                    .Add("goods_name");
            }

			browser.MoveToEnd();
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            if (mode == BrowserMode.Main)
            {
                string sql = string.Format(baseSelectMain, "rg.doc_date between :from_date and :to_date and rg.organization_id = :organization_id");
                return connection.Query<ReadyGoods>(sql, new
                {
                    from_date = parameters.DateFrom,
                    to_date = parameters.DateTo,
                    organization_id = parameters.OrganizationId
                }).AsList();
            }
            else
            {
                string sql = string.Format(baseSelectChild, "rg.owner_id = :owner_id");
                return connection.Query<ReadyGoods>(sql, new
                {
                    owner_id = parameters.OwnerId
                }).AsList();
            }
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<ReadyGoods>(string.Format(mode == BrowserMode.Main ? baseSelectMain : baseSelectChild, "rg.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from ready_goods where id = :id", new { id }, transaction);
        }

        IEditorCode IDataEditor.CreateEditor()
        {
            return new ReadyGoodsEditor();
        }
    }

    public class ReadyGoodsEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private bool main;

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const int labelWidth = 190;
            const string goodsSelect = "with recursive r as (select id, status_id, name, parent_id from goods where id = '4da429d1-cd8f-4757-bea8-49c99adc48d8' union all select g.id, g.status_id, g.name, g.parent_id from goods g join r on (r.id = g.parent_id and g.status_id = 500)) select id, status_id, name, parent_id from r union all select g.id, g.status_id, g.name, g.parent_id from goods g join production_order_detail pod on (pod.owner_id = :owner_id and pod.goods_id = g.id) order by name";
            const string ownerSelect = "select po.id, po.status_id, '№' || po.doc_number || ' от ' || to_char(po.doc_date, 'DD.MM.YYYY') as name from production_order po where po.status_id = 3100 or po.id = :owner_id";

            main = editor.Browser == null || editor.Browser.Mode == BrowserMode.Main;

            IControl order_name;
            if (main)
            {
                order_name = editor.CreateSelectBox("owner_id", "Заказ на изготовление", (c) =>
                    {
                        ReadyGoods rg = editor.Entity as ReadyGoods;
                        return c.Query<GroupDataItem>(ownerSelect, new { rg.owner_id });
                    })
                    .ValueChangedAction((s, e) => {
                        using (IDbConnection conn = database.CreateConnection())
                        {
                            editor.Populates["goods_id"].Populate(conn, editor.Entity);
                        }
                    })
                    .SetLabelWidth(labelWidth)
                    .SetControlWidth(350);
            }
            else
            {
                order_name = editor.CreateTextBox("document_name", "Заказ на изготовление")
                    .SetLabelWidth(labelWidth)
                    .SetControlWidth(350)
                    .SetEnabled(false);
            }

            IControl doc_date = editor.CreateDateTimePicker("doc_date", "Дата изготовления")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(170);

            IControl goods_id = editor.CreateSelectBox("goods_id", "Изделие", (e, c) =>
                {
                    ReadyGoods rg = e.Entity as ReadyGoods;
                    return c.Query<GroupDataItem>(goodsSelect, new { rg.owner_id });
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(300);

            IControl amount = editor.CreateNumeric("amount", "Количество")
                .SetLabelWidth(labelWidth);

            IControl price = editor.CreateCurrency("price", "Цена")
                .SetLabelWidth(labelWidth);

            IControl cost = editor.CreateCurrency("cost", "Сумма")
                .SetLabelWidth(labelWidth);

            editor.Container.Add(new IControl[]
            {
                order_name,
                doc_date,
                goods_id,
                amount,
                price,
                cost
            });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = @"
                select 
                    rg.id, 
                    rg.owner_id, 
                    '№' || po.doc_number || ' от ' || to_char(po.doc_date, 'DD.MM.YYYY') as document_name, 
                    rg.doc_date, 
                    rg.doc_number, 
                    rg.goods_id, 
                    rg.amount, 
                    rg.price, 
                    rg.cost
                from ready_goods rg 
                    left join production_order po on (po.id = rg.owner_id) 
                where rg.id = :id";
            return connection.QuerySingleOrDefault<ReadyGoods>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            if (main)
            {
                string sql = "insert into ready_goods default values returning id";
                return connection.QuerySingle<Guid>(sql, transaction: transaction);
            }
            else
            {
                string sql = "insert into ready_goods (owner_id) values (:owner_id) returning id";
                return connection.QuerySingle<Guid>(sql, new { owner_id = parameters.OwnerId }, transaction: transaction);
            }
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update ready_goods set owner_id = :owner_id, doc_date = :doc_date, goods_id = :goods_id, amount = :amount, price = :price, cost = :cost where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from ready_goods where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            if (dataName == "document_name")
                return false;

            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }
    }
}
