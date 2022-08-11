//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

using SqlKata;

namespace DocumentFlow.Entities.PaymentOrders.Documents;

public class DocumentPaymentRepository : DocumentRepository<DocumentPayment>, IDocumentPaymentRepository
{
    public DocumentPaymentRepository(IDatabase database) : base(database) { }

    protected override Query GetQueryOwner(Query query, Guid owner_id)
    {
        return query
            .From("payment_order")
            .Select("payment_order.*")
            .Select("pp.transaction_amount as posting_transaction")
            .Select("pp.document_id")
            .Join("posting_payments as pp", "pp.owner_id", "payment_order.id")
            .Where("document_id", owner_id)
            .WhereTrue("payment_order.carried_out")
            .WhereTrue("pp.carried_out");
    }
}
