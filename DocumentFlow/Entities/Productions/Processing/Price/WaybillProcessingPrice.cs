//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Products;
using DocumentFlow.Entities.Products.Core;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Productions.Processing;

[ProductContent(ProductContent.Materials)]
public class WaybillProcessingPrice : ProductPrice
{
    [Display(AutoGenerateField = false)]
    public decimal written_off { get; set; }
}
