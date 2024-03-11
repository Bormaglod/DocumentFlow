//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Tools;
using DocumentFlow.Tools;

using Humanizer;

namespace DocumentFlow.Data.Models;

[EntityName("Материал")]
public class CalculationMaterial : CalculationItem
{
    private decimal amount;
    private string priceMethod = "average";

    /// <summary>
    /// Возвращает или устанавливает расход материала на изделие.
    /// </summary>
    public decimal Amount 
    { 
        get => amount;
        set => SetProperty(ref amount, value);
    }

    [EnumType("price_setting_method")]
    public string PriceMethod
    {
        get => priceMethod;
        set => SetProperty(ref priceMethod, value);
    }

    [Computed]
    public string? MaterialName { get; protected set; }

    [Computed]
    public decimal Weight { get; protected set; }

    [Write(false)]
    public PriceSettingMethod PriceSettingMethod
    {
        get { return Enum.Parse<PriceSettingMethod>(PriceMethod.Pascalize()); }
        set { PriceMethod = value.ToString().Underscore(); }
    }

    public string MethodPriceName => PriceSettingMethod.Description();

    public override string ToString() => MaterialName ?? "[NULL]";
}
