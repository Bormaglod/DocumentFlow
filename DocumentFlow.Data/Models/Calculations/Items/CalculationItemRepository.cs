//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Repository;

using SqlKata.Execution;

namespace DocumentFlow.Data.Models;

public abstract class CalculationItemRepository<T> : OwnedRepository<Guid, T>
    where T : CalculationItem
{
    public CalculationItemRepository(IDatabase database) : base(database)
    {
    }

    public decimal GetSumItemCost(Guid owner_id)
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .AsSum("item_cost")
            .Where("owner_id", owner_id)
            .Get<decimal>()
            .First();
    }
}
