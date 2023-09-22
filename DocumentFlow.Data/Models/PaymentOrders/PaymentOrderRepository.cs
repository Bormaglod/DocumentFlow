//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Data.Models;

public class PaymentOrderRepository : DocumentRepository<PaymentOrder>, IPaymentOrderRepository
{
    public PaymentOrderRepository(IDatabase database) : base(database)
    {
    }

    public decimal GetPaymentBalance(PaymentOrder order)
    {
        using var conn = GetConnection();
        var balance = GetQuery(conn)
            .SelectRaw("sum(pp.transaction_amount)")
            .Join("posting_payments as pp", "pp.owner_id", "payment_order.id")
            .GroupBy("pp.owner_id")
            .Where("payment_order.id", order.Id)
            .WhereTrue("pp.carried_out")
            .First<decimal>();
        return order.TransactionAmount - balance;
    }

    public decimal GetPaymentBalance(Guid? orderId)
    {
        if (orderId == null) 
        {
            return 0;
        }

        using var conn = GetConnection();
        return GetQuery(conn)
            .SelectRaw("payment_order.transaction_amount - coalesce(sum(pp.transaction_amount), 0)")
            .LeftJoin("posting_payments as pp", q => q.WhereColumns("pp.owner_id", "=", "payment_order.id").WhereTrue("pp.carried_out"))
            .GroupBy("payment_order.id")
            .Where("payment_order.id", orderId)
            .First<decimal>();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
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
