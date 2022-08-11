//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Wages.Core;

public class WageEmployee : Entity<long>, IWageEmployee
{
    [Display(AutoGenerateField = false)]
    public Guid employee_id { get; set; }

    [Display(Name = "Сотрудник", Order = 1)]
    [Exclude]
    public string employee_name { get; set; } = string.Empty;

    [Display(Name = "Зар. плата", Order = 100)]
    public decimal wage { get; set; }

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((WageEmployee)copy).id = 0;

        return copy;
    }

    public override string ToString() => employee_name;
}
