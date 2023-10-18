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
using DocumentFlow.Dialogs;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Payroll), RepositoryType = typeof(IPayrollRepository))]
public partial class PayrollEditor : EditorPanel, IPayrollEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public PayrollEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        Payroll.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;

        gridEmps.AddCommand("Заполнить", Properties.Resources.icons8_miltiedit_16, PopulateDataGrid);
        gridEmps.RegisterDialog<IWageEmployeeDialog, PayrollEmployee>();
    }

    protected Payroll Payroll { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Payroll.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(Payroll.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(Payroll.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(Payroll.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectGrossPayroll.DataBindings.Add(nameof(selectGrossPayroll.SelectedItem), DataContext, nameof(Payroll.OwnerId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectGrossPayroll.DataSource = services.GetRequiredService<IGrossPayrollRepository>().GetListUserDefined();
        gridEmps.DataSource = Payroll.Employees;
    }

    private void PopulateDataGrid()
    {
        if (Payroll.OwnerId != null)
        {
            if (MessageBox.Show("Перед заполнением таблица будет очищена. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            FillEmployeeList(Payroll.OwnerId.Value);
        }
    }

    private void FillEmployeeList(Guid payrollId)
    {
        Payroll.Employees.Clear();

        foreach (var emp in services.GetRequiredService<IGrossPayrollRepository>().GetSummaryWage(payrollId))
        {
            Payroll.Employees.Add(new PayrollEmployee()
            {
                EmployeeId = emp.EmployeeId,
                EmployeeName = emp.EmployeeName,
                Wage = emp.Wage
            });
        }
    }

    private void SelectGrossPayroll_UserDocumentModified(object sender, DocumentChangedEventArgs e)
    {
        if (MessageBox.Show("Перед заполнением таблица будет очищена. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        {
            return;
        }

        FillEmployeeList(e.NewDocument.Id);
    }

    private void SelectGrossPayroll_DocumentDialogColumns(object sender, DocumentDialogColumnsEventArgs e)
    {
        ModelsHelper.CreatePayrollColumns(e.Columns);
    }

    private void SelectGrossPayroll_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IGrossPayrollEditor>(e.Document);
    }
}
