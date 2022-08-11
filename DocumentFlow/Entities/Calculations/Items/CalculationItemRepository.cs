//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

using SqlKata.Execution;

namespace DocumentFlow.Entities.Calculations;

public class CalculationItemRepository<T> : OwnedRepository<Guid, T>
    where T : CalculationItem
{
    public CalculationItemRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
    }

    public decimal GetSumItemCost(Guid owner_id)
    {
        using var conn = Database.OpenConnection();
        return GetBaseQuery(conn)
            .AsSum("item_cost")
            .Where("owner_id", owner_id)
            .Get<decimal>()
            .First();
    }
}
