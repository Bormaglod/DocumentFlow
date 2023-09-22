//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;

using SqlKata;

namespace DocumentFlow.Controls.Filters;

public class PurchaseRequestFilter : DocumentFilter, IPurchaseRequestFilter
{
    public PurchaseRequestFilter(IOrganizationRepository orgs) : base(orgs)
    {
    }

    public override Query? CreateQuery(string tableName)
    {
        var query = base.CreateQuery(tableName);
        if (query != null)
        {
            return query.OrWhere(q => q.WhereRaw("state = 'active'::purchase_state"));
        }

        return query;
    }
}
