//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Banks;

public class BankRepository : Repository<Guid, Bank>, IBankRepository
{
    public BankRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
        ExcludeField(x => x.owner_id);
    }
}
