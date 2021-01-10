using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.System;
using DocumentFlow.Data;
using DocumentFlow.Data.Base;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Implementation.GoodsImp
{
    public class Goods : Directory
    {
        public string ext_article { get; set; }
        public Guid? measurement_id { get; set; }
        public string abbreviation { get; set; }
        public decimal price { get; set; }
        public int tax { get; set; }
        public decimal min_order { get; set; }
        public bool is_service { get; set; }
        public decimal cost { get; set; }
        public decimal profit_percent { get; set; }
        public decimal profit_value { get; set; }
        public decimal calc_price { get; set; }
        public DateTime? approved { get; set; }
		public decimal? balance { get; set; }
		public bool goods_use { get; protected set; }
    }

    public class GoodsBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelect = @"
            with goods_sum as
            (
	            select reference_id as id, sum(amount * sign(operation_summa::numeric)) as balance from balance_goods group by reference_id
            )
            select 
                g.id,
                g.parent_id,
                g.status_id,
                s.note as status_name,
                g.code,
                g.ext_article,
                g.name,
                m.abbreviation,
                g.price,
                g.tax,
                g.min_order,
                g.is_service,
                c.cost,
                c.profit_percent,
                c.profit_value,
                c.price as calc_price,
                ( 
                    select history.changed
                        from history
                        where history.reference_id = c.id and history.to_status_id = 1002
                        order by history.changed desc
                        limit 1
                ) as approved,
				null_if_default(goods_sum.balance) as balance,
				cig.item_id is not null as goods_use
            from goods g
                join status s ON s.id = g.status_id
                left join measurement m on g.measurement_id = m.id
                left join goods_sum on (g.id = goods_sum.id)
                left join calculation c on c.owner_id = g.id and c.status_id = 1002
				left join (select distinct item_id from calc_item_goods where status_id = 1001) cig on (cig.item_id = g.id)
            where {0}";

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.AllowGrouping = true;
            browser.AllowSorting = true;
            browser.DataType = DataType.Directory;

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

                columns.CreateText("code", "Артикул")
                    .SetWidth(140)
                    .SetVisible(false);

                columns.CreateText("name", "Наименование")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateText("ext_article", "Доп. артикул")
                    .SetWidth(120)
                    .SetVisible(false)
                    .SetVisibility(false);

                columns.CreateText("abbreviation", "Ед. изм.")
                    .SetWidth(90)
                    .SetAllowGrouping(true)
                    .SetHorizontalAlignment(HorizontalAlignment.Center)
                    .SetVisibility(false);

                columns.CreateNumeric("price", "Цена", NumberFormatMode.Currency)
                    .SetWidth(100)
                    .SetAllowGrouping(true)
                    .SetHorizontalAlignment(HorizontalAlignment.Right)
                    .SetVisibility(false);

                columns.CreateInteger("tax", "НДС", NumberFormatMode.Percent)
                    .SetWidth(80)
                    .SetAllowGrouping(true)
                    .SetHorizontalAlignment(HorizontalAlignment.Center)
                    .SetVisibility(false);

                columns.CreateNumeric("min_order", "Мин. партия")
                    .SetDecimalDigits(3)
                    .SetWidth(100)
                    .SetAllowGrouping(true)
                    .SetVisible(false)
                    .SetVisibility(false);

                columns.CreateBoolean("is_service", "Услуга")
                    .SetWidth(100)
                    .SetVisibility(false)
                    .SetAllowGrouping(true)
                    .SetHorizontalAlignment(HorizontalAlignment.Center);

                columns.CreateNumeric("cost", "Себестоимость", NumberFormatMode.Currency)
                    .SetWidth(120)
                    .SetVisibility(false)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("profit_percent", "Прибыль, %", NumberFormatMode.Percent)
                    .SetDecimalDigits(2)
                    .SetWidth(110)
                    .SetVisibility(false)
                    .SetHorizontalAlignment(HorizontalAlignment.Center);

                columns.CreateNumeric("profit_value", "Прибыль", NumberFormatMode.Currency)
                    .SetWidth(100)
                    .SetVisibility(false)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("calc_price", "Цена расчётная", NumberFormatMode.Currency)
                    .SetWidth(75)
                    .SetVisible(false)
                    .SetVisibility(false)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateDate("approved", "Дата утв.")
                    .SetWidth(120)
                    .SetVisibility(false);

                columns.CreateNumeric("balance", "Тек. остаток")
					.SetDecimalDigits(3)
                    .SetWidth(120)
                    .SetVisibility(false)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

				columns.CreateBoolean("goods_use", "Используется")
					.SetWidth(120)
                    .SetVisibility(false);

                columns.CreateSortedColumns()
                    .Add("code", ListSortDirection.Ascending);
            });

            browser.ChangeParent += Browser_ChangeParent;
        }

        IEditorCode IDataEditor.CreateEditor() => new GoodsEditor();

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Goods>(string.Format(baseSelect, "g.parent_id is not distinct from :parent_id"), new { parent_id = parameters.ParentId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Goods>(string.Format(baseSelect, "g.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from goods where id = :id", new { id }, transaction);
        }

        private void Browser_ChangeParent(object sender, ChangeParentEventArgs e)
        {
            if (sender is IBrowser browser)
            {
                string root = string.Empty;
                if (browser.Parameters.ParentId.HasValue)
                {
                    Goods g = browser.ExecuteSqlCommand<Goods>("select * from goods where id = :id", new { id = browser.Parameters.ParentId });
                    if (g.parent_id.HasValue)
                        root = browser.ExecuteSqlCommand<string>("select root_code_goods(:id)", new { id = browser.Parameters.ParentId });
                    else
                        root = g.code;
                }

                foreach (string column in new string[] { "is_service", "cost", "profit_percent", "profit_value", "calc_price", "approved" })
                {
                    browser.Columns[column].Visibility = root == "Прд";
                }

                foreach (string column in new string[] { "code", "abbreviation", "price", "tax", "balance", "goods_use" })
                {
                    browser.Columns[column].Visibility = !string.IsNullOrEmpty(root);
                }

                browser.Columns["code"].Visible = !string.IsNullOrEmpty(root);
                browser.Columns["ext_article"].Visibility = !string.IsNullOrEmpty(root) || root == "Мат";
            }
        }
    }

    public class GoodsEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 190;

        public void Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const string folderSelect = "select id, parent_id, name, status_id from goods where status_id = 500 order by name";
            const string measurementSelect = "select id, name from measurement where status_id = 1001 order by name";

            IControl code = editor.CreateTextBox("code", "Артикул")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(160);

            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(360);

            IControl parent = editor.CreateSelectBox("parent_id", "Группа", (c) => { return c.Query<GroupDataItem>(folderSelect); }, showOnlyFolder: true)
                .SetLabelWidth(labelWidth)
                .SetControlWidth(360);

            IControl ext_article = editor.CreateTextBox("ext_article", "Доп. артикул")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(450);

            IControl measurement = editor.CreateComboBox("measurement_id", "Еденица измерения", (conn) => { return conn.Query<NameDataItem>(measurementSelect); })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(450);

            IControl price = editor.CreateCurrency("price", "Цена без НДС")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);

            IControl tax = editor.CreateChoice("tax", "НДС", new Dictionary<int, string>() { 
                [0] = "Без НДС",
                [10] = "10%",
                [20] = "20%" })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);

            IControl min_order = editor.CreateNumeric("min_order", "Мин. заказ")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);

            IControl is_service = editor.CreateCheckBox("is_service", "Услуга")
                .SetLabelWidth(labelWidth);

            editor.Container.Add(new IControl[] {
                code,
                name,
                parent,
                ext_article,
                measurement,
                price,
                tax,
                min_order,
                is_service
            });

            string root = database.ExecuteSqlCommand<string>("select root_code_goods(:oid)", new { editor.Entity.oid });
            if (root == "Прд")
            {
                dependentViewer.AddDependentViewer("view-calculation");
            }

            dependentViewer.AddDependentViewers(new string[] { "view-balance-goods", "view-archive-price" });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select id, parent_id, code, name, ext_article, measurement_id, price, tax, min_order, is_service from goods where id = :id";
            return connection.QuerySingleOrDefault<Goods>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into goods default values returning id";
            return connection.QuerySingle<Guid>(sql, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update goods set code = :code, name = :name, parent_id = :parent_id, ext_article = :ext_article, measurement_id = :measurement_id, price = :price, tax = :tax, min_order = :min_order, is_service = :is_service where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from goods where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }

    }
}
