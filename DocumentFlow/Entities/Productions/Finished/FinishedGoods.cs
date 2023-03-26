//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//
// Версия 2023.2.23
//  - свойство quantity стало decimal
//  - добавлено свойство measurement_name
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Productions.Finished;

[Description("Готовая продукция")]
public class FinishedGoods : AccountingDocument
{
    public Guid GoodsId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal ProductCost { get; set; }
    public string GoodsName { get; protected set; } = string.Empty;
    public int LotNumber { get; protected set; }
    public DateTime LotDate { get; protected set; }
    public string? MeasurementName { get; protected set; }
}
