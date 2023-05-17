//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2022
//
// Версия 2022.12.3
//  - в функции AsSummary заменён параметр includeDeleted имеющий значение
//    true на options равный SelectOptions.All
// Версия 2023.1.15
//  - изменён критерий отбора заказов
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.5.17
//  - в поле OwnerId (Заказ) добавлена кнопка для просмотра заказа
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Entities.Productions.Processing;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Productions.Returns;

public class ReturnMaterialsEditor : DocumentEditor<ReturnMaterials>, IReturnMaterialsEditor
{
    public ReturnMaterialsEditor(IReturnMaterialsRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        EditorControls
            .AddDirectorySelectBox<Contractor>(x => x.ContractorId, "Контрагент", select =>
                select
                    .SetDataSource(GetContractors)
                    .EnableEditor<IContractorEditor>()
                    .DirectoryChanged(ContractorChanged)
                    .SetHeaderWidth(80)
                    .SetEditorWidth(400))
            .AddDirectorySelectBox<Contract>(x => x.ContractId, "Договор", select =>
                select
                    .SetDataSource(GetContracts)
                    .EnableEditor<IContractEditor>()
                    .DirectoryChanged(ContractChanged)
                    .SetHeaderWidth(80)
                    .SetEditorWidth(400))
            .AddDocumentSelectBox<ProductionOrder>(x => x.OwnerId, "Заказ", select => 
                select
                    .SetDataSource(GetOrders, DataRefreshMethod.OnOpen)
                    .EnableEditor<IProductionOrderEditor>()
                    .CreateColumns(ProductionOrder.CreateGridColumns)
                    .SetHeaderWidth(80)
                    .SetEditorWidth(400))
            .AddDataGrid<ReturnMaterialsRows>(grid =>
                grid
                    .SetRepository<IReturnMaterialsRowsRepository>()
                    .Dialog<IMaterialQuantityDialog>()
                    .AddCommand("Заполнить", Properties.Resources.icons8_incoming_data_16, PopulateCommand)
                    .SetDock(DockStyle.Fill));
    }

    private IEnumerable<ProductionOrder>? GetOrders()
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>(x => x.ContractId);
        var repo = Services.Provider.GetService<IProductionOrderRepository>();
        if (repo != null && contract.SelectedItem != null)
        {
            return repo.GetWithReturnMaterial(contract.SelectedItem);
        }

        return null;
    }

    private IEnumerable<Contractor>? GetContractors()
    {
        var repo = Services.Provider.GetService<IContractorRepository>();
        if (repo != null)
        {
            return repo.GetSuppliers();
        }

        return null;
    }

    private IEnumerable<Contract>? GetContracts()
    {
        var contractor = EditorControls.GetControl<IDirectorySelectBoxControl<Contractor>>(x => x.ContractorId);
        if (contractor.SelectedItem != null)
        {
            var repo = Services.Provider.GetService<IContractRepository>();
            if (repo != null)
            {
                return repo.GetSuppliers(contractor.SelectedItem.Id);
            }
        }

        return null;
    }

    private void ContractorChanged(Contractor? _)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>(x => x.ContractId);
        if (contract is IDataSourceControl<Guid, Contract> contractSource)
        {
            contractSource.RefreshDataSource(Document.ContractId);
        }

        if (contract.SelectedItem != null)
        {
            var order = EditorControls.GetControl<IDocumentSelectBoxControl<ProductionOrder>>();
            if (order is IDataSourceControl<Guid, ProductionOrder> orderSource)
            {
                orderSource.RefreshDataSource(Document.OwnerId);
            }
        }
    }

    private void ContractChanged(Contract? newValue)
    {
        var order = EditorControls.GetControl<IDocumentSelectBoxControl<ProductionOrder>>();
        if (order is IDataSourceControl<Guid, ProductionOrder> orderSource)
        {
            orderSource.RefreshDataSource(Document.OwnerId);
        }
    }

    private void PopulateCommand(IDataGridControl<ReturnMaterialsRows> grid)
    {
        if (MessageBox.Show("Перед заполнением таблица будет очищена. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        {
            return;
        }

        var order = EditorControls.GetControl<IDocumentSelectBoxControl<ProductionOrder>>();
        if (order.SelectedItem != null)
        {
            var repo = Services.Provider.GetService<IWaybillProcessingRepository>();
            if (repo != null)
            {
                var m = repo.GetReturnMaterials(order.SelectedItem);
                grid.Fill(m);
            }
        }
    }
}
