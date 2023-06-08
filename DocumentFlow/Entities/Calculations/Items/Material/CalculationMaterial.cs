//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.10.2021
//
// Версия 2023.6.8
//  - добавлены свойства и методы для реализации поля price_setting_method
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using Humanizer;

namespace DocumentFlow.Entities.Calculations;

public enum PriceSettingMethod { Average, Dictionary, Manual }

[Description("Материал")]
public class CalculationMaterial : CalculationItem
{
    public static readonly Dictionary<PriceSettingMethod, string> methods = new()
    {
        [Calculations.PriceSettingMethod.Average] = "Средняя цена",
        [Calculations.PriceSettingMethod.Dictionary] = "Справочник",
        [Calculations.PriceSettingMethod.Manual] = "Ручной ввод"
    };

    public string? MaterialName { get; protected set; }
    public decimal Amount { get; set; }
    public bool IsGiving { get; set; }
    public decimal Weight { get; protected set; }

    [EnumType("price_setting_method")]
    public string PriceSettingMethod { get; set; } = "average";

    public PriceSettingMethod MethodPrice
    {
        get { return Enum.Parse<PriceSettingMethod>(PriceSettingMethod.Pascalize()); }
        protected set { PriceSettingMethod = value.ToString().Underscore(); }
    }

    public string MethodPriceName => Methods[MethodPrice];

    public static IReadOnlyDictionary<PriceSettingMethod, string> Methods => methods;

    public void SetPriceSettingMethod(PriceSettingMethod method) => MethodPrice = method;

    public override string ToString() => MaterialName ?? "[NULL]";
}
