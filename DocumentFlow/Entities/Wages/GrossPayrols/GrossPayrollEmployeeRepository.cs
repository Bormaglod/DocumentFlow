//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.1.28
//  - в выборке метода GetDefaultQuery поле ii.item_name отображается как
//    income_item_name
// Версия 2023.2.11
//  - добавлен метод GetSummaryWage
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Entities.Wages;

public class GrossPayrollEmployeeRepository : OwnedRepository<long, GrossPayrollEmployee>, IGrossPayrollEmployeeRepository
{
    public GrossPayrollEmployeeRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<GrossPayrollEmployee> GetSummaryWage(GrossPayroll payroll)
    {
        using var conn = Database.OpenConnection();
        return GetQuery(conn, "gpe")
            .Select("oe.id as employee_id")
            .Select("oe.item_name as employee_name")
            .SelectRaw("sum(gpe.wage) as wage")
            .Join("our_employee as oe", "oe.id", "gpe.employee_id")
            .Where("gpe.owner_id", payroll.Id)
            .GroupBy("oe.id")
            .Get<GrossPayrollEmployee>()
            .ToList();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter = null)
    {
        return query
            .Select("gross_payroll_employee.*")
            .Select("oe.item_name as employee_name")
            .Select("ii.item_name as income_item_name")
            .Join("our_employee as oe", "oe.id", "gross_payroll_employee.employee_id")
            .Join("income_item as ii", "ii.id", "gross_payroll_employee.income_item_id")
            .OrderBy("oe.item_name");
    }
}
