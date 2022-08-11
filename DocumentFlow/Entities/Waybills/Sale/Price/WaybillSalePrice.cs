//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Products;
using DocumentFlow.Entities.Products.Core;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Waybills;

[ProductContent(ProductContent.All)]
public class WaybillSalePrice : ProductPrice, IDiscriminator
{
    bool IDiscriminator.UseGetId => true;

    string IDiscriminator.TableName { get => table_name; set => table_name = value; }

    [Exclude]
    [Display(AutoGenerateField = false)]
    public string table_name { get; set; } = string.Empty;
}
