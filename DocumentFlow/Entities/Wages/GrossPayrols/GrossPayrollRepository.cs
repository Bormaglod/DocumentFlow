//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.2.23
//  - добавлена ссылка на DocumentFlow.Core.Exceptions
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Wages;

public class GrossPayrollRepository : DocumentRepository<GrossPayroll>, IGrossPayrollRepository
{
    public GrossPayrollRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.owner_id);
    }

    public void CalculateEmployeeWages(Guid gross_id)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            conn.Execute("call calculate_employee_wages(:gross_id)",
                     new { gross_id },
                     transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public void CalculateEmployeeWages(GrossPayroll grossPayroll) => CalculateEmployeeWages(grossPayroll.id);

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
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
