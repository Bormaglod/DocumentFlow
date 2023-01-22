//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.05.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Infrastructure.Data;

using SqlKata;
using SqlKata.Execution;

using System.Data;

namespace DocumentFlow.Entities.Productions.Performed;

public class OperationsPerformedRepository : DocumentRepository<OperationsPerformed>, IOperationsPerformedRepository
{
    public OperationsPerformedRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<OurEmployee> GetWorkedEmployes(Guid? lot_id)
    {
        if (lot_id == null)
        {
            throw new ArgumentNullException(nameof(lot_id));
        }

        using var conn = Database.OpenConnection();
        var query = GetBaseQuery(conn)
            .Distinct()
            .Select("e.id", "e.item_name")
            .Join("our_employee as e", "e.id", "operations_performed.employee_id")
            .Where("operations_performed.owner_id", lot_id)
            .WhereTrue("operations_performed.carried_out");
        return query.Get<OurEmployee>().ToList();
    }

    public IReadOnlyList<OperationsPerformed> GetSummary(Guid lot_id)
    {
        using var conn = Database.OpenConnection();
        return QuerySummary(conn, lot_id).Get<OperationsPerformed>().ToList();
    }

    public OperationsPerformed? GetSummary(Guid lot_id, CalculationOperation operation, OurEmployee employee)
    {
        using var conn = Database.OpenConnection();
        return QuerySummary(conn, lot_id)
            .Where("operations_performed.operation_id", operation.id)
            .Where("operations_performed.employee_id", employee.id)
            .FirstOrDefault<OperationsPerformed>();
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("operations_performed.*")
            .Select("o.item_name as organization_name")
            .SelectRaw("'№' || po.document_number || ' от ' || to_char(po.document_date, 'DD.MM.YYYY') as order_name")
            .SelectRaw("'№' || pl.document_number || ' от ' || to_char(pl.document_date, 'DD.MM.YYYY') as lot_name")
            .Select("g.code as goods_name")
            .Select("c.id as calculation_id")
            .Select("c.code as calculation_name")
            .SelectRaw("case when co.item_name is null then op.item_name else co.item_name end AS operation_name")
            .Select("e.item_name as employee_name")
            .SelectRaw("case when rm.item_name is null then um.item_name else rm.item_name end as material_name")
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

    private Query QuerySummary(IDbConnection conn, Guid lot_id)
    {
        return GetBaseQuery(conn)
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
            .Where("operations_performed.owner_id", lot_id)
            .WhereTrue("operations_performed.carried_out")
            .GroupBy("operation_id", "co.code", "operation_name", "employee_id", "employee_name");
    }
}
