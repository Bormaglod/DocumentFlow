//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

public class PriceApproval: Entity<long>, ICopyable, IDependentEntity
{
    [Display(AutoGenerateField = false)]
    public Guid ProductId { get; set; }

    [Computed]
    [Display(Name = "Материал / Изделие")]
    [ColumnMode(AutoSizeColumnsMode = AutoSizeColumnsMode.Fill)]
    public string ProductName { get; set; } = string.Empty;

    [Computed]
    [Display(AutoGenerateField = false)]
    public Guid? MeasurementId { get; set; }

    [Computed]
    [Display(Name = "Ед. изм.")]
    [ColumnMode(Width = 100)]
    public string MeasurementName { get; set; } = string.Empty;

    [Display(Name = "Цена")]
    [ColumnMode(Format = ColumnFormat.Currency, Width = 100)]
    public decimal Price { get; set; }

    public object Copy()
    {
        var copy = (PriceApproval)MemberwiseClone();
        copy.Id = 0;
        
        return copy;
    }

    public void SetOwner(Guid ownerId) => OwnerId = ownerId;
}