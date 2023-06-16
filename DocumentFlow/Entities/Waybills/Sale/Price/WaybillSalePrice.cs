//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//
// Версия 2022.08.17
//  - удалено свойство UseGetId
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.6.16
//  - Атрибут Exclude заменен на AllowOperation(DataOperation.None)
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Products;
using DocumentFlow.Entities.Products.Core;
using DocumentFlow.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Waybills;

[ProductContent(ProductContent.All)]
public class WaybillSalePrice : ProductPrice, IDiscriminator
{
    string IDiscriminator.TableName { get => TableName; set => TableName = value; }

    [AllowOperation(DataOperation.None)]
    [Display(AutoGenerateField = false)]
    public string TableName { get; set; } = string.Empty;
}
