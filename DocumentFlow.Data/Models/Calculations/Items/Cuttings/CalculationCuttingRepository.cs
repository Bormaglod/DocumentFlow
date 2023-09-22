//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using SqlKata;

namespace DocumentFlow.Data.Models;

public class CalculationCuttingRepository : CalculationItemRepository<CalculationCutting>, ICalculationCuttingRepository
{
    public CalculationCuttingRepository(IDatabase database) : base(database) { }

    public void RecalculatePrices(Guid calculate_id)
    {
        using var conn = GetConnection();
        conn.Execute("call make_prices_operations_relevant(:calculate_id)", new { calculate_id });
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        var using_operations = new Query("calculation_operation as co")
            .SelectRaw("array_agg([co].[code])")
            .WhereRaw("[calculation_cutting].[code] = any ([co].[previous_operation])")
            .WhereColumns("co.owner_id", "=", "calculation_cutting.owner_id");

        return query
            .Select("calculation_cutting.*")
            .Select("calculation_cutting.material_amount as total_material")
            .Select("o.item_name as operation_name")
            .Select("c.code as calculation_name")
            .Select("e.item_name as equipment_name")
            .Select("t.item_name as tools_name")
            .Select("m.code as material_name")
            .SelectRaw("round((3600 * [calculation_cutting].[repeats])::numeric / [o].[production_rate], 1) as [produced_time]")
            .SelectRaw("[calculation_cutting].[material_amount] * [calculation_cutting].[repeats] as [total_material]")
            .Select(using_operations, "using_operations")
            .Join("calculation as c", "c.id", "calculation_cutting.owner_id")
            .LeftJoin("operation as o", "o.id", "calculation_cutting.item_id")
            .LeftJoin("equipment as e", "e.id", "calculation_cutting.equipment_id")
            .LeftJoin("equipment as t", "t.id", "calculation_cutting.tools_id")
            .LeftJoin("material as m", "m.id", "calculation_cutting.material_id");
    }
}
