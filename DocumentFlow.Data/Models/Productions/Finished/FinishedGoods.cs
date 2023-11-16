//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Готовая продукция")]
public class FinishedGoods : AccountingDocument
{
    private Guid goodsId;
    private decimal quantity;
    private decimal? price;
    private decimal? productCost;

    public Guid GoodsId 
    { 
        get => goodsId;
        set => SetProperty(ref goodsId, value);
    }

    public decimal Quantity
    {
        get => quantity;
        set
        {
            if (SetProperty(ref quantity, value) && IsLoaded)
            {
                ProductCost = quantity * price;
            }
        }
    }

    public decimal? Price 
    { 
        get => price; 
        set
        {
            if (SetProperty(ref price, value) && IsLoaded) 
            {
                ProductCost = quantity * price;
            }
        }
    }

    public decimal? ProductCost 
    { 
        get => productCost;
        set => SetProperty(ref productCost, value);
    }

    public string GoodsName { get; protected set; } = string.Empty;
    public int LotNumber { get; protected set; }
    public DateTime LotDate { get; protected set; }
    public string? MeasurementName { get; protected set; }
}
