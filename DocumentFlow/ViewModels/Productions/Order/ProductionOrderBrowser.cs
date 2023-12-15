//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class ProductionOrderBrowser : BrowserPage<ProductionOrder>, IProductionOrderBrowser
{
    public ProductionOrderBrowser(IServiceProvider services, IProductionOrderRepository repository, IConfiguration configuration, IDocumentFilter filter, IEnumerable<ICreationBased>? creations)
        : base(services, repository, configuration, filter: filter, creations: creations)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var contractor = CreateText(x => x.ContractorName, "Контрагент");
        var contract = CreateText(x => x.ContractName, "Договор", width: 200, visible: false);
        var cost_order = CreateCurrency(x => x.CostOrder, "Сумма", width: 120);
        var tax = CreateNumeric(x => x.Tax, "НДС%", width: 80, mode: FormatMode.Percent);
        var tax_value = CreateCurrency(x => x.TaxValue, "НДС", width: 120);
        var full_cost = CreateCurrency(x => x.FullCost, "Всего c НДС", width: 120);
        var closed = CreateBoolean(x => x.Closed, "Закрыт", width: 100);

        CreateSummaryRow(VerticalPosition.Bottom, true)
            .AsSummary(cost_order, SummaryColumnFormat.Currency)
            .AsSummary(tax_value, SummaryColumnFormat.Currency)
            .AsSummary(full_cost, SummaryColumnFormat.Currency);

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        tax.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

        AddColumns(new GridColumn[] { id, date, number, contractor, contract, cost_order, tax, tax_value, full_cost, closed });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        CreateGrouping()
            .Register(date, "DateByDay", "По дням", GridGroupingHelper.DocumentByDay)
            .Register(date, "DateByMonth", "По месяцам", GridGroupingHelper.DocumentByMonth);

        MoveToEnd();
    }
}
