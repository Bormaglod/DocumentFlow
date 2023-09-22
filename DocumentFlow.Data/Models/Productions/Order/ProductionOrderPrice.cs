//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

[ProductContent(ProductContent.Goods)]
public class ProductionOrderPrice : ProductPrice, IProductCalculation
{
    private int completeStatus;
    private Guid calculationId;

    [Display(Name = "Выполнено", Order = 1000)]
    [ColumnMode(Format = ColumnFormat.Progress)]
    public int CompleteStatus 
    { 
        get => completeStatus; 
        set
        {
            if (completeStatus !=  value) 
            { 
                completeStatus = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Display(AutoGenerateField = false)]
    public Guid CalculationId 
    { 
        get => calculationId; 
        set
        {
            if (calculationId != value) 
            { 
                calculationId = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Display(Name = "Калькуляция", Order = 20)]
    [ColumnMode(Width = 150)]
    public string CalculationName { get; protected set; } = string.Empty;

    public void SetCalculation(Calculation calculation)
    {
        CalculationId = calculation.Id;
        CalculationName = calculation.Code;
    }
}
