//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Wages;

public class PayrollPaymentEditor : DocumentEditor<PayrollPayment>, IPayrollPaymentEditor
{
    public PayrollPaymentEditor(IPayrollPaymentRepository repository, IPageManager pageManager) : base(repository, pageManager, true)
    {
        var payment_number = CreateTextBox(x => x.PaymentNumber, "Номер пл. ордера", 150, 100);
        payment_number.Dock = DockStyle.Left;
        payment_number.Width = 250;

        var date_operation = CreateDateTimePicker(x => x.DateOperation, "от", 25, 100, format: DateTimePickerFormat.Custom, required: true);
        date_operation.CustomFormat = "dd.MM.yyyy";
        date_operation.Dock = DockStyle.Left;
        date_operation.Width = 200;
        date_operation.HeaderTextAlign = ContentAlignment.MiddleCenter;

        var panel_pp = new Panel()
        {
            Dock = DockStyle.Top,
            Height = 32
        };

        panel_pp.Controls.AddRange(new Control[] { date_operation, payment_number });

        var payroll = CreateDocumentSelectBox<Payroll, IPayrollEditor>(x => x.OwnerId, "Платёжная ведомость", 150, 400, data: GetPayrolls);
        var transaction = CreateCurrencyTextBox(x => x.TransactionAmount, "Сумма", 150, 200, defaultAsNull: false);

        payroll.Columns += (sender, e) => BasePayroll.CreateGridColumns(e.Columns);

        payroll.ManualValueChange += (sender, e) =>
        {
            if (e.NewValue != null)
            {
                transaction.NumericValue = e.NewValue.Wage;
            }
        };

        AddControls(new Control[]
        {
            panel_pp,
            payroll,
            transaction
        });
    }

    private IEnumerable<Payroll> GetPayrolls() => Services.Provider.GetService<IPayrollRepository>()!.GetAllDefault();
}