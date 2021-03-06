using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Core;
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code.Implementation.PerformOperationImp
{
    public class PerformOperation : Document
    {
        public Guid? order_id { get; set; }
        public string order_name { get; protected set; }
        public Guid? goods_id { get; set; }
        public string goods_name { get; protected set; }
        public Guid? operation_id { get; set; }
        public string operation_name { get; protected set; }
        public Guid? employee_id { get; set; }
        public string employee_name { get; protected set; }
        public Guid? using_goods_id { get; set; }
        public string using_goods_name { get; protected set; }
        public Guid? replacing_goods_id { get; set; }
        public string replacing_goods_name { get; protected set; }
        public int amount { get; set; }
        public decimal salary { get; set; }
    }

    public class PerformOperationBrowser : IBrowserCode, IBrowserOperation, IDataEditor
    {
        private const string baseSelect = @"
            select 
                po.id, 
                po.status_id, 
                s.note as status_name, 
                ua.name as user_created, 
                po.doc_date, 
                '№' || p_ord.doc_number || ' от ' || to_char(p_ord.doc_date, 'DD.MM.YYYY') as order_name, 
                g.name as goods_name, 
                o.name as operation_name, 
                p.name as employee_name, 
                ug.name as using_goods_name, 
                case 
                    when ur.name is null then ug.name 
                    else ur.name 
                end as replacing_goods_name, 
                po.amount, 
                po.salary 
            from perform_operation po 
                join status s on (s.id = po.status_id) 
                join user_alias ua on (ua.id = po.user_created_id) 
                left join production_order p_ord on (p_ord.id = po.order_id) 
                left join goods g on (g.id = po.goods_id) 
                left join operation o on (o.id = po.operation_id) 
                left join employee e on (e.id = po.employee_id) 
                left join person p on (p.id = e.person_id) 
                left join goods ug on (ug.id = po.using_goods_id) 
                left join goods ur on (ur.id = po.replacing_goods_id) 
            where {0}";

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.AllowGrouping = true;
            browser.AllowSorting = true;
            browser.DataType = DataType.Document;
            browser.FromDate = DateRanges.CurrentDay;
            browser.ToDate = DateRanges.CurrentDay;

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

                columns.CreateText("order_name", "Заказ")
                    .SetWidth(200)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateText("goods_name", "Изделие")
                    .SetWidth(200)
                    .SetVisible(false)
                    .SetAllowGrouping(true);

                columns.CreateText("operation_name", "Операция")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.Fill)
                    .SetAllowGrouping(true);

                columns.CreateText("employee_name", "Исполнитель")
                    .SetWidth(200)
                    .SetAllowGrouping(true);

                columns.CreateText("replacing_goods_name", "Использованный материал")
                    .SetWidth(270)
                    .SetAllowGrouping(true);

                columns.CreateInteger("amount", "Количество")
                    .SetWidth(140)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateNumeric("salary", "Зарплата", NumberFormatMode.Currency)
                    .SetWidth(120)
                    .SetHorizontalAlignment(HorizontalAlignment.Right);

                columns.CreateGroupSummaryRow()
                    .AddColumn("salary", RowSummaryType.DoubleAggregate, "{Sum:c}");

                columns.CreateTableSummaryRow(GroupVerticalPosition.Bottom)
                    .AddColumn("salary", RowSummaryType.DoubleAggregate, "{Sum:c}");

                columns.CreateSortedColumns()
                    .Add("doc_date", ListSortDirection.Descending);
            });

            browser.CreateGroups()
                .Add("order_name")
                .Add("goods_name");
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<PerformOperation>(string.Format(baseSelect, "po.doc_date between :from_date and :to_date and po.organization_id = :organization_id"), new
            {
                from_date = parameters.DateFrom,
                to_date = parameters.DateTo,
                organization_id = parameters.OrganizationId
            }).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<PerformOperation>(string.Format(baseSelect, "po.id = :id"), new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute("delete from perform_operation where id = :id", new { id }, transaction);
        }

        IEditorCode IDataEditor.CreateEditor() => new PerformOperationEditor();
    }

    public class PerformOperationEditor : IEditorCode, IDataOperation, IControlEnabled, IControlVisible, IActionStatus
    {
        void IEditorCode.Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer)
        {
            const int labelWidth = 210;

            IControl doc_date = editor.CreateDateTimePicker("doc_date", "Дата/время")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(170);

            IControl order_id = editor.CreateSelectBox<PerformOperation>("order_id", "Заказ", GetOrders)
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = database.CreateConnection())
                    {
                        editor.Populates["goods_id"].Populate(conn, editor.Entity);
                    }
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);

            IControl goods_id = editor.CreateSelectBox<PerformOperation>("goods_id", "Изделие", GetGoods)
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = database.CreateConnection())
                    {
                        editor.Populates["operation_id"].Populate(conn, editor.Entity);
                    }
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);

            IControl operation_id = editor.CreateSelectBox<PerformOperation>("operation_id", "Операция", GetOperations)
                .ValueChangedAction((s, e) =>
                {
                    using (var conn = database.CreateConnection())
                    {
                        editor.Populates["using_goods_id"].Populate(conn, editor.Entity);
                    }
                })
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);

            IControl using_goods_id = editor.CreateSelectBox<PerformOperation>("using_goods_id", "Материал (по спецификации)", GetUsingGoods)
                .ValueChangedAction((s, e) => UpdateAmountCheckLabel(editor, database))
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);

            IControl replacing_goods_id = editor.CreateSelectBox<PerformOperation>("replacing_goods_id", "Использованный материал", GetReplacingGoods)
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);

            IContainer amount_panel = editor.CreateContainer(32);

            IControl amount = editor.CreateInteger("amount", "Количество")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150)
                .SetDock(DockStyle.Left);

            IControl label = editor.CreateLabel("amountCheckLabel", string.Empty)
                .SetWidth(200)
                .SetDock(DockStyle.Left);

            amount_panel.Add(new IControl[]
            {
                amount,
                label
            });

            IControl employee_id = editor.CreateSelectBox<PerformOperation>("employee_id", "Исполнитель", GetEmployees)
                .SetLabelWidth(labelWidth)
                .SetControlWidth(350);

            IControl salary = editor.CreateCurrency("salary", "Зарплата")
                .SetLabelWidth(labelWidth)
                .SetControlWidth(150);

            editor.Container.Add(new IControl[] {
				doc_date,
                order_id,
                goods_id,
                operation_id,
                using_goods_id,
                replacing_goods_id,
				amount_panel.AsControl(),
                employee_id,
                salary
            });

            UpdateAmountCheckLabel(editor, database);
        }

        object IDataOperation.Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters)
        {
            string sql = @"
                select 
                    id, 
                    doc_date, 
                    order_id, 
                    goods_id, 
                    operation_id, 
                    amount, 
                    using_goods_id, 
                    replacing_goods_id, 
                    employee_id, 
                    salary
                from perform_operation
                where id = :id";
            return connection.QuerySingleOrDefault<PerformOperation>(sql, new { id = id.oid });
        }

        object IDataOperation.Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            string sql = "insert into perform_operation default values returning id";
            return connection.QuerySingle<Guid>(sql, transaction: transaction);
        }

        int IDataOperation.Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            string sql = "update perform_operation set doc_date = :doc_date, order_id = :order_id, goods_id = :goods_id, operation_id = :operation_id, amount = :amount, using_goods_id = :using_goods_id, replacing_goods_id = :replacing_goods_id, employee_id = :employee_id, salary = :salary where id = :id";
            return connection.Execute(sql, editor.Entity, transaction);
        }

        int IDataOperation.Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id)
        {
            return connection.Execute("delete from perform_operation where id = :id", new { id = id.oid }, transaction);
        }

        bool IControlEnabled.Ability(object entity, string dataName, IInformation info)
        {
            return new string[] { "compiled", "is changing", "corrected" }.Contains(info.StatusCode);
        }

        bool IControlVisible.Ability(object entity, string dataName, IInformation info)
        {
            if (dataName == "amountCheckLabel")
            {
                return new string[] { "compiled", "is changing", "corrected" }.Contains(info.StatusCode);
            }

            return true;
        }

        void IActionStatus.ActionStatusChanged(IValueEditor editor, IDatabase database, IInformation info, ActionStatus actionStatus)
        {
            UpdateAmountCheckLabel(editor, database);
        }

        private IEnumerable<IIdentifier> GetOrders(PerformOperation operation, IDbConnection connection)
        {
            const string orderSelect = "select po.id, po.status_id, '№' || po.doc_number || ' от ' || to_char(po.doc_date, 'DD.MM.YYYY') || ' на сумму ' || sum(pod.cost_with_tax) || ' (' || c.name || ')' as name from production_order po join production_order_detail pod on (pod.owner_id = po.id) join contractor c on (c.id = po.contractor_id) where po.status_id = status_code('in production') or po.id = :order_id group by po.id, c.name order by po.doc_date, po.doc_number";
            return connection.Query<GroupDataItem>(orderSelect, new { operation.order_id });
        }

        private IEnumerable<IIdentifier> GetGoods(PerformOperation operation, IDbConnection connection)
        {
            const string goodsSelect = "select g.id, g.status_id, g.name, g.parent_id from goods g left join production_order_detail pod on (pod.goods_id = g.id) where (pod.owner_id = :order_id and pod.complete_status < 100) or g.status_id = 500";
            return connection.Query<GroupDataItem>(goodsSelect, new { operation.order_id });
        }

        private IEnumerable<IIdentifier> GetOperations(PerformOperation operation, IDbConnection connection)
        {
            const string operationSelect = "select o.id, o.status_id, o.name, o.parent_id from operation o left join calc_item_operation cio on (cio.item_id = o.id) left join calculation c on (cio.owner_id = c.id) left join production_order_detail pod on (c.id = pod.calculation_id) where (pod.owner_id = :order_id and c.owner_id = :goods_id and (o.status_id = 1002 or o.id = :operation_id)) or o.status_id = 500 order by o.status_id, o.name";
            return connection.Query<GroupDataItem>(operationSelect, new { operation.order_id, operation.goods_id, operation.operation_id });
        }

        private IEnumerable<IIdentifier> GetUsingGoods(PerformOperation operation, IDbConnection connection)
        {
            const string usingGoodsSelect = @"
				select 
					gu.id, 
					gu.status_id, 
					gu.name || coalesce(', ' || m.abbreviation, '') as name,
					gu.parent_id 
				from goods gp 
					join production_order_detail pod on (pod.goods_id = gp.id) 
					join calc_item_operation cio on (cio.owner_id = pod.calculation_id) 
					join used_material um on (um.calc_item_operation_id = cio.id) 
					join goods gu on (gu.id = um.goods_id) 
					left join measurement m on (gu.measurement_id = m.id) 
				where (pod.owner_id = :order_id and cio.item_id = :operation_id and gp.id = :goods_id) or (gp.id = :using_goods_id) union select g.id, g.status_id, g.name, g.parent_id from goods g where g.status_id = 500 order by name";
            return connection.Query<GroupDataItem>(usingGoodsSelect, new { operation.order_id, operation.goods_id, operation.operation_id, operation.using_goods_id });
        }

        private IEnumerable<IIdentifier> GetReplacingGoods(PerformOperation operation, IDbConnection connection)
        {
            const string replacingGoodsSelect = @"
				select 
					g.id, 
					g.status_id, 
					g.name || coalesce(', ' || m.abbreviation, '') as name,
					g.parent_id 
				from goods g 
					left join measurement m on (g.measurement_id = m.id) 
				where g.status_id in (500, 1002) order by g.name";
            return connection.Query<GroupDataItem>(replacingGoodsSelect);
        }

        private IEnumerable<IIdentifier> GetEmployees(PerformOperation operation, IDbConnection connection)
        {
            const string empSelect = "select e.id, e.status_id, p.name from perform_operation po join organization org on (org.id = po.organization_id) join employee e on (e.owner_id = org.id) join person p on (p.id = e.person_id) where po.id = :id and (e.status_id = 1002 or e.id = :employee_id)";
            return connection.Query<GroupDataItem>(empSelect, new { operation.id, operation.employee_id });
        }

        private void UpdateAmountCheckLabel(IValueEditor editor, IDatabase database)
        {
            PerformOperation po = editor.Entity as PerformOperation;
            var cnt = database.ExecuteSqlCommand<int>("select get_required_operations(:order_id, :goods_id, :operation_id, :using_goods_id)", po);
            editor.Values["amountCheckLabel"].Value = string.Format("Максимальное значение: {0}", cnt);
        }
    }
}
