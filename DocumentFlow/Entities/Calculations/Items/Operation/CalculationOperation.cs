//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Calculations;

[Description("Произв. операция")]
public class CalculationOperation : CalculationItem
{
    public string? operation_name { get; protected set; }
    public decimal produced_time { get; protected set; }
    public Guid? equipment_id { get; set; }
    public string? equipment_name { get; protected set; }
    public Guid? tools_id { get; set; }
    public string? tools_name { get; protected set; }
    public Guid? material_id { get; set; }
    public string? material_name { get; protected set; }
    public decimal material_amount { get; set; }
    public decimal total_material { get; protected set; }
    public int repeats { get; set; }
    public string[]? previous_operation { get; set; }
    public string previous_operation_value => previous_operation == null ? string.Empty : string.Join(',', previous_operation);
    public string[]? using_operations { get; protected set; }
    public string using_operations_value => using_operations == null ? string.Empty : string.Join(',', using_operations);
    public string? note { get; set; }
    public decimal stimul_cost { get; set; }
    public byte[]? preview { get; set; }
}
