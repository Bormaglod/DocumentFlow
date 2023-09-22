//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;
using SqlKata.Execution;

using System.Data;

namespace DocumentFlow.Data.Models;

public class GrossPayrollRepository : DocumentRepository<GrossPayroll>, IGrossPayrollRepository
{
    public GrossPayrollRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<GrossPayrollEmployee> GetSummaryWage(GrossPayroll payroll) => GetSummaryWage(payroll.Id);

    public IReadOnlyList<GrossPayrollEmployee> GetSummaryWage(Guid payrollId)
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .From("gross_payroll_employee as gpe")
            .Select("oe.id as employee_id")
            .Select("oe.item_name as employee_name")
            .SelectRaw("sum(gpe.wage) as wage")
            .Join("our_employee as oe", "oe.id", "gpe.employee_id")
            .Where("gpe.owner_id", payrollId)
            .GroupBy("oe.id")
            .Get<GrossPayrollEmployee>()
            .ToList();
    }

    protected override bool CreatingAdjustedQuery() => true;

    protected override GrossPayroll GetAdjustedQuery(Guid id, IDbConnection connection)
    {
        var payrollDictionary = new Dictionary<Guid, GrossPayroll>();

        string sql = @"
            select 
                gp.*, 
                gpe.*, 
                e.item_name as employee_name,
                ii.item_name as income_item_name
            from gross_payroll gp
                left join gross_payroll_employee gpe on gpe.owner_id = gp.id 
                left join our_employee e on e.id = gpe.employee_id 
                left join income_item ii on ii.id = gpe.income_item_id
            where gp.id = :id";

        return connection.Query<GrossPayroll, GrossPayrollEmployee, GrossPayroll>(
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
        var q = new Query("gross_payroll_employee as gpe")
            .Select("gpe.owner_id")
            .SelectRaw("sum(gpe.wage) as wage")
            .SelectRaw("array_agg(oe.item_name) as employee_names")
            .Join("our_employee as oe", "oe.id", "gpe.employee_id")
            .GroupBy("gpe.owner_id");

        return query
            .Select("gross_payroll.*")
            .Select("o.item_name as organization_name")
            .Select("q.wage")
            .Select("q.employee_names")
            .Join("organization as o", "o.id", "gross_payroll.organization_id")
            .LeftJoin(q.As("q"), j => j.On("q.owner_id", "gross_payroll.id"));
    }
}
