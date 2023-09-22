//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class PaymentOrderBrowser : BrowserPage<PaymentOrder>, IPaymentOrderBrowser
{
    public PaymentOrderBrowser(IServiceProvider services, IPageManager pageManager, IPaymentOrderRepository repository, IConfiguration configuration, IDocumentFilter filter)
        : base(services, pageManager, repository, configuration, filter: filter)
    {
        AllowGrouping();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата/время", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var contractor = CreateText(x => x.ContractorName, "Контрагент");
        var date_operation = CreateDateTime(x => x.DateOperation, "Дата операции", width: 150, format: "dd.MM.yyyy");
        var payment_number = CreateText(x => x.PaymentNumber, "Номер п/п", width: 100);
        var income = CreateCurrency(x => x.Income, "Приход", width: 120);
        var expense = CreateCurrency(x => x.Expense, "Расход", width: 120);

        CreateSummaryRow(VerticalPosition.Bottom, true)
            .AsSummary(income, SummaryColumnFormat.Currency)
            .AsSummary(expense, SummaryColumnFormat.Currency);

        CreateStackedColumns("Сумма операции", new GridColumn[] { income, expense });

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        payment_number.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, date, number, contractor, date_operation, payment_number, income, expense });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        MoveToEnd();
    }
}
