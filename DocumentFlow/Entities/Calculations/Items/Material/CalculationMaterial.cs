//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Calculations;

[Description("Материал")]
public class CalculationMaterial : CalculationItem
{
    public string? material_name { get; protected set; }
    public decimal amount { get; set; }
    public bool is_giving { get; set; }
    public decimal weight { get; protected set; }
    public override string ToString() => material_name ?? "[NULL]";
}
