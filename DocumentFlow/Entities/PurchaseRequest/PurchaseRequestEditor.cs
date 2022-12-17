//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.PaymentOrders.Documents;
using DocumentFlow.Entities.Products.Dialogs;
using DocumentFlow.Entities.Waybills;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public class PurchaseRequestEditor : DocumentEditor<PurchaseRequest>, IPurchaseRequestEditor
{
    private readonly DfDirectorySelectBox<Contractor> contractor;

    public PurchaseRequestEditor(IPurchaseRequestRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        contractor = new DfDirectorySelectBox<Contractor>("contractor_id", "Контрагент", 80, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractorEditor, Contractor>(t)
        };

        var contract = new DfDirectorySelectBox<Contract>("contract_id", "Договор", 80, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractEditor, Contract>(t)
        };

        var details = new DfDataGrid<PurchaseRequestPrice>(Services.Provider.GetService<IPurchaseRequestPriceRepository>()!) 
        { 
            Padding = new Padding(0, 0, 0, 7)
        };

        var note = new DfTextBox("note", "Доп. информация", 130, 400) 
        { 
            Multiline = true, 
            Height = 75, 
            Dock = DockStyle.Bottom,
            EditorFitToSize = true
        };

        contractor.SetDataSource(() => Services.Provider.GetService<IContractorRepository>()!.GetSuppliers());

        contractor.ValueChanged += (sender, args) =>
        {
            contract.RefreshDataSource();
            contract.Value = Document.contract_id;
        };

        contract.SetDataSource(() =>
        {
            if (contractor.SelectedItem != null)
            {
                var repo = Services.Provider.GetService<IContractRepository>();
                return repo!.GetSuppliers(contractor.SelectedItem.id);
            }

            return null;
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
                    args.Cancel = true;
                    break;
                case "product_name":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
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
}
