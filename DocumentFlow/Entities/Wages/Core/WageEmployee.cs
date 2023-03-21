//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//
// Версия 2023.1.28
//  - у свойство employee_name изменилась защита метода set с public на 
//    protected
//  - добавлен метод SetEmployeeName
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Wages.Core;

public class WageEmployee : Entity<long>, IWageEmployee
{
    [Display(AutoGenerateField = false)]
    public Guid employee_id { get; set; }

    [Display(Name = "Сотрудник", Order = 1)]
    [Exclude]
    public string employee_name { get; protected set; } = string.Empty;

    [Display(Name = "Зар. плата", Order = 100)]
    public decimal wage { get; set; }

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((WageEmployee)copy).Id = 0;

        return copy;
    }

    public void SetEmployeeName(string employeeName) => employee_name = employeeName;

    public override string ToString() => employee_name;
}
