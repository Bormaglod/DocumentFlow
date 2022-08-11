//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.OperationTypes;

public class OperationTypeRepository : Repository<Guid, OperationType>, IOperationTypeRepository
{
    public OperationTypeRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
        ExcludeField(x => x.owner_id);
    }
}
