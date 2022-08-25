//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2022
//
// Версия 2022.8.25
//  - процедура CopyChilds заменена на процедуру CopyNestedRows
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Products;

using SqlKata;
using SqlKata.Execution;

using System.Data;

namespace DocumentFlow.Entities.Calculations;

public class CalculationRepository : OwnedRepository<Guid, Calculation>, ICalculationRepository
{
    public CalculationRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        var materials = new Query("calculation_material as cm")
            .Select("cm.owner_id")
            .SelectRaw("round(sum([m].[weight] * [cm].[amount]), 3) as [weight]")
            .Join("material as m", "m.id", "cm.item_id")
            .GroupBy("cm.owner_id");

        var operations = new Query("calculation_operation as co")
            .Select("co.owner_id")
            .SelectRaw("sum(round(3600.0 * [co].[repeats] / [o].[production_rate], 1)) as [produced_time]")
            .Join("operation as o", "o.id", "co.item_id")
            .GroupBy("co.owner_id");

        return query
            .Select("calculation.*")
            .Select("g.item_name as goods_name")
            .SelectRaw("coalesce([materials].[weight], 0) as [weight]")
            .Select("operations.produced_time")
            .Join("goods as g", "g.id", "calculation.owner_id")
            .LeftJoin(materials.As("materials"), j => j.On("materials.owner_id", "calculation.id"))
            .LeftJoin(operations.As("operations"), j => j.On("operations.owner_id", "calculation.id"));
    }

    public IReadOnlyList<Calculation> GetApproved(Goods goods)
    {
        using var conn = Database.OpenConnection();
        return GetBaseQuery(conn)
            .Select("id", "code as item_name")
            .Where("id", goods.calculation_id)
            .OrWhere(q => q
                .WhereFalse("deleted")
                .WhereRaw("[state] = 'approved'::calculation_state")
                .Where("owner_id", goods.id))
            .Get<Calculation>()
            .ToList();
    }

    protected override void CopyNestedRows(Calculation from, Calculation to, IDbTransaction transaction)
    {
        base.CopyNestedRows(from, to, transaction);
        
        var conn = transaction.Connection;
        if (conn == null)
        {
            return;
        }

        var sql = "insert into calculation_operation (owner_id, code, item_name, item_id, equipment_id, tools_id, material_id, material_amount, repeats, previous_operation, note) select :id_to, code, item_name, item_id, equipment_id, tools_id, material_id, material_amount, repeats, previous_operation, note from only calculation_operation where owner_id = :id_from";
        conn.Execute(sql, new { id_to = to.id, id_from = from.id }, transaction: transaction);

        sql = "insert into calculation_cutting (owner_id, code, item_name, item_id, equipment_id, tools_id, material_id, material_amount, repeats, previous_operation, note) select :id_to, code, item_name, item_id, equipment_id, tools_id, material_id, material_amount, repeats, previous_operation, note from calculation_cutting where owner_id = :id_from";
        conn.Execute(sql, new { id_to = to.id, id_from = from.id }, transaction: transaction);

        sql = "insert into calculation_deduction (owner_id, code, item_name, item_id, price, item_cost, value) select :id_to, code, item_name, item_id, price, item_cost, value from calculation_deduction where owner_id = :id_from";
        conn.Execute(sql, new { id_to = to.id, id_from = from.id }, transaction: transaction);
    }
}
