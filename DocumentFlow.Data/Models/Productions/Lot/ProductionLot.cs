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
