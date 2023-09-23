//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class DocumentPaymentRepository : DocumentRepository<DocumentPayment>, IDocumentPaymentRepository
{
    public DocumentPaymentRepository(IDatabase database) : base(database) { }

    protected override Query GetQueryOwner(Query query, Guid owner_id)
    {
        return query
            .FromRaw($"get_list_payments('{owner_id}') as document_payment");
    }
}
