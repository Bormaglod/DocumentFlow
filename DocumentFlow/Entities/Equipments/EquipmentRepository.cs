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
// Версия 2023.2.10
//  - реализация GetApplicators изменена на использование GetViewList
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Equipments;

public class EquipmentRepository : Repository<Guid, Equipment>, IEquipmentRepository
{
    public EquipmentRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
        ExcludeField(x => x.owner_id);
    }

    public IReadOnlyList<Applicator> GetApplicators() => GetViewList<Applicator>("applicator_usage");
}
