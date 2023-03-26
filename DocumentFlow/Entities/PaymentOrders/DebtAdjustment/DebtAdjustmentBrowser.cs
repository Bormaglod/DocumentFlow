//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.12.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.PaymentOrders;

public class DebtAdjustmentBrowser : Browser<DebtAdjustment>, IDebtAdjustmentBrowser
{
    public DebtAdjustmentBrowser(IDebtAdjustmentRepository repository, IPageManager pageManager, IDocumentFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        AllowGrouping();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата/время", hidden: false, width: 130);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var contractor = CreateText(x => x.ContractorName, "Контрагент");
        var document_debt_date = CreateDateTime(x => x.DocumentDebtDate, "Дата", width: 110, format: "dd.MM.yyyy");
        var document_debt_number = CreateNumeric(x => x.DocumentDebtNumber, "Номер", width: 100);
        var document_debt_amount = CreateCurrency(x => x.DocumentDebtAmount, "Сумма", width: 120);
        var document_debt_payment = CreateCurrency(x => x.DocumentDebtPayment, "Оплата", width: 120);
        var document_credit_date = CreateDateTime(x => x.DocumentCreditDate, "Дата", width: 110, format: "dd.MM.yyyy");
        var document_credit_number = CreateNumeric(x => x.DocumentCreditNumber, "Номер", width: 100);
        var document_credit_amount = CreateCurrency(x => x.DocumentCreditAmount, "Сумма", width: 120);
        var document_credit_payment = CreateCurrency(x => x.DocumentCreditPayment, "Оплата", width: 120);
        var transaction_amount = CreateCurrency(x => x.TransactionAmount, "Корректировка", width: 150);

        CreateStackedColumns("Долг контрагента", new GridColumn[] { document_debt_date, document_debt_number, document_debt_amount, document_debt_payment });
        CreateStackedColumns("Долг организации", new GridColumn[] { document_credit_date, document_credit_number, document_credit_amount, document_credit_payment });

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        document_debt_payment.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        document_credit_payment.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { 
            id, 
            date, 
            number, 
            contractor, 
            document_debt_date, 
            document_debt_number, 
            document_debt_amount, 
            document_debt_payment,
            document_credit_date, 
            document_credit_number, 
            document_credit_amount, 
            document_credit_payment,
            transaction_amount
        });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Корректировка долга";
}
