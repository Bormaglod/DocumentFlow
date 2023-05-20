﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2023
//
// Версия 2023.5.20
//  - добавлена реализация интерфейса IReferenceItem
//  - удалены методы SetGoodsData и ClearGoodsData
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Operations;

public class OperationGoods : Entity<long>, IReferenceItem, ICloneable, IEntityClonable
{
    [Display(AutoGenerateField = false)]
    public Guid GoodsId { get; set; }

    [Display(AutoGenerateField = false)]
    public Guid ReferenceId => GoodsId;

    [Display(Name = "Артикул")]
    [ColumnMode(Width = 150)]
    public string Code { get; protected set; } = string.Empty;

    [Display(Name = "Наименование")]
    [ColumnMode(AutoSizeColumnsMode = AutoSizeColumnsMode.Fill)]
    public string Name { get; protected set; } = string.Empty;

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((OperationGoods)copy).Id = 0;

        return copy;
    }

    public void SetData(IDirectory directory)
    {
        GoodsId = directory.Id;
        Code = directory.Code;
        Name = directory.ItemName ?? string.Empty;
    }

    public void Clear() => (Code, Name) = (string.Empty, string.Empty);
}
