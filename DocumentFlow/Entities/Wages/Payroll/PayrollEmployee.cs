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
        OwnerId = payroll.Id;
        EmployeeId = grossPayroll.EmployeeId;
        EmployeeName = grossPayroll.EmployeeName;
        Wage = grossPayroll.Wage;
    }
}