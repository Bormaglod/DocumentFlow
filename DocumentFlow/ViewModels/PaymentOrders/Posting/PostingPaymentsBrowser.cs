//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class PostingPaymentsBrowser : BrowserPage<PostingPayments>, IPostingPaymentsBrowser
{
    public PostingPaymentsBrowser(IServiceProvider services, IPageManager pageManager, IPostingPaymentsRepository repository, IConfiguration configuration)
        : base(services, pageManager, repository, configuration) 
    {
        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата/время", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var contractor = CreateText(x => x.ContractorName, "Контрагент");
        var document_name = CreateText(x => x.DocumentName, "Документ");
        var transaction_amount = CreateCurrency(x => x.TransactionAmount, "Сумма", width: 120);

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
}
