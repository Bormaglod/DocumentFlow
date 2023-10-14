//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.01.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Tools;

[AttributeUsage(AttributeTargets.Class)]
public class ProductExcludingPriceAttribute : Attribute
{
    public ProductExcludingPriceAttribute() { }
}
