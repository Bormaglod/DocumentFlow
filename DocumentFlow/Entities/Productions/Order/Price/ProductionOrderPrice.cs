﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//
// Версия 2022.8.18
//  - изменен порядок столбцов
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Products;
using DocumentFlow.Entities.Products.Core;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Productions.Order;

[ProductContent(ProductContent.Goods)]
public class ProductionOrderPrice : ProductPrice
{
    [Display(Name = "Выполнено", Order = 1000)]
    public int complete_status { get; set; }

    [Display(AutoGenerateField = false)]
    public Guid calculation_id { get; set; }

    [Display(Name = "Калькуляция", Order = 20)]
    public string calculation_name { get; protected set; } = string.Empty;
}
