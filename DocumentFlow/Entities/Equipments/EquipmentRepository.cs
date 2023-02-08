//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2023.1.24
//  - IDatabase перенесён из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
// Версия 2023.2.8
//  - добавлен метод GetApplicators
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata.Execution;

namespace DocumentFlow.Entities.Equipments;

public class EquipmentRepository : Repository<Guid, Equipment>, IEquipmentRepository
{
    public EquipmentRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
        ExcludeField(x => x.owner_id);
    }

    public IReadOnlyList<Applicator> GetApplicators()
    {
        using var conn = Database.OpenConnection();
        return GetBaseQuery(conn)
            .Select("e.{id, item_name, commissioning, starting_hits}")
            .SelectRaw("sum(op.quantity) as quantity")
            .Join("calculation_operation as co", "co.id", "op.operation_id")
            .Join("equipment as e", "e.id", "co.tools_id")
            .WhereStarts("e.item_name", "Апп%")
            .GroupBy("e.id")
            .Get<Applicator>()
            .ToList();
    }
}
