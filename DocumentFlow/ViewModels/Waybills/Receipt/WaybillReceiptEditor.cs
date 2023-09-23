﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.09.2023
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

[Entity(typeof(WaybillReceipt), RepositoryType = typeof(IWaybillReceiptRepository))]
public partial class WaybillReceiptEditor : EditorPanel, IWaybillReceiptEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public WaybillReceiptEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        gridContent.GridSummaryRow<WaybillReceiptPrice>(VerticalPosition.Bottom, summary =>
            summary
                .AsSummary(x => x.ProductCost, SummaryColumnFormat.Currency, SelectOptions.All)
                .AsSummary(x => x.TaxValue, SummaryColumnFormat.Currency, SelectOptions.All)
                .AsSummary(x => x.FullCost, SummaryColumnFormat.Currency, SelectOptions.All));

        Waybill.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<IDocumentPaymentBrowser>();
    }

    protected WaybillReceipt Waybill { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Waybill.Id != Guid.Empty;

        UpdatePanelDoc1C();
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(WaybillReceipt.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(WaybillReceipt.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(WaybillReceipt.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectContractor.DataBindings.Add(nameof(selectContractor.SelectedItem), DataContext, nameof(WaybillReceipt.ContractorId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectContract.DataBindings.Add(nameof(selectContract.SelectedItem), DataContext, nameof(WaybillReceipt.ContractId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectPurchase.DataBindings.Add(nameof(selectPurchase.SelectedItem), DataContext, nameof(WaybillReceipt.OwnerId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textInvoiceNumber.DataBindings.Add(nameof(textInvoiceNumber.TextValue), DataContext, nameof(WaybillReceipt.InvoiceNumber), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        dateInvoice.DataBindings.Add(nameof(dateInvoice.DateTimeValue), DataContext, nameof(WaybillReceipt.InvoiceDate), true, DataSourceUpdateMode.OnPropertyChanged, DateTime.MinValue);
        textWaybillNumber.DataBindings.Add(nameof(textWaybillNumber.TextValue), DataContext, nameof(WaybillReceipt.WaybillNumber), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        dateWaybill.DataBindings.Add(nameof(dateWaybill.DateTimeValue), DataContext, nameof(WaybillReceipt.WaybillDate), true, DataSourceUpdateMode.OnPropertyChanged, DateTime.MinValue);
        toggleUpd.DataBindings.Add(nameof(toggleUpd.ToggleValue), DataContext, nameof(WaybillReceipt.Upd), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectContractor.DataSource = services.GetRequiredService<IContractorRepository>().GetSuppliers();
        selectContract.DataSource = services
            .GetRequiredService<IContractRepository>()
            .GetList(callback: q => q.Where("id", Waybill.ContractId));
        selectPurchase.DataSource = services
            .GetRequiredService<IPurchaseRequestRepository>()
            .GetList(callback: q => q.Where("id", Waybill.OwnerId));

        gridContent.DataSource = Waybill.Prices;
    }

    private void UpdateInvoiceText()
    {
        labelDoc.Text = $"Счёт-фактура №{Waybill.WaybillNumber ?? "?"} от {(Waybill.WaybillDate == null ? "?" : Waybill.WaybillDate.Value.ToString("d"))}";
    }

    private void UpdatePanelDoc1C()
    {
        panelInvoice.Visible = !toggleUpd.ToggleValue;
        panelDoc1C.Height = toggleUpd.ToggleValue ? 99 : 131;
        labelDoc.Visible = toggleUpd.ToggleValue;

        if (toggleUpd.ToggleValue)
        {
            UpdateInvoiceText();
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

    private void ToggleUpd_ToggleValueChanged(object sender, EventArgs e) => UpdatePanelDoc1C();

    private void SelectContract_SelectedItemChanged(object sender, EventArgs e)
    {
        if (selectContract.SelectedItem == Guid.Empty)
        {
            panelDoc1C.Visible = false;
        }
        else
        {
            var contract = (Contract)selectContract.SelectedDocument;
            panelUpd.Visible = contract.TaxPayer;
            if (contract.TaxPayer)
            {
                panelInvoice.Visible = !Waybill.Upd;
            }
            else
            {
                panelInvoice.Visible = false;
            }

            int height = 67;
            if (contract.TaxPayer)
            {
                height += 32;
                if (!Waybill.Upd)
                {
                    height += 32;
                }
            }

            panelDoc1C.Height = height;
            panelDoc1C.Visible = contract.TaxPayer || contract.ContractorType == ContractorType.Buyer;
        }
    }

    private void GridContent_CreateRow(object sender, DependentEntitySelectEventArgs e)
    {
        var dialog = services.GetRequiredService<ProductPriceDialog>();
        if (selectContract.SelectedItem != Guid.Empty)
        {
            dialog.Contract = (Contract)selectContract.SelectedDocument;
        }

        if (dialog.Create(out WaybillReceiptPrice? price))
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
        if (e.DependentEntity is WaybillReceiptPrice price)
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

    private void SelectPurchase_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IPurchaseRequestEditor>(e.Document);
    }

    private void SelectPurchase_DataSourceOnLoad(object sender, DataSourceLoadEventArgs e)
    {
        if (Waybill.ContractorId != null)
        {
            e.Values = services.GetRequiredService<IPurchaseRequestRepository>().GetActiveByContractor(Waybill.ContractorId.Value, Waybill.OwnerId);
        }
    }

    private void SelectPurchase_DocumentDialogColumns(object sender, DocumentDialogColumnsEventArgs e)
    {
        ModelsHelper.CreatePurchaseRequestColumns(e.Columns);
    }

    private void SelectPurchase_SelectedItemChanged(object sender, EventArgs e)
    {
        selectContract.Enabled = selectPurchase.SelectedItem == Guid.Empty;
    }

    private void SelectPurchase_DocumentSelectedChanged(object sender, DocumentSelectedEventArgs e)
    {
        if (selectPurchase.SelectedItem == Guid.Empty)
        {
            return;
        }

        if (MessageBox.Show("Заполнить таблицу по данным заявки?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            Waybill.Prices.Clear();
            foreach (var product in services.GetRequiredService<IWaybillReceiptRepository>().GetProductsFromPurchaseRequest((PurchaseRequest)selectPurchase.SelectedDocument))
            {
                Waybill.Prices.Add(product);
            }
        }
    }
}
