//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//
// Версия 2022.8.17
//  - добавлено поле code
//  - изменены видимость методов set для полей product_name и code с 
//    public на protected
//  - добавлена группа методов SetProductInfo
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Products;

public abstract class ProductPrice : Entity<long>, ICloneable, IEntityClonable
{
    [Display(AutoGenerateField = false)]
    public Guid ReferenceId { get; set; }

    [Display(Name = "Материал / Изделие", Order = 1)]
    public string ProductName { get; protected set; } = string.Empty;

    [Display(Name = "Артикул", Order = 100)]
    public string Code { get; protected set; } = string.Empty;

    [Display(Name = "Количество", Order = 200)]
    public decimal Amount { get; set; }

    [Display(Name = "Цена", Order = 300)]
    public decimal Price { get; set; }

    [Display(Name = "Сумма", Order = 400)]
    public decimal ProductCost { get; set; }

    [Display(Name = "%НДС", Order = 500)]
    public int Tax { get; set; }

    [Display(Name = "НДС", Order = 600)]
    public decimal TaxValue { get; set; }

    [Display(Name = "Всего с НДС", Order = 700)]
    public decimal FullCost { get; set; }

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((ProductPrice)copy).Id = 0;

        return copy;
    }

    public void SetProductInfo(string code, string name) => (this.Code, ProductName) = (code, name);
    public void SetProductInfo(Product? product) => SetProductInfo(product?.Code ?? string.Empty, product?.ItemName ?? string.Empty);
}
