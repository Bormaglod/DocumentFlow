//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Tools;
using DocumentFlow.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Удержание")]
public class CalculationDeduction : CalculationItem
{
    private decimal decimalValue;

    public decimal Value 
    { 
        get => decimalValue; 
        set
        {
            if (decimalValue != value) 
            {
                decimalValue = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Computed]
    public string? DeductionName { get; protected set; }

    public BaseDeduction CalculationBase { get; protected set; }

    public string? CalculationBaseText => CalculationBase.Description();

    public override string ToString() => DeductionName ?? "[NULL]";
}
