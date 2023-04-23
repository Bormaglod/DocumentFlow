//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.02.2022
//
// Версия 2022.12.3
//  - в функции AsSummary заменён параметр includeDeleted имеющий значение
//    true на options равный SelectOptions.All
// Версия 2022.12.9
//  - добавлено зависимое окно "Поступление"
// Версия 2022.12.17
//  - метод UpdateCurrencyColumn стал статическим
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Dialogs;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.PaymentOrders.Documents;
using DocumentFlow.Entities.Waybills;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public class PurchaseRequestEditor : DocumentEditor<PurchaseRequest>, IPurchaseRequestEditor
{
    public PurchaseRequestEditor(IPurchaseRequestRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        EditorControls
            .AddDirectorySelectBox<Contractor>(x => x.ContractorId, "Контрагент", select =>
                select
                    .SetDataSource(GetSuppliers)
                    .EnableEditor<IContractorEditor>()
                    .DirectoryChanged(ContractorChanged)
                    .SetHeaderWidth(80)
                    .SetEditorWidth(400))
            .AddDirectorySelectBox<Contract>(x => x.ContractId, "Договор", select =>
                select
                    .SetDataSource(GetContracts)
                    .EnableEditor<IContractEditor>()
                    .SetHeaderWidth(80)
                    .SetEditorWidth(400))
            .AddTextBox(x => x.Note, "Доп. информация", text =>
                text
                    .Multiline()
                    .SetHeaderWidth(130)
                    .SetEditorWidth(400)
                    .SetDock(DockStyle.Bottom)
                    .EditorFitToSize())
            .AddDataGrid<PurchaseRequestPrice>(grid =>
                grid
                    .DoCreate(CreatePrice)
                    .DoUpdate(UpdatePrice)
                    .DoCopy(CopyPrice)
                    .GridSummaryRow(VerticalPosition.Bottom, summary =>
                        summary
                            .AsSummary(x => x.ProductCost, SummaryColumnFormat.Currency, SelectOptions.All)
                            .AsSummary(x => x.TaxValue, SummaryColumnFormat.Currency, SelectOptions.All)
                            .AsSummary(x => x.FullCost, SummaryColumnFormat.Currency, SelectOptions.All))
                    .SetRepository<IPurchaseRequestPriceRepository>()
                    .SetPadding(0, 0, 0, 7)
                    .SetDock(DockStyle.Fill));

        RegisterReport(new PurchaseRequestReport());
    }

    protected override void DoAfterRefreshData()
    {
        base.DoAfterRefreshData();
        RegisterNestedBrowser<IDocumentPaymentBrowser, DocumentPayment>();
        RegisterNestedBrowser<IWaybillReceiptNestedBrowser, WaybillReceipt>();
    }

    private bool CreatePrice(PurchaseRequestPrice price)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>();
        ProductPriceDialog<PurchaseRequestPrice> form = new(contract.SelectedItem);
        return form.Create(price);
    }

    private DataOperationResult UpdatePrice(PurchaseRequestPrice price)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>();
        ProductPriceDialog<PurchaseRequestPrice> form = new(contract.SelectedItem);
        if (form.Edit(price))
        {
            return DataOperationResult.Update;
        }
        else
        {
            return DataOperationResult.Cancel;
        }
    }

    private bool CopyPrice(PurchaseRequestPrice price)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>();
        ProductPriceDialog<PurchaseRequestPrice> form = new(contract.SelectedItem);
        return form.Edit(price);
    }

    private IEnumerable<Contractor> GetSuppliers() => Services.Provider.GetService<IContractorRepository>()!.GetSuppliers();

    private IEnumerable<Contract> GetContracts()
    {
        var contractor = EditorControls.GetControl<IDirectorySelectBoxControl<Contractor>>();
        if (contractor.SelectedItem != null)
        {
            var repo = Services.Provider.GetService<IContractRepository>();
            return repo!.GetSuppliers(contractor.SelectedItem.Id);
        }

        return Array.Empty<Contract>();
    }

    private void ContractorChanged(Contractor? _)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>();
        if (contract is IDataSourceControl<Guid, Contract> source)
        {
            source.RefreshDataSource(Document.ContractId);
        }
    }
}
