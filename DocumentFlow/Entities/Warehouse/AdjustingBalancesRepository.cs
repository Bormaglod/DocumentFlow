//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Warehouse;

public class AdjustingBalancesRepository : DocumentRepository<AdjustingBalances>, IAdjustingBalancesRepository
{
    public AdjustingBalancesRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.owner_id);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("adjusting_balances.*")
            .Select("o.item_name as organization_name")
            .Select("m.item_name as material_name")
            .Join("organization as o", "o.id", "adjusting_balances.organization_id")
            .Join("material as m", "m.id", "adjusting_balances.material_id");
    }
}
