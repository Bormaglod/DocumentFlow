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

public class PayrollRepository : DocumentRepository<Payroll>, IPayrollRepository
{
    public PayrollRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        var q = new Query("payroll_employee as pe")
            .Select("pe.owner_id")
            .SelectRaw("sum(pe.wage) as wage")
            .SelectRaw("array_agg(oe.item_name) as employee_names")
            .Join("our_employee as oe", "oe.id", "pe.employee_id")
            .GroupBy("pe.owner_id");

        return query
            .Select("payroll.*")
            .Select("o.item_name as organization_name")
            .Select("q.{wage, employee_names}")
            .Select("gp.{billing_year, billing_month}")
            .Join("organization as o", "o.id", "payroll.organization_id")
            .LeftJoin("gross_payroll as gp", "gp.id", "payroll.owner_id")
            .LeftJoin(q.As("q"), j => j.On("q.owner_id", "payroll.id"));
    }
}
