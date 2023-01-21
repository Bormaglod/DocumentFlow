//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//
// Версия 2023.1.21
//  - добавлен метод GetPaymentBalance
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Entities.PaymentOrders;

public class PaymentOrderRepository : DocumentRepository<PaymentOrder>, IPaymentOrderRepository
{
    public PaymentOrderRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.owner_id);
    }

    public decimal GetPaymentBalance(PaymentOrder order)
    {
        using var conn = Database.OpenConnection();
        var balance = GetBaseQuery(conn)
            .SelectRaw("sum(pp.transaction_amount)")
            .Join("posting_payments as pp", "pp.owner_id", "payment_order.id")
            .GroupBy("pp.owner_id")
            .Where("payment_order.id", order.id)
            .WhereTrue("pp.carried_out")
            .First<decimal>();
        return order.transaction_amount - balance;
    }

    public decimal GetPaymentBalance(Guid orderId)
    {
        using var conn = Database.OpenConnection();
        return GetBaseQuery(conn)
            .SelectRaw("payment_order.transaction_amount - sum(pp.transaction_amount)")
            .Join("posting_payments as pp", "pp.owner_id", "payment_order.id")
            .GroupBy("payment_order.id")
            .Where("payment_order.id", orderId)
            .WhereTrue("pp.carried_out")
            .First<decimal>();
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
