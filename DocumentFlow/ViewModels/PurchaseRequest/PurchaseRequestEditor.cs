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

[Entity(typeof(PurchaseRequest), RepositoryType = typeof(IPurchaseRequestRepository))]
public partial class PurchaseRequestEditor : EditorPanel, IPurchaseRequestEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public PurchaseRequestEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        gridContent.GridSummaryRow<PurchaseRequestPrice>(VerticalPosition.Bottom, summary =>
            summary
                .AsSummary(x => x.ProductCost, SummaryColumnFormat.Currency, SelectOptions.All)
                .AsSummary(x => x.TaxValue, SummaryColumnFormat.Currency, SelectOptions.All)
                .AsSummary(x => x.FullCost, SummaryColumnFormat.Currency, SelectOptions.All));

        Purchase.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<IDocumentPaymentBrowser>();
        EditorPage.RegisterNestedBrowser<IWaybillReceiptNestedBrowser>();
    }

    protected override void RegisterReports()
    {
        EditorPage.RegisterReport<PurchaseRequestReport>();
    }

    protected PurchaseRequest Purchase { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Purchase.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(PurchaseRequest.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(PurchaseRequest.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(PurchaseRequest.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectContractor.DataBindings.Add(nameof(selectContractor.SelectedItem), DataContext, nameof(PurchaseRequest.ContractorId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectContract.DataBindings.Add(nameof(selectContract.SelectedItem), DataContext, nameof(PurchaseRequest.ContractId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textNote.DataBindings.Add(nameof(textNote.TextValue), DataContext, nameof(PurchaseRequest.Note), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectContractor.DataSource = services.GetRequiredService<IContractorRepository>().GetSuppliers();
        selectContract.DataSource = services
            .GetRequiredService<IContractRepository>()
            .GetList(callback: q => q.Where("id", Purchase.ContractId));

        gridContent.DataSource = Purchase.Prices;
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
        if (selectContract.SelectedItem != Guid.Empty)
        {
            dialog.Contract = (Contract)selectContract.SelectedDocument;
        }

        if (dialog.Create(out PurchaseRequestPrice? price))
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
        if (e.DependentEntity is PurchaseRequestPrice price)
        {
            var dialog = services.GetRequiredService<ProductPriceDialog>();

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
