﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
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
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.PurchaseRequestLib;

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
                new { waybill_receipt_id = waybillReceipt.id, purchase_request_id = purchaseRequest?.id },
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
            .Select("d.{product_cost, tax_value, full_cost}")
            .Select("p.transaction_amount as paid")
            .Select("pr.document_number as purchase_request_number")
            .Select("pr.document_date as purchase_request_date")
            .Join("organization as o", "o.id", "waybill_receipt.organization_id")
            .Join("contractor as c", "c.id", "waybill_receipt.contractor_id")
            .LeftJoin("purchase_request as pr", "pr.id", "waybill_receipt.owner_id")
            .LeftJoin("contract", "contract.id", "waybill_receipt.contract_id")
            .LeftJoin(q.As("d"), j => j.On("d.owner_id", "waybill_receipt.id"))
            .LeftJoin(p.As("p"), j => j.On("p.document_id", "waybill_receipt.id"));
    }
}
