//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Продукция")]
public class Goods : Product
{
    private bool isService;
    private Guid? calculationId;
    private string? note;
    private int? length;
    private int? width;
    private int? height;

    /// <summary>
    /// Возвращает или устанавливает флаг означающий, что данный продукт является услугой (или нет).
    /// </summary>
    public bool IsService 
    { 
        get => isService;
        set => SetProperty(ref isService, value);
    }

    /// <summary>
    /// Возвращает или устанавливает идентификатор калькуляции.
    /// </summary>
    public Guid? CalculationId 
    { 
        get => calculationId;
        set => SetProperty(ref calculationId, value);
    }

    /// <summary>
    /// Возвращает или устанавливает дополнительную информацию об изделии/услуге.
    /// </summary>
    public string? Note 
    { 
        get => note;
        set => SetProperty(ref note, value);
    }

    /// <summary>
    /// Возвращает или устанавливает длину изделия.
    /// </summary>
    public int? Length 
    { 
        get => length;
        set => SetProperty(ref length, value);
    }

    /// <summary>
    /// Возвращает или устанавливает ширину изделия.
    /// </summary>
    public int? Width 
    { 
        get => width;
        set => SetProperty(ref width, value);
    }

    /// <summary>
    /// Возвращает или устанавливает высоту изделия.
    /// </summary>
    public int? Height 
    { 
        get => height;
        set => SetProperty(ref height, value);
    }

    /// <summary>
    /// Возвращает себестоимость изделия/услуги.
    /// </summary>
    [Computed]
    public decimal? CostPrice { get; protected set; }

    /// <summary>
    /// Возвращает рентабельность изготовления изделия (или оказания услуги).
    /// </summary>
    [Computed]
    public decimal? ProfitPercent { get; protected set; }

    /// <summary>
    /// Возвращает норму прибыли при изготовлении изделия (или оказания услуги).
    /// </summary>
    [Computed]
    public decimal? ProfitValue { get; protected set; }

    /// <summary>
    /// Возвращает дату утверждения калькуляции <see cref="CalculationId"/>.
    /// </summary>
    [Computed]
    public DateTime? DateApproval { get; protected set; }
}
