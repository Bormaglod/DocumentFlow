//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.01.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using SqlKata;

using System.Data;

namespace DocumentFlow.Data.Models;

public class CalculationOperationRepository : CalculationItemRepository<CalculationOperation>, ICalculationOperationRepository
{
    public CalculationOperationRepository(IDatabase database) : base(database) { }

    public void RecalculatePrices(Guid calculate_id)
    {
        using var conn = GetConnection();
        conn.Execute("call make_prices_operations_relevant(:calculate_id)", new { calculate_id });
    }

    public IReadOnlyList<CalculationOperation> GetCodes(CalculationOperation excludeOperation)
    {
        return GetListExisting(callback: q =>
        {
            return q
                .Select("id", "code")
                .SelectRaw("code || ', ' || item_name as item_name")
                .Where("owner_id", excludeOperation.OwnerId)
                .WhereNotNull("item_name")
                .Where("code", "!=", excludeOperation.Code)
                .OrderBy("code");
        });
    }

    protected override bool CreatingAdjustedQuery() => true;

    protected override CalculationOperation GetAdjustedQuery(Guid id, IDbConnection connection)
    {
        var operationsDictionary = new Dictionary<Guid, CalculationOperation>();

        string sql = @$"
            select 
                co.*, cop.*, p.*
            from calculation_operation co
                left join calculation_operation_property cop on cop.operation_id = co.id
                left join property p on p.id = cop.property_id
            where co.id = :id";

        return connection.Query<CalculationOperation, CalculationOperationProperty, Property, CalculationOperation>(
            sql,
            (oper, opProp, prop) =>
            {
                if (!operationsDictionary.TryGetValue(oper.Id, out var operEntry))
                {
                    operEntry = oper;
                    operationsDictionary.Add(operEntry.Id, operEntry);
                }

                if (opProp != null)
                {
                    operEntry.Properties.Add(opProp);
                    if (prop != null)
                    {
                        opProp.Property = prop;
                    }
                }

                return operEntry;
            },
            new { id })
            .First();
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
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
