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
        var payment_number = new DfTextBox("payment_number", "Номер пл. ордера", 150, 100)
        {
            Dock = DockStyle.Left,
            Width = 250
        };

        var date_operation = new DfDateTimePicker("date_operation", "от", 25, 100)
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd.MM.yyyy",
            Required = true,
            Dock = DockStyle.Left,
            Width = 200,
            HeaderTextAlign = ContentAlignment.MiddleCenter
        };

        var panel_pp = new Panel()
        {
            Dock = DockStyle.Top,
            Height = 32
        };

        panel_pp.Controls.AddRange(new Control[] { date_operation, payment_number });

        var payroll = new DfDocumentSelectBox<Payroll>("owner_id", "Платёжная ведомость", 150, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IPayrollEditor, Payroll>(t)
        };

        var transaction = new DfCurrencyTextBox("transaction_amount", "Сумма", 150, 200) { DefaultAsNull = false };

        payroll.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IPayrollRepository>();
            return repo?.GetAllDefault();
        });

        payroll.Columns += (sender, e) => BasePayroll.CreateGridColumns(e.Columns);

        payroll.ManualValueChange += (sender, e) =>
        {
            if (e.NewValue != null)
            {
                transaction.NumericValue = e.NewValue.wage;
            }
        };

        AddControls(new Control[]
        {
            panel_pp,
            payroll,
            transaction
        });
    }
}