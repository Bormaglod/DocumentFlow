//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//
// Весрия 2022.8.12
//  - из-за появления в классе ProductPrice поля code, содержащего
//    артикул, внесено изменение для подавления вывода этого столбца
//    поскольку оно частично дублируется полем calculation_name
// Версия 2022.12.3
//  - в функции AsSummary заменён параметр includeDeleted имеющий значение
//    true на options равный SelectOptions.All
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Dialogs;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Productions.Order;

public class ProductionOrderEditor : DocumentEditor<ProductionOrder>, IProductionOrderEditor
{
    public ProductionOrderEditor(IProductionOrderRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        EditorControls
            .AddDirectorySelectBox<Contractor>(x => x.ContractorId, "Контрагент", select =>
                select
                    .SetDataSource(GetCustomers)
                    .EnableEditor<IContractorEditor>()
                    .DirectoryChanged(ContractorChanged)
                    .SetHeaderWidth(80)
                    .SetEditorWidth(400))
            .AddDirectorySelectBox<Contract>(x => x.ContractId, "Договор", select =>
                select
                    .SetDataSource(GetContracts)
                    .EnableEditor<IContractEditor>()
                    .SetHeaderWidth(80)
                    .SetEditorWidth(400))
            .AddDataGrid<ProductionOrderPrice>(grid =>
                grid
                    .SetRepository<IProductionOrderPriceRepository>()
                    .GridSummaryRow(VerticalPosition.Bottom, row =>
                        row
                            .AsSummary(x => x.ProductCost, SummaryColumnFormat.Currency, SelectOptions.All)
                            .AsSummary(x => x.TaxValue, SummaryColumnFormat.Currency, SelectOptions.All)
                            .AsSummary(x => x.FullCost, SummaryColumnFormat.Currency, SelectOptions.All))
                    .AutoGeneratingColumn(AutoGenerateColumn)
                    .DoCreate(DataCreate)
                    .DoUpdate(DataUpdate)
                    .DoCopy(DataCopy)
                    .SetDock(DockStyle.Fill));
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IProductionLotNestedBrowser, ProductionLot>();
    }

    private bool DataCreate(ProductionOrderPrice data)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>();
        ProductionOrderPriceDialog form = new(contract.SelectedItem);
        return form.Create(data);
    }

    private DataOperationResult DataUpdate(ProductionOrderPrice data) 
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>();
        ProductionOrderPriceDialog form = new(contract.SelectedItem);
        if (form.Edit(data))
        {
            return DataOperationResult.Update;
        }
        else
        {
            return DataOperationResult.Cancel;
        }
    }

    private bool DataCopy(ProductionOrderPrice data)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>();
        ProductionOrderPriceDialog form = new(contract.SelectedItem);
        return form.Edit(data);
    }

    private bool AutoGenerateColumn(GridColumn column)
    {
        return column.MappingName switch
        {
            "Code" => true,
            _ => false,
        };
    }

    private void ContractorChanged(Contractor? _)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>();
        if (contract is IDataSourceControl<Guid, Contract> source)
        {
            
            source.RefreshDataSource(Document?.ContractId ?? contract.Items.FirstOrDefault(x => x.IsDefault)?.Id);
        }
    }

    private IEnumerable<Contract>? GetContracts()
    {
        var contractor = EditorControls.GetControl<IDirectorySelectBoxControl<Contractor>>();
        if (contractor.SelectedItem != null)
        {
            var repo = Services.Provider.GetService<IContractRepository>();
            return repo!.GetCustomers(contractor.SelectedItem.Id);
        }

        return null;
    }

    private IEnumerable<Contractor> GetCustomers() => Services.Provider.GetService<IContractorRepository>()!.GetCustomers();
}
