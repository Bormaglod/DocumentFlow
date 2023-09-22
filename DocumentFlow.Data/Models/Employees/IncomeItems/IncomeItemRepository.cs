//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class IncomeItemRepository : Repository<Guid, IncomeItem>, IIncomeItemRepository
{
    public IncomeItemRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("id", "code")
            .SelectRaw("code || ', ' || item_name as item_name")
            .OrderBy("code");
    }
}
