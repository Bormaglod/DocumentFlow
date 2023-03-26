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
    public string? CrossName { get; protected set; }
    public decimal MinOrder { get; set; }
    public string? ExtArticle { get; set; }
    public bool MaterialUsing { get; protected set; }
    public int PriceStatus { get; protected set; }
    public Guid? WireId { get; set; }
    public string? WireName { get; protected set; }
}
