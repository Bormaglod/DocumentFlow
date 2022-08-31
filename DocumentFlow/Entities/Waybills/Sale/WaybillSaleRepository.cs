//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2022.8.25
//  - добавлен метод CopyNestedRows
// Версия 2022.8.31
//  - доработан метод GetDefaultQuery - если поле item_name таблицы
//    conttactor содержит null, то будет возвращено значение поля code
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

using System.Data;

namespace DocumentFlow.Entities.Waybills;

public class WaybillSaleRepository : DocumentRepository<WaybillSale>, IWaybillSaleRepository
{
    public WaybillSaleRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.owner_id);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        var q = new Query("waybill_sale_price")
            .Select("owner_id")
            .SelectRaw("sum([product_cost]) as [product_cost]")
            .SelectRaw("sum([tax_value]) as [tax_value]")
            .SelectRaw("sum([full_cost]) as [full_cost]")
            .GroupBy("owner_id");

        return query
            .Select("waybill_sale.*")
            .Select("o.item_name as organization_name")
            .SelectRaw("case when c.item_name is null then c.code else c.item_name end as contractor_name")
            .Select("contract.tax_payer")
            .SelectRaw("case [contract].[tax_payer] when true then 20 else 0 end as [tax]")
            .Select("contract.item_name as contract_name")
            .Select("d.product_cost")
            .Select("d.tax_value")
            .Select("d.full_cost")
            .Join("organization as o", "o.id", "waybill_sale.organization_id")
            .Join("contractor as c", "c.id", "waybill_sale.contractor_id")
            .LeftJoin("contract", "contract.id", "waybill_sale.contract_id")
            .LeftJoin(q.As("d"), j => j.On("d.owner_id", "waybill_sale.id"));
    }

    protected override void CopyNestedRows(WaybillSale from, WaybillSale to, IDbTransaction transaction)
    {
        base.CopyNestedRows(from, to, transaction);

        string[] tables = new string[] { "goods", "material" };

        foreach (var table in tables)
        {
            var sql = $"insert into waybill_sale_price_{table} (owner_id, reference_id, amount, price, product_cost, tax, tax_value, full_cost) select :id_to, reference_id, amount, price, product_cost, tax, tax_value, full_cost from waybill_sale_price_{table} where owner_id = :id_from";
            transaction.Connection?.Execute(sql, new { id_to = to.id, id_from = from.id }, transaction: transaction);
        }
    }
}
