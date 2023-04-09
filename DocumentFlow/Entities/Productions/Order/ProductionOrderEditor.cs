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

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Entities.Products.Dialogs;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Productions.Order;

public class ProductionOrderEditor : DocumentEditor<ProductionOrder>, IProductionOrderEditor
{
    private readonly DfDirectorySelectBox<Contractor> contractor;

    public ProductionOrderEditor(IProductionOrderRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        contractor = CreateDirectorySelectBox<Contractor, IContractorEditor>(x => x.ContractorId, "Контрагент", 80, 400, data: GetCustomers);
        var contract = CreateDirectorySelectBox<Contract, IContractEditor>(x => x.ContractId, "Договор", 80, 400);
        var details = new DfDataGrid<ProductionOrderPrice>(Services.Provider.GetService<IProductionOrderPriceRepository>()!)
        {
            Dock = DockStyle.Fill
        };

        contractor.ValueChanged += (sender, args) =>
        {
            contract.RefreshDataSource();
            contract.Value = contract.Items.FirstOrDefault(x => x.IsDefault)?.Id;
        };

        contract.SetDataSource(() =>
        {
            if (contractor.SelectedItem != null)
            {
                var repo = Services.Provider.GetService<IContractRepository>();
                return repo!.GetCustomers(contractor.SelectedItem.Id);
            }

            return null;
        });

        details.CreateTableSummaryRow(VerticalPosition.Bottom)
            .AsSummary("ProductCost", SummaryColumnFormat.Currency, SelectOptions.All)
            .AsSummary("TaxValue", SummaryColumnFormat.Currency, SelectOptions.All)
            .AsSummary("FullCost", SummaryColumnFormat.Currency, SelectOptions.All);

        details.AutoGeneratingColumn += (sender, args) =>
        {
            switch (args.Column.MappingName)
            {
                case "Id":
                case "Code":
                    args.Cancel = true;
                    break;
                case "ProductName":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                    break;
                case "CalculationName":
                    args.Column.Width = 150;
                    break;
                case "Amount":
                    args.Column.Width = 100;
                    break;
                case "Price":
                    UpdateCurrencyColumn(args.Column, 100);
                    break;
                case "ProductCost":
                    UpdateCurrencyColumn(args.Column, 140);
                    break;
                case "Tax":
                    args.Column.Width = 80;
                    args.Column.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case "TaxValue":
                    UpdateCurrencyColumn(args.Column, 140);
                    break;
                case "FullCost":
                    UpdateCurrencyColumn(args.Column, 140);
                    break;
                case "CompleteStatus":
                    args.Column = new GridProgressBarColumn()
                    {
                        MappingName = args.Column.MappingName,
                        HeaderText = "Выполнено",
                        Maximum = 100,
                        Minimum = 0,
                        ValueMode = ProgressBarValueMode.Percentage
                    };
                    break;
            }
        };

        details.DataCreate += (sender, args) =>
        {
            args.Cancel = FormProductPrice<ProductionOrderPrice>.Create(args.CreatingData, contract.SelectedItem, true) == DialogResult.Cancel;
        };

        details.DataEdit += (sender, args) =>
        {
            args.Cancel = FormProductPrice<ProductionOrderPrice>.Edit(args.EditingData, contract.SelectedItem, true) == DialogResult.Cancel;
        };

        details.DataCopy += (sender, args) =>
        {
            args.Cancel = FormProductPrice<ProductionOrderPrice>.Edit(args.CopiedData, contract.SelectedItem, true) == DialogResult.Cancel;
        };

        AddControls(new Control[]
        {
            contractor,
            contract,
            details
        });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IProductionLotNestedBrowser, ProductionLot>();
    }

    private IEnumerable<Contractor> GetCustomers() => Services.Provider.GetService<IContractorRepository>()!.GetCustomers();

    private static void UpdateCurrencyColumn(GridColumn column, int width)
    {
        if (column is GridNumericColumn c)
        {
            c.FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency;
        }

        column.Width = width;
        column.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
    }
}
