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

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Entities.Products.Dialogs;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Productions.Processing;

public class WaybillProcessingEditor : DocumentEditor<WaybillProcessing>, IWaybillProcessingEditor
{
    public WaybillProcessingEditor(IWaybillProcessingRepository repository, IPageManager pageManager) : base(repository, pageManager, true)
    {
        var contractor = new DfDirectorySelectBox<Contractor>("contractor_id", "Контрагент", 120, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractorEditor, Contractor>(t)
        };

        var contract = new DfDirectorySelectBox<Contract>("contract_id", "Договор", 120, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractEditor, Contract>(t)
        };

        var order = new DfDocumentSelectBox<ProductionOrder>("owner_id", "Заказ", 120, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IProductionOrderEditor, ProductionOrder>(t)
        };

        order.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IProductionOrderRepository>();
            return repo!.GetAllDefault(callback: q => q
                .WhereFalse("production_order.deleted")
                .WhereTrue("carried_out")
                .WhereFalse("closed"));
        });

        order.Columns += (sender, e) => ProductionOrder.CreateGridColumns(e.Columns);

        var waybill_number = new DfTextBox("waybill_number", "Накладная №", 110, 120) { Dock = DockStyle.Left, Width = 235 };
        var waybill_date = new DfDateTimePicker("waybill_date", "от", 25, 170) { Dock = DockStyle.Left, Width = 200, Format = DateTimePickerFormat.Short };
        var waybill_panel = CreatePanel(new Control[] { waybill_date, waybill_number });

        var doc1c = new DfPanel()
        {
            Header = "Докуметы",
            Height = 80,
            Dock = DockStyle.Bottom,
            Padding = new Padding(0, 10, 0, 0),
            Visible = false
        };

        doc1c.AddControls(new Control[] { waybill_panel });

        var details = new DfDataGrid<WaybillProcessingPrice>(GetDetailsRepository()) { Dock = DockStyle.Fill };

        contractor.ValueChanged += (sender, e) =>
        {
            contract.RefreshDataSource();
            contract.Value = Document.contract_id;
        };

        contractor.SetDataSource(() => Services.Provider.GetService<IContractorRepository>()?.GetSuppliers());

        contract.ValueChanged += (sender, e) =>
        {
            doc1c.Visible = contract.SelectedItem != null;
        };

        contract.SetDataSource(() =>
        {
            if (contractor.SelectedItem != null)
            {
                return Services.Provider.GetService<IContractRepository>()?.GetSuppliers(contractor.SelectedItem.Id);
            }

            return null;
        });

        details.AutoGeneratingColumn += (sender, args) =>
        {
            switch (args.Column.MappingName)
            {
                case "id":
                    args.Cancel = true;
                    break;
                case "product_name":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                    break;
                case "code":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.AllCells;
                    break;
                case "amount":
                    args.Column.Width = 100;
                    break;
                case "price":
                case "product_cost":
                case "tax":
                case "tax_value":
                case "full_cost":
                    args.Cancel = true;
                    break;
            }
        };

        details.DataCreate += (sender, args) =>
        {
            args.Cancel = FormProductPrice<WaybillProcessingPrice>.Create(args.CreatingData, contract.SelectedItem) == DialogResult.Cancel;
        };

        details.DataEdit += (sender, args) =>
        {
            args.Cancel = FormProductPrice<WaybillProcessingPrice>.Edit(args.EditingData, contract.SelectedItem) == DialogResult.Cancel;
        };

        details.DataCopy += (sender, args) =>
        {
            args.Cancel = FormProductPrice<WaybillProcessingPrice>.Edit(args.CopiedData, contract.SelectedItem) == DialogResult.Cancel;
        };

        details.AddCommand("Заполнить", Properties.Resources.icons8_incoming_data_16, (sender, e) =>
        {
            if (order.SelectedItem != null)
            {
                var repo = Services.Provider.GetService<IProductionOrderRepository>();
                if (repo != null)
                {
                    var rows = repo.GetOnlyGivingMaterials<WaybillProcessingPrice>(order.SelectedItem);
                    details.Fill(rows);
                }
            }
        });

        AddControls(new Control[]
        {
            contractor,
            contract,
            order,
            details,
            doc1c
        });

        details.BringToFront();
    }

    private static IOwnedRepository<long, WaybillProcessingPrice> GetDetailsRepository()
    {
        return Services.Provider.GetService<IWaybillProcessingPriceRepository>()!;
    }
}
