//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.PaymentOrders.Posting;

public class PostingPaymentsBrowser : Browser<PostingPayments>, IPostingPaymentsBrowser
{
    public PostingPaymentsBrowser(IPostingPaymentsRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        GridTextColumn id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        GridDateTimeColumn date = CreateDateTime(x => x.DocumentDate, "Дата/время", hidden: false, width: 150);
        GridNumericColumn number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        GridTextColumn contractor = CreateText(x => x.contractor_name, "Контрагент");
        GridTextColumn document_name = CreateText(x => x.document_name, "Документ");
        GridNumericColumn transaction_amount = CreateCurrency(x => x.transaction_amount, "Сумма", width: 120);

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(transaction_amount, SummaryColumnFormat.Currency);

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        document_name.AutoSizeColumnsMode = AutoSizeColumnsMode.AllCells;

        AddColumns(new GridColumn[] { id, date, number, contractor, document_name, transaction_amount });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Распределение";
}
