//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Products;

public abstract class ProductPrice : Entity<long>, ICloneable, IEntityClonable
{
    [Display(AutoGenerateField = false)]
    public Guid reference_id { get; set; }

    [Display(Name = "Материал / Изделие", Order = 10)]
    [Exclude]
    public string product_name { get; set; } = string.Empty;

    [Display(Name = "Количество", Order = 20)]
    public decimal amount { get; set; }

    [Display(Name = "Цена", Order = 30)]
    public decimal price { get; set; }

    [Display(Name = "Сумма", Order = 40)]
    public decimal product_cost { get; set; }

    [Display(Name = "%НДС", Order = 50)]
    public int tax { get; set; }

    [Display(Name = "НДС", Order = 60)]
    public decimal tax_value { get; set; }

    [Display(Name = "Всего с НДС", Order = 70)]
    public decimal full_cost { get; set; }

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((ProductPrice)copy).id = 0;

        return copy;
    }
}
