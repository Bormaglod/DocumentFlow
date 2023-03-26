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
    public string? OperationName { get; protected set; }
    public decimal ProducedTime { get; protected set; }
    public Guid? EquipmentId { get; set; }
    public string? EquipmentName { get; protected set; }
    public Guid? ToolsId { get; set; }
    public string? ToolsName { get; protected set; }
    public Guid? MaterialId { get; set; }
    public string? MaterialName { get; protected set; }
    public decimal MaterialAmount { get; set; }
    public decimal TotalMaterial { get; protected set; }
    public int Repeats { get; set; }
    public string[]? PreviousOperation { get; set; }
    public string PreviousOperationValue => PreviousOperation == null ? string.Empty : string.Join(',', PreviousOperation);
    public string[]? UsingOperations { get; protected set; }
    public string UsingOperationsValue => UsingOperations == null ? string.Empty : string.Join(',', UsingOperations);
    public string? Note { get; set; }
    public decimal StimulCost { get; set; }
    public byte[]? Preview { get; set; }
}
