//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repository;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class AdjustingBalancesRepository : DocumentRepository<AdjustingBalances>, IAdjustingBalancesRepository
{
    public AdjustingBalancesRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("adjusting_balances.*")
            .Select("o.item_name as organization_name")
            .Select("m.item_name as material_name")
            .Join("organization as o", "o.id", "adjusting_balances.organization_id")
            .Join("material as m", "m.id", "adjusting_balances.material_id");
    }
}
