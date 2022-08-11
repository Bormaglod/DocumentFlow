//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.PaymentOrders.Posting;

public class PostingPaymentsRepository : DocumentRepository<PostingPayments>, IPostingPaymentsRepository
{
    public PostingPaymentsRepository(IDatabase database) : base(database) { }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query.FromRaw("posting_payments()");
    }

    protected override Query GetQueryOwner(Query query, Guid owner_id) => query.FromRaw("posting_payments(?)", new[] { owner_id });
}
