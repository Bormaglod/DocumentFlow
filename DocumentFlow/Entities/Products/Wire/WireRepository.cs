//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.06.2022
//
// Версия 2023.1.24
//  - IDatabase перенесён из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Products;

public class WireRepository : Repository<Guid, Wire>, IWireRepository
{
    public WireRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.ParentId);
        ExcludeField(x => x.OwnerId);
    }
}
