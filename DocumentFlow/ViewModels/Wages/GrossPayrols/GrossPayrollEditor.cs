//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.09.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs;
using DocumentFlow.Tools;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;

using System.Globalization;

namespace DocumentFlow.ViewModels;

[Entity(typeof(GrossPayroll), RepositoryType = typeof(IGrossPayrollRepository))]
public partial class GrossPayrollEditor : EditorPanel, IGrossPayrollEditor
{
    private readonly IServiceProvider services;

    public GrossPayrollEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();

        this.services = services;

        var months = new Dictionary<short, string>();
        for (short i = 1; i < 13; i++)
        {
            months.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Humanize());
        }

        choiceMonth.From(months);
        Payroll.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;

        gridEmps.AddCommand("Заполнить", Properties.Resources.icons8_miltiedit_16, PopulateDataGrid);
    }

    protected GrossPayroll Payroll { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Payroll.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(GrossPayroll.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(GrossPayroll.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(GrossPayroll.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        choiceMonth.DataBindings.Add(nameof(choiceMonth.ChoiceValue), DataContext, nameof(GrossPayroll.BillingMonth), true, DataSourceUpdateMode.OnPropertyChanged);
        textYear.DataBindings.Add(nameof(textYear.IntegerValue), DataContext, nameof(GrossPayroll.BillingYear), true, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        gridEmps.DataSource = Payroll.Employees;
    }

    private void PopulateDataGrid()
    {
        if (MessageBox.Show("Перед заполнением таблица будет очищена. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        {
            return;
        }

        Payroll.Employees.Clear();

        var emps = services.GetRequiredService<IOurEmployeeRepository>().GetList();
        var items = services.GetRequiredService<IIncomeItemRepository>();
        var wage1c = services.GetRequiredService<IWage1cRepository>().Get(Payroll);
        var salary = services.GetRequiredService<IOperationsPerformedRepository>().GetWages(Payroll);
        foreach (var emp in emps)
        {
            if (emp.IncomeItems == null)
            {
                continue;
            }

            foreach (var itemName in emp.IncomeItems)
            {
                decimal w1c = 0;
                decimal emp_wage = 0;

                if (itemName == "ЗПЛ1С" || itemName == "СДЛ_1С")
                {
                    var w = wage1c.FirstOrDefault(x => x.EmployeeId == emp.Id);
                    if (w != null)
                    {
                        emp_wage = w.Wage;
                        w1c = w.Wage;
                    }
                }

                if (itemName == "СДЛ" || itemName == "СДЛ_1С")
                {
                    var w = salary.FirstOrDefault(x => x.EmployeeId == emp.Id);
                    if (w != null)
                    {
                        emp_wage = w.Wage;
                        w1c = emp_wage - w1c;
                        if (w1c < 0)
                        {
                            w1c = 0;
                        }
                    }
                }

                if (itemName == "СДЛ_1С")
                {
                    emp_wage = w1c;
                }

                if (emp_wage > 0)
                {
                    var item = items.GetList(q => q.Where("code", itemName))[0];
                    var payrollEmployee = new GrossPayrollEmployee()
                    {
                        EmployeeId = emp.Id,
                        Wage = emp_wage,
                        IncomeItemId = item.Id,
                        EmployeeName = emp.ItemName ?? string.Empty,
                        IncomeItemName = item.ItemName ?? string.Empty
                    };

                    Payroll.Employees.Add(payrollEmployee);
                }
            }
        }
    }

    private void GridEmps_CreateRow(object sender, DependentEntitySelectEventArgs e)
    {
        var dialog = services.GetRequiredService<WageEmployeeDialog>();
        if (dialog.Create(out GrossPayrollEmployee? wage))
        {
            e.DependentEntity = wage;
        }
        else
        {
            e.Accept = false;
        }
    }

    private void GridEmps_EditRow(object sender, DependentEntitySelectEventArgs e)
    {
        if (e.DependentEntity is GrossPayrollEmployee wage)
        {
            var dialog = services.GetRequiredService<WageEmployeeDialog>();

            if (dialog.Edit(wage))
            {
                return;
            }
        }

        e.Accept = false;
    }
}
