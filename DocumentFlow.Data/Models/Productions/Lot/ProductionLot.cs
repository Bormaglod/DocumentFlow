//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Tools;
using DocumentFlow.Tools;

using Humanizer;


namespace DocumentFlow.Data.Models;

[EntityName("Партия")]
public class ProductionLot : AccountingDocument
{
    private Guid calculationId;
    private decimal quantity;
    private string state = "created";
    private bool? sold;

    /// <summary>
    /// Возвращает или устанавливает идентификатор калькуляции используемой для изготовления партии.
    /// </summary>
    public Guid CalculationId 
    { 
        get => calculationId; 
        set
        {
            if (calculationId != value) 
            { 
                calculationId = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает количество изделий в партии.
    /// </summary>
    public decimal Quantity 
    { 
        get => quantity; 
        set
        {
            if (quantity != value)
            {
                quantity = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает значение состояния партии (значение должно быть строковым представлением типа lot_state из Postgresql).
    /// </summary>
    [DenyCopying]
    [EnumType("lot_state")]
    public string State
    {
        get => state;
        set
        {
            if (state != value) 
            {
                state = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает флаг определяющий, что партия реализована (если партия реализована частично - то NULL).
    /// </summary>
    [Write(false)]
    public bool? Sold 
    { 
        get => sold; 
        set
        {
            if (sold != value) 
            { 
                sold = value;
                NotifyPropertyChanged();
            }
        }
    }

    public int OrderNumber { get; protected set; }
    public DateTime OrderDate { get; protected set; }
    public Guid GoodsId { get; protected set; }
    public string GoodsName { get; protected set; } = string.Empty;
    public string CalculationName { get; protected set; } = string.Empty;
    public int ExecutePercent { get; protected set; }
    public decimal FreeQuantity { get; protected set; }

    public string StateName => LotState.Description();

    public LotState LotState => Enum.Parse<LotState>(State.Pascalize());
}
