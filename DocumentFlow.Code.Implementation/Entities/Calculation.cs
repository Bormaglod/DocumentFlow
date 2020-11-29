using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.CalculationImp
{
    public class Calculation : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string goods_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public decimal cost { get; set; }
        public decimal profit_percent { get; set; }
        public decimal profit_value { get; set; }
        public decimal price { get; set; }
        public string note { get; set; }
    }

    public class CalculationBrowser : BrowserCodeBase<Calculation>, IBrowserCode
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

        public void Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.ToolBar.ButtonStyle = ButtonDisplayStyle.Image;
            browser.ToolBar.IconSize = ButtonIconSize.Small;
            browser.CommandBarVisible = false;

            browser.CreateStatusColumnRenderer();

            IColumnCollection columns = browser.Columns;

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

            columns.CreateNumeric("cost", "Себестоимость", NumberFormatMode.Currency, 2)
                .SetWidth(150)
                .SetHorizontalAlignment(HorizontalAlignmentText.Right);

            columns.CreateNumeric("profit_percent", "Прибыль, %", NumberFormatMode.Percent, 2)
                .SetWidth(120)
                .SetHorizontalAlignment(HorizontalAlignmentText.Center);

            columns.CreateNumeric("profit_value", "Прибыль", NumberFormatMode.Currency, 2)
                .SetWidth(150)
                .SetHorizontalAlignment(HorizontalAlignmentText.Right);

            columns.CreateNumeric("price", "Цена", NumberFormatMode.Currency)
                .SetWidth(150)
                .SetHorizontalAlignment(HorizontalAlignmentText.Right);

            columns.CreateSortedColumns()
                .Add("code", SortDirection.Ascending);
        }

        public IEditorCode CreateEditor()
        {
            return new CalculationEditor();
        }

        public override IEnumerable<Calculation> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Calculation>(GetSelect(), new { owner_id = parameters.OwnerId });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "c.owner_id = :owner_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "c.id = :id");
        }
    }

    public class CalculationEditor : EditorCodeBase<Calculation>, IEditorCode
    {
        private const int labelWidth = 130;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
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

        public override TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            return connection.QuerySingle<TId>(GetInsert(), new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        protected override string GetSelect()
        {
            return "select c.id, g.name as goods_name, c.code, c.name, c.cost, c.profit_percent, c.profit_value, c.price, c.note from calculation c join goods g on (g.id = c.owner_id) where c.id = :id";
        }

        protected override string GetInsert()
        {
            return "insert into calculation (owner_id) values (:owner_id) returning id";
        }

        protected override string GetUpdate(Calculation calculation)
        {
            return "update calculation set code = :code, name = :name, cost = :cost, profit_percent = :profit_percent, profit_value = :profit_value, price = :price, note = :note where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            if (field == "goods_name")
                return false;

            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }
    }
}
