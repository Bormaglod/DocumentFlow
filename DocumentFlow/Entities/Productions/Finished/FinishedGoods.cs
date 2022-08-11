//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Productions.Finished;

[Description("Готовая продукция")]
public class FinishedGoods : AccountingDocument
{
    public Guid goods_id { get; set; }
    public int quantity { get; set; }
    public decimal price { get; set; }
    public decimal product_cost { get; set; }
    public string goods_name { get; protected set; } = string.Empty;
    public int lot_number { get; protected set; }
    public DateTime lot_date { get; protected set; }
}
