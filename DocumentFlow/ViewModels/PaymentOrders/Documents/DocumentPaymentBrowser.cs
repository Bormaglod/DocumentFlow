//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2022
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;
using DocumentFlow.Messages;
using DocumentFlow.Properties;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class DocumentPaymentBrowser : BrowserPage<DocumentPayment>, IDocumentPaymentBrowser
{
    public DocumentPaymentBrowser(IServiceProvider services, IDocumentPaymentRepository repository, IConfiguration configuration)
        : base(services, repository, configuration)
    {
        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var document_name = CreateText(x => x.DocumentName, "Документ");
        var date = CreateDateTime(x => x.DocumentDate, "Дата/время", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var payment_number = CreateText(x => x.PaymentNumber, "Номер плат. ордера", width: 150);
        var date_operation = CreateDateTime(x => x.DateOperation, "Дата операции", width: 150, format: "dd.MM.yyyy");
        var transaction_amount = CreateCurrency(x => x.TransactionAmount, "Документа", width: 120);
        var posting = CreateCurrency(x => x.PostingTransaction, "Распределено", width: 120);

        document_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(posting, SummaryColumnFormat.Currency);

        CreateStackedColumns("Сумма", new GridColumn[] { transaction_amount, posting });

        AddColumns(new GridColumn[] { id, document_name, date, number, payment_number, date_operation, transaction_amount, posting });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        ToolBar.Add("Открыть документ", Resources.icons8_open_document_16, Resources.icons8_open_document_30, () =>
        {
            if (CurrentDocument != null)
            {
                try
                {
                    WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IPaymentOrderBrowser), CurrentDocument.Id));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        });
    }
}
