//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Waybills;

public class WaybillReceiptNestedBrowser : Browser<WaybillReceipt>, IWaybillReceiptNestedBrowser
{
    public WaybillReceiptNestedBrowser(IWaybillReceiptRepository repository, IPageManager pageManager)
        : base(repository, pageManager)
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var waybill_date = CreateDateTime(x => x.waybill_date, "Дата", width: 150, format: "dd.MM.yyyy", visible: false);
        var waybill_number = CreateNumeric(x => x.waybill_number, "Номер", width: 100, visible: false);
        var invoice_date = CreateDateTime(x => x.invoice_date, "Дата", width: 150, format: "dd.MM.yyyy");
        var invoice_number = CreateNumeric(x => x.invoice_number, "Номер", width: 100);
        var contract = CreateText(x => x.contract_name, "Договор", width: 200, visible: false);
        var product_cost = CreateCurrency(x => x.product_cost, "Сумма", width: 120);
        var tax = CreateNumeric(x => x.tax, "НДС%", width: 80, mode: FormatMode.Percent);
        var tax_value = CreateCurrency(x => x.tax_value, "НДС", width: 120);
        var full_cost = CreateCurrency(x => x.full_cost, "Всего c НДС", width: 120);

        CreateSummaryRow(VerticalPosition.Bottom, true)
            .AsSummary(product_cost, SummaryColumnFormat.Currency)
            .AsSummary(tax_value, SummaryColumnFormat.Currency)
            .AsSummary(full_cost, SummaryColumnFormat.Currency);

        tax.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

        AddColumns(new GridColumn[] { id, date, number, waybill_date, waybill_number, invoice_date, invoice_number, contract, product_cost, tax, tax_value, full_cost });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        CreateStackedColumns("Счёт-фактура", new GridColumn[] { invoice_date, invoice_number });
    }
    protected override string HeaderText => "Поступление";
}
