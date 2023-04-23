//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//
// Версия 2022.8.18
//  - изменен порядок столбцов
// Версия 2023.1.21
//  - добавлен метод SetCalculation
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Products;
using DocumentFlow.Entities.Products.Core;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Productions.Order;

[ProductContent(ProductContent.Goods)]
public class ProductionOrderPrice : ProductPrice
{
    [Display(Name = "Выполнено", Order = 1000)]
    [ColumnMode(Format = ColumnFormat.Progress)]
    public int CompleteStatus { get; set; }

    [Display(Name = "Калькуляция", Order = 20)]
    [ColumnMode(Width = 150)]
    public string CalculationName { get; protected set; } = string.Empty;

    [Display(AutoGenerateField = false)]
    public Guid CalculationId { get; set; }

    public void SetCalculation(Calculation calculation)
    {
        CalculationId = calculation.Id;
        CalculationName = calculation.Code;
    }
}
