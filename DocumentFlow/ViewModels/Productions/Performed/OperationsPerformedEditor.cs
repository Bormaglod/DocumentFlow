//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.08.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

using System.Data;

namespace DocumentFlow.ViewModels;

[Entity(typeof(OperationsPerformed), RepositoryType = typeof(IOperationsPerformedRepository))]
public partial class OperationsPerformedEditor : EditorPanel, IOperationsPerformedEditor, IDocumentEditor
{
    private class LotOrder : Data.Directory
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
            ProductName = lot.GoodsName;
            CalculationName = lot.CalculationName;
            CalculationId = lot.CalculationId;
        }

        public string? ProductName { get; }
        public string? CalculationName { get; }
        public Guid CalculationId { get; }
    }

    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public OperationsPerformedEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;
    }

    public Guid? OwnerId
    {
        get => Performed.OwnerId;
        set => Performed.OwnerId = value;
    }

    protected OperationsPerformed Performed { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Performed.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(OperationsPerformed.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(OperationsPerformed.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        selectLot.DataBindings.Add(nameof(selectLot.SelectedItem), DataContext, nameof(OperationsPerformed.OwnerId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textProduct.DataBindings.Add(nameof(textProduct.TextValue), DataContext, nameof(OperationsPerformed.GoodsName), false, DataSourceUpdateMode.Never);
        textCalc.DataBindings.Add(nameof(textCalc.TextValue), DataContext, nameof(OperationsPerformed.CalculationName), false, DataSourceUpdateMode.Never);
        selectOper.DataBindings.Add(nameof(selectOper.SelectedItem), DataContext, nameof(OperationsPerformed.OperationId), false, DataSourceUpdateMode.OnPropertyChanged);
        textMaterialSpec.DataBindings.Add(nameof(textMaterialSpec.TextValue), DataContext, nameof(OperationsPerformed.MaterialName), false, DataSourceUpdateMode.Never);
        selectMaterial.DataBindings.Add(nameof(selectMaterial.SelectedItem), DataContext, nameof(OperationsPerformed.ReplacingMaterialId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textQuantity.DataBindings.Add(nameof(textQuantity.IntegerValue), DataContext, nameof(OperationsPerformed.Quantity), false, DataSourceUpdateMode.OnPropertyChanged);
        selectEmp.DataBindings.Add(nameof(selectEmp.SelectedItem), DataContext, nameof(OperationsPerformed.EmployeeId), false, DataSourceUpdateMode.OnPropertyChanged);
        textSalary.DataBindings.Add(nameof(textSalary.DecimalValue), DataContext, nameof(OperationsPerformed.Salary), false, DataSourceUpdateMode.OnPropertyChanged);
        checkDoubleRate.DataBindings.Add(nameof(checkDoubleRate.CheckValue), DataContext, nameof(OperationsPerformed.DoubleRate), true, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        selectLot.DataSource = GetLotOrders();
        selectOper.DataSource = GetCalculationOperation(Performed.CalculationId);
        selectMaterial.DataSource = services.GetRequiredService<IMaterialRepository>().GetListExisting();
        selectEmp.DataSource = services.GetRequiredService<IOurEmployeeRepository>().GetListExisting();
    }

    private IEnumerable<LotOrder>? GetLotOrders()
    {
        var orders = services
            .GetRequiredService<IProductionOrderRepository>()
            .GetListExisting(callback: query => query
                .Distinct()
                .Select("production_order.*")
                .Select("c.item_name as contractor_name")
                .Join("contractor as c", "c.id", "production_order.contractor_id")
                .Join("production_lot as pl", "pl.owner_id", "production_order.id")
                .WhereTrue("production_order.carried_out")
                .WhereTrue("pl.carried_out"))
            .Select(x => new LotOrder(x));

        if (orders.Any())
        {
            var lots = services
                .GetRequiredService<IProductionLotRepository>()
                .GetListExisting(callback: query => query
                    .Select("production_lot.*")
                    .Select("g.item_name as goods_name")
                    .Select("c.code as calculation_name")
                    .WhereTrue("production_lot.carried_out")
                    .Join("calculation as c", "c.id", "production_lot.calculation_id")
                    .Join("goods as g", "g.id", "c.owner_id"))
                .Select(x => new LotOrder(x));

            return orders.Union(lots);
        }

        return null;
    }

    private IEnumerable<CalculationOperation>? GetCalculationOperation(Guid calculationId)
    {
        return services
            .GetRequiredService<ICalculationOperationRepository>()
            .GetListExisting(callback: query => query
                .Select("calculation_operation.*")
                .Select("m.item_name as material_name")
                .SelectRaw("calculation_operation.tableoid::regclass::varchar = 'calculation_cutting' as is_cutting")
                .LeftJoin("material as m", "m.id", "material_id")
                .Where("calculation_operation.owner_id", calculationId)
                .OrderBy("code"));
    }

    private void UpdateLotInfo(LotOrder lot)
    {
        Performed.GoodsName = lot.ProductName ?? string.Empty;
        Performed.CalculationName = lot.CalculationName ?? string.Empty;
    }

    private void ClearLotInfo()
    {
        Performed.GoodsName = string.Empty;
        Performed.CalculationName = string.Empty;
    }

    private void SelectLot_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IProductionLotEditor>(e.Document);
    }

    private void SelectLot_SelectedItemChanged(object sender, EventArgs e)
    {
        if (selectLot.SelectedItem != Guid.Empty)
        {
            UpdateLotInfo((LotOrder)selectLot.SelectedDocument);
        }
        else
        {
            ClearLotInfo();
        }
    }

    private void SelectOper_SelectedItemChanged(object sender, EventArgs e)
    {
        if (selectOper.SelectedItem != Guid.Empty)
        {
            var operation = (CalculationOperation)selectOper.SelectedDocument;
            string materialName = string.Empty;
            if (string.IsNullOrEmpty(operation.MaterialName))
            {
                if (operation.MaterialId != null)
                {
                    materialName = services.GetRequiredService<IMaterialRepository>().Get(operation.MaterialId.Value, false).ToString();
                }
            }
            else
            {
                materialName = operation.MaterialName;
            }

            Performed.MaterialName = materialName;
            selectMaterial.EnabledEditor = operation.MaterialId != null;
        }
        else
        {
            Performed.MaterialName = string.Empty;
            selectMaterial.EnabledEditor = false;
        }
    }

    private void SelectLot_DocumentSelectedChanged(object sender, DocumentChangedEventArgs e)
    {
        var lot = (LotOrder)e.NewDocument;
        selectOper.DataSource = GetCalculationOperation(lot.CalculationId);
    }

    private void SelectLot_DeleteButtonClick(object sender, EventArgs e) => selectOper.DataSource = null;

    private void SelectOper_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        var oper = (CalculationOperation)e.Document;
        if (oper.IsCutting)
        {
            pageManager.ShowEditor<ICalculationCuttingEditor>(e.Document);
        }
        else
        {
            pageManager.ShowEditor<ICalculationOperationEditor>(e.Document);
        }
    }

    private void SelectMaterial_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IMaterialEditor>(e.Document);
    }

    private void SelectEmp_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IOurEmployeeEditor>(e.Document);
    }
}
