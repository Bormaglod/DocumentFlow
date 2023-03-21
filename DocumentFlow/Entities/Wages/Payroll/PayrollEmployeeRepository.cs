//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Wages;

public class PayrollEmployeeRepository : OwnedRepository<long, PayrollEmployee>, IPayrollEmployeeRepository
{
    public PayrollEmployeeRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter = null)
    {
        return query
            .Select("payroll_employee.*")
            .Select("oe.item_name as employee_name")
            .Join("our_employee as oe", "oe.id", "payroll_employee.employee_id")
            .OrderBy("oe.item_name");
    }
}
