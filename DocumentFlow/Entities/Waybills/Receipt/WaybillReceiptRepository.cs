//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2022.8.31
//  - доработан метод GetDefaultQuery - если поле item_name таблицы
//    conttactor содержит null, то будет возвращено значение поля code
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Waybills;

public class WaybillReceiptRepository : DocumentRepository<WaybillReceipt>, IWaybillReceiptRepository
{
    public WaybillReceiptRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        var q = new Query("waybill_receipt_price")
            .Select("owner_id")
            .SelectRaw("sum([product_cost]) as [product_cost]")
            .SelectRaw("sum([tax_value]) as [tax_value]")
            .SelectRaw("sum([full_cost]) as [full_cost]")
            .GroupBy("owner_id");

        var p = new Query("posting_payments_receipt")
            .Select("document_id")
            .SelectRaw("sum([transaction_amount]) as [transaction_amount]")
            .WhereTrue("carried_out")
            .GroupBy("document_id");

        return query
            .Select("waybill_receipt.*")
            .Select("o.item_name as organization_name")
            .SelectRaw("case when c.item_name is null then c.code else c.item_name end as contractor_name")
            .Select("contract.tax_payer")
            .SelectRaw("case [contract].[tax_payer] when true then 20 else 0 end as [tax]")
            .Select("contract.item_name as contract_name")
            .Select("d.product_cost")
            .Select("d.tax_value")
            .Select("d.full_cost")
            .Select("p.transaction_amount as paid")
            .Join("organization as o", "o.id", "waybill_receipt.organization_id")
            .Join("contractor as c", "c.id", "waybill_receipt.contractor_id")
            .LeftJoin("contract", "contract.id", "waybill_receipt.contract_id")
            .LeftJoin(q.As("d"), j => j.On("d.owner_id", "waybill_receipt.id"))
            .LeftJoin(p.As("p"), j => j.On("p.document_id", "waybill_receipt.id"));
    }
}
