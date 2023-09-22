//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Tools;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class CalculationMaterialRepository : CalculationItemRepository<CalculationMaterial>, ICalculationMaterialRepository
{
    public CalculationMaterialRepository(IDatabase database) : base(database) { }

    public void RecalculateCount(Guid calculate_id) => Call("recalculate_amount_material", calculate_id);

    public void RecalculatePrices(Guid calculate_id) => Call("make_prices_materials_relevant", calculate_id);

    public IReadOnlyList<CalculationMaterial> GetOnlyGivingMaterials(Calculation calculation)
    {
        return GetListUserDefined(callback: q => q.WhereTrue("calculation_material.is_giving"));
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
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
        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            conn.Execute($"call {proc_name}(:calculate_id)", new { calculate_id }, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e, CurrentDatabase), e);
        }
    }
}
