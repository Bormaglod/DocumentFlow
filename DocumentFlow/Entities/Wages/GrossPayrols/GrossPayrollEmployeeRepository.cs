//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Wages;

public class GrossPayrollEmployeeRepository : OwnedRepository<long, GrossPayrollEmployee>, IGrossPayrollEmployeeRepository
{
    public GrossPayrollEmployeeRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter = null)
    {
        return query
            .Select("gross_payroll_employee.*")
            .Select("oe.item_name as employee_name")
            .Select("ii.item_name")
            .Join("our_employee as oe", "oe.id", "gross_payroll_employee.employee_id")
            .Join("income_item as ii", "ii.id", "gross_payroll_employee.income_item_id")
            .OrderBy("oe.item_name");
    }
}
