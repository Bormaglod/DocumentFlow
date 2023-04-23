//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//
// Версия 2023.1.27
//  - добавлено поле without_distrib
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.PaymentOrders.Posting;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.PaymentOrders;

public class PaymentOrderEditor : DocumentEditor<PaymentOrder>, IPaymentOrderEditor
{
    public PaymentOrderEditor(IPaymentOrderRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        EditorControls
            .AddPanel((panel) =>
                panel
                    .SetDock(DockStyle.Top)
                    .SetHeight(32)
                    .AddControls((controls) => 
                        controls
                            .AddTextBox(x => x.PaymentNumber, "Номер пл. ордера", (text) =>
                                text
                                    .SetHeaderWidth(120)
                                    .SetDock(DockStyle.Left)
                                    .SetWidth(220))
                            .AddDateTimePicker(x => x.DateOperation, "от", (date) =>
                                date
                                    .SetCustomFormat("dd.MM.yyyy")
                                    .SetHeaderWidth(25)
                                    .SetDock(DockStyle.Left)
                                    .SetWidth(200)
                                    .SetHeaderTextAlign(ContentAlignment.MiddleCenter))))
            .AddDirectorySelectBox<Contractor>(x => x.ContractorId, "Контрагент", (select) =>
                select
                    .SetDataSource(GetContractors)
                    .SetHeaderWidth(120)
                    .SetEditorWidth(400))
            .AddChoice<PaymentDirection>(x => x.PaymentDirection, "Тип платежа", (choice) =>
                choice
                    .SetChoiceValues(PaymentOrder.Directions)
                    .SetHeaderWidth(120)
                    .SetEditorWidth(200))
            .AddCurrencyTextBox(x => x.TransactionAmount, "Сумма", (text) =>
                text
                    .SetHeaderWidth(120)
                    .SetEditorWidth(200)
                    .DefaultAsValue())
            .AddCheckBox(x => x.WithoutDistrib, "Не распределять", (check) =>
                check
                    .SetHeaderWidth(120));
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IPostingPaymentsBrowser, PostingPayments>();
    }

    private IEnumerable<Contractor> GetContractors() => Services.Provider.GetService<IContractorRepository>()!.GetAll();
}