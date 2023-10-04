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

public class WaybillSaleRepository : DocumentRepository<WaybillSale>, IWaybillSaleRepository
{
    public WaybillSaleRepository(IDatabase database) : base(database)
    {
    }

    public IReadOnlyList<WaybillSale> GetByContractor(Guid? contractorId)
    {
        return GetListUserDefined(callback: q => q
            .WhereTrue("waybill_sale.carried_out")
            .WhereFalse("waybill_sale.deleted")
            .Where("waybill_sale.contractor_id", contractorId)
            .OrderBy("waybill_sale.document_date", "waybill_sale.document_number"));
    }

    protected override bool CreatingAdjustedQuery() => true;

    protected override WaybillSale GetAdjustedQuery(Guid id, IDbConnection connection)
    {
        var wsDictionary = new Dictionary<Guid, WaybillSale>();

        string sql = @"
            select 
                ws.*, 
                wsp.*, 
                p.item_name as product_name, 
                p.code, 
                m.abbreviation as measurement_name,
                regexp_replace(wsp.tableoid::regclass::varchar, '^.*_', '') as discriminator,
                '№' || pl.document_number || ' от ' || to_char(pl.document_date, 'DD.MM.YYYY') as lot_name
            from waybill_sale ws
                left join waybill_sale_price wsp on wsp.owner_id = ws.id 
                left join product p on p.id = wsp.reference_id 
                left join measurement m on m.id = p.measurement_id
                left join production_lot pl on pl.id = wsp.lot_id
            where ws.id = :id";

        return connection.Query<WaybillSale, WaybillSalePrice, WaybillSale>(
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
        var q = new Query("waybill_sale_price")
            .Select("owner_id")
            .SelectRaw("sum(product_cost) as product_cost")
            .SelectRaw("sum(tax_value) as tax_value")
            .SelectRaw("sum(full_cost) as full_cost")
            .GroupBy("owner_id");

        var p = new Query("posting_payments_sale")
            .Select("document_id")
            .SelectRaw("sum(transaction_amount) as transaction_amount")
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

    protected override void CopyNestedRows(IDbConnection connection, WaybillSale from, WaybillSale to, IDbTransaction? transaction = null)
    {
        base.CopyNestedRows(connection, from, to, transaction);

        string[] tables = new string[] { "goods", "material" };

        foreach (var table in tables)
        {
            var sql = $"insert into waybill_sale_price_{table} (owner_id, reference_id, amount, price, product_cost, tax, tax_value, full_cost) select :id_to, reference_id, amount, price, product_cost, tax, tax_value, full_cost from waybill_sale_price_{table} where owner_id = :id_from";
            connection.Execute(sql, new { id_to = to.Id, id_from = from.Id }, transaction: transaction);
        }
    }
}
