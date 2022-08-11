//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using Humanizer;

namespace DocumentFlow.Entities.Calculations;

public enum CalculationState { Prepare, Approved, Expired }

public enum StimulatingValue { Money, Percent }

[Description("Калькуляция")]
public class Calculation : Directory
{
    public string? goods_name { get; protected set; }
    public decimal cost_price { get; set; }
    public decimal profit_percent { get; set; }
    public decimal profit_value { get; set; }
    public decimal price { get; set; }
    public string? note { get; set; }

    [DataOperation(DataOperation.Add | DataOperation.Update)]
    [EnumType("calculation_state")]
    public string state { get; set; } = "prepare";
    public string state_name => StateNameFromValue(CalculationState);
    public decimal weight { get; protected set; }
    public decimal produced_time { get; protected set; }

    [EnumType("stimulating_value")]
    public string stimul_type { get; set; } = "money";
    public string stimul_type_name => StimulTypeNameFromValue(StimulatingValue);
    public decimal stimul_payment { get; set; }
    public DateTime? date_approval { get; set; }

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
        get { return Enum.Parse<CalculationState>(state.Pascalize()); }
        set { state = value.ToString().Underscore(); }
    }

    [Exclude]
    public StimulatingValue StimulatingValue
    {
        get { return Enum.Parse<StimulatingValue>(stimul_type.Pascalize()); }
        set { stimul_type = value.ToString().Underscore(); }
    }

    public static Dictionary<StimulatingValue, string> StimulatingValues = new()
    {
        [StimulatingValue.Money] = "Сумма",
        [StimulatingValue.Percent] = "Процент"
    };
}
