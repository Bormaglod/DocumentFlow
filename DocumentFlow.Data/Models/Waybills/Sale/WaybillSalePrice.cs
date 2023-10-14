//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

[ProductContent(ProductContent.All)]
public class WaybillSalePrice : ProductPrice, IDiscriminator, IProductionLotSupport
{
    [Write(false)]
    [Display(AutoGenerateField = false)]
    public string? Discriminator { get; set; }

    [Display(AutoGenerateField = false)]
    public Guid? LotId { get; set; }

    [Computed]
    [Display(Name = "Партия", Order = 1000)]
    [ColumnMode(Width = 150)]
    public string? LotName { get; set; }

}
