//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Data;

using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Operations;

public class OperationGoods : Entity<long>, ICloneable, IEntityClonable
{
    [Display(AutoGenerateField = false)]
    public Guid GoodsId { get; set; }

    [Display(Name = "Артикул")]
    [ColumnMode(Width = 150)]
    public string GoodsCode { get; protected set; } = string.Empty;

    [Display(Name = "Наименование")]
    [ColumnMode(AutoSizeColumnsMode = AutoSizeColumnsMode.Fill)]
    public string GoodsName { get; protected set; } = string.Empty;

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((OperationGoods)copy).Id = 0;

        return copy;
    }

    public void SetGoodsData(Goods goods) => (GoodsCode, GoodsName) = (goods.Code, goods.ItemName ?? string.Empty);

    public void ClearGoodsData() => (GoodsCode, GoodsName) = (string.Empty, string.Empty);
}
