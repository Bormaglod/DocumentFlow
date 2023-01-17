﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//
// Версия 2022.9.2
//  - доработан метод GetDefaultQuery - если поле item_name таблицы
//    conttactor содержит null, то будет возвращено значение поля code
// Версия 2023.1.15
//  - добавлен метод GetWithReturnMaterial
// Версия 2023.1.17
//  - изменён запрос в GetWithReturnMaterial в связи с удалением
//    поля written_off из waybill_processing_price и добавлением
//    таблицы waybill_processing_writeoff, которая теперь хранит
//    записи о списании давальческого материала
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Accounts;
using DocumentFlow.Entities.Companies;
using Microsoft.Extensions.DependencyInjection;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Entities.Productions.Order;

public class ProductionOrderRepository : DocumentRepository<ProductionOrder>, IProductionOrderRepository
{
    public ProductionOrderRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.owner_id);
    }

    public IReadOnlyList<ProductionOrderPrice> GetList(ProductionOrder order)
    {
        var repo = Services.Provider.GetService<IProductionOrderPriceRepository>();
        return repo!.GetByOwner(order.id);
    }

    public IReadOnlyList<ProductionOrder> GetWithReturnMaterial(Contract contract)
    {
        using var conn = Database.OpenConnection();

        var wpw = new Query("waybill_processing_writeoff")
            .Select("waybill_processing_id")
            .Select("material_id")
            .SelectRaw("sum(amount) as written_off")
            .GroupBy("waybill_processing_id", "material_id");

        var rm = new Query("waybill_processing_price as wpp")
            .Distinct()
            .Select("wpp.owner_id as waybill_processing_id")
            .Join(wpw.As("wpw"), j => j.On("wpw.waybill_processing_id", "wpp.owner_id").On("wpw.material_id", "wpp.reference_id"))
            .WhereColumns("wpp.amount", "!=", "wpw.written_off");

        var d = new Query("production_order_price")
            .Select("owner_id")
            .SelectRaw("sum(product_cost) as product_cost")
            .SelectRaw("sum(tax_value) as tax_value")
            .SelectRaw("sum(full_cost) as full_cost")
            .GroupBy("owner_id");

        return GetBaseQuery(conn, "po")
            .With("rm_waybills", rm)
            .Select("po.*")
            .Select("c.item_name as contractor_name")
            .Select("d.*")
            .Select("wp.contractor_id as wp_contractor_id")
            .Select("wp.contract_id as wp_contract_id")
            .Join("contractor as c", "c.id", "po.contractor_id")
            .LeftJoin(d.As("d"), j => j.On("d.owner_id", "po.id"))
            .Join("waybill_processing as wp", "wp.owner_id", "po.id")
            .Join("rm_waybills as rm", "rm.waybill_processing_id", "wp.id")
            .WhereFalse("po.deleted")
            .WhereTrue("po.carried_out")
            .Where("wp.contractor_id", contract.owner_id)
            .Where("wp.contract_id", contract.id)
            .Get<ProductionOrder>()
            .ToList();
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        var d = new Query("production_order_price")
            .Select("owner_id")
            .SelectRaw("sum([product_cost]) as [cost_order]")
            .SelectRaw("sum([tax_value]) as [tax_value]")
            .SelectRaw("sum([full_cost]) as [full_cost]")
            .GroupBy("owner_id");

        return query
            .Select("production_order.*")
            .Select("o.item_name as organization_name")
            .SelectRaw("case when c.item_name is null then c.code else c.item_name end as contractor_name")
            .Select("contract.tax_payer")
            .SelectRaw("case [contract].[tax_payer] when true then 20 else 0 end as [tax]")
            .Select("contract.item_name as contract_name")
            .Select("d.*")
            .Join("organization as o", "o.id", "production_order.organization_id")
            .Join("contractor as c", "c.id", "production_order.contractor_id")
            .LeftJoin("contract", "contract.id", "production_order.contract_id")
            .LeftJoin(d.As("d"), j => j.On("d.owner_id", "production_order.id"));
    }
}
