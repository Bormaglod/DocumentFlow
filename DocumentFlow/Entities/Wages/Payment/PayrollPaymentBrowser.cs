//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Wages;

public class PayrollPaymentBrowser : Browser<PayrollPayment>, IPayrollPaymentBrowser
{
    public PayrollPaymentBrowser(IPayrollPaymentRepository repository, IPageManager pageManager, IDocumentFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        AllowGrouping();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var payroll_date = CreateDateTime(x => x.payroll_date, "Дата", hidden: false, width: 150);
        var payroll_number = CreateNumeric(x => x.payroll_number, "Номер", width: 100);
        var billing_range = CreateText(x => x.billing_range, "Расчётный период");
        var date_operation = CreateDateTime(x => x.date_operation, "Дата операции", width: 150, format: "dd.MM.yyyy");
        var payment_number = CreateText(x => x.payment_number, "Номер п/п", width: 100);
        var transaction = CreateCurrency(x => x.transaction_amount, "Сумма платежа", width: 120);

        billing_range.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, date, number, payroll_date, payroll_number, billing_range, date_operation, payment_number, transaction });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        this.CreateStackedColumns("Платёжная ведомость", new GridColumn[] { payroll_date, payroll_number });
    }

    protected override string HeaderText => "Выплата зар. платы";
}
