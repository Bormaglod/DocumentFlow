//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.02.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Products.Core;

public enum ProductContent { Materials, Goods, All }

[AttributeUsage(AttributeTargets.Class)]
public class ProductContentAttribute : Attribute
{
    public ProductContentAttribute(ProductContent content) => Content = content;

    public ProductContent Content { get; }
}
