//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class InitialBalanceMaterialRepository : DocumentRepository<InitialBalanceMaterial>, IInitialBalanceMaterialRepository
{
    public InitialBalanceMaterialRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("initial_balance_material.*")
            .Select("m.code as material_code")
            .Select("m.item_name as material_name")
            .Join("material as m", "m.id", "initial_balance_material.reference_id")
            .OrderBy("m.item_name");
    }
}
