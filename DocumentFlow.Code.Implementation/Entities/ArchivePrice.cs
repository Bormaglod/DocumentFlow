using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code.System;
using DocumentFlow.Data;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Implementation.ArchivePriceImp
{
    public class ArchivePrice : Directory
    {
        public DateTime? date_from { get; set; }
        public DateTime date_to { get; set; }
        public decimal price_value { get; set; }
        public string user_name { get; set; }
    }

    public class ArchivePriceBrowser : IBrowserCode, IBrowserOperation, IDataEditor
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

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.ToolBar.ButtonStyle = ToolStripItemDisplayStyle.Image;
            browser.ToolBar.IconSize = ButtonIconSize.Small;
            browser.CommandBarVisible = false;
            browser.AllowSorting = false;

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

                columns.CreateDate("date_from", "С даты...")
                    .SetHideable(false)
                    .SetWidth(150);

                columns.CreateDate("date_to", "По дату...")
                    .SetHideable(false)
                    .SetWidth(150);

                columns.CreateNumeric("price_value", "Цена", NumberFormatMode.Currency)
                    .SetHideable(false)
                    .SetWidth(100);

                columns.CreateText("user_name", "Пользователь")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);

                columns.CreateStackedColumns()
                    .Add("date_from")
                    .Add("date_to")
                    .Header("Период действия цены");
            });
        }

        IEditorCode IDataEditor.CreateEditor() => new ArchivePriceEditor();

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<ArchivePrice>(string.Format(baseSelect, "ap.owner_id = :owner_id and status_id = 1100"), new { owner_id = parameters.OwnerId }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<ArchivePrice>(string.Format(baseSelect, "ap.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("select delete_archive_price(:id)", new { id }, transaction);
        }
    }

    public class ArchivePriceEditor : IEditorCode, IDataOperation
    {
        private const int labelWidth = 180;

        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
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

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select id, date_to, price_value from archive_price where id = :id";
            return connection.QuerySingleOrDefault<ArchivePrice>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into archive_price (owner_id) values (:owner_id) returning id";
            return connection.QuerySingle<Guid>(sql, new { owner_id = parameters.OwnerId }, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update archive_price set date_to = :date_to, price_value = :price_value where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            string sql = "delete from archive_price where id = :id";
            return connection.Execute(sql, new { id = id.oid }, transaction);
        }
    }
}