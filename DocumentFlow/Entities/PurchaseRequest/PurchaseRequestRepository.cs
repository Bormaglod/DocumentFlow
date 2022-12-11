//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.02.2022
//
// Версия 2022.11.26
//  - в выборку добавлено поле executed возвращающее флаг наличия
//    поставок по заказу
// Версия 2022.12.11
//  - из запроса исключены не проведенные документы поставки
//  - добавлены методы Cancel, Complete и SetState
//  - добавлен метод CopyNestedRows
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

using System.Data;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public class PurchaseRequestRepository : DocumentRepository<PurchaseRequest>, IPurchaseRequestRepository
{
    public PurchaseRequestRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.owner_id);
    }

    public void Cancel(PurchaseRequest purchaseRequest) => SetState(purchaseRequest, PurchaseState.Canceled);

    public void Complete(PurchaseRequest purchaseRequest) => SetState(purchaseRequest, PurchaseState.Completed);

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        var d = new Query("purchase_request_price")
            .Select("owner_id")
            .SelectRaw("sum([product_cost]) as [cost_order]")
            .SelectRaw("sum([tax_value]) as [tax_value]")
            .SelectRaw("sum([full_cost]) as [full_cost]")
            .GroupBy("owner_id");

        var p = new Query("posting_payments_purchase")
            .Select("document_id")
            .SelectRaw("sum([transaction_amount]) as [transaction_amount]")
            .WhereTrue("carried_out")
            .GroupBy("document_id");

        return query
            .Select("purchase_request.*")
            .Select("o.item_name as organization_name")
            .Select("c.item_name as contractor_name")
            .Select("contract.tax_payer")
            .SelectRaw("case [contract].[tax_payer] when true then 20 else 0 end as [tax]")
            .Select("contract.item_name as contract_name")
            .Select("d.*")
            .Select("p.transaction_amount as paid")
            .SelectRaw("exists (select * from waybill_receipt wr where wr.owner_id = purchase_request.id and wr.carried_out) as executed")
            .Join("organization as o", "o.id", "purchase_request.organization_id")
            .Join("contractor as c", "c.id", "purchase_request.contractor_id")
            .LeftJoin("contract", "contract.id", "purchase_request.contract_id")
            .LeftJoin(d.As("d"), j => j.On("d.owner_id", "purchase_request.id"))
            .LeftJoin(p.As("p"), j => j.On("p.document_id", "purchase_request.id"));
    }

    protected override void CopyNestedRows(PurchaseRequest from, PurchaseRequest to, IDbTransaction transaction)
    {
        base.CopyNestedRows(from, to, transaction);

        var conn = transaction.Connection;
        if (conn == null)
        {
            return;
        }

        var sql = "insert into purchase_request_price (owner_id, reference_id, amount, price, product_cost, tax, tax_value, full_cost) select :id_to, prp.reference_id, prp.amount, prp.price, prp.product_cost, prp.tax, prp.tax_value, prp.full_cost from purchase_request_price prp where owner_id = :id_from";
        conn.Execute(sql, new { id_to = to.id, id_from = from.id }, transaction: transaction);
    }

    private void SetState(PurchaseRequest purchaseRequest, PurchaseState state)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            conn.Execute($"call set_purchase_request_state(:purchase_request_id, '{PurchaseRequest.StateFromValue(state)}'::purchase_state)",
                new { purchase_request_id = purchaseRequest.id },
                transaction);
            transaction.Commit();

            purchaseRequest.PurchaseState = state;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException($"Установка состояния заявки в {PurchaseRequest.StateNameFromValue(state)}.", e);
        }
    }
}
