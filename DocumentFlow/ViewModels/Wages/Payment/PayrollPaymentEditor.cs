//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.09.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(PayrollPayment), RepositoryType = typeof(IPayrollPaymentRepository))]
public partial class PayrollPaymentEditor : EditorPanel, IPayrollPaymentEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public PayrollPaymentEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        Payroll.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    protected PayrollPayment Payroll { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Payroll.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(PayrollPayment.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(PayrollPayment.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(PayrollPayment.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        textPaymentNumber.DataBindings.Add(nameof(textPaymentNumber.TextValue), DataContext, nameof(PayrollPayment.PaymentNumber), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        dateOperation.DataBindings.Add(nameof(dateOperation.DateTimeValue), DataContext, nameof(PayrollPayment.DateOperation), false, DataSourceUpdateMode.OnPropertyChanged);
        selectPayroll.DataBindings.Add(nameof(selectPayroll.SelectedItem), DataContext, nameof(PayrollPayment.OwnerId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textSumma.DataBindings.Add(nameof(textSumma.DecimalValue), DataContext, nameof(PayrollPayment.TransactionAmount), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectPayroll.DataSource = services.GetRequiredService<IPayrollRepository>().GetListUserDefined();
    }

    private void SelectPayroll_DocumentDialogColumns(object sender, DocumentDialogColumnsEventArgs e)
    {
        ModelsHelper.CreatePayrollColumns(e.Columns);
    }

    private void SelectPayroll_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IPayrollEditor>(e.Document);
    }

    private void SelectPayroll_UserDocumentModified(object sender, DocumentSelectedEventArgs e)
    {
        Payroll.TransactionAmount = ((Payroll)e.Document).Wage;
    }
}
