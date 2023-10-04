//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.07.2022
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

[ProductContent(ProductContent.Materials)]
[ProductExcludingPrice]
public class WaybillProcessingPrice : ProductPrice
{
    [Display(AutoGenerateField = false)]
    public decimal WrittenOff { get; protected set; }

    [Display(Name = "Остаток", Order = 900)]
    public decimal Remainder => Amount - WrittenOff;
}
