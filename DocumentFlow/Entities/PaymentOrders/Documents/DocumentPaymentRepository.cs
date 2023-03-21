//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.02.2022
//
// Версия 2022.12.17
//  - запрос возвращал документы оплаты отглсящиеся только к текущему,
//    однако документ поступления может содержать не только оплату
//    по самому документу, но и предоплату по заявке на закупку материалов.
// Версия 2023.1.24
//  - IDatabase перенесён из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

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
