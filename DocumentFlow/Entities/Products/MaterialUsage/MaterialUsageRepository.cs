//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.05.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Products;

public class MaterialUsageRepository : OwnedRepository<Guid, MaterialUsage>, IMaterialUsageRepository
{
    public MaterialUsageRepository(IDatabase database) : base(database) { }

    protected override Query GetQueryOwner(Query query, Guid owner_id) => query.Where($"item_id", owner_id);

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query.From("calculation_material as cm")
            .Select("c.id")
            .Select("cm.item_id as owner_id")
            .Select("c.item_name as calculation_name")
            .Select("c.code as calculation_code")
            .Select("g.id as goods_id")
            .Select("g.code as goods_code")
            .Select("g.item_name as goods_name")
            .Select("cm.amount")
            .Join("calculation as c", "c.id", "cm.owner_id")
            .Join("goods as g", "g.id", "c.owner_id")
            .WhereRaw("[state] = 'approved'::calculation_state");
    }
}
