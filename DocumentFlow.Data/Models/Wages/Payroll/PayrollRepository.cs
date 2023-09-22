//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

using System.Data;

namespace DocumentFlow.Data.Models;

public class PayrollRepository : DocumentRepository<Payroll>, IPayrollRepository
{
    public PayrollRepository(IDatabase database) : base(database)
    {
    }

    protected override bool CreatingAdjustedQuery() => true;

    protected override Payroll GetAdjustedQuery(Guid id, IDbConnection connection)
    {
        var payrollDictionary = new Dictionary<Guid, Payroll>();

        string sql = @"
            select 
                p.*, 
                pe.*, 
                e.item_name as employee_name
            from payroll p
                left join payroll_employee pe on pe.owner_id = p.id 
                left join our_employee e on e.id = pe.employee_id 
            where p.id = :id";

        return connection.Query<Payroll, PayrollEmployee, Payroll>(
            sql,
            (payroll, emp) =>
            {
                if (!payrollDictionary.TryGetValue(payroll.Id, out var payrollEntry))
                {
                    payrollEntry = payroll;
                    payrollDictionary.Add(payroll.Id, payrollEntry);
                }

                if (emp != null)
                {
                    payrollEntry.Employees.Add(emp);
                }

                return payrollEntry;
            },
            new { id })
            .First();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
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
