//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.1.28
//  - перемкщено из DocumentFlow.Entities.Wages.IncomeItems в
//    DocumentFlow.Entities.Employees.IncomeItems
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Employees.IncomeItems;

public class IncomeItemRepository : Repository<Guid, IncomeItem>, IIncomeItemRepository
{
    public IncomeItemRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("id", "code")
            .SelectRaw("code || ', ' || item_name as item_name")
            .OrderBy("code");
    }
}
