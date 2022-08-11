//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceMaterialRepository : DocumentRepository<InitialBalanceMaterial>, IInitialBalanceMaterialRepository
{
    public InitialBalanceMaterialRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("initial_balance_material.*")
            .Select("m.code as material_code")
            .Select("m.item_name as material_name")
            .Join("material as m", "m.id", "initial_balance_material.reference_id")
            .OrderBy("m.item_name");
    }
}
