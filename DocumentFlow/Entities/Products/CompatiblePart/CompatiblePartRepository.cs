//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.05.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Products;

public class CompatiblePartRepository : OwnedRepository<long, CompatiblePart>, ICompatiblePartRepository
{
    public CompatiblePartRepository(IDatabase database) : base(database) { }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("compatible_part.*")
            .Select("m.code as code")
            .Select("m.item_name as name")
            .Join("material as m", "m.id", "compatible_part.compatible_id");
    }
}
