//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Products;

[Description("Продукция")]
public class Goods : Product
{
    public bool is_service { get; set; }
    public Guid? calculation_id { get; set; }
    public string? note { get; set; }
    public decimal? cost_price { get; protected set; }
    public decimal? profit_percent { get; protected set; }
    public decimal? profit_value { get; protected set; }
    public DateTime? date_approval { get; protected set; }
}
