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
        var payment_number = CreateTextBox(x => x.PaymentNumber, "Номер пл. ордера", 120, 100);
        payment_number.Dock = DockStyle.Left;
        payment_number.Width = 220;

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

        var contractor = CreateDirectorySelectBox<Contractor, IContractorEditor>(x => x.ContractorId, "Контрагент", 120, 400, data: GetContractors);
        var direction = CreateChoice(x => x.PaymentDirection, "Тип платежа", 120, 200, choices: PaymentOrder.Directions);
        var amount = CreateCurrencyTextBox(x => x.TransactionAmount, "Сумма", 120, 200, defaultAsNull: false);
        var without_distrib = CreateCheckBox(x => x.WithoutDistrib, "Не распределять", 120);

        AddControls(new Control[]
        {
            panel_pp,
            contractor,
            direction,
            amount,
            without_distrib
        });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IPostingPaymentsBrowser, PostingPayments>();
    }

    private IEnumerable<Contractor> GetContractors() => Services.Provider.GetService<IContractorRepository>()!.GetAll();
}