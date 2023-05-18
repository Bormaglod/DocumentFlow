//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
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
// Версия 2022.12.2
//  - поле double_rate теперь редактируется с помощью DfCheckBox, что бы
//    была возможность установки неопределенного знаяения
//  - для предотвращения ошибки при обращении к свойству Document до
//    его инициализации в поле operations значение свойства RefreshMethod
//    заменено на DataRefreshMethod.OnOpen
//  - удалена инициализпция значения свойства double_rate
// Версия 2022.12.7
//  - добавлена принудительная инициализация using_material - из-за того 
//    что заполнение списка операций (operations) происходит теперь при
//    открытии, то начальное значение это поле получает от простого
//    GetById без доп. информации и соответственно не содержит
//    наименование материала учавствующего в операции
// Версия 2023.3.14
//  - GetAllMaterials заменен на GetAllValid
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Productions.Performed;

public class BaseOperationsPerformedEditor : Editor<OperationsPerformed>
{
    private const int headerWidth = 210;

    private class LotOrder : Directory
    {
        public LotOrder(ProductionOrder order)
        {
            Id = order.Id;
            ItemName = $"Заказ №{order.DocumentNumber} от {order.DocumentDate:d} ({order.ContractorName})";
            IsFolder = true;
        }

        public LotOrder(ProductionLot lot)
        {
            Id = lot.Id;
            ItemName = $"№{lot.DocumentNumber} от {lot.DocumentDate:d}";
            ParentId = lot.OwnerId;
            GoodName = lot.GoodsName;
            CalculationName = lot.CalculationName;
            CalculationId = lot.CalculationId;
        }

        public string? GoodName { get; }
        public string? CalculationName { get; }
        public Guid CalculationId { get; }
    }

    public BaseOperationsPerformedEditor(IOperationsPerformedRepository repository, IPageManager pageManager, bool nested)
        : base(repository, pageManager) 
    {
        EditorControls
            .AddIntergerTextBox(x => x.DocumentNumber, "Номер", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(170)
                    .DefaultAsValue())
            .AddDateTimePicker(x => x.DocumentDate, "Дата/время", date =>
                date
                    .SetCustomFormat("dd.MM.yyyy HH:mm:ss")
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(170))
            .AddDirectorySelectBox<LotOrder>(x => x.OwnerId, "Производственная партия", select =>
                select
                    .ReadOnly(nested)
                    .SetDataSource(GetLotOrders)
                    .EnableEditor<IProductionLotEditor>(true)
                    .DirectoryChanged(LotOrderChanged)
                    .DirectorySelected(LotOrderSelected)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(350))
            .AddTextBox(x => x.GoodsName, "Изделие", text =>
                text
                    .ReadOnly()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(600))
            .AddTextBox(x => x.CalculationName, "Калькуляция", text =>
                text
                    .ReadOnly()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(600))
            .AddDirectorySelectBox<CalculationOperation>(x => x.OperationId, "Операция", select =>
                select
                    .SetDataSource(GetCalculationOperations, DataRefreshMethod.OnOpen)
                    .RemoveEmptyFolders()
                    .DirectoryChanged(OperationChanged)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(350))
            .AddTextBox(x => x.MaterialName, "Материал (по спецификации)", text =>
                text
                    .ReadOnly()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(350))
            .AddDirectorySelectBox<Material>(x => x.ReplacingMaterialId, "Использованный материал", select =>
                select
                    .SetDataSource(GetMaterials)
                    .EnableEditor<IMaterialEditor>()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(350))
            .AddIntergerTextBox<long>(x => x.Quantity, "Количество", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150))
            .AddDirectorySelectBox<OurEmployee>(x => x.EmployeeId, "Исполнитель", select =>
                select
                    .SetDataSource(GetEmployees)
                    .EnableEditor<IOurEmployeeEditor>()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(350))
            .AddCurrencyTextBox(x => x.Salary, "Зарплата", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150))
            .AddCheckBox(x => x.DoubleRate, "Двойная плата", check =>
                check
                    .AllowThreeState()
                    .SetHeaderWidth(headerWidth));
    }

    private static IEnumerable<CalculationOperation>? GetCalculationOperation(Guid calculationId)
    {
        var repo = Services.Provider.GetService<ICalculationOperationRepository>();
        if (repo != null)
        {
            var op = repo.GetListExisting(callback: query =>
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

    private void LotOrderChanged(LotOrder? newValue) 
    {
        if (IsCreating && newValue != null)
        {
            var operations = EditorControls.GetControl<IDirectorySelectBoxControl<CalculationOperation>>(x => x.OperationId);
            operations.SetDataSource(() => GetCalculationOperation(newValue.CalculationId));
        }

        EditorControls.GetControl<ITextBoxControl>(x => x.GoodsName)
                      .SetText(newValue?.GoodName);
        EditorControls.GetControl<ITextBoxControl>(x => x.CalculationName)
                      .SetText(newValue?.CalculationName);
    }

    private void LotOrderSelected(LotOrder? newValue)
    {
        var operations = EditorControls.GetControl<IDirectorySelectBoxControl<CalculationOperation>>(x => x.OperationId);
        if (newValue != null)
        {
            operations.SetDataSource(() => GetCalculationOperation(newValue.CalculationId));
        }
        else
        {
            if (operations is IDataSourceControl source)
            {
                source.RemoveDataSource();
            }
        }
    }

    private void OperationChanged(CalculationOperation? newValue)
    {
        var using_material = EditorControls.GetControl<ITextBoxControl>(x => x.MaterialName);
        if (newValue?.MaterialName == null && newValue?.MaterialId != null)
        {
            var repo = Services.Provider.GetService<IMaterialRepository>();
            using_material.SetText(repo!.Get(newValue.MaterialId.Value, false).ItemName);
        }
        else
        {
            using_material.SetText(newValue?.MaterialName);
        }

        EditorControls.GetControl(x => x.ReplacingMaterialId)
                      .SetEnabled(newValue?.MaterialId != null);
    }

    private IEnumerable<CalculationOperation>? GetCalculationOperations()
    {
        return IsCreating ? null : GetCalculationOperation(Document.CalculationId);
    }

    private IEnumerable<LotOrder>? GetLotOrders()
    {
        var repo_orders = Services.Provider.GetService<IProductionOrderRepository>();
        var repo_lots = Services.Provider.GetService<IProductionLotRepository>();
        if (repo_orders == null || repo_lots == null)
        {
            return null;
        }

        var orders = repo_orders.GetListExisting(callback: query =>
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
            var lots = repo_lots.GetListExisting(callback: query =>
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
    }

    private IEnumerable<OurEmployee> GetEmployees() => Services.Provider.GetService<IOurEmployeeRepository>()!.GetListExisting();

    private IEnumerable<Material> GetMaterials() => Services.Provider.GetService<IMaterialRepository>()!.GetListExisting();
}
