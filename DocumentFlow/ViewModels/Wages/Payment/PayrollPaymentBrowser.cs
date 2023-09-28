//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class PayrollPaymentBrowser : BrowserPage<PayrollPayment>, IPayrollPaymentBrowser
{
    public PayrollPaymentBrowser(IServiceProvider services, IPageManager pageManager, IPayrollPaymentRepository repository, IConfiguration configuration, IDocumentFilter filter)
        : base(services, pageManager, repository, configuration, filter: filter)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var payroll_date = CreateDateTime(x => x.PayrollDate, "Дата", hidden: false, width: 150);
        var payroll_number = CreateNumeric(x => x.PayrollNumber, "Номер", width: 100);
        var billing_range = CreateText(x => x.BillingRange, "Расчётный период");
        var date_operation = CreateDateTime(x => x.DateOperation, "Дата операции", width: 150, format: "dd.MM.yyyy");
        var payment_number = CreateText(x => x.PaymentNumber, "Номер п/п", width: 100);
        var transaction = CreateCurrency(x => x.TransactionAmount, "Сумма платежа", width: 120);

        billing_range.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, date, number, payroll_date, payroll_number, billing_range, date_operation, payment_number, transaction });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        CreateStackedColumns("Платёжная ведомость", new GridColumn[] { payroll_date, payroll_number });

        CreateGrouping()
            .Register(date, "DateByDay", "По дням", (string ColumnName, object o) =>
            {
                var op = (PayrollPayment)o;
                if (op.DocumentDate.HasValue)
                {
                    return op.DocumentDate.Value.ToShortDateString();
                }

                return "NONE";
            });
    }
}
