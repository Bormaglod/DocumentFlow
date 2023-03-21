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
    public string? calculation_name { get; protected set; }
    public Guid? item_id { get; set; }
    public decimal price { get; set; }
    public decimal item_cost { get; set; }
}
