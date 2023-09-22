//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;
using SqlKata.Execution;

using System.Data;

namespace DocumentFlow.Data.Models;

public class CalculationRepository : OwnedRepository<Guid, Calculation>, ICalculationRepository
{
    public CalculationRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        var materials = new Query("calculation_material as cm")
            .Select("cm.owner_id")
            .SelectRaw("round(sum(m.weight * cm.amount), 3) as weight")
            .Join("material as m", "m.id", "cm.item_id")
            .GroupBy("cm.owner_id");

        var operations = new Query("calculation_operation as co")
            .Select("co.owner_id")
            .SelectRaw("sum(round(3600.0 * co.repeats / o.production_rate, 1)) as produced_time")
            .Join("operation as o", "o.id", "co.item_id")
            .GroupBy("co.owner_id");

        return query
            .Select("calculation.*")
            .Select("g.item_name as goods_name")
            .SelectRaw("coalesce(materials.weight, 0) as weight")
            .Select("operations.produced_time")
            .Join("goods as g", "g.id", "calculation.owner_id")
            .LeftJoin(materials.As("materials"), j => j.On("materials.owner_id", "calculation.id"))
            .LeftJoin(operations.As("operations"), j => j.On("operations.owner_id", "calculation.id"));
    }

    public IReadOnlyList<Calculation> GetApproved(Goods goods)
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .Select("id", "code as item_name")
            .When(goods.CalculationId != null, q => q
                .Where("id", goods.CalculationId))
            .OrWhere(q => q
                .WhereFalse("deleted")
                .WhereRaw("[state] = 'approved'::calculation_state")
                .Where("owner_id", goods.Id))
            .Get<Calculation>()
            .ToList();
    }

    protected override void CopyNestedRows(IDbConnection connection, Calculation from, Calculation to, IDbTransaction? transaction = null)
    {
        base.CopyNestedRows(connection, from, to, transaction);
        
        var sql = "insert into calculation_operation (owner_id, code, item_name, item_id, equipment_id, tools_id, material_id, material_amount, repeats, previous_operation, note) select :id_to, code, item_name, item_id, equipment_id, tools_id, material_id, material_amount, repeats, previous_operation, note from only calculation_operation where owner_id = :id_from";
        connection.Execute(sql, new { id_to = to.Id, id_from = from.Id }, transaction: transaction);

        sql = "insert into calculation_cutting (owner_id, code, item_name, item_id, equipment_id, tools_id, material_id, material_amount, repeats, previous_operation, note) select :id_to, code, item_name, item_id, equipment_id, tools_id, material_id, material_amount, repeats, previous_operation, note from calculation_cutting where owner_id = :id_from";
        connection.Execute(sql, new { id_to = to.Id, id_from = from.Id }, transaction: transaction);

        sql = "insert into calculation_deduction (owner_id, code, item_name, item_id, price, item_cost, value) select :id_to, code, item_name, item_id, price, item_cost, value from calculation_deduction where owner_id = :id_from";
        connection.Execute(sql, new { id_to = to.Id, id_from = from.Id }, transaction: transaction);
    }
}
