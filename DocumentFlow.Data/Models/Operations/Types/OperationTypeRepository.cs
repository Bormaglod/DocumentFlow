//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Repository;

namespace DocumentFlow.Data.Models;

public class OperationTypeRepository : Repository<Guid, OperationType>, IOperationTypeRepository
{
    public OperationTypeRepository(IDatabase database) : base(database)
    {
    }
}
