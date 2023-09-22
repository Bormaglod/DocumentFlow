//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.02.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;
using DocumentFlow.Tools;

using Humanizer;

using SqlKata;

using System.Data;

namespace DocumentFlow.Data.Models;

public class PurchaseRequestRepository : DocumentRepository<PurchaseRequest>, IPurchaseRequestRepository
{
    public PurchaseRequestRepository(IDatabase database) : base(database)
    {
    }

    public void Cancel(PurchaseRequest purchaseRequest) => SetState(purchaseRequest, PurchaseState.Canceled);

    public void Complete(PurchaseRequest purchaseRequest) => SetState(purchaseRequest, PurchaseState.Completed);

    public IReadOnlyList<PurchaseRequest> GetByContractor(Guid? contractorId)
    {
        return GetListUserDefined(callback: q => q
            .WhereTrue("carried_out")
            .WhereFalse("deleted")
            .Where("contractor_id", contractorId))
            .OrderBy(x => x.DocumentDate)
            .ThenBy(x => x.DocumentNumber)
            .ToList();
    }

    public IReadOnlyList<PurchaseRequest> GetActiveByContractor(Guid contractorId, Guid? purchaseId)
    {
        return GetListUserDefined(callback: q => q
            .Where(q => q
                .WhereTrue("carried_out")
                .WhereFalse("deleted")
                .WhereRaw($"state = '{PurchaseState.Active.ToString().Humanize(LetterCasing.LowerCase)}'::purchase_state")
                .Where("contractor_id", contractorId))
            .When(purchaseId != null, q => q.Or().Where("id", purchaseId)))
            .OrderBy(x => x.DocumentDate)
            .ThenBy(x => x.DocumentNumber)
            .ToList();
    }

    public IReadOnlyList<PurchaseRequest> GetByContractor(Guid? contractorId, PurchaseState? state)
    {
        return GetListUserDefined(callback: q => q
            .WhereTrue("carried_out")
            .WhereFalse("deleted")
            .When(state != null, q => q.WhereRaw($"state = '{PurchaseRequest.StateFromValue(state!.Value)}'::purchase_state"))
            .Where("contractor_id", contractorId))
            .OrderBy(x => x.DocumentDate)
            .ThenBy(x => x.DocumentNumber)
            .ToList();
    }

    public IReadOnlyList<PurchaseRequestPrice> GetProducts(PurchaseRequest purchaseRequest)
    {
        using var conn = GetConnection();

        return conn.Query<PurchaseRequestPrice>("select * from purchase_request_price where owner_id = :id", new { id = purchaseRequest.Id }).ToList();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter) => query.From("purchase_request_receipt");

    protected override bool CreatingAdjustedQuery() => true;

    protected override PurchaseRequest GetAdjustedQuery(Guid id, IDbConnection connection)
    {
        var prDictionary = new Dictionary<Guid, PurchaseRequest>();

        string sql = @"
            select 
                pr.*, prp.*, m.item_name as product_name, m.code, m2.abbreviation as measurement_name
            from purchase_request pr
                left join purchase_request_price prp on prp.owner_id = pr.id 
                left join material m on m.id = prp.reference_id 
                left join measurement m2 on m2.id = m.measurement_id
            where pr.id = :id";

        return connection.Query<PurchaseRequest, PurchaseRequestPrice, PurchaseRequest>(
            sql,
            (purchase, price) =>
            {
                if (!prDictionary.TryGetValue(purchase.Id, out var purchaseEntry))
                {
                    purchaseEntry = purchase;
                    prDictionary.Add(purchase.Id, purchaseEntry);
                }

                if (price != null)
                {
                    purchaseEntry.Prices.Add(price);
                }

                return purchaseEntry;
            },
            new { id })
            .First();
    }

    protected override void CopyNestedRows(IDbConnection connection, PurchaseRequest from, PurchaseRequest to, IDbTransaction? transaction = null)
    {
        base.CopyNestedRows(connection, from, to, transaction);

        var sql = "insert into purchase_request_price (owner_id, reference_id, amount, price, product_cost, tax, tax_value, full_cost) select :id_to, prp.reference_id, prp.amount, prp.price, prp.product_cost, prp.tax, prp.tax_value, prp.full_cost from purchase_request_price prp where owner_id = :id_from";
        connection.Execute(sql, new { id_to = to.Id, id_from = from.Id }, transaction: transaction);
    }

    private void SetState(PurchaseRequest purchaseRequest, PurchaseState state)
    {
        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            conn.Execute($"call set_purchase_request_state(:purchase_request_id, '{PurchaseRequest.StateFromValue(state)}'::purchase_state)",
                new { purchase_request_id = purchaseRequest.Id },
                transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException($"Не удалось выполнить установку состояния заявки в {state.Description()}.", e);
        }
    }
}
