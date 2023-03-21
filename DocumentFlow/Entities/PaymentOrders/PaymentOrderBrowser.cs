//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//
// Версия 2023.1.5
//  - добавлен вызов MoveToEnd для перемещения в конец таблицы
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.PaymentOrders;

public class PaymentOrderBrowser : Browser<PaymentOrder>, IPaymentOrderBrowser
{
    public PaymentOrderBrowser(IPaymentOrderRepository repository, IPageManager pageManager, IDocumentFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        AllowGrouping();

        GridTextColumn id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        GridDateTimeColumn date = CreateDateTime(x => x.DocumentDate, "Дата/время", hidden: false, width: 150);
        GridNumericColumn number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        GridTextColumn contractor = CreateText(x => x.contractor_name, "Контрагент");
        GridDateTimeColumn date_operation = CreateDateTime(x => x.date_operation, "Дата операции", width: 150, format: "dd.MM.yyyy");
        GridTextColumn payment_number = CreateText(x => x.payment_number, "Номер п/п", width: 100);
        GridNumericColumn income = CreateCurrency(x => x.income, "Приход", width: 120);
        GridNumericColumn expense = CreateCurrency(x => x.expense, "Расход", width: 120);

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

    protected override string HeaderText => "Банк/касса";
}
