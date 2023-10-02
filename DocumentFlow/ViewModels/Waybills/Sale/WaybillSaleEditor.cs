//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.09.2023
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

[Entity(typeof(WaybillSale), RepositoryType = typeof(IWaybillSaleRepository))]
public partial class WaybillSaleEditor : EditorPanel, IWaybillSaleEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public WaybillSaleEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        gridContent.GridSummaryRow<WaybillSalePrice>(VerticalPosition.Bottom, summary =>
            summary
                .AsSummary(x => x.ProductCost, SummaryColumnFormat.Currency, SelectOptions.All)
                .AsSummary(x => x.TaxValue, SummaryColumnFormat.Currency, SelectOptions.All)
                .AsSummary(x => x.FullCost, SummaryColumnFormat.Currency, SelectOptions.All));
        gridContent.AddCommand("Добавить партию", Properties.Resources.icons8_production_finished_16, AddGoodsFromLot);

        Waybill.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<IDocumentPaymentBrowser>();
    }

    public void AddGoodsFromLot()
    {
        if (selectContractor.SelectedItem == Guid.Empty)
        {
            return;
        }

        var dialog = new DocumentDialog()
        {
            Documents = services
                .GetRequiredService<IProductionLotRepository>()
                .GetActiveLots((Contractor)selectContractor.SelectedDocument)
                .OrderBy(x => x.DocumentDate)
                .ThenBy(x => x.DocumentNumber)
        };

        ModelsHelper.CreateProductionLotColumns(dialog.Columns, true);

        if (dialog.ShowDialog() == DialogResult.OK && dialog.SelectedDocumentItem is ProductionLot lot)
        {
            var goods = services.GetRequiredService<IGoodsRepository>().Get(lot.GoodsId, ignoreAdjustedQuery: true);
            decimal price = goods.Price;
            int tax = 0;

            if (selectContract.SelectedItem != Guid.Empty)
            {
                var contract = (Contract)selectContract.SelectedDocument;
                var info = services
                    .GetRequiredService<IContractRepository>()
                    .GetPriceProduct(contract, goods);
                if (info != null)
                {
                    price = info.Price;
                }

                tax = contract.TaxPayer ? 20 : 0;
            }

            var productCost = price * lot.FreeQuantity;
            var taxValue = productCost * tax / 100;
            var product = new WaybillSalePrice()
            {
                ReferenceId = lot.GoodsId,
                Amount = lot.FreeQuantity,
                Price = price,
                ProductCost = productCost,
                Tax = tax,
                TaxValue = taxValue,
                FullCost = productCost + taxValue,
                Discriminator = "goods",
                LotId = lot.Id,
                LotName = lot.ToString()
            };


            product.SetProductInfo(goods);

            Waybill.Prices.Add(product);
        }
    }

    protected override void RegisterReports()
    {
        EditorPage.RegisterReport<WaybillSaleReport>();
    }

    protected WaybillSale Waybill { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Waybill.Id != Guid.Empty;

        UpdatePanelDoc1C();
    }

    protected override void OnEntityPropertyChanged(string? propertyName)
    {
        base.OnEntityPropertyChanged(propertyName);
        switch (propertyName)
        {
            case nameof(WaybillSale.ContractId):
                if (Waybill.ContractId == Guid.Empty)
                {
                    panelDoc1C.Visible = false;
                }
                else
                {
                    UpdatePanelDoc1C();
                }

                break;
            case nameof(WaybillSale.Upd):
                UpdatePanelDoc1C();
                break;
            default:
                break;
        }
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(WaybillSale.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(WaybillSale.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(WaybillSale.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectContractor.DataBindings.Add(nameof(selectContractor.SelectedItem), DataContext, nameof(WaybillSale.ContractorId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectContract.DataBindings.Add(nameof(selectContract.SelectedItem), DataContext, nameof(WaybillSale.ContractId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textInvoiceNumber.DataBindings.Add(nameof(textInvoiceNumber.TextValue), DataContext, nameof(WaybillSale.InvoiceNumber), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        dateInvoice.DataBindings.Add(nameof(dateInvoice.DateTimeValue), DataContext, nameof(WaybillSale.InvoiceDate), true, DataSourceUpdateMode.OnPropertyChanged, DateTime.MinValue);
        textWaybillNumber.DataBindings.Add(nameof(textWaybillNumber.TextValue), DataContext, nameof(WaybillSale.WaybillNumber), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        dateWaybill.DataBindings.Add(nameof(dateWaybill.DateTimeValue), DataContext, nameof(WaybillSale.WaybillDate), true, DataSourceUpdateMode.OnPropertyChanged, DateTime.MinValue);
        toggleUpd.DataBindings.Add(nameof(toggleUpd.ToggleValue), DataContext, nameof(WaybillSale.Upd), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectContractor.DataSource = services.GetRequiredService<IContractorRepository>().GetCustomers();
        selectContract.DataSource = services
            .GetRequiredService<IContractRepository>()
            .GetList(callback: q => q.Where("id", Waybill.ContractId));

        gridContent.DataSource = Waybill.Prices;
    }

    private void UpdateInvoiceText()
    {
        labelDoc.Text = $"Счёт-фактура №{Waybill.WaybillNumber ?? "?"} от {(Waybill.WaybillDate == null ? "?" : Waybill.WaybillDate.Value.ToString("d"))}";
    }

    private void UpdatePanelDoc1C()
    {
        if (selectContract.DataSource == null)
        {
            return;
        }

        var contract = selectContract.DataSource.OfType<Contract>().FirstOrDefault(x => x.Id == Waybill.ContractId);
        var taxPayer = contract != null && contract.TaxPayer;

        panelDoc1C.Visible = taxPayer;

        if (taxPayer)
        {
            panelInvoice.Visible = !Waybill.Upd;
            panelUpd.Visible = true;
            labelDoc.Visible = Waybill.Upd;

            panelDoc1C.Height = Waybill.Upd ? 99 : 131;

            if (Waybill.Upd)
            {
                UpdateInvoiceText();
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
        pageManager.ShowEditor<IContractorEditor>(e.Document);
    }

    private void SelectContract_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IContractEditor>(e.Document);
    }

    private void GridContent_CreateRow(object sender, DependentEntitySelectEventArgs e)
    {
        var dialog = services.GetRequiredService<ProductPriceDialog>();
        if (selectContract.SelectedItem != Guid.Empty)
        {
            dialog.Contract = (Contract)selectContract.SelectedDocument;
        }

        if (dialog.Create(out WaybillSalePrice? price))
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
        if (e.DependentEntity is WaybillSalePrice price)
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

    private void SelectContractor_UserDocumentModified(object sender, DocumentChangedEventArgs e)
    {
        if (e.OldDocument != e.NewDocument)
        {
            Waybill.ContractId = Guid.Empty;
        }
    }

    private void SelectContractor_DeleteButtonClick(object sender, EventArgs e)
    {
        Waybill.ContractId = Guid.Empty;
    }
}
