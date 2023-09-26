//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.08.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.ViewModels;

[Entity(typeof(ProductionOrder), RepositoryType = typeof(IProductionOrderRepository))]
public partial class ProductionOrderEditor : EditorPanel, IProductionOrderEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public ProductionOrderEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        gridContent.GridSummaryRow<ProductionOrderPrice>(VerticalPosition.Bottom, summary =>
            summary
                .AsSummary(x => x.ProductCost, SummaryColumnFormat.Currency, SelectOptions.All)
                .AsSummary(x => x.TaxValue, SummaryColumnFormat.Currency, SelectOptions.All)
                .AsSummary(x => x.FullCost, SummaryColumnFormat.Currency, SelectOptions.All));
        gridContent.AddCommand("Изменить калькуляцию изделия", Properties.Resources.icons8_one_page_down_16, ChangeCalculation);

        Order.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<IProductionLotNestedBrowser>();
    }

    protected ProductionOrder Order { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Order.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(ProductionOrder.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(ProductionOrder.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(ProductionOrder.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectContractor.DataBindings.Add(nameof(selectContractor.SelectedItem), DataContext, nameof(ProductionOrder.ContractorId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectContract.DataBindings.Add(nameof(selectContract.SelectedItem), DataContext, nameof(ProductionOrder.ContractId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectContractor.DataSource = services.GetRequiredService<IContractorRepository>().GetCustomers();
        selectContract.DataSource = services
            .GetRequiredService<IContractRepository>()
            .GetList(callback: q => q.Where("id", Order.ContractId));

        gridContent.DataSource = Order.Prices;
    }

    private void ChangeCalculation()
    {
        if (gridContent.SelectedItem is ProductionOrderPrice item)
        {
            var dialog = services.GetRequiredService<DirectoryItemDialog>();

            var newCalc = dialog.Get(services.GetRequiredService<ICalculationRepository>().GetApproved(item.ReferenceId), withColumns: false);
            if (newCalc != null && newCalc.Id != item.CalculationId)
            {
                services.GetRequiredService<IProductionOrderRepository>().ChangeCalculation(Order, item.CalculationId, newCalc.Id);
                item.SetCalculation(newCalc);
            }
        }
    }

    private void SelectContract_DataSourceOnLoad(object sender, DataSourceLoadEventArgs e)
    {
        if (selectContractor.SelectedItem != Guid.Empty)
        {
            e.Values = services
                .GetRequiredService<IContractRepository>()
                .GetByOwner(selectContractor.SelectedItem);
        }
        else
        {
            e.Values = null;
        }
    }

    private void SelectContractor_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IContractorBrowser>(e.Document);
    }

    private void SelectContract_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IContractBrowser>(e.Document);
    }

    private void GridContent_CreateRow(object sender, DependentEntitySelectEventArgs e)
    {
        var dialog = services.GetRequiredService<ProductPriceDialog>();
        dialog.WithCalculation = true;

        if (selectContract.SelectedItem != Guid.Empty)
        {
            dialog.Contract = (Contract)selectContract.SelectedDocument;
        }

        if (dialog.Create(out ProductionOrderPrice? price))
        {
            e.DependentEntity = price;
        }
        else
        {
            e.Accept = false;
        }
    }

    private void GridContent_EditRow(object sender, DependentEntitySelectEventArgs e)
    {
        if (e.DependentEntity is ProductionOrderPrice price)
        {
            var dialog = services.GetRequiredService<ProductPriceDialog>();
            dialog.WithCalculation = true;

            if (selectContract.SelectedItem != Guid.Empty)
            {
                dialog.Contract = (Contract)selectContract.SelectedDocument;
            }

            if (dialog.Edit(price))
            {
                return;
            }
        }

        e.Accept = false;
    }
}
