//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Wages.Core;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Wages;

public class GrossPayrollEmployee : WageEmployee
{
    [Display(AutoGenerateField = false)]
    public Guid income_item_id { get; set; }

    [Display(Name = "Статья дохода", Order = 100)]
    public string income_item_name { get; protected set; } = string.Empty;
}