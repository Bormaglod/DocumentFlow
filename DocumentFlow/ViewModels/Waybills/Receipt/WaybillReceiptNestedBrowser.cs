//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class WaybillReceiptNestedBrowser : BrowserPage<WaybillReceipt>, IWaybillReceiptNestedBrowser
{
    public WaybillReceiptNestedBrowser(IServiceProvider services, IWaybillReceiptRepository repository, IConfiguration configuration)
        : base(services, repository, configuration)
    {
        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var waybill_date = CreateDateTime(x => x.WaybillDate, "Дата", width: 150, format: "dd.MM.yyyy", visible: false);
        var waybill_number = CreateNumeric(x => x.WaybillNumber, "Номер", width: 100, visible: false);
        var invoice_date = CreateDateTime(x => x.InvoiceDate, "Дата", width: 150, format: "dd.MM.yyyy");
        var invoice_number = CreateNumeric(x => x.InvoiceNumber, "Номер", width: 100);
        var contract = CreateText(x => x.ContractName, "Договор");
        var product_cost = CreateCurrency(x => x.ProductCost, "Сумма", width: 120);
        var tax = CreateNumeric(x => x.Tax, "НДС%", width: 80, mode: FormatMode.Percent);
        var tax_value = CreateCurrency(x => x.TaxValue, "НДС", width: 120);
        var full_cost = CreateCurrency(x => x.FullCost, "Всего c НДС", width: 120);

        CreateSummaryRow(VerticalPosition.Bottom, true)
            .AsSummary(product_cost, SummaryColumnFormat.Currency)
            .AsSummary(tax_value, SummaryColumnFormat.Currency)
            .AsSummary(full_cost, SummaryColumnFormat.Currency);

        contract.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        tax.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

        AddColumns(new GridColumn[] { id, date, number, waybill_date, waybill_number, invoice_date, invoice_number, contract, product_cost, tax, tax_value, full_cost });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        CreateStackedColumns("Счёт-фактура", new GridColumn[] { invoice_date, invoice_number });
    }
}
