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
    private string calculationName = string.Empty;

    [Display(Name = "Выполнено", Order = 1000)]
    [ColumnMode(Format = ColumnFormat.Progress)]
    public int CompleteStatus 
    { 
        get => completeStatus;
        set => SetProperty(ref completeStatus, value);
    }

    [Display(AutoGenerateField = false)]
    public Guid CalculationId 
    { 
        get => calculationId;
        set => SetProperty(ref calculationId, value);
    }

    [Display(Name = "Калькуляция", Order = 20)]
    [ColumnMode(Width = 150)]
    public string CalculationName 
    { 
        get => calculationName; 
        protected set => SetProperty(ref calculationName, value);
    }
    public void SetCalculation(Calculation calculation)
    {
        CalculationId = calculation.Id;
        CalculationName = calculation.Code;
    }
}
