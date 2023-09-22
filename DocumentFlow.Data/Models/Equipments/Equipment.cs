//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Оборудование")]
public class Equipment : Directory
{
    private bool isTools;
    private string? serialNumber;
    private DateTime? commissioning;
    private int? startingHits;

    /// <summary>
    /// Определяет является ли оборудование инструментом.
    /// </summary>
    public bool IsTools 
    { 
        get => isTools; 
        set
        {
            if (isTools != value)
            {
                isTools = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Серийный номер оборудования.
    /// </summary>
    public string? SerialNumber 
    { 
        get => serialNumber; 
        set
        {
            if (serialNumber != value) 
            { 
                serialNumber = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Дата ввода в эксплуатацию.
    /// </summary>
    public DateTime? Commissioning 
    { 
        get => commissioning; 
        set 
        {
            if (commissioning != value)
            {
                commissioning = value;
                NotifyPropertyChanged();
            }
        } 
    }

    /// <summary>
    /// Начальное количество опрессовок. Используется только для аппликаторов.
    /// </summary>
    public int? StartingHits 
    { 
        get => startingHits; 
        set 
        {
            if (startingHits != value)
            {
                startingHits = value;
                NotifyPropertyChanged();
            }
        } 
    }
}
