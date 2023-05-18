//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Wages;

public class PayrollPaymentEditor : DocumentEditor<PayrollPayment>, IPayrollPaymentEditor
{
    public PayrollPaymentEditor(IPayrollPaymentRepository repository, IPageManager pageManager) : base(repository, pageManager, true)
    {
        EditorControls
            .AddPanel(panel =>
                panel
                    .SetDock(DockStyle.Top)
                    .SetHeight(32)
                    .AddControls(controls =>
                        controls
                            .AddTextBox(x => x.PaymentNumber, "Номер пл. ордера", text =>
                                text
                                    .SetHeaderWidth(150)
                                    .SetDock(DockStyle.Left)
                                    .SetWidth(250))
                            .AddDateTimePicker(x => x.DateOperation, "от", date => 
                                date
                                    .SetCustomFormat("dd.MM.yyyy")
                                    .SetDock(DockStyle.Left)
                                    .SetWidth(200)
                                    .SetHeaderTextAlign(ContentAlignment.MiddleCenter))))
            .AddDocumentSelectBox<Payroll>(x => x.OwnerId, "Платёжная ведомость", select =>
                select
                    .SetDataSource(GetPayrolls)
                    .EnableEditor<IPayrollEditor>()
                    .CreateColumns(BasePayroll.CreateGridColumns)
                    .SetHeaderWidth(150)
                    .SetEditorWidth(400))
            .AddCurrencyTextBox(x => x.TransactionAmount, "Сумма", text =>
                text
                    .SetHeaderWidth(150)
                    .SetEditorWidth(200)
                    .DefaultAsValue());
    }

    private void PayrollSelected(Payroll? oldValue, Payroll? newValue)
    {
        if (newValue != null)
        {
            var transaction = EditorControls.GetControl<ICurrencyTextBoxControl>(x => x.TransactionAmount);
            transaction.NumericValue = newValue.Wage;
        }
    }

    private IEnumerable<Payroll> GetPayrolls() => Services.Provider.GetService<IPayrollRepository>()!.GetListUserDefined();
}