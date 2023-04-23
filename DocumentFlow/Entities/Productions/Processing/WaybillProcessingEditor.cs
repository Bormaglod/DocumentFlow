//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//
// Версия 2023.1.15
//  - изменено наследование с WaybillEditor на DocumentEditor
//  - из табличной части удалены колонки с ценой
//  - таблица теперь корректно размещается в окне
// Версия 2023.1.21
//  - в табличную часть добавлена кнопка "Заполнить"
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Dialogs;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Productions.Processing;

public class WaybillProcessingEditor : DocumentEditor<WaybillProcessing>, IWaybillProcessingEditor
{
    public WaybillProcessingEditor(IWaybillProcessingRepository repository, IPageManager pageManager) : base(repository, pageManager, true)
    {
        EditorControls
            .AddDirectorySelectBox<Contractor>(x => x.ContractorId, "Контрагент", select =>
                select
                    .SetDataSource(GetContractors)
                    .EnableEditor<IContractorEditor>()
                    .DirectoryChanged(ContractorChanged)
                    .SetHeaderWidth(120)
                    .SetEditorWidth(400))
            .AddDirectorySelectBox<Contract>(x => x.ContractId, "Договор", select =>
                select
                    .SetDataSource(GetContracts)
                    .EnableEditor<IContractEditor>()
                    .DirectoryChanged(ContractChanged)
                    .SetHeaderWidth(120)
                    .SetEditorWidth(400))
            .AddDocumentSelectBox<ProductionOrder>(x => x.OwnerId, "Заказ", select =>
                select
                    .SetDataSource(GetOrders)
                    .CreateColumns(ProductionOrder.CreateGridColumns)
                    .EnableEditor<IProductionOrderEditor>()
                    .SetHeaderWidth(120)
                    .SetEditorWidth(400))
            .AddPanel(panel =>
                panel
                    .SetName("Doc1C")
                    .ShowHeader("Докуметы")
                    .SetHeight(80)
                    .SetDock(DockStyle.Bottom)
                    .SetVisible(false)
                    .AddControls(controls =>
                        controls
                            .AddTextBox(x => x.WaybillNumber, "Накладная №", text =>
                                text
                                    .SetHeaderWidth(110)
                                    .SetEditorWidth(120)
                                    .SetDock(DockStyle.Left)
                                    .SetWidth(235))
                            .AddDateTimePicker(x => x.WaybillDate, "от", text =>
                                text
                                    .SetFormat(DateTimePickerFormat.Short)
                                    .SetHeaderWidth(25)
                                    .SetEditorWidth(170)
                                    .SetDock(DockStyle.Left)
                                    .SetWidth(200))))
            .AddDataGrid<WaybillProcessingPrice>(grid =>
                grid
                    .SetRepository<IWaybillProcessingPriceRepository>()
                    .AutoGeneratingColumn(AutoGenerateColumn)
                    .DoCreate(DataCreate)
                    .DoUpdate(DataUpdate)
                    .DoCopy(DataCopy)
                    .AddCommand("Заполнить", Properties.Resources.icons8_incoming_data_16, PopulateCommand)
                    .SetDock(DockStyle.Fill));
    }

    private void PopulateCommand(IDataGridControl<WaybillProcessingPrice> grid)
    {
        var order = EditorControls.GetControl<IDocumentSelectBoxControl<ProductionOrder>>();

        if (order.SelectedItem != null)
        {
            var repo = Services.Provider.GetService<IProductionOrderRepository>();
            if (repo != null)
            {
                var rows = repo.GetOnlyGivingMaterials<WaybillProcessingPrice>(order.SelectedItem);
                grid.Fill(rows);
            }
        }
    }

    private bool DataCreate(WaybillProcessingPrice data)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>(x => x.ContractId);
        ProductPriceDialog<WaybillProcessingPrice> form = new(contract.SelectedItem);
        return form.Create(data);
    }

    private DataOperationResult DataUpdate(WaybillProcessingPrice data)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>(x => x.ContractId);
        ProductPriceDialog<WaybillProcessingPrice> form = new(contract.SelectedItem);
        if (form.Edit(data))
        {
            return DataOperationResult.Update;
        }
        else
        {
            return DataOperationResult.Cancel;
        }
    }

    private bool DataCopy(WaybillProcessingPrice data)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>(x => x.ContractId);
        ProductPriceDialog<WaybillProcessingPrice> form = new(contract.SelectedItem);
        return form.Edit(data);
    }

    private bool AutoGenerateColumn(GridColumn column)
    {
        switch (column.MappingName)
        {
            case "ProductName":
                column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
            case "Code":
                column.AutoSizeColumnsMode = AutoSizeColumnsMode.AllCells;
                break;
            case "Amount":
                column.Width = 100;
                break;
            case "Id":
            case "Price":
            case "ProductCost":
            case "Tax":
            case "TaxValue":
            case "FullCost":
                return true;
        }

        return false;
    }

    private IEnumerable<ProductionOrder> GetOrders()
    {
        var repo = Services.Provider.GetService<IProductionOrderRepository>();
        return repo!.GetAllDefault(callback: q => q
            .WhereFalse("production_order.deleted")
            .WhereTrue("carried_out")
            .WhereFalse("closed"));
    }

    private IEnumerable<Contractor> GetContractors() => Services.Provider.GetService<IContractorRepository>()!.GetSuppliers();

    private IEnumerable<Contract>? GetContracts()
    {
        var contractor = EditorControls.GetControl<IDirectorySelectBoxControl<Contractor>>(x => x.ContractorId);
        if (contractor.SelectedItem != null)
        {
            return Services.Provider.GetService<IContractRepository>()?.GetSuppliers(contractor.SelectedItem.Id);
        }

        return null;
    }

    private void ContractorChanged(Contractor? _)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>(x => x.ContractId);
        if (contract is IDataSourceControl<Guid, Contract> source)
        {
            source.RefreshDataSource(Document.ContractId);
        }
    }

    private void ContractChanged(Contract? newValue)
    {
        var doc1c = EditorControls.GetContainer("Doc1C");
        doc1c.SetVisible(newValue != null);
    }
}
