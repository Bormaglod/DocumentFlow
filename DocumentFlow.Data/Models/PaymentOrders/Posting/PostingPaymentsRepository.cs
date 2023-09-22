//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class PostingPaymentsRepository : DocumentRepository<PostingPayments>, IPostingPaymentsRepository
{
    public PostingPaymentsRepository(IDatabase database) : base(database) { }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query.FromRaw("posting_payments()");
    }

    protected override Query GetQueryOwner(Query query, Guid owner_id) => query.FromRaw("posting_payments(?)", new[] { owner_id });
}
