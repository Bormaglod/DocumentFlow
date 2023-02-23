//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
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
// Версия 2022.12.17
//  - запрос заменен на использование представления purchase_request_receipt
//  - добавлен метод GetByContractor(Guid?)
// Версия 2022.12.21
//  - добавлен метод GetByContractor(Guid?, PurchaseState?)
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.2.23
//  - добавлена ссылка на DocumentFlow.Core.Exceptions
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

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

    public IReadOnlyList<PurchaseRequest> GetByContractor(Guid? contractorId)
    {
        return GetAllDefault(callback: q => q
            .WhereTrue("carried_out")
            .WhereFalse("deleted")
            .Where("contractor_id", contractorId));
    }

    public IReadOnlyList<PurchaseRequest> GetByContractor(Guid? contractorId, PurchaseState? state)
    {
        return GetAllDefault(callback: q => q
            .WhereTrue("carried_out")
            .WhereFalse("deleted")
            .When(state != null, q => q.WhereRaw($"state = '{PurchaseRequest.StateFromValue(state!.Value)}'::purchase_state"))
            .Where("contractor_id", contractorId));
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter) => query.From("purchase_request_receipt");
   
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
