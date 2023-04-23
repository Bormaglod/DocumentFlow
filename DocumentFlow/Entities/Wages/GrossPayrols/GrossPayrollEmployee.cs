//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Wages.Core;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Wages;

public class GrossPayrollEmployee : WageEmployee
{
    [Display(AutoGenerateField = false)]
    public Guid IncomeItemId { get; set; }

    [Display(Name = "Статья дохода", Order = 100)]
    [ColumnMode(Width = 250)]
    public string IncomeItemName { get; protected set; } = string.Empty;
}