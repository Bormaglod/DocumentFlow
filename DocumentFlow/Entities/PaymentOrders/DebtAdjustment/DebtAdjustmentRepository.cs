//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.12.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.PaymentOrders;

public class DebtAdjustmentRepository : DocumentRepository<DebtAdjustment>, IDebtAdjustmentRepository
{
    public DebtAdjustmentRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.OwnerId);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        var price = GetSum("waybill_receipt_price", "owner_id", "full_cost");
        var pay = GetSum("posting_payments_receipt", "document_id", "transaction_amount");
        var prepay = GetSum("posting_payments_purchase", "document_id", "transaction_amount");

        return query
            .Select("debt_adjustment.*")
            .Select("o.item_name as organization_name")
            .Select("c.item_name as contractor_name")
            .Select("wr_debt.document_date as document_debt_date")
            .Select("wr_debt.document_number as document_debt_number")
            .Select("price_debt.amount as document_debt_amount")
            .SelectRaw("coalesce(pay_debt.amount, 0) + coalesce(pp_debt.amount, 0) as document_debt_payment")
            .Select("wr_credit.document_date as document_credit_date")
            .Select("wr_credit.document_number as document_credit_number")
            .Select("price_credit.amount as document_credit_amount")
            .SelectRaw("coalesce(pay_credit.amount, 0) + coalesce(pp_credit.amount, 0) as document_credit_payment")
            .Join("organization as o", "o.id", "organization_id")
            .Join("contractor as c", "c.id", "contractor_id")
            .LeftJoin("waybill_receipt as wr_debt", "wr_debt.id", "debt_adjustment.document_debt_id")
            .LeftJoin("waybill_receipt as wr_credit", "wr_credit.id", "debt_adjustment.document_credit_id")
            .LeftJoin("contract as c_debt", "c_debt.owner_id", "wr_debt.contract_id")
            .LeftJoin("contract as c_credit", "c_credit.owner_id", "wr_credit.contract_id")
            .LeftJoin(price.As("price_debt"), j => j.On("price_debt.doc_id", "debt_adjustment.document_debt_id"))
            .LeftJoin(price.As("price_credit"), j => j.On("price_credit.doc_id", "debt_adjustment.document_credit_id"))
            .LeftJoin(pay.As("pay_debt"), j => j.On("pay_debt.doc_id", "debt_adjustment.document_debt_id"))
            .LeftJoin(pay.As("pay_credit"), j => j.On("pay_credit.doc_id", "debt_adjustment.document_credit_id"))
            .LeftJoin(prepay.As("pp_debt"), j => j.On("pp_debt.doc_id", "wr_debt.owner_id"))
            .LeftJoin(prepay.As("pp_credit"), j => j.On("pp_credit.doc_id", "wr_credit.owner_id"));
    }

    private static Query GetSum(string table, string owner, string amount)
    {
        return new Query(table)
            .Select($"{owner} as doc_id")
            .SelectRaw($"sum({amount}) as amount")
            .GroupBy(owner);
    }
}
