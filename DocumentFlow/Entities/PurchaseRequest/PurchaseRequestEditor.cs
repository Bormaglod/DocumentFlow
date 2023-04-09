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

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.PaymentOrders.Documents;
using DocumentFlow.Entities.Products.Dialogs;
using DocumentFlow.Entities.Waybills;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public class PurchaseRequestEditor : DocumentEditor<PurchaseRequest>, IPurchaseRequestEditor
{
    private readonly DfDirectorySelectBox<Contractor> contractor;

    public PurchaseRequestEditor(IPurchaseRequestRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        contractor = CreateDirectorySelectBox<Contractor, IContractorEditor>(x => x.ContractorId, "Контрагент", 80, 400, data: GetSuppliers);
        var contract = CreateDirectorySelectBox<Contract, IContractEditor>(x => x.ContractId, "Договор", 80, 400);
        var details = new DfDataGrid<PurchaseRequestPrice>(Services.Provider.GetService<IPurchaseRequestPriceRepository>()!) 
        { 
            Padding = new Padding(0, 0, 0, 7)
        };

        var note = CreateMultilineTextBox(x => x.Note, "Доп. информация", 130, 400);
        note.Dock = DockStyle.Bottom;
        note.EditorFitToSize = true;

        contractor.ValueChanged += (sender, args) =>
        {
            contract.RefreshDataSource();
            contract.Value = Document.ContractId;
        };

        contract.SetDataSource(() =>
        {
            if (contractor.SelectedItem != null)
            {
                var repo = Services.Provider.GetService<IContractRepository>();
                return repo!.GetSuppliers(contractor.SelectedItem.Id);
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
                    args.Cancel = true;
                    break;
                case "ProductName":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
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
            }
        };

        details.DataCreate += (sender, args) =>
        {
            args.Cancel = FormProductPrice<PurchaseRequestPrice>.Create(args.CreatingData, contract.SelectedItem) == DialogResult.Cancel;
        };

        details.DataEdit += (sender, args) =>
        {
            args.Cancel = FormProductPrice<PurchaseRequestPrice>.Edit(args.EditingData, contract.SelectedItem) == DialogResult.Cancel;
        };

        details.DataCopy += (sender, args) =>
        {
            args.Cancel = FormProductPrice<PurchaseRequestPrice>.Edit(args.CopiedData, contract.SelectedItem) == DialogResult.Cancel;
        };

        AddControls(new Control[]
        {
            contractor,
            contract,
            note,
            details            
        });

        details.Dock = DockStyle.Fill;

        RegisterReport(new PurchaseRequestReport());
    }

    protected override void DoAfterRefreshData()
    {
        base.DoAfterRefreshData();
        RegisterNestedBrowser<IDocumentPaymentBrowser, DocumentPayment>();
        RegisterNestedBrowser<IWaybillReceiptNestedBrowser, WaybillReceipt>();
    }

    private static void UpdateCurrencyColumn(GridColumn column, int width)
    {
        if (column is GridNumericColumn c)
        {
            c.FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency;
        }

        column.Width = width;
        column.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
    }

    private IEnumerable<Contractor> GetSuppliers() => Services.Provider.GetService<IContractorRepository>()!.GetSuppliers();
}
