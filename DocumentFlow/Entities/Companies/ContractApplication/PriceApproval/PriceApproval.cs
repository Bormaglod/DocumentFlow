//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Companies;

public class PriceApproval: Entity<long>, ICloneable, IEntityClonable
{
    [Display(AutoGenerateField = false)]
    public Guid product_id { get; set; }

    public string product_name { get; protected set; } = string.Empty;

    [Display(AutoGenerateField = false)]
    public Guid? measurement_id { get; protected set; }

    [Display(Name = "Ед. изм.")]
    public string measurement_name { get; protected set; } = string.Empty;

    [Display(Name = "Цена")]
    public decimal price { get; set; }

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((PriceApproval)copy).id = 0;
        
        return copy;
    }

    public void SetProductName(string value) => product_name = value;
}