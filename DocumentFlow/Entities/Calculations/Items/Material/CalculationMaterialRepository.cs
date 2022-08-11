//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Calculations;

public class CalculationMaterialRepository : CalculationItemRepository<CalculationMaterial>, ICalculationMaterialRepository
{
    public CalculationMaterialRepository(IDatabase database) : base(database) { }

    public void RecalculateCount(Guid calculate_id) => ExecuteSql("call recalculate_amount_material(:calculate_id)", new { calculate_id });

    public void RecalculatePrices(Guid calculate_id) => ExecuteSql("call make_prices_materials_relevant(:calculate_id)", new { calculate_id });

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("calculation_material.*")
            .Select("m.item_name as material_name")
            .Select("c.item_name as calculation_name")
            .SelectRaw("round([m].[weight] * [calculation_material].[amount], 3) as [weight]")
            .Join("calculation as c", "c.id", "calculation_material.owner_id")
            .LeftJoin("material as m", "m.id", "calculation_material.item_id");
    }

    private void ExecuteSql(string sql, object? param = null)
    {
        using var conn = Database.OpenConnection();
        conn.Execute(sql, param);
    }
}
