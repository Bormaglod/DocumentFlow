//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Repository;

namespace DocumentFlow.Data.Models;

public class EmployeeWagesRepository : ReadOnlyRepository<Guid, EmployeeWages>, IEmployeeWagesRepository
{
    public EmployeeWagesRepository(IDatabase database) : base(database)
    {
    }
}
