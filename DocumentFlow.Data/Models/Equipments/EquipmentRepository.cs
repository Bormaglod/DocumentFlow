//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Repository;

using SqlKata.Execution;

namespace DocumentFlow.Data.Models;

public class EquipmentRepository : Repository<Guid, Equipment>, IEquipmentRepository
{
    public EquipmentRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<Applicator> GetApplicators()
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .From("applicator_usage")
            .Get<Applicator>()
            .ToList();
    }
}
