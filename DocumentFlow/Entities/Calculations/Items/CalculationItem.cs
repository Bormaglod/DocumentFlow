//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data;

namespace DocumentFlow.Entities.Calculations;

public class CalculationItem : Directory
{
    public string? CalculationName { get; protected set; }
    public Guid? ItemId { get; set; }
    public decimal Price { get; set; }
    public decimal ItemCost { get; set; }
}
