//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Wages.Core;

namespace DocumentFlow.Entities.Wages;

public class PayrollEmployee : WageEmployee
{
    public PayrollEmployee() { }

    public PayrollEmployee(Payroll payroll, GrossPayrollEmployee grossPayroll)
    {
        owner_id = payroll.id;
        employee_id = grossPayroll.employee_id;
        employee_name = grossPayroll.employee_name;
        wage = grossPayroll.wage;
    }
}