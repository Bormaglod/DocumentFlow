//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Repository;

namespace DocumentFlow.Data.Models;

public class WireRepository : Repository<Guid, Wire>, IWireRepository
{
    public WireRepository(IDatabase database) : base(database)
    {
    }
}
