//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Entities.Productions.Processing;
using DocumentFlow.Entities.Productions.Returns.Dialogs;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Productions.Returns;

public class ReturnMaterialsEditor : DocumentEditor<ReturnMaterials>, IReturnMaterialsEditor
{
    public ReturnMaterialsEditor(IReturnMaterialsRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        var contractor = new DfDirectorySelectBox<Contractor>("contractor_id", "Контрагент", 80, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractorEditor, Contractor>(t)
        };

        var contract = new DfDirectorySelectBox<Contract>("contract_id", "Договор", 80, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractEditor, Contract>(t)
        };

        var order = new DfDocumentSelectBox<ProductionOrder>("owner_id", "Заказ", 80, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IProductionOrderEditor, ProductionOrder>(t)
        };

        order.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IProductionOrderRepository>();
            if (repo != null && contractor.SelectedItem != null && contract.SelectedItem != null)
            {
                return repo!.GetAllDefault(callback: q => q
                    .WhereFalse("production_order.deleted")
                    .WhereTrue("carried_out")
                    .WhereFalse("closed")
                    .Where("production_order.contractor_id", contractor.SelectedItem.id)
                    .Where("production_order.contract_id", contract.SelectedItem.id));
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
            contract.Value = Document.contract_id;

            if (contract.Value != null)
            {
                order.RefreshDataSource();
                order.Value = Document.owner_id;
            }
        };

        contractor.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IContractorRepository>();
            if (repo != null)
            {
                return repo.GetCustomers();
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
                    return repo.GetCustomers(contractor.SelectedItem.id);
                }
            }

            return null;
        });

        contract.ValueChanged += (sender, e) =>
        {
            if (e.NewValue != e.OldValue)
            {
                order.RefreshDataSource();
                order.Value = Document.owner_id;
            }
        };

        details.CreateTableSummaryRow(VerticalPosition.Bottom)
            .AsSummary("product_cost", SummaryColumnFormat.Currency, true)
            .AsSummary("tax_value", SummaryColumnFormat.Currency, true)
            .AsSummary("full_cost", SummaryColumnFormat.Currency, true);

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
