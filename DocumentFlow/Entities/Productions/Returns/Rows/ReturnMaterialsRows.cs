﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.6.16
//  - Атрибут Exclude заменен на AllowOperation(DataOperation.None)
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Products.Core;
using DocumentFlow.Infrastructure.Data;

using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Productions.Returns;

[ProductContent(ProductContent.Materials)]
public class ReturnMaterialsRows : Entity<long>, ICloneable, IEntityClonable
{
    [Display(AutoGenerateField = false)]
    public Guid MaterialId { get; set; }

    [Display(Name = "Материал / Изделие", Order = 10)]
    [AllowOperation(DataOperation.None)]
    [ColumnMode(AutoSizeColumnsMode = AutoSizeColumnsMode.Fill)]
    public string MaterialName { get; set; } = string.Empty;

    [Display(Name = "Количество", Order = 20)]
    [ColumnMode(Width = 100)]
    public decimal Quantity { get; set; }

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((ReturnMaterialsRows)copy).Id = 0;

        return copy;
    }
}
