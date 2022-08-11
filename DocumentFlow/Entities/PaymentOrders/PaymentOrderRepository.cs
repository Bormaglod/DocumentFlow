//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.PaymentOrders;

public class PaymentOrderRepository : DocumentRepository<PaymentOrder>, IPaymentOrderRepository
{
    public PaymentOrderRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.owner_id);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("payment_order.*")
            .Select("o.item_name as organization_name")
            .Select("c.item_name as contractor_name")
            .SelectRaw("iif([direction] = 'expense'::payment_direction, transaction_amount, NULL::numeric) as expense")
            .SelectRaw("iif([direction] = 'income'::payment_direction, transaction_amount, NULL::numeric) as income")
            .Join("organization as o", "o.id", "organization_id")
            .Join("contractor as c", "c.id", "contractor_id");
    }
}
