//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2021
//
// Версия 2023.1.21
//  - поле StimulatingValues заменено на свойство и поменяло тип на
//    IReadOnlyDictionary
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

using Humanizer;

namespace DocumentFlow.Entities.Calculations;

public enum CalculationState { Prepare, Approved, Expired }

public enum StimulatingValue { Money, Percent }

[Description("Калькуляция")]
public class Calculation : Directory
{
    private static readonly Dictionary<StimulatingValue, string> stimulatingValues = new()
    {
        [StimulatingValue.Money] = "Сумма",
        [StimulatingValue.Percent] = "Процент"
    };

    public string? GoodsName { get; protected set; }
    public decimal CostPrice { get; set; }
    public decimal ProfitPercent { get; set; }
    public decimal ProfitValue { get; set; }
    public decimal Price { get; set; }
    public string? Note { get; set; }

    [DataOperation(DataOperation.Add | DataOperation.Update)]
    [EnumType("calculation_state")]
    public string State { get; set; } = "prepare";
    public string StateName => StateNameFromValue(CalculationState);
    public decimal Weight { get; protected set; }
    public decimal ProducedTime { get; protected set; }

    [EnumType("stimulating_value")]
    public string StimulType { get; set; } = "money";
    public string StimulTypeName => StimulTypeNameFromValue(StimulatingValue);
    public decimal StimulPayment { get; set; }
    public DateTime? DateApproval { get; set; }

    public static string StateNameFromValue(CalculationState state) => state switch
    {
        CalculationState.Prepare => "Подготовлена",
        CalculationState.Approved => "Утверждена",
        CalculationState.Expired => "В архиве",
        _ => throw new NotImplementedException()
    };

    public static string StimulTypeNameFromValue(StimulatingValue value) => value switch
    {
        StimulatingValue.Money => "Значение",
        StimulatingValue.Percent => "Процент",
        _ => throw new NotImplementedException()
    };

    [Exclude]
    public CalculationState CalculationState
    {
        get { return Enum.Parse<CalculationState>(State.Pascalize()); }
        set { State = value.ToString().Underscore(); }
    }

    [Exclude]
    public StimulatingValue StimulatingValue
    {
        get { return Enum.Parse<StimulatingValue>(StimulType.Pascalize()); }
        set { StimulType = value.ToString().Underscore(); }
    }

    public static IReadOnlyDictionary<StimulatingValue, string> StimulatingValues => stimulatingValues;
}
