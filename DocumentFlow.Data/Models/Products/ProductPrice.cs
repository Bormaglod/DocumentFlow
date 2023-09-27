//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

using Humanizer;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

public abstract class ProductPrice : Entity<long>, ICopyable, IDependentEntity
{
    private Guid referenceId;
    private decimal amount;
    private decimal price;
    private decimal productCost;
    private int tax;
    private decimal taxValue;
    private decimal fullCost;

    [Display(AutoGenerateField = false)]
    public Guid ReferenceId 
    { 
        get => referenceId; 
        set
        {
            if (referenceId != value) 
            { 
                referenceId = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Display(Name = "Материал / Изделие", Order = 1)]
    [ColumnMode(AutoSizeColumnsMode = AutoSizeColumnsMode.Fill)]
    public string ProductName { get; protected set; } = string.Empty;

    [Display(Name = "Артикул", Order = 100)]
    [ColumnMode(Width = 150)]
    public string Code { get; protected set; } = string.Empty;

    [Display(Name = "Ед. изм.", Order = 150)]
    [ColumnMode(Width = 80, Alignment = HorizontalAlignment.Center)]
    public string MeasurementName { get; protected set; } = string.Empty;

    [Display(Name = "Количество", Order = 200)]
    [ColumnMode(Width = 100, Alignment = HorizontalAlignment.Right, DecimalDigits = 3)]
    public decimal Amount 
    { 
        get => amount; 
        set
        {
            if (amount !=  value) 
            { 
                amount = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Display(Name = "Цена", Order = 300)]
    [ColumnMode(Format = ColumnFormat.Currency, Width = 80)]
    public decimal Price 
    { 
        get => price; 
        set 
        {
            if (price != value)
            {
                price = value;
                NotifyPropertyChanged();
            }
        } 
    }

    [Display(Name = "Сумма", Order = 400)]
    [ColumnMode(Format = ColumnFormat.Currency, Width = 120)]
    public decimal ProductCost 
    { 
        get => productCost; 
        set 
        {
            if (productCost != value)
            {
                productCost = value;
                NotifyPropertyChanged();
            }
        } 
    }

    [Display(Name = "%НДС", Order = 500)]
    [ColumnMode(Width = 70, Alignment = HorizontalAlignment.Center)]
    public int Tax 
    { 
        get => tax; 
        set 
        {
            if (tax != value)
            {
                tax = value;
                NotifyPropertyChanged();
            }
        } 
    }

    [Display(Name = "НДС", Order = 600)]
    [ColumnMode(Format = ColumnFormat.Currency, Width = 100)]
    public decimal TaxValue 
    { 
        get => taxValue; 
        set 
        {
            if (taxValue != value)
            {
                taxValue = value;
                NotifyPropertyChanged();
            }
        } 
    }

    [Display(Name = "Всего с НДС", Order = 700)]
    [ColumnMode(Format = ColumnFormat.Currency, Width = 130)]
    public decimal FullCost 
    { 
        get => fullCost; 
        set 
        {
            if (fullCost != value)
            {
                fullCost = value;
                NotifyPropertyChanged();
            }
        } 
    }

    public object Copy()
    {
        var copy = (ProductPrice)MemberwiseClone();
        copy.Id = 0;

        return copy;
    }

    public void CopyFrom(ProductPrice source) 
    {
        ReferenceId = source.ReferenceId;
        Amount = source.Amount;
        Price = source.Price;
        ProductCost = source.ProductCost;
        Tax = source.Tax;
        TaxValue = source.TaxValue;
        FullCost = source.FullCost;
        ProductName = source.ProductName;
        Code = source.Code;
        MeasurementName = source.MeasurementName;
    }

    public void SetProductInfo(Product product)
    {
        (Code, ProductName, MeasurementName) = (product.Code, product.ItemName ?? string.Empty, product.MeasurementName ?? string.Empty);
        if (this is IDiscriminator discriminator)
        {
            discriminator.Discriminator = product.GetType().Name.Underscore();
        }
    }

    public void SetOwner(Guid ownerId) => OwnerId = ownerId;
}
