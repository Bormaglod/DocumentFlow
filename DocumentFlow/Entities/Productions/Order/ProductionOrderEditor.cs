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
        contractor = new DfDirectorySelectBox<Contractor>("contractor_id", "Контрагент", 80, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractorEditor, Contractor>(t)
        };

        var contract = new DfDirectorySelectBox<Contract>("contract_id", "Договор", 80, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractEditor, Contract>(t)
        };

        var details = new DfDataGrid<ProductionOrderPrice>(Services.Provider.GetService<IProductionOrderPriceRepository>()!)
        {
            Dock = DockStyle.Fill
        };

        contractor.SetDataSource(() => Services.Provider.GetService<IContractorRepository>()!.GetCustomers());

        contractor.ValueChanged += (sender, args) =>
        {
            contract.RefreshDataSource();
            contract.Value = contract.Items.FirstOrDefault(x => x.is_default)?.Id;
        };

        contract.SetDataSource(() =>
        {
            if (contractor.SelectedItem != null)
            {
                var repo = Services.Provider.GetService<IContractRepository>();
                return repo!.GetCustomers(contractor.SelectedItem.Id);
            }

            return Array.Empty<Contract>();
        });

        details.CreateTableSummaryRow(VerticalPosition.Bottom)
            .AsSummary("product_cost", SummaryColumnFormat.Currency, SelectOptions.All)
            .AsSummary("tax_value", SummaryColumnFormat.Currency, SelectOptions.All)
            .AsSummary("full_cost", SummaryColumnFormat.Currency, SelectOptions.All);

        details.AutoGeneratingColumn += (sender, args) =>
        {
            switch (args.Column.MappingName)
            {
                case "id":
                case "code":
                    args.Cancel = true;
                    break;
                case "product_name":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                    break;
                case "calculation_name":
                    args.Column.Width = 150;
                    break;
                case "amount":
                    args.Column.Width = 100;
                    break;
                case "price":
                    UpdateCurrencyColumn(args.Column, 100);
                    break;
                case "product_cost":
                    UpdateCurrencyColumn(args.Column, 140);
                    break;
                case "tax":
                    args.Column.Width = 80;
                    args.Column.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case "tax_value":
                    UpdateCurrencyColumn(args.Column, 140);
                    break;
                case "full_cost":
                    UpdateCurrencyColumn(args.Column, 140);
                    break;
                case "complete_status":
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

    private void UpdateCurrencyColumn(GridColumn column, int width)
    {
        if (column is GridNumericColumn c)
        {
            c.FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency;
        }

        column.Width = width;
        column.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
    }
}
