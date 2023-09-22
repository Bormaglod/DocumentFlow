//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;

using SqlKata;

namespace DocumentFlow.Controls.Filters;

public class ProductionLotFilter : DocumentFilter, IProductionLotFilter
{
    public ProductionLotFilter(IOrganizationRepository orgs) : base(orgs)
    {
    }

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
