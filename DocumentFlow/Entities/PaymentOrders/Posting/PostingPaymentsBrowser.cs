//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.PaymentOrders.Posting;

public class PostingPaymentsBrowser : Browser<PostingPayments>, IPostingPaymentsBrowser
{
    public PostingPaymentsBrowser(IPostingPaymentsRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridDateTimeColumn date = CreateDateTime(x => x.document_date, "Дата/время", hidden: false, width: 150);
        GridNumericColumn number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        GridTextColumn contractor = CreateText(x => x.contractor_name, "Контрагент");
        GridTextColumn document_name = CreateText(x => x.document_name, "Документ");
        GridNumericColumn transaction_amount = CreateCurrency(x => x.transaction_amount, "Сумма", width: 120);

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(transaction_amount, SummaryColumnFormat.Currency);

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        document_name.AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsExceptHeader;

        AddColumns(new GridColumn[] { id, date, number, contractor, document_name, transaction_amount });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Распределение";
}
