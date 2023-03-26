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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Entities.Productions.Processing;
using DocumentFlow.Entities.Productions.Returns.Dialogs;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Productions.Returns;

public class ReturnMaterialsEditor : DocumentEditor<ReturnMaterials>, IReturnMaterialsEditor
{
    public ReturnMaterialsEditor(IReturnMaterialsRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        var contractor = CreateDirectorySelectBox<Contractor, IContractorEditor>(x => x.ContractorId, "Контрагент", 80, 400);
        var contract = CreateDirectorySelectBox<Contract, IContractEditor>(x => x.ContractId, "Договор", 80, 400);
        var order = CreateDocumentSelectBox<ProductionOrder, IProductionOrderEditor>(x => x.OwnerId, "Заказ", 80, 400, refreshMethod: DataRefreshMethod.OnOpen);

        order.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IProductionOrderRepository>();
            if (repo != null && contract.SelectedItem != null)
            {
                return repo.GetWithReturnMaterial(contract.SelectedItem);
            }

            return null;
        });

        order.Columns += (sender, e) => ProductionOrder.CreateGridColumns(e.Columns);

        var rowRepo = Services.Provider.GetService<IReturnMaterialsRowsRepository>()!;
        var details = new DfDataGrid<ReturnMaterialsRows>(rowRepo) { Dock = DockStyle.Fill };

        details.AddCommand("Заполнить", Properties.Resources.icons8_incoming_data_16, (sender, e) =>
        {
            if (MessageBox.Show("Перед заполнением таблица будет очищена. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (order.SelectedItem != null)
            {
                var repo = Services.Provider.GetService<IWaybillProcessingRepository>();
                if (repo != null)
                {
                    var m = repo.GetReturnMaterials(order.SelectedItem);
                    details.Fill(m);
                }
            }
        });

        contractor.ValueChanged += (sender, e) =>
        {
            contract.RefreshDataSource();
            contract.Value = Document.ContractId;

            if (contract.Value != null)
            {
                order.RefreshDataSource();
                order.Value = Document.OwnerId;
            }
        };

        contractor.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IContractorRepository>();
            if (repo != null)
            {
                return repo.GetSuppliers();
            }

            return null;
        });

        contract.SetDataSource(() =>
        {
            if (contractor.SelectedItem != null)
            {
                var repo = Services.Provider.GetService<IContractRepository>();
                if (repo != null)
                {
                    return repo.GetSuppliers(contractor.SelectedItem.Id);
                }
            }

            return null;
        });

        contract.ValueChanged += (sender, e) =>
        {
            if (e.NewValue != e.OldValue)
            {
                order.RefreshDataSource();
                order.Value = Document.OwnerId;
            }
        };

        details.CreateTableSummaryRow(VerticalPosition.Bottom)
            .AsSummary("product_cost", SummaryColumnFormat.Currency, SelectOptions.All)
            .AsSummary("tax_value", SummaryColumnFormat.Currency, SelectOptions.All)
            .AsSummary("full_cost", SummaryColumnFormat.Currency, SelectOptions.All);

        details.AutoGeneratingColumn += (sender, args) =>
        {
            switch (args.Column.MappingName)
            {
                case "id":
                    args.Cancel = true;
                    break;
                case "material_name":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                    break;
                case "quantity":
                    args.Column.Width = 100;
                    break;
            }
        };

        details.DataCreate += (sender, args) =>
        {
            args.Cancel = FormMaterialQuantity.Create(args.CreatingData) == DialogResult.Cancel;
        };

        details.DataEdit += (sender, args) =>
        {
            args.Cancel = FormMaterialQuantity.Edit(args.EditingData) == DialogResult.Cancel;
        };

        details.DataCopy += (sender, args) =>
        {
            args.Cancel = FormMaterialQuantity.Edit(args.CopiedData) == DialogResult.Cancel;
        };

        AddControls(new Control[]
        {
            contractor,
            contract,
            order,
            details
        });
    }
}
