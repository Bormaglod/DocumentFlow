//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class OperationUsageRepository : OwnedRepository<Guid, OperationUsage>, IOperationUsageRepository
{
    public OperationUsageRepository(IDatabase database) : base(database) { }

    protected override Query GetQueryOwner(Query query, Guid owner_id) => query.Where($"item_id", owner_id);

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query.From("calculation_operation as co")
            .Select("c.id")
            .Select("co.item_id as owner_id")
            .Select("c.item_name as calculation_name")
            .Select("c.code as calculation_code")
            .Select("g.id as goods_id")
            .Select("g.code as goods_code")
            .Select("g.item_name as goods_name")
            .SelectRaw("sum(co.repeats) as amount")
            .Join("calculation as c", "c.id", "co.owner_id")
            .Join("goods as g", "g.id", "c.owner_id")
            .WhereRaw("[state] = 'approved'::calculation_state")
            .GroupBy("c.id", "co.item_id", "c.item_name", "c.code", "g.id", "g.code", "g.item_name");
    }
}
