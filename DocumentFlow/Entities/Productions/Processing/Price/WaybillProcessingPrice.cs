//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.07.2022
//
// Версия 2023.1.15
//  - добавлен атрибут ProductExcludingPrice
// Версия 2023.1.17
//  - свойство written_off стало protected set
//
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Products;
using DocumentFlow.Entities.Products.Core;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Productions.Processing;

[ProductContent(ProductContent.Materials)]
[ProductExcludingPrice]
public class WaybillProcessingPrice : ProductPrice
{
    [Display(AutoGenerateField = false)]
    public decimal written_off { get; protected set; }

    [Display(Name = "Остаток", Order = 900)]
    public decimal remainder => amount - written_off;
}
