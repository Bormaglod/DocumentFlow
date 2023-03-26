﻿//-----------------------------------------------------------------------
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
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Settings;
using DocumentFlow.Properties;

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

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var document_name = CreateText(x => x.DocumentName, "Документ");
        var date = CreateDateTime(x => x.DocumentDate, "Дата/время", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var payment_number = CreateText(x => x.PaymentNumber, "Номер плат. ордера", width: 150);
        var date_operation = CreateDateTime(x => x.DateOperation, "Дата операции", width: 150, format: "dd.MM.yyyy");
        var transaction_amount = CreateCurrency(x => x.TransactionAmount, "Сумма", width: 150);

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
                pageManager.ShowEditor<IPaymentOrderEditor>(CurrentDocument.Id);
            }
            catch (BrowserException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
