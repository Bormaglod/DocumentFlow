//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2022.8.25
//  - добавлен метод CopyNestedRows
// Версия 2022.8.31
//  - доработан метод GetDefaultQuery - если поле item_name таблицы
//    conttactor содержит null, то будет возвращено значение поля code
// Версия 2022.12.1
//  - в выборку добавлено поле paid
// Версия 2022.12.17
//  - добавлен метод GetByContractor
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.6.3
//  - в выборку добавлено поле payment_date
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

using System.Data;

namespace DocumentFlow.Entities.Waybills;

public class WaybillSaleRepository : DocumentRepository<WaybillSale>, IWaybillSaleRepository
{
    public WaybillSaleRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.OwnerId);
    }

    public IReadOnlyList<WaybillSale> GetByContractor(Guid? contractorId)
    {
        return GetListUserDefined(callback: q => q
            .WhereTrue("waybill_sale.carried_out")
            .WhereFalse("waybill_sale.deleted")
            .Where("waybill_sale.contractor_id", contractorId)
            .OrderBy("waybill_sale.document_date", "waybill_sale.document_number"));
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        var q = new Query("waybill_sale_price")
            .Select("owner_id")
            .SelectRaw("sum(product_cost) as product_cost")
            .SelectRaw("sum(tax_value) as tax_value")
            .SelectRaw("sum(full_cost) as full_cost")
            .GroupBy("owner_id");

        var p = new Query("posting_payments_sale")
            .Select("document_id")
            .SelectRaw("sum([transaction_amount]) as [transaction_amount]")
            .WhereTrue("carried_out")
            .GroupBy("document_id");

        return query
            .Select("waybill_sale.*")
            .Select("o.item_name as organization_name")
            .SelectRaw("case when c.item_name is null then c.code else c.item_name end as contractor_name")
            .Select("contract.tax_payer")
            .SelectRaw("case contract.tax_payer when true then 20 else 0 end as tax")
            .Select("contract.item_name as contract_name")
            .Select("d.{product_cost, tax_value, full_cost}")
            .Select("p.transaction_amount as paid")
            .SelectRaw("waybill_sale.document_date::date + contract.payment_period as payment_date")
            .Join("organization as o", "o.id", "waybill_sale.organization_id")
            .Join("contractor as c", "c.id", "waybill_sale.contractor_id")
            .LeftJoin("contract", "contract.id", "waybill_sale.contract_id")
            .LeftJoin(q.As("d"), j => j.On("d.owner_id", "waybill_sale.id"))
            .LeftJoin(p.As("p"), j => j.On("p.document_id", "waybill_sale.id"));
    }

    protected override void CopyNestedRows(WaybillSale from, WaybillSale to, IDbTransaction transaction)
    {
        base.CopyNestedRows(from, to, transaction);

        string[] tables = new string[] { "goods", "material" };

        foreach (var table in tables)
        {
            var sql = $"insert into waybill_sale_price_{table} (owner_id, reference_id, amount, price, product_cost, tax, tax_value, full_cost) select :id_to, reference_id, amount, price, product_cost, tax, tax_value, full_cost from waybill_sale_price_{table} where owner_id = :id_from";
            transaction.Connection?.Execute(sql, new { id_to = to.Id, id_from = from.Id }, transaction: transaction);
        }
    }
}
