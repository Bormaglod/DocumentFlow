//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//
// Версия 2022.11.26
//  - параметр autoRefresh метода SetDataSource в классе
//    DataSourceControl был удален. Вместо него используется свойство
//    RefreshMethod этого класса в значении DataRefreshMethod.Immediately
// Версия 2023.1.28
//  - для установки значения employee_name в интерфейсе IWageEmployeeInfo
//    используется метод SetEmployeeName
//
//-----------------------------------------------------------------------

using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Wages.Core;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Wages.Dialogs;

public partial class WageEmployeeDialog<T> : Form, IWageEmployeeDialog<T>
    where T : WageEmployee, new()
{
    private readonly IControls<T> controls;

    public WageEmployeeDialog(IControls<T> controls)
    {
        InitializeComponent();

        this.controls = controls;
        
        controls.Container = Controls;
        controls
            .AddDirectorySelectBox<OurEmployee>(x => x.EmployeeId, "Сотрудник", select =>
                select
                    .SetDataSource(GetOurEmployees, DataRefreshMethod.Immediately)
                    .Required()
                    .SetHeaderWidth(120)
                    .EditorFitToSize())
            .AddCurrencyTextBox(x => x.Wage, "Зар. плата", text =>
                text
                    .SetHeaderWidth(120)
                    .DefaultAsValue()
                    .EditorFitToSize());
    }

    private IDirectorySelectBoxControl<OurEmployee> Employee => controls.GetControl<IDirectorySelectBoxControl<OurEmployee>>(x => x.EmployeeId);

    private ICurrencyTextBoxControl Wage => controls.GetControl<ICurrencyTextBoxControl>(x => x.Wage);

    public bool Create(T employeePayroll)
    {
        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(employeePayroll);
            return true;
        }

        return false;
    }

    public bool Edit(T employeePayroll)
    {
        if (Employee is IDataSourceControl<Guid, OurEmployee> source)
        {
            source.Select(employeePayroll.EmployeeId);
        }
        
        Wage.NumericValue = employeePayroll.Wage;

        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(employeePayroll);
            return true;
        }

        return false;
    }

    private IEnumerable<OurEmployee> GetOurEmployees() => Services.Provider.GetService<IOurEmployeeRepository>()!.GetAllDefault();

    private void SaveControlData(T dest)
    {
        dest.EmployeeId = Employee.SelectedItem?.Id ?? Guid.Empty;
        dest.Wage = Wage.NumericValue.GetValueOrDefault();
        dest.SetEmployeeName(Employee.ValueText);
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (Employee.SelectedItem == null)
        {
            MessageBox.Show("Выберите сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
        else if (Wage.NumericValue <= 0)
        {
            MessageBox.Show("Сумма зар. платы должна быть больше 0", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
