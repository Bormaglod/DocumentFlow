//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.09.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(PaymentOrder), RepositoryType = typeof(IPaymentOrderRepository))]
public partial class PaymentOrderEditor : EditorPanel, IPaymentOrderEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public PaymentOrderEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        choiceDirection.FromEnum<PaymentDirection>(KeyGenerationMethod.PostgresEnumValue);

        Order.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    protected PaymentOrder Order { get; set; } = null!;

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<IPostingPaymentsBrowser>();
    }

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Order.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(PaymentOrder.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(PaymentOrder.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(PaymentOrder.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        textPaymentNumber.DataBindings.Add(nameof(textPaymentNumber.TextValue), DataContext, nameof(PaymentOrder.PaymentNumber), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        dateOperation.DataBindings.Add(nameof(dateOperation.DateTimeValue), DataContext, nameof(PaymentOrder.DateOperation), false, DataSourceUpdateMode.OnPropertyChanged);
        selectContractor.DataBindings.Add(nameof(selectContractor.SelectedItem), DataContext, nameof(PaymentOrder.ContractorId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        choiceDirection.DataBindings.Add(nameof(choiceDirection.ChoiceValue), DataContext, nameof(PaymentOrder.Direction), false, DataSourceUpdateMode.OnPropertyChanged);
        textSumma.DataBindings.Add(nameof(textSumma.DecimalValue), DataContext, nameof(PaymentOrder.TransactionAmount), false, DataSourceUpdateMode.OnPropertyChanged);
        toggleNotDistrib.DataBindings.Add(nameof(toggleNotDistrib.ToggleValue), DataContext, nameof(PaymentOrder.WithoutDistrib), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectContractor.DataSource = services.GetRequiredService<IContractorRepository>().GetList();
    }

    private void SelectContractor_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IContractorEditor>(e.Document);
    }
}
