//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Operations;

public class OperationGoods : Entity<long>, ICloneable, IEntityClonable
{
#pragma warning disable IDE1006 // Стили именования
    [Display(AutoGenerateField = false)]
    public Guid goods_id { get; set; }

    [Display(Name = "Артикул")]
    public string goods_code { get; protected set; } = string.Empty;

    [Display(Name = "Наименование")]
    public string goods_name { get; protected set; } = string.Empty;

#pragma warning restore IDE1006 // Стили именования

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((OperationGoods)copy).Id = 0;

        return copy;
    }

    public void SetGoodsData(Goods goods) => (goods_code, goods_name) = (goods.code, goods.item_name ?? string.Empty);

    public void ClearGoodsData() => (goods_code, goods_name) = (string.Empty, string.Empty);
}
