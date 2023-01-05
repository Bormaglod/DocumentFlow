//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//
// Версия 2023.1.5
//  - добавлен вызов MoveToEnd для перемещения в конец таблицы
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.PaymentOrders;

public class PaymentOrderBrowser : Browser<PaymentOrder>, IPaymentOrderBrowser
{
    public PaymentOrderBrowser(IPaymentOrderRepository repository, IPageManager pageManager, IDocumentFilter filter)
        : base(repository, pageManager, filter: filter)
    {
        AllowGrouping();

        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridDateTimeColumn date = CreateDateTime(x => x.document_date, "Дата/время", hidden: false, width: 150);
        GridNumericColumn number = CreateNumeric(x => x.document_number, "Номер", width: 100);
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
