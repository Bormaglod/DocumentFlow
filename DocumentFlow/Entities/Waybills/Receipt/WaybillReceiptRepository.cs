//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2022.8.31
//  - доработан метод GetDefaultQuery - если поле item_name таблицы
//    conttactor содержит null, то будет возвращено значение поля code
// Версия 2022.11.26
//  - в выборку добавлены поля document_number и  document_date из 
//    таблицы purchase_request
// Версия 2022.12.11
//  - добавлен метод FillProductListFromPurchaseRequest
// Версия 2022.12.17
//  - добавлен метод GetByContractor
// Версия 2022.12.18
//  - изменен расчёт суммы оплаты за поставку - добавлена предоплата
// Версия 2022.12.26
//  - добавлен метод GetQuery
//  - в выборке суммы поступления и оплаты скорректированы на сумму
//    корректировки долга
// Версия 2023.1.19
//  - суммы поступления и оплаты были неверно скорректированы - исправлено
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.2.23
//  - добавлена ссылка на DocumentFlow.Core.Exceptions
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Repositiry;
using DocumentFlow.Entities.PurchaseRequestLib;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Waybills;

public class WaybillReceiptRepository : DocumentRepository<WaybillReceipt>, IWaybillReceiptRepository
{
    public WaybillReceiptRepository(IDatabase database) : base(database)
    {
    }

    public void FillProductListFromPurchaseRequest(WaybillReceipt waybillReceipt, PurchaseRequest? purchaseRequest)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            conn.Execute("call populate_waybill_receipt(:waybill_receipt_id, :purchase_request_id)",
                new { waybill_receipt_id = waybillReceipt.Id, purchase_request_id = purchaseRequest?.Id },
                transaction);

            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public IReadOnlyList<WaybillReceipt> GetByContractor(Guid? contractorId)
    {
        return GetAllDefault(callback: q => q
            .WhereTrue("waybill_receipt.carried_out")
            .WhereFalse("waybill_receipt.deleted")
            .Where("waybill_receipt.contractor_id", contractorId));
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
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
