//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Deductions;

namespace DocumentFlow.Entities.Calculations;

[Description("Удержание")]
public class CalculationDeduction : CalculationItem
{
    public string? deduction_name { get; protected set; }
    public decimal value { get; set; }
    public BaseDeduction calculation_base { get; protected set; }
    public string? calculation_base_text => Deduction.BaseDeductions[calculation_base];
    public override string ToString() => deduction_name ?? "[NULL]";
}
