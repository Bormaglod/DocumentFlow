//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;

using SqlKata;

namespace DocumentFlow.Controls.Filters;

public class WaybillReceiptFilter : DocumentFilter, IWaybillReceiptFilter
{
    public WaybillReceiptFilter(IOrganizationRepository orgs) : base(orgs)
    {
    }

    public override Query? CreateQuery(string tableName)
    {
        var query = base.CreateQuery(tableName);
        if (query != null)
        {
            return query.OrWhere(q => q.WhereRaw("full_cost + coalesce(debt.transaction_amount, 0) - coalesce(ppr.transaction_amount, 0) - coalesce(ppp.transaction_amount, 0) - coalesce(credit.transaction_amount, 0) != 0"));
        }
        
        return query;
    }
}
