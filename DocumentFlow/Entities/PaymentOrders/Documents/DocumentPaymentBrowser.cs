//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Infrastructure;
using DocumentFlow.Properties;
using DocumentFlow.Settings.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.PaymentOrders.Documents;

public class DocumentPaymentBrowser : Browser<DocumentPayment>, IDocumentPaymentBrowser
{
    private readonly IPageManager pageManager;

    public DocumentPaymentBrowser(IDocumentPaymentRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        this.pageManager = pageManager;

        Toolbar.IconSize = ButtonIconSize.Small;

        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn document_name = CreateText(x => x.document_name, "Документ");
        GridDateTimeColumn date = CreateDateTime(x => x.document_date, "Дата/время", hidden: false, width: 150);
        GridNumericColumn number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        GridTextColumn payment_number = CreateText(x => x.payment_number, "Номер плат. ордера", width: 150);
        GridDateTimeColumn date_operation = CreateDateTime(x => x.date_operation, "Дата операции", width: 150, format: "dd.MM.yyyy");
        GridNumericColumn transaction_amount = CreateCurrency(x => x.transaction_amount, "Сумма", width: 150);

        document_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(transaction_amount, SummaryColumnFormat.Currency);

        AddColumns(new GridColumn[] { id, document_name, date, number, payment_number, date_operation, transaction_amount });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        Toolbar.Add("Открыть документ", Resources.icons8_open_document_16, Resources.icons8_open_document_30, OpenDocument);
    }

    protected override string HeaderText => "Платежи";

    private void OpenDocument()
    {
        if (CurrentDocument != null)
        {
            try
            {
                pageManager.ShowEditor<IPaymentOrderEditor>(CurrentDocument.id);
            }
            catch (BrowserException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
