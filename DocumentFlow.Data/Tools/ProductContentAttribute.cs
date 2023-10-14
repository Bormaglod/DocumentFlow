//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;

namespace DocumentFlow.Data.Tools;

[AttributeUsage(AttributeTargets.Class)]
public class ProductContentAttribute : Attribute
{
    public ProductContentAttribute(ProductContent content) => Content = content;

    public ProductContent Content { get; }
}
