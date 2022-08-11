//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Products;

public class MaterialUsage : DocumentInfo
{
    public string calculation_name { get; protected set; } = string.Empty;
    public string calculation_code { get; protected set; } = string.Empty;
    public Guid goods_id { get; protected set; }
    public string goods_code { get; protected set; } = string.Empty;
    public string goods_name { get; protected set; } = string.Empty;
    public decimal amount { get; protected set; }
}
