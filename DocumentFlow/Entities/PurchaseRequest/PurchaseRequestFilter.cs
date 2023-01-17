//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.12.2022
//
// Версия 2023.1.17
//  - DocumentFlow.Controls заменен на DocumentFlow.Controls.Filters
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Filters;

using SqlKata;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public class PurchaseRequestFilter : DocumentFilter, IPurchaseRequestFilter
{
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
