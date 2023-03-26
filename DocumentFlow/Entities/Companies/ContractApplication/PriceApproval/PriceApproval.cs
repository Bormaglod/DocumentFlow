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

using DocumentFlow.Data;
using DocumentFlow.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Companies;

public class PriceApproval: Entity<long>, ICloneable, IEntityClonable
{
    [Display(AutoGenerateField = false)]
    public Guid ProductId { get; set; }

    public string ProductName { get; protected set; } = string.Empty;

    [Display(AutoGenerateField = false)]
    public Guid? MeasurementId { get; protected set; }

    [Display(Name = "Ед. изм.")]
    public string MeasurementName { get; protected set; } = string.Empty;

    [Display(Name = "Цена")]
    public decimal Price { get; set; }

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((PriceApproval)copy).Id = 0;
        
        return copy;
    }

    public void SetProductName(string value) => ProductName = value;
}