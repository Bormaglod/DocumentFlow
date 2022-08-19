//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.01.2022
//
// Версия 2022.8.19
//  - calculation_name теперь ссылается не на item_name, а на code
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

using System.Data;

namespace DocumentFlow.Entities.Calculations;

public class CalculationOperationRepository : CalculationItemRepository<CalculationOperation>, ICalculationOperationRepository
{
    public CalculationOperationRepository(IDatabase database) : base(database) { }

    public void RecalculatePrices(Guid calculate_id)
    {
        using var conn = Database.OpenConnection();
        conn.Execute("call make_prices_operations_relevant(:calculate_id)", new { calculate_id });
    }

    public IReadOnlyList<Property> GetListProperties()
    {
        using var conn = Database.OpenConnection();

        return conn.Query<Property>("select * from property").ToList();
    }

    public IReadOnlyList<CalculationOperationProperty> GetProperties(Guid operation_id)
    {
        using var conn = Database.OpenConnection();

        string sql = "select * from calculation_operation_property cop join property p on p.id = cop.property_id where operation_id = :operation_id";
        return conn.Query<CalculationOperationProperty, Property, CalculationOperationProperty>(sql, (cop, p) =>
        {
            cop.Property = p;
            return cop;
        }, new { operation_id }).ToList();
    }

    public void AddProperty(CalculationOperationProperty prop, IDbTransaction transaction)
    {
        if (transaction.Connection == null || prop.id != default)
        {
            return;
        }

        prop.id = transaction.Connection.QuerySingle<long>("insert into calculation_operation_property (operation_id, property_id, property_value) values (:operation_id, :property_id, :property_value) returning id", prop, transaction);
    }

    public void UpdateProperty(CalculationOperationProperty prop, IDbTransaction transaction)
    {
        if (transaction.Connection == null || prop.id == default)
        {
            return;
        }

        transaction.Connection.Execute("update calculation_operation_property set property_id = :property_id, property_value = :property_value where id = :id", prop, transaction);
    }

    public void DeleteProperty(CalculationOperationProperty prop, IDbTransaction transaction)
    {
        if (transaction.Connection == null || prop.id == default)
        {
            return;
        }

        transaction.Connection.Execute("delete from calculation_operation_property where id = :id", prop, transaction);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        var using_operations = new Query("calculation_operation as co")
            .SelectRaw("array_agg([co].[code])")
            .WhereRaw("[calculation_operation].[code] = any ([co].[previous_operation])")
            .WhereColumns("co.owner_id", "=", "calculation_operation.owner_id");

        return query
            .FromRaw("only calculation_operation")
            .Select("calculation_operation.*")
            .Select("o.item_name as operation_name")
            .Select("c.code as calculation_name")
            .Select("e.item_name as equipment_name")
            .Select("t.item_name as tools_name")
            .Select("m.code as material_name")
            .SelectRaw("round((3600 * [calculation_operation].[repeats])::numeric / [o].[production_rate], 1) as [produced_time]")
            .SelectRaw("[calculation_operation].[material_amount] * [calculation_operation].[repeats] as [total_material]")
            .Select(using_operations, "using_operations")
            .Join("calculation as c", "c.id", "calculation_operation.owner_id")
            .LeftJoin("operation as o", "o.id", "calculation_operation.item_id")
            .LeftJoin("equipment as e", "e.id", "calculation_operation.equipment_id")
            .LeftJoin("equipment as t", "t.id", "calculation_operation.tools_id")
            .LeftJoin("material as m", "m.id", "calculation_operation.material_id");
    }
}
