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
    public bool IsService { get; set; }
    public Guid? CalculationId { get; set; }
    public string? Note { get; set; }
    public decimal? CostPrice { get; protected set; }
    public decimal? ProfitPercent { get; protected set; }
    public decimal? ProfitValue { get; protected set; }
    public DateTime? DateApproval { get; protected set; }
}
