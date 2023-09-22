//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

public class CalculationOperationProperty : Identifier<long>, IDependentEntity
{
    [Display(AutoGenerateField = false)]
    public Guid OperationId { get; set; }
    
    [Display(AutoGenerateField = false)]
    public Guid PropertyId { get; set; }

    [Write(false)]
    [Display(AutoGenerateField = false)]
    public Property? Property { get; set; }

    [Display(Name = "Параметр")]
    public string PropertyName => Property?.ToString() ?? string.Empty;

    [Display(Name = "Значение")]
    public string? PropertyValue { get; set; }

    public void SetOwner(Guid ownerId) => OperationId = ownerId;
}
