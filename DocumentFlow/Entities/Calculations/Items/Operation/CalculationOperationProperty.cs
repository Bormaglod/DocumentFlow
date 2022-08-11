//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Calculations;

public class CalculationOperationProperty : Identifier<long>
{
    [Display(AutoGenerateField = false)]
    public Guid operation_id { get; set; }
    
    [Display(AutoGenerateField = false)]
    public Guid property_id { get; set; }

    [Display(AutoGenerateField = false)]
    public Property? Property { get; set; }

    [Display(Name = "Параметр")]
    public string property_name => Property?.ToString() ?? string.Empty;

    [Display(Name = "Значение")]
    public string? property_value { get; set; }
}
