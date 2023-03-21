//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Warehouse;

[Description("Корректировка остатков")]
public class AdjustingBalances : AccountingDocument
{
    public Guid material_id { get; set; }
    public string material_name { get; protected set; } = string.Empty;
    public decimal quantity { get; set; }
}
