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
    private bool isGiving;
    private string priceSettingMethod = "average";

    /// <summary>
    /// Возвращает или устанавливает расход материала на изделие.
    /// </summary>
    public decimal Amount 
    { 
        get => amount; 
        set
        {
            if (amount != value) 
            {
                amount = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает флаг определяющий - является ли материал давальческим.
    /// </summary>
    public bool IsGiving 
    { 
        get => isGiving; 
        set
        {
            if (isGiving != value) 
            { 
                isGiving = value;
                NotifyPropertyChanged();
            }
        }
    }

    [EnumType("price_setting_method")]
    public string PriceSettingMethod 
    { 
        get => priceSettingMethod; 
        set
        {
            if (priceSettingMethod != value) 
            { 
                priceSettingMethod = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Computed]
    public string? MaterialName { get; protected set; }

    [Computed]
    public decimal Weight { get; protected set; }

    [Write(false)]
    public PriceSettingMethod MethodPrice
    {
        get { return Enum.Parse<PriceSettingMethod>(PriceSettingMethod.Pascalize()); }
        set { PriceSettingMethod = value.ToString().Underscore(); }
    }

    public string MethodPriceName => MethodPrice.Description();

    public override string ToString() => MaterialName ?? "[NULL]";
}
