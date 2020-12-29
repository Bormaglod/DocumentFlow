using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.System;
using DocumentFlow.Data;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Implementation.CalculationImp
{
    public class Calculation : Directory
    {
        public string goods_name { get; set; }
        public decimal cost { get; set; }
        public decimal profit_percent { get; set; }
        public decimal profit_value { get; set; }
        public decimal price { get; set; }
        public string note { get; set; }
    }

    public class CalculationBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelect = @"
            select 
                c.id, 
                c.status_id, 
                s.note as status_name, 
                c.code, 
                c.name, 
                c.cost, 
                c.profit_percent, 
                c.profit_value, 
                c.price, 
                c.note 
            from calculation c 
                join status s on (s.id = c.status_id) 
            where {0}";

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
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
                    .SetVisible(false);

                columns.CreateText("status_name", "Состояние")
                    .SetWidth(110)
                    .SetVisible(false);

                columns.CreateText("code", "Код")
                    .SetWidth(150);

                columns.CreateText("name", "Наименование")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateNumeric("cost", "Себестоимость", NumberFormatMode.Currency)
                    .SetWidth(150)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("profit_percent", "Прибыль, %", NumberFormatMode.Percent)
                    .SetDecimalDigits(2)
                    .SetWidth(120)
                    .SetHorizontalAlignment(HorizontalAlignment.Center);

                columns.CreateNumeric("profit_value", "Прибыль", NumberFormatMode.Currency)
                    .SetWidth(150)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("price", "Цена", NumberFormatMode.Currency)
                    .SetWidth(150)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateSortedColumns()
                    .Add("code", ListSortDirection.Ascending);
            });
        }

        IEditorCode IDataEditor.CreateEditor()
        {
            return new CalculationEditor();
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Calculation>(string.Format(baseSelect, "c.owner_id = :owner_id"), new { owner_id = parameters.OwnerId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Calculation>(string.Format(baseSelect, "c.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from calculation where id = :id", new { id }, transaction);
        }
    }

    public class CalculationEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        private const int labelWidth = 130;

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            IControl product = editor.CreateTextBox("goods_name", "Продукция")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400)
                .SetEnabled(false);

            IControl code = editor.CreateTextBox("code", "Код")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(160);

            IControl name = editor.CreateTextBox("name", "Наименование")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400);

            IControl cost = editor.CreateCurrency("cost", "Себестоимость")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(130);

            IControl profit_percent = editor.CreatePercent("profit_percent", "Рентабельность", 2)
                .SetLabelWidth(labelWidth)
                .SetControlWidth(70);

            IControl profit_value = editor.CreateCurrency("profit_value", "Прибыль")
                .SetLabelWidth(labelWidth);

            IControl price = editor.CreateCurrency("price", "Цена")
                .SetLabelWidth(labelWidth);

            IControl note = editor.CreateTextBox("note", "Описание", true)
                .SetLabelWidth(labelWidth)
                .SetControlWidth(400)
                .SetHeight(75);

            editor.Container.Add(new IControl[] {
                product,
                code,
                name,
                cost,
                profit_percent,
                profit_value,
                price,
                note
            });

            dependentViewer.AddDependentViewers(new string[] {
                "view-item-goods",
                "view-item-oper",
                "view-item-deduction"
            });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select c.id, g.name as goods_name, c.code, c.name, c.cost, c.profit_percent, c.profit_value, c.price, c.note from calculation c join goods g on (g.id = c.owner_id) where c.id = :id";
            return connection.QuerySingleOrDefault<Calculation>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into calculation (owner_id) values (:owner_id) returning id";
            return connection.QuerySingle<Guid>(sql, new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update calculation set code = :code, name = :name, cost = :cost, profit_percent = :profit_percent, profit_value = :profit_value, price = :price, note = :note where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from calculation where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            if (dataName == "goods_name")
                return false;

            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }
    }
}
