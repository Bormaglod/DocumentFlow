//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Корректировка остатков")]
public class AdjustingBalances : AccountingDocument
{
    private Guid materialId;
    private decimal quantity;

    public Guid MaterialId 
    { 
        get => materialId; 
        set
        {
            if (materialId != value) 
            { 
                materialId = value;
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

    public string MaterialName { get; protected set; } = string.Empty;
}
