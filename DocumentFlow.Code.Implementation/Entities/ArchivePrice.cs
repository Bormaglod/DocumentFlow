using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.ArchivePriceImp
{
    public class ArchivePrice : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; protected set; }
        public string code { get; set; }
        public string name { get; set; }
        public DateTime? date_from { get; set; }
        public DateTime date_to { get; set; }
        public decimal price_value { get; set; }
        public string user_name { get; set; }
    }

    public class ArchivePriceBrowser : BrowserCodeBase<ArchivePrice>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                ap.id, 
                ap.status_id, 
                s.note as status_name, 
                lead(ap.date_to) over (order by ap.date_to desc) as date_from,
				ap.date_to,
                ap.price_value, 
                u.name as user_name 
            from archive_price ap 
                join user_alias u on (u.id = ap.user_updated_id) 
                join status s on (s.id = ap.status_id)
            where
                {0}";

        public void Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.ToolBar.ButtonStyle = ButtonDisplayStyle.Image;
            browser.ToolBar.IconSize = ButtonIconSize.Small;
            browser.CommandBarVisible = false;
            browser.AllowSorting = false;

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

            columns.CreateDate("date_from", "С даты...")
                .SetHideable(false)
                .SetWidth(150);

            columns.CreateDate("date_to", "По дату...")
                .SetHideable(false)
                .SetWidth(150);

            columns.CreateNumeric("price_value", "Цена", NumberFormatMode.Currency, decimalDigits: 2)
                .SetHideable(false)
                .SetWidth(100);

            columns.CreateText("user_name", "Пользователь")
                .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

            columns.CreateStackedColumns()
                .Add("date_from")
                .Add("date_to")
                .Header("Период действия цены");
        }

        public IEditorCode CreateEditor()
        {
            return new ArchivePriceEditor();
        }

        public override IEnumerable<ArchivePrice> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<ArchivePrice>(GetSelect(), new { owner_id = parameters.OwnerId });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "ap.owner_id = :owner_id and status_id = 1100");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "ap.id = :id");
        }

        protected override string GetDelete()
        {
            return "select delete_archive_price(:id)";
        }
    }

    public class ArchivePriceEditor : EditorCodeBase<ArchivePrice>, IEditorCode
    {
        private const int labelWidth = 180;

        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            IControl date_to = editor.CreateDateTimePicker("date_to", "Дата окончания действия")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(200);
            IControl price_value = editor.CreateCurrency("price_value", "Значение цены")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);

            editor.Container.Add(new IControl[] {
                date_to,
                price_value
            });
        }

        public override TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {

            return connection.QuerySingle<TId>(GetInsert(), new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        protected override string GetSelect()
        {
            return @"select id, date_to, price_value from archive_price where id = :id";
        }

        protected override string GetInsert()
        {
            return "insert into archive_price (owner_id) values (:owner_id) returning id";
        }

        protected override string GetUpdate(ArchivePrice archivePrice)
        {
            return "update archive_price set date_to = :date_to, price_value = :price_value where id = :id";
        }
    }
}