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
    public string? DeductionName { get; protected set; }
    public decimal Value { get; set; }
    public BaseDeduction CalculationBase { get; protected set; }
    public string? CalculationBaseText => Deduction.BaseDeductions[CalculationBase];
    public override string ToString() => DeductionName ?? "[NULL]";
}
