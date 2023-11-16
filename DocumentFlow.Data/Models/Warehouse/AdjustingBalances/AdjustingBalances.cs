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
        set => SetProperty(ref materialId, value);
    }

    public decimal Quantity 
    { 
        get => quantity;
        set => SetProperty(ref quantity, value);
    }

    public string MaterialName { get; protected set; } = string.Empty;
}
