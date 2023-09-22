//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Заказ")]
public class ProductionOrder : ShipmentDocument
{
    private bool closed;

    public bool Closed 
    { 
        get => closed; 
        set
        {
            if (closed != value) 
            { 
                closed = value;
                NotifyPropertyChanged();
            }
        }
    }

    public decimal CostOrder { get; protected set; }
    public bool TaxPayer { get; protected set; }
    public int Tax { get; protected set; }
    public decimal TaxValue { get; protected set; }
    public decimal FullCost { get; protected set; }

    [WritableCollection]
    public IList<ProductionOrderPrice> Prices { get; protected set; } = null!;
}