//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.PaymentOrders.Posting;

public class PostingPaymentsRepository : DocumentRepository<PostingPayments>, IPostingPaymentsRepository
{
    public PostingPaymentsRepository(IDatabase database) : base(database) { }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query.FromRaw("posting_payments()");
    }

    protected override Query GetQueryOwner(Query query, Guid owner_id) => query.FromRaw("posting_payments(?)", new[] { owner_id });
}
