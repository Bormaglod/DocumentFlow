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

public class PaymentOrderEditor : Editor<PaymentOrder>, IPaymentOrderEditor
{
    private readonly DfIntegerTextBox<int> document_number;
    private readonly DfComboBox<Organization> organization;

    public PaymentOrderEditor(IPaymentOrderRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        document_number = new DfIntegerTextBox<int>("document_number", "Номер", 120, 100)
        {
            Dock = DockStyle.Left,
            Width = 220
        };

        var document_date = new DfDateTimePicker("document_date", "от", 25, 170)
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd.MM.yyyy HH:mm:ss",
            Required = true,
            Dock = DockStyle.Left,
            Width = 200,
        };

        organization = new DfComboBox<Organization>("organization_id", "Организация", 100, 200)
        {
            Dock = DockStyle.Left,
            Width = 305
        };

        organization.SetDataSource(() => Services.Provider.GetService<IOrganizationRepository>()!.GetAll());

        var panel_header = new Panel()
        {
            Dock = DockStyle.Top,
            Height = 32
        };

        panel_header.Controls.AddRange(new Control[] { organization, document_date, document_number });

        var payment_number = new DfTextBox("payment_number", "Номер пл. ордера", 120, 100)
        {
            Dock = DockStyle.Left,
            Width = 220
        };

        var date_operation = new DfDateTimePicker("date_operation", "от", 25, 170)
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd.MM.yyyy",
            Required = true,
            Dock = DockStyle.Left,
            Width = 200,
        };

        var panel_pp = new Panel()
        {
            Dock = DockStyle.Top,
            Height = 32
        };

        panel_pp.Controls.AddRange(new Control[] { date_operation, payment_number });

        var line = new DfLine();
        var contractor = new DfDirectorySelectBox<Contractor>("contractor_id", "Контрагент", 100, 400);
        var direction = new DfChoice<PaymentDirection>("PaymentDirection", "Тип платежа", 100, 200);
        var amount = new DfCurrencyTextBox("transaction_amount", "Сумма", 100, 200) { DefaultAsNull = false };

        contractor.SetDataSource(() => Services.Provider.GetService<IContractorRepository>()!.GetAll());
        direction.SetChoiceValues(PaymentOrder.Directions);

        AddControls(new Control[]
        {
            panel_header,
            panel_pp,
            line,
            contractor,
            direction,
            amount
        });
    }

    protected override void DoAfterRefreshData()
    {
        base.DoAfterRefreshData();

        organization.Value = Services.Provider.GetService<IOrganizationRepository>()!.GetMain().id;
        document_number.Enabled = Document.id != Guid.Empty;

        RegisterNestedBrowser<IPostingPaymentsBrowser, PostingPayments>();
    }
}