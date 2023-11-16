//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

public class OperationGoods : Entity<long>, ICopyable, IDependentEntity
{
    private Guid goodsId;
    private string code = string.Empty;
    private string name = string.Empty;

    [Display(AutoGenerateField = false)]
    public Guid GoodsId 
    { 
        get => goodsId;
        set => SetProperty(ref goodsId, value);
    }

    [Write(false)]
    [Display(Name = "Артикул")]
    [ColumnMode(Width = 150)]
    public string Code 
    { 
        get => code;
        set => SetProperty(ref code, value);
    }

    [Write(false)]
    [Display(Name = "Наименование")]
    [ColumnMode(AutoSizeColumnsMode = AutoSizeColumnsMode.Fill)]
    public string ItemName 
    { 
        get => name;
        set => SetProperty(ref name, value);
    }

    public object Copy()
    {
        var copy = (OperationGoods)MemberwiseClone();
        copy.Id = 0;

        return copy;
    }

    public void SetOwner(Guid ownerId) => OwnerId = ownerId;
}
