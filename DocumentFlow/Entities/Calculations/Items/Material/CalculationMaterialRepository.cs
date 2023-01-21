//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//
// Версия 2022.8.28
//  - процедура ExecuteSql заменена на Call
// Версия 2023.1.21
//  - добавлен метод GetOnlyGivingMaterials
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Calculations;

public class CalculationMaterialRepository : CalculationItemRepository<CalculationMaterial>, ICalculationMaterialRepository
{
    public CalculationMaterialRepository(IDatabase database) : base(database) { }

    public void RecalculateCount(Guid calculate_id) => Call("recalculate_amount_material", calculate_id);

    public void RecalculatePrices(Guid calculate_id) => Call("make_prices_materials_relevant", calculate_id);

    public IReadOnlyList<CalculationMaterial> GetOnlyGivingMaterials(Calculation calculation)
    {
        return GetAllDefault(callback: q => q.WhereTrue("calculation_material.is_giving"));
    }

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

    private void Call(string proc_name, Guid calculate_id)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            conn.Execute($"call {proc_name}(:calculate_id)", new { calculate_id }, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            ExceptionHelper.Message(e);
        }
    }
}
