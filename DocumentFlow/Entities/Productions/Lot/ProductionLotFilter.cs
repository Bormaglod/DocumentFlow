//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.01.2023
//
// Версия 2023.1.17
//  - DocumentFlow.Controls заменен на DocumentFlow.Controls.Filters
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Filters;

using SqlKata;

namespace DocumentFlow.Entities.Productions.Lot;

public class ProductionLotFilter : DocumentFilter, IProductionLotFilter
{
    public override Query? CreateQuery(string tableName)
    {
        var query = base.CreateQuery(tableName);
        if (query != null)
        {
            return query.OrWhere(q => q.WhereRaw("production_lot.state in ('created'::lot_state, 'production'::lot_state)"));
        }
        
        return query;
    }
}
