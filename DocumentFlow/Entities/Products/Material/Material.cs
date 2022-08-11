//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Products;

[Description("Материал")]
public class Material : Product
{
    public static readonly Guid WireGroup = new("0525748e-e98c-4296-bd0e-dcacee7224f3");
    public string? cross_name { get; protected set; }
    public decimal min_order { get; set; }
    public string? ext_article { get; set; }
    public bool material_using { get; protected set; }
    public int price_status { get; protected set; }
    public Guid? wire_id { get; set; }
    public string? wire_name { get; protected set; }
}
