using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Dapper;
using DocumentFlow.Core;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.InventoryImp
{
    public class Inventory : IDocument
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string document_name { get; protected set; }
        public string user_created { get; protected set; }
        public Guid? employee_id { get; set; }
        public string employee_name { get; protected set; }
        public DateTime doc_date { get; set; }
        public long doc_number { get; set; }
        public string view_number { get; set; }
        public Guid organization_id { get; set; }
        public string organization_name { get; protected set; }
    }

    public class InventoryBrowser : BrowserCodeBase<Inventory>, IBrowserCode
    {
        private const string baseSelect = @"
            select 
                i.id, 
                i.status_id, 
                s.note as status_name, 
                ua.name as user_created, 
                p.name as employee_name, 
                i.doc_date, 
                i.view_number, 
                o.name as organization_name 
            from inventory i 
                join status s on (s.id  = i.status_id) 
                join user_alias ua on (ua.id = i.user_created_id) 
                join organization o on (o.id = i.organization_id) 
                left join employee e on (e.id = i.employee_id) 
                left join person p on (p.id = e.person_id) 
            where {0}";

        public void Initialize(IBrowser browser)
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

                columns.CreateText("view_number", "Номер")
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
                    .Add("view_number");
            });
        }

        public override IEnumerable<Inventory> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Inventory>(GetSelect(), new
            {
                from_date = parameters.DateFrom,
                to_date = parameters.DateTo,
                organization_id = parameters.OrganizationId
            });
        }

        protected override string GetSelect()
        {
            return string.Format(baseSelect, "i.doc_date between :from_date and :to_date and i.organization_id = :organization_id");
        }

        protected override string GetSelectById()
        {
            return string.Format(baseSelect, "i.id = :id");
        }

        public IEditorCode CreateEditor()
        {
            return new InventoryEditor();
        }
    }

    public class InventoryDetail : IDetail
    {
        public long id { get; protected set; }
        public Guid owner_id { get; set; }
        public Guid goods_id { get; set; }
        public string goods_name { get; protected set; }
        public decimal amount { get; set; }
    }

    public class InventoryEditor : EditorCodeBase<Inventory>, IEditorCode
    {
        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const string orgSelect = "select id, name from organization where status_id = 1002";
            const string empSelect = "select e.id, e.status_id, p.name from inventory i join organization org on (org.id = i.organization_id) join employee e on (e.owner_id = org.id) join person p on (p.id = e.person_id) where i.id = :id and (e.status_id = 1002 or e.id = :employee_id)";
            const string gridSelect = "select i.id, i.owner_id, g.name as goods_name, i.amount from inventory_detail i left join goods g on (g.id = i.goods_id) where i.owner_id = :id";

            IContainer container = editor.CreateContainer(32);

            IControl doc_number = editor.CreateTextBox("doc_number", "Номер")
                .SetControlWidth(110)
                .SetLabelAutoSize(true)
                .SetWidth(165)
                .SetDock(DockStyleControl.Left);

            IControl doc_date = editor.CreateDateTimePicker("doc_date", "от")
                .SetLabelWidth(40)
                .SetControlWidth(170)
                .SetLabelTextAlignment(ContentAlignment.TopCenter)
                .SetWidth(210)
                .SetDock(DockStyleControl.Left);

            IControl organization_id = editor.CreateComboBox("organization_id", "Организация", (conn) => { return conn.Query<ComboBoxDataItem>(orgSelect); })
                .SetLabelWidth(150)
                .SetControlWidth(300)
                .SetLabelTextAlignment(ContentAlignment.TopRight);

            container.Add(new IControl[]
            {
                doc_number,
                doc_date,
                organization_id
            });

            IControl employee_id = editor.CreateSelectBox("employee_id", "Исполнитель", (e, c) =>
                {
                    Inventory i = e.Entity as Inventory;
                    return c.Query<GroupDataItem>(empSelect, new { i.id, i.employee_id });
                })
                .SetLabelWidth(120)
                .SetControlWidth(350);

            IControl datagrid = editor.CreateDataGrid("datagrid", (c) => { return c.Query<InventoryDetail>(gridSelect, new { ((IIdentifier)editor.Entity).id }).AsList(); })
                .CreateColumns((columns) =>
                {
                    columns.CreateText("goods_name", "Материал")
                        .SetHideable(false)
                        .SetAutoSizeColumnsMode(SizeColumnsMode.Fill);
                    columns.CreateNumeric("amount", "Количество", decimalDigits: 3)
                        .SetWidth(100)
                        .SetHideable(false)
                        .SetHorizontalAlignment(HorizontalAlignmentText.Right);
                })
                .SetEditor("Номенклатура", new InventoryDetailEditor())
                .SetHeight(350);

            editor.Container.Add(new IControl[] {
                container.AsControl(),
                employee_id,
                datagrid
            });
        }

        protected override string GetSelect()
        {
            return "select id, '№' || view_number || ' от ' || to_char(doc_date, 'DD.MM.YYYY') as document_name, doc_date, doc_number, organization_id, employee_id from inventory where id = :id";
        }

        protected override string GetUpdate(Inventory entity)
        {
            return "update inventory set doc_date = :doc_date, doc_number = :doc_number, employee_id = :employee_id, organization_id = :organization_id where id = :id";
        }

        public override bool GetEnabledValue(string field, string status_name)
        {
            return new string[] { "compiled", "is changing" }.Contains(status_name);
        }
    }

    public class InventoryDetailEditor : EditorCodeBase<InventoryDetail>, IEditorCode
    {
        public void Initialize(IEditor editor, IDependentViewer dependentViewer)
        {
            const int labelWidth = 100;
            const string goodsSelect = "with recursive r as (select id, status_id, name, parent_id from goods where code = 'Мат' union all select g.id, g.status_id, g.name, g.parent_id from goods g join r on (g.parent_id = r.id)) select id, status_id, name, parent_id from r where status_id in (500, 1002) or id = :goods_id order by name";

            IControl goods_id = editor.CreateSelectBox("goods_id", "Материал", (c) => { return c.Query<GroupDataItem>(goodsSelect, new { goods_id = editor["goods_id"].Value }); })
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

        public override TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            InventoryDetail detail = editor.Entity as InventoryDetail;
            return connection.QuerySingle<TId>(GetInsert(),
                new
                {
                    owner_id = parameters.OwnerId,
                    detail.goods_id,
                    detail.amount
                },
                transaction: transaction);
        }

        protected override string GetSelect()
        {
            return "select id, owner_id, goods_id, amount from inventory_detail where id = :id";
        }

        protected override string GetInsert()
        {
            return "insert into inventory_detail (owner_id, goods_id, amount) values (:owner_id, :goods_id, :amount) returning id";
        }

        protected override string GetUpdate(InventoryDetail entity)
        {
            return "update inventory_detail set goods_id = :goods_id, amount = :amount where id = :id";
        }
    }
}
