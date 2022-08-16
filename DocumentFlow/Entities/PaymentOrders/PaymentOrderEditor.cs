//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
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
        var payment_number = new DfTextBox("payment_number", "Номер пл. ордера", 120, 100)
        {
            Dock = DockStyle.Left,
            Width = 220
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

        var contractor = new DfDirectorySelectBox<Contractor>("contractor_id", "Контрагент", 120, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractorEditor, Contractor>(t)
        };

        var direction = new DfChoice<PaymentDirection>("PaymentDirection", "Тип платежа", 120, 200);
        var amount = new DfCurrencyTextBox("transaction_amount", "Сумма", 120, 200) { DefaultAsNull = false };

        contractor.SetDataSource(() => Services.Provider.GetService<IContractorRepository>()!.GetAll());
        direction.SetChoiceValues(PaymentOrder.Directions);

        AddControls(new Control[]
        {
            panel_pp,
            contractor,
            direction,
            amount
        });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IPostingPaymentsBrowser, PostingPayments>();
    }
}