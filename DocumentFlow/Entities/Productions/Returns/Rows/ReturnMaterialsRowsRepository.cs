//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Productions.Returns;

public class ReturnMaterialsRowsRepository : OwnedRepository<long, ReturnMaterialsRows>, IReturnMaterialsRowsRepository
{
    public ReturnMaterialsRowsRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("return_materials_rows.*")
            .Select("m.item_name as material_name")
            .Join("material as m", "m.id", "return_materials_rows.material_id");
    }
}
