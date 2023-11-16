//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Tools;
using DocumentFlow.Tools;

using Humanizer;

namespace DocumentFlow.Data.Models;


[EntityName("Калькуляция")]
public class Calculation : Directory
{
    private decimal costPrice;
    private decimal profitPercent;
    private decimal profitValue;
    private decimal price;
    private string? note;
    private string state = "prepare";
    private string stimulType = "money";
    private decimal stimulPayment;
    private DateTime? dateApproval;

    /// <summary>
    /// Возвращает или устанавливает себестоимость изделия или услуги.
    /// </summary>
    public decimal CostPrice 
    { 
        get => costPrice;
        set => SetProperty(ref costPrice, value);
    }

    /// <summary>
    /// Возвращает или устанавливает рентабельность при изготовлении изделия или оказания услуги.
    /// </summary>
    public decimal ProfitPercent 
    { 
        get => profitPercent;
        set => SetProperty(ref profitPercent, value);
    }

    /// <summary>
    /// Возвращает или устанавливает норму прибыли при изготовлении изделия или оказания услуги.
    /// </summary>
    public decimal ProfitValue 
    { 
        get => profitValue;
        set => SetProperty(ref profitValue, value);
    }

    /// <summary>
    /// Возвращает или устанавливает цену изделия или услуги без учёта НДС.
    /// </summary>
    public decimal Price 
    { 
        get => price;
        set => SetProperty(ref price, value);
    }

    /// <summary>
    /// Возвращает или устанавливает дополнительную информацию.
    /// </summary>
    public string? Note 
    { 
        get => note;
        set => SetProperty(ref note, value);
    }

    /// <summary>
    /// Возвращает или устанавливает состояние калькуляции.
    /// </summary>
    [EnumType("calculation_state")]
    public string State 
    { 
        get => state;
        set => SetProperty(ref state, value);
    }

    /// <summary>
    /// Возвращает или устанавливает способ начисления стимулирующих выплат.
    /// </summary>
    [EnumType("stimulating_value")]
    public string StimulType 
    { 
        get => stimulType;
        set => SetProperty(ref stimulType, value);
    }

    /// <summary>
    /// Возвращает или устанавливает размер стимулирующей выплаты.
    /// </summary>
    public decimal StimulPayment 
    { 
        get => stimulPayment;
        set => SetProperty(ref stimulPayment, value);
    }

    /// <summary>
    /// Возвращает или устанавливает дату утверждения калькуляции.
    /// </summary>
    public DateTime? DateApproval 
    { 
        get => dateApproval;
        set => SetProperty(ref dateApproval, value);
    }

    [Computed]
    public string? GoodsName { get; set; }
    public decimal Weight { get; protected set; }
    public decimal ProducedTime { get; protected set; }

    [Write(false)]
    public CalculationState CalculationState
    {
        get { return Enum.Parse<CalculationState>(State.Pascalize()); }
        set { State = value.ToString().Underscore(); }
    }

    public string CalculationStateName => CalculationState.Description();

    [Write(false)]
    public StimulatingValue StimulatingValue
    {
        get { return Enum.Parse<StimulatingValue>(StimulType.Pascalize()); }
        set { StimulType = value.ToString().Underscore(); }
    }

    public string StimulTypeName => StimulatingValue.Description();
}
