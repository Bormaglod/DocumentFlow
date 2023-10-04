//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

[ProductContent(ProductContent.Materials)]
public class ReturnMaterialsRows : Entity<long>, ICopyable, IDependentEntity
{
    [Display(AutoGenerateField = false)]
    public Guid MaterialId { get; set; }

    [Display(Name = "Материал / Изделие", Order = 10)]
    [Computed]
    [ColumnMode(AutoSizeColumnsMode = AutoSizeColumnsMode.Fill)]
    public string MaterialName { get; set; } = string.Empty;

    [Display(Name = "Количество", Order = 20)]
    [ColumnMode(Width = 100)]
    public decimal Quantity { get; set; }

    [Display(Name = "Ед. изм.", Order = 150)]
    [Computed]
    [ColumnMode(Width = 80, Alignment = HorizontalAlignment.Center)]
    public string MeasurementName { get; set; } = string.Empty;

    public object Copy()
    {
        var copy = (ReturnMaterialsRows)MemberwiseClone();
        copy.Id = 0;

        return copy;
    }

    public void SetOwner(Guid ownerId) => OwnerId = ownerId;
}
