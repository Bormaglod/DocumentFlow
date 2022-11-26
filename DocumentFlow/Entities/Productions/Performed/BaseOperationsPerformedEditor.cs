//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2022
//
// Версия 2022.9.9
//  - добавлено поле double_rate
// Версия 2022.11.26
//  - параметр autoRefresh метода SetDataSource в классе
//    DataSourceControl был удален. Вместо него используется свойство
//    RefreshMethod этого класса в значении DataRefreshMethod.Immediately
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Productions.Performed;

public class BaseOperationsPerformedEditor : Editor<OperationsPerformed>
{
    private const int headerWidth = 210;

    private class LotOrder : Directory
    {
        public LotOrder(ProductionOrder order)
        {
            id = order.id;
            item_name = $"Заказ №{order.document_number} от {order.document_date:d} ({order.contractor_name})";
            is_folder = true;
        }

        public LotOrder(ProductionLot lot)
        {
            id = lot.id;
            item_name = $"№{lot.document_number} от {lot.document_date:d}";
            parent_id = lot.owner_id;
            good_name = lot.goods_name;
            calculation_name = lot.calculation_name;
            calculation_id = lot.calculation_id;
        }

        public string? good_name { get; }
        public string? calculation_name { get; }
        public Guid calculation_id { get; }
    }

    public BaseOperationsPerformedEditor(IOperationsPerformedRepository repository, IPageManager pageManager, bool nested)
        : base(repository, pageManager) 
    {
        var document_date = new DfDateTimePicker("document_date", "Дата/время", headerWidth, 170)
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd.MM.yyyy HH:mm:ss",
            Required = true
        };

        var lot = new DfDirectorySelectBox<LotOrder>("owner_id", "Производственная партия", headerWidth, 350)
        {
            ReadOnly = nested,
            OpenAction = (t) => pageManager.ShowEditor<IProductionLotEditor>(t.id)
        };

        var goods = new DfTextBox("goods_name", "Изделие", headerWidth, 600) { ReadOnly = true };
        var calculation = new DfTextBox("calculation_name", "Калькуляция", headerWidth, 600) { ReadOnly = true };
        var operations = new DfDirectorySelectBox<CalculationOperation>("operation_id", "Операция", headerWidth, 350) 
        { 
            RemoveEmptyFolders = true,
            RefreshMethod = DataRefreshMethod.Immediately
        };

        var using_material = new DfTextBox("material_name", "Материал (по спецификации)", headerWidth, 350) { ReadOnly = true };
        var replacing_material = new DfDirectorySelectBox<Material>("replacing_material_id", "Использованный материал", headerWidth, 350);
        var quantity = new DfIntegerTextBox<long>("quantity", "Количество", headerWidth, 150);
        var employee = new DfDirectorySelectBox<OurEmployee>("employee_id", "Исполнитель", headerWidth, 350);
        var salary = new DfCurrencyTextBox("salary", "Зарплата", headerWidth, 150);
        var double_rate = new DfToggleButton("double_rate", "Двойная плата", headerWidth);

        lot.SetDataSource(() =>
        {
            var repo_orders = Services.Provider.GetService<IProductionOrderRepository>();
            var repo_lots = Services.Provider.GetService<IProductionLotRepository>();
            if (repo_orders == null || repo_lots == null)
            {
                return null;
            }

            var orders = repo_orders.GetAllValid(callback: query =>
                query
                    .Distinct()
                    .Select("production_order.*")
                    .Select("c.item_name as contractor_name")
                    .Join("contractor as c", "c.id", "production_order.contractor_id")
                    .Join("production_lot as pl", "pl.owner_id", "production_order.id")
                    .WhereTrue("production_order.carried_out")
                    .WhereTrue("pl.carried_out")
                ).Select(x => new LotOrder(x));

            if (orders.Any())
            {
                var lots = repo_lots.GetAllValid(callback: query =>
                    query
                        .Select("production_lot.*")
                        .Select("g.item_name as goods_name")
                        .Select("c.code as calculation_name")
                        .WhereTrue("production_lot.carried_out")
                        .Join("calculation as c", "c.id", "production_lot.calculation_id")
                        .Join("goods as g", "g.id", "c.owner_id")
                    ).Select(x => new LotOrder(x));

                return orders.Union(lots);
            }

            return null;
        });

        lot.ValueChanged += (sender, e) =>
        {
            if (IsCreating && e.NewValue != null)
            {
                operations.SetDataSource(() => GetCalculationOperation(e.NewValue.calculation_id));
            }

            if (e.NewValue != null)
            {
                goods.Value = e.NewValue.good_name;
                calculation.Value = e.NewValue.calculation_name;
            }
            else
            {
                goods.Value = null;
                calculation.Value = null;
            }
        };

        lot.ManualValueChange += (sender, args) =>
        {
            if (args.NewValue != null)
            {
                operations.SetDataSource(() => GetCalculationOperation(args.NewValue.calculation_id));
            }
            else
            {
                operations.DeleteDataSource();
            }
        };

        replacing_material.SetDataSource(() => Services.Provider.GetService<IMaterialRepository>()?.GetAllMaterials());

        operations.SetDataSource(() => IsCreating ? null : GetCalculationOperation(Document.calculation_id));

        operations.ValueChanged += (sender, e) =>
        {
            using_material.Value = e.NewValue?.material_name;
            replacing_material.Enabled = e.NewValue?.material_id != null;
        };

        employee.SetDataSource(() => Services.Provider.GetService<IOurEmployeeRepository>()?.GetAllValid());

        double_rate.ToggleValue = DateTime.Today.DayOfWeek == DayOfWeek.Sunday || DateTime.Today.DayOfWeek == DayOfWeek.Saturday;

        AddControls(new Control[]
        {
            document_date,
            lot,
            goods,
            calculation,
            operations, 
            using_material,
            replacing_material,
            quantity,
            employee,
            salary,
            double_rate
        });
    }

    private static IEnumerable<CalculationOperation>? GetCalculationOperation(Guid calculationId)
    {
        var repo = Services.Provider.GetService<ICalculationOperationRepository>();
        if (repo != null)
        {
            var op = repo.GetAllValid(callback: query =>
                query
                    .Select("calculation_operation.*")
                    .Select("m.item_name as material_name")
                    .LeftJoin("material as m", "m.id", "material_id")
                    .Where("calculation_operation.owner_id", calculationId)
                    .OrderBy("code")
                );

            return op;
        }

        return null;
    }
}
