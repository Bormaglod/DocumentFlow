//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;
using SqlKata.Execution;

using System.Data;

namespace DocumentFlow.Data.Models;

public class OperationsPerformedRepository : DocumentRepository<OperationsPerformed>, IOperationsPerformedRepository
{
    public OperationsPerformedRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<OurEmployee> GetWorkedEmployes(ProductionLot lot)
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .Distinct()
            .Select("e.id", "e.item_name")
            .Join("our_employee as e", "e.id", "operations_performed.employee_id")
            .Where("operations_performed.owner_id", lot.Id)
            .WhereTrue("operations_performed.carried_out")
            .Get<OurEmployee>()
            .ToList();
    }

    public IReadOnlyList<OperationsPerformed> GetSummary(ProductionLot lot)
    {
        using var conn = GetConnection();
        return QuerySummary(conn, lot)
            .Get<OperationsPerformed>()
            .ToList();
    }

    public IReadOnlyList<WageEmployee> GetWages(BillingDocument billing) => GetWages(billing.BillingYear, billing.BillingMonth);

    public IReadOnlyList<WageEmployee> GetWages(int year, short month)
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .Select("employee_id")
            .SelectRaw("sum(salary) as wage")
            .WhereRaw("extract(year from document_date) = ?", year)
            .WhereRaw("extract(month from document_date) = ?", month)
            .WhereTrue("carried_out")
            .GroupBy("employee_id")
            .Get<WageEmployee>()
            .ToList();
    }

    public OperationsPerformed? GetSummary(ProductionLot lot, CalculationOperation operation, OurEmployee employee)
    {
        using var conn = GetConnection();
        return QuerySummary(conn, lot)
            .Where("operations_performed.operation_id", operation.Id)
            .Where("operations_performed.employee_id", employee.Id)
            .FirstOrDefault<OperationsPerformed>();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("operations_performed.*")
            .Select("o.item_name as organization_name")
            .SelectRaw("'№' || po.document_number || ' от ' || to_char(po.document_date, 'DD.MM.YYYY') as order_name")
            .SelectRaw("'№' || pl.document_number || ' от ' || to_char(pl.document_date, 'DD.MM.YYYY') as lot_name")
            .Select("g.code as goods_code")
            .Select("g.item_name as goods_name")
            .Select("c.id as calculation_id")
            .Select("c.code as calculation_name")
            .SelectRaw("case when co.item_name is null then op.item_name else co.item_name end AS operation_name")
            .Select("e.item_name as employee_name")
            .SelectRaw("iif(skip_material, null, coalesce(rm.item_name, um.item_name)) as material_name")
            .Join("organization as o", "o.id", "operations_performed.organization_id")
            .Join("production_lot as pl", "pl.id", "operations_performed.owner_id")
            .Join("production_order as po", "po.id", "pl.owner_id")
            .Join("calculation as c", "c.id", "pl.calculation_id")
            .Join("goods as g", "g.id", "c.owner_id")
            .Join("our_employee as e", "e.id", "operations_performed.employee_id")
            .Join("calculation_operation as co", "co.id", "operations_performed.operation_id")
            .Join("operation as op", "op.id", "co.item_id")
            .LeftJoin("material as um", "um.id", "co.material_id")
            .LeftJoin("material as rm", "rm.id", "operations_performed.replacing_material_id");
    }

    private Query QuerySummary(IDbConnection conn, ProductionLot lot)
    {
        return GetQuery(conn)
            .Select("operations_performed.operation_id")
            .Select("co.code as operation_code")
            .SelectRaw("case when co.item_name is null then op.item_name else co.item_name end AS operation_name")
            .Select("operations_performed.employee_id")
            .Select("e.item_name AS employee_name")
            .SelectRaw("sum(operations_performed.quantity) as quantity")
            .SelectRaw("sum(operations_performed.salary) as salary")
            .Join("our_employee as e", "e.id", "operations_performed.employee_id")
            .Join("calculation_operation as co", "co.id", "operations_performed.operation_id")
            .Join("operation as op", "op.id", "co.item_id")
            .Where("operations_performed.owner_id", lot.Id)
            .WhereTrue("operations_performed.carried_out")
            .GroupBy("operation_id", "co.code", "operation_name", "employee_id", "employee_name");
    }
}
