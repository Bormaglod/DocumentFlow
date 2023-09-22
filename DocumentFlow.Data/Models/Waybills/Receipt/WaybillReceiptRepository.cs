//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Repository;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;

using SqlKata;

using System.Data;

namespace DocumentFlow.Data.Models;

public class WaybillReceiptRepository : DocumentRepository<WaybillReceipt>, IWaybillReceiptRepository
{
    private readonly IPurchaseRequestRepository purchaseRequestRepository;

    public WaybillReceiptRepository(IDatabase database, IPurchaseRequestRepository purchaseRequestRepository) : base(database)
    {
        this.purchaseRequestRepository = purchaseRequestRepository;
    }

    public IReadOnlyList<WaybillReceiptPrice> GetProductsFromPurchaseRequest(PurchaseRequest purchaseRequest)
    {
        using var conn = GetConnection();

        var list = new List<WaybillReceiptPrice>();
        foreach (var purchaseProduct in purchaseRequestRepository.GetProducts(purchaseRequest))
        {
            var product = new WaybillReceiptPrice();
            product.CopyFrom(purchaseProduct);
            list.Add(product);
        }

        return list;
    }

    public IReadOnlyList<WaybillReceipt> GetByContractor(Guid? contractorId)
    {
        return GetListUserDefined(callback: q => q
            .WhereTrue("waybill_receipt.carried_out")
            .WhereFalse("waybill_receipt.deleted")
            .Where("waybill_receipt.contractor_id", contractorId));
    }

    protected override bool CreatingAdjustedQuery() => true;

    protected override WaybillReceipt GetAdjustedQuery(Guid id, IDbConnection connection)
    {
        var wsDictionary = new Dictionary<Guid, WaybillReceipt>();

        string sql = @"
            select 
                wr.*, 
                wrp.*, 
                p.item_name as product_name, 
                p.code, 
                m.abbreviation as measurement_name
            from waybill_receipt wr
                left join waybill_receipt_price wrp on wrp.owner_id = wr.id 
                left join product p on p.id = wrp.reference_id 
                left join measurement m on m.id = p.measurement_id
            where wr.id = :id";

        return connection.Query<WaybillReceipt, WaybillReceiptPrice, WaybillReceipt>(
            sql,
            (waybill, price) =>
            {
                if (!wsDictionary.TryGetValue(waybill.Id, out var waybillEntry))
                {
                    waybillEntry = waybill;
                    wsDictionary.Add(waybill.Id, waybillEntry);
                }

                if (price != null)
                {
                    waybillEntry.Prices.Add(price);
                }

                return waybillEntry;
            },
            new { id })
            .First();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        var q = new Query("waybill_receipt_price")
            .Select("owner_id")
            .SelectRaw("sum(product_cost) as product_cost")
            .SelectRaw("sum(tax_value) as tax_value")
            .SelectRaw("sum(full_cost) as full_cost")
            .GroupBy("owner_id");

        var ppr = GetQuery("posting_payments_receipt", "document_id");
        var ppp = GetQuery("posting_payments_purchase", "document_id");
        var debt = GetQuery("debt_adjustment", "document_debt_id");
        var credit = GetQuery("debt_adjustment", "document_credit_id");

        return query
            .Select("waybill_receipt.*")
            .Select("o.item_name as organization_name")
            .SelectRaw("case when c.item_name is null then c.code else c.item_name end as contractor_name")
            .Select("contract.tax_payer")
            .SelectRaw("case contract.tax_payer when true then 20 else 0 end as tax")
            .Select("contract.item_name as contract_name")
            .Select("d.{product_cost, tax_value}")
            .SelectRaw("full_cost")
            .SelectRaw("coalesce(ppr.transaction_amount, 0) + coalesce(ppp.transaction_amount, 0) + coalesce(credit.transaction_amount, 0) - coalesce(debt.transaction_amount, 0) as paid")
            .Select("pr.document_number as purchase_request_number")
            .Select("pr.document_date as purchase_request_date")
            .Join("organization as o", "o.id", "waybill_receipt.organization_id")
            .Join("contractor as c", "c.id", "waybill_receipt.contractor_id")
            .LeftJoin("purchase_request as pr", "pr.id", "waybill_receipt.owner_id")
            .LeftJoin("contract", "contract.id", "waybill_receipt.contract_id")
            .LeftJoin(q.As("d"), j => j.On("d.owner_id", "waybill_receipt.id"))
            .LeftJoin(ppr.As("ppr"), j => j.On("ppr.document_id", "waybill_receipt.id"))
            .LeftJoin(ppp.As("ppp"), j => j.On("ppp.document_id", "pr.id"))
            .LeftJoin(debt.As("debt"), j => j.On("debt.document_debt_id", "waybill_receipt.id"))
            .LeftJoin(credit.As("credit"), j => j.On("credit.document_credit_id", "waybill_receipt.id"));
    }

    private static Query GetQuery(string table, string doc)
    {
        return new Query(table)
            .Select(doc)
            .SelectRaw("sum(transaction_amount) as transaction_amount")
            .WhereTrue("carried_out")
            .GroupBy(doc);
    }
}
