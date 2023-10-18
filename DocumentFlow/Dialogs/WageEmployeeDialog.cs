//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs.Interfaces;

using System.Diagnostics.CodeAnalysis;

namespace DocumentFlow.Dialogs;

public partial class WageEmployeeDialog : Form, IWageEmployeeDialog
{
    private readonly IOurEmployeeRepository repository;

    public WageEmployeeDialog(IOurEmployeeRepository repository)
    {
        InitializeComponent();

        this.repository = repository;
    }

    public bool Create<T>([MaybeNullWhen(false)] out T row) where T : new()
    {
        selectEmp.DataSource = repository.GetListUserDefined();
        if (ShowDialog() == DialogResult.OK)
        {
            row = new T();
            if (row is WageEmployee wage)
            {
                var emp = (OurEmployee)selectEmp.SelectedDocument;
                wage.EmployeeId = emp.Id;
                wage.EmployeeName = emp.ItemName ?? string.Empty;
                wage.Wage = textSalary.DecimalValue;
            }

            return true;
        }

        row = default;
        return false;
    }

    public bool Edit<T>(T row)
    {
        if (row is not WageEmployee wage)
        {
            return false;
        }

        selectEmp.DataSource = repository.GetListUserDefined();

        selectEmp.SelectedItem = wage.EmployeeId;
        textSalary.DecimalValue = wage.Wage;

        if (ShowDialog() == DialogResult.OK)
        {
            var emp = (OurEmployee)selectEmp.SelectedDocument;

            wage.EmployeeId = emp.Id;
            wage.EmployeeName = emp.ItemName ?? string.Empty;
            wage.Wage = textSalary.DecimalValue;

            return true;
        }

        return false;
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (selectEmp.SelectedItem == Guid.Empty)
        {
            MessageBox.Show("Выберите сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
        else if (textSalary.DecimalValue <= 0)
        {
            MessageBox.Show("Сумма зар. платы должна быть больше 0", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
