//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public class PurchaseRequestBrowser : Browser<PurchaseRequest>, IPurchaseRequestBrowser
{
    public PurchaseRequestBrowser(IPurchaseRequestRepository repository, IPageManager pageManager, IDocumentFilter filter)
        : base(repository, pageManager, filter: filter)
    {
        AllowGrouping();

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var contractor = CreateText(x => x.contractor_name, "Контрагент");
        var contract = CreateText(x => x.contract_name, "Договор", width: 200, visible: false);
        var cost_order = CreateCurrency(x => x.cost_order, "Сумма", width: 120);
        var tax = CreateNumeric(x => x.tax, "НДС%", width: 80, mode: FormatMode.Percent);
        var tax_value = CreateCurrency(x => x.tax_value, "НДС", width: 120);
        var full_cost = CreateCurrency(x => x.full_cost, "Всего c НДС", width: 120);
        var paid = CreateCurrency(x => x.paid, "Оплачено", width: 120);

        CreateSummaryRow(VerticalPosition.Bottom, true)
            .AsSummary(cost_order, SummaryColumnFormat.Currency)
            .AsSummary(tax_value, SummaryColumnFormat.Currency)
            .AsSummary(full_cost, SummaryColumnFormat.Currency);

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        tax.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

        AddColumns(new GridColumn[] { id, date, number, contractor, contract, cost_order, tax, tax_value, full_cost, paid });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Заявка";
}
