//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Products.Core;
using DocumentFlow.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Productions.Returns;

[ProductContent(ProductContent.Materials)]
public class ReturnMaterialsRows : Entity<long>, ICloneable, IEntityClonable
{
    [Display(AutoGenerateField = false)]
    public Guid material_id { get; set; }

    [Display(Name = "Материал / Изделие", Order = 10)]
    [Exclude]
    public string material_name { get; set; } = string.Empty;

    [Display(Name = "Количество", Order = 20)]
    public decimal quantity { get; set; }

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((ReturnMaterialsRows)copy).id = 0;

        return copy;
    }
}
