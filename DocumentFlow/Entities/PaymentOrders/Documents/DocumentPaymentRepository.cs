//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.02.2022
//
// Версия 2022.12.17
//  - запрос возвращал документы оплаты отглсящиеся только к текущему,
//    однако документ поступления может содержать не только оплату
//    по самому документу, но и предоплату по заявке на закупку материалов.
//
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
            .FromRaw($"get_list_payments('{owner_id}')");
    }
}
