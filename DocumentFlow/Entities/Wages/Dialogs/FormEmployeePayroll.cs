//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Wages.Core;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Wages.Dialogs;

public partial class FormEmployeePayroll : Form
{
    private readonly DfDirectorySelectBox<OurEmployee> emp;
    private readonly DfCurrencyTextBox payroll;

    protected FormEmployeePayroll()
    {
        InitializeComponent();

        emp = new("employee_id", "Сотрудник", 120) { Required = true };
        payroll = new("payroll", "Зар. плата", 120) { DefaultAsNull = false };

        var controls = new List<Control>() 
        { 
            payroll,
            emp
        };

        emp.SetDataSource(() => Services.Provider.GetService<IOurEmployeeRepository>()!.GetAllDefault(), true);

        Controls.AddRange(controls.ToArray());
    }

    public static DialogResult Create(IWageEmployeeInfo employeePayroll)
    {
        FormEmployeePayroll form = new();
        if (form.ShowDialog() == DialogResult.OK)
        {
            form.SaveControlData(employeePayroll);
            return DialogResult.OK;
        }

        return DialogResult.Cancel;
    }

    public static DialogResult Edit(IWageEmployeeInfo employeePayroll)
    {
        FormEmployeePayroll form = new();
        
        form.emp.Value = employeePayroll.employee_id;
        form.payroll.Value = employeePayroll.wage;

        if (form.ShowDialog() == DialogResult.OK)
        {
            form.SaveControlData(employeePayroll);
            return DialogResult.OK;
        }

        return DialogResult.Cancel;
    }

    private void SaveControlData(IWageEmployeeInfo dest)
    {
        dest.employee_id = emp.SelectedItem?.id ?? Guid.Empty;
        dest.employee_name = emp.ValueText;
        dest.wage = payroll.NumericValue.GetValueOrDefault();
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (emp.Value == null)
        {
            MessageBox.Show("Выберите сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
        else if (payroll.NumericValue <= 0)
        {
            MessageBox.Show("Сумма зар. платы должна быть больше 0", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
