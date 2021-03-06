using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Core;
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code.Implementation.InventoryImp
{
    public class Inventory : Document
    {
        public Guid? employee_id { get; set; }
        public string employee_name { get; protected set; }
        public string organization_name { get; protected set; }
    }

    public class InventoryDetail : DetailEntity
    {
        public Guid goods_id { get; set; }
        public string goods_name { get; protected set; }
        public decimal amount { get; set; }
    }

    public class InventoryBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelect = @"
            select 
                i.id, 
                i.status_id, 
                s.note as status_name, 
                ua.name as user_created, 
                p.name as employee_name, 
                i.doc_date, 
                i.doc_number, 
                o.name as organization_name 
            from inventory i 
                join status s on (s.id  = i.status_id) 
                join user_alias ua on (ua.id = i.user_created_id) 
                join organization o on (o.id = i.organization_id) 
                left join employee e on (e.id = i.employee_id) 
                left join person p on (p.id = e.person_id) 
            where {0}";

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.AllowGrouping = true;
            browser.DataType = DataType.Document;
            browser.FromDate = DateRanges.FirstYearDay;
            browser.ToDate = DateRanges.LastYearDay;

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

                columns.CreateText("user_created", "Автор")
                    .SetWidth(110)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateDate("doc_date", "Дата/время")
                    .SetWidth(150)
                    .SetHideable(false);

                columns.CreateText("doc_number", "Номер")
                    .SetWidth(150)
                    .SetHideable(false);

                columns.CreateText("organization_name", "Организация")
                    .SetWidth(150)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateText("employee_name", "Сотрудник")
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill)
                    .SetAllowGrouping(true);

                columns.CreateSortedColumns()
                    .Add("doc_date")
                    .Add("doc_number");
            });
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Inventory>(string.Format(baseSelect, "(i.doc_date between :from_date and :to_date and i.organization_id = :organization_id) or (i.status_id not in (1011, 3000))"), new
            {
                from_date = parameters.DateFrom,
                to_date = parameters.DateTo,
                organization_id = parameters.OrganizationId
            }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Inventory>(string.Format(baseSelect, "i.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from inventory where id = :id", new { id }, transaction);
        }

        IEditorCode IDataEditor.CreateEditor() => new InventoryEditor();
    }

    public class InventoryEditor : IEditorCode, IDataOperation, IControlEnabled
    {
        public void Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const string orgSelect = "select id, name from organization where status_id = 1002";
            const string empSelect = "select e.id, e.status_id, p.name from inventory i join organization org on (org.id = i.organization_id) join employee e on (e.owner_id = org.id) join person p on (p.id = e.person_id) where i.id = :id and (e.status_id = 1002 or e.id = :employee_id)";
            const string gridSelect = "select i.id, i.owner_id, g.name as goods_name, i.amount from inventory_detail i left join goods g on (g.id = i.goods_id) where i.owner_id = :oid";

            IContainer container = editor.CreateContainer(32);

            IControl doc_number = editor.CreateTextBox("doc_number", "Номер")
                .SetControlWidth(110)
                .SetLabelAutoSize(true)
                .SetWidth(165)
                .SetDock(DockStyle.Left);

            IControl doc_date = editor.CreateDateTimePicker("doc_date", "от")
                .SetLabelWidth(40)
                .SetControlWidth(170)
                .SetLabelTextAlignment(ContentAlignment.TopCenter)
                .SetWidth(210)
                .SetDock(DockStyle.Left);

            IControl organization_id = editor.CreateComboBox("organization_id", "Организация", (conn) => { return conn.Query<NameDataItem>(orgSelect); })
                .SetLabelWidth(150)
                .SetControlWidth(300)
                .SetLabelTextAlignment(ContentAlignment.TopRight);

            container.Add(new IControl[]
            {
                doc_number,
                doc_date,
                organization_id
            });

            IControl employee_id = editor.CreateSelectBox<Inventory>("employee_id", "Исполнитель", (e, c) => { return c.Query<GroupDataItem>(empSelect, new { e.id, e.employee_id }); })
                .SetLabelWidth(120)
                .SetControlWidth(350);

            IControl datagrid = editor.CreateDataGrid("datagrid", (c) => { return c.Query<InventoryDetail>(gridSelect, new { editor.Entity.oid }).AsList(); })
                .CreateColumns((columns) =>
                {
                    columns.CreateText("goods_name", "Материал")
                        .SetHideable(false)
                        .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);
                    columns.CreateNumeric("amount", "Количество")
                        .SetDecimalDigits(3)
                        .SetWidth(100)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignment.Right);
                })
                .SetEditor("Номенклатура", new InventoryDetailEditor())
                .SetHeight(350);

            editor.Container.Add(new IControl[] {
                container.AsControl(),
                employee_id,
                datagrid
            });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select id, '№' || doc_number || ' от ' || to_char(doc_date, 'DD.MM.YYYY') as document_name, doc_date, doc_number, organization_id, employee_id from inventory where id = :id";
            return connection.QuerySingleOrDefault<Inventory>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into inventory default values returning id";
            return connection.QuerySingle<Guid>(sql, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update inventory set doc_date = :doc_date, doc_number = :doc_number, employee_id = :employee_id, organization_id = :organization_id where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from inventory where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            return new string[] { "compiled", "is changing" }.Contains(info.StatusCode);
        }
    }

    public class InventoryDetailEditor : IEditorCode, IDataOperation
    {
        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const int labelWidth = 100;
            const string goodsSelect = "with recursive r as (select id, status_id, name, parent_id, measurement_id from goods where code = 'Мат' union all select g.id, g.status_id, g.name, g.parent_id, g.measurement_id from goods g join r on (g.parent_id = r.id)) select r.id, r.status_id, r.name || ', ' || m.abbreviation as name, r.parent_id from r left join measurement m on (m.id = r.measurement_id) where r.status_id in (500, 1002) or r.id = :goods_id order by r.name";

            IControl goods_id = editor.CreateSelectBox("goods_id", "Материал", (c) => { return c.Query<GroupDataItem>(goodsSelect, new { goods_id = editor.Data["goods_id"] }); })
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);

            IControl amount = editor.CreateNumeric("amount", "Количество")
                .SetLabelWidth(labelWidth)
                .SetFitToSize(true);

            editor.Container.Add(new IControl[] {
                goods_id,
                amount
            });
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = "select id, owner_id, goods_id, amount from inventory_detail where id = :id";
            return connection.QuerySingleOrDefault<InventoryDetail>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into inventory_detail (owner_id, goods_id, amount) values (:owner_id, :goods_id, :amount) returning id";
            if (editor.Entity is InventoryDetail detail)
            {
                return connection.QuerySingle<long>(sql,
                    new
                    {
                        owner_id = parameters.OwnerId,
                        detail.goods_id,
                        detail.amount
                    },
                    transaction: transaction);
            }

            return default;
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update inventory_detail set goods_id = :goods_id, amount = :amount where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from inventory_detail where id = :id", new { id = id.oid }, transaction);
        }
    }
}
