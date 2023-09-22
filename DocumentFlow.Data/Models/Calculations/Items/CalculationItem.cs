﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

public class CalculationItem : Directory
{
    private Guid? itemId;
    private decimal price;
    private decimal itemCost;

    /// <summary>
    /// Возвращает или устанавливает идентификатор элемента калькуляции (операция, материал и др.)
    /// </summary>
    public Guid? ItemId 
    { 
        get => itemId; 
        set
        {
            if (itemId != value) 
            { 
                itemId = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает цену за 1 ед. элемента калькуляции
    /// </summary>
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

    /// <summary>
    /// Возвращает или устанавливает стоимость элемента калькуляции
    /// </summary>
    public decimal ItemCost 
    { 
        get => itemCost; 
        set
        {
            if (itemCost != value)
            {
                itemCost = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Computed]
    public string? CalculationName { get; protected set; }
}