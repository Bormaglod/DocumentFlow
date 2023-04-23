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
using DocumentFlow.Infrastructure.Data;

using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Wages.Core;

public class WageEmployee : Entity<long>, IEntityClonable, ICloneable
{
    [Display(AutoGenerateField = false)]
    public Guid EmployeeId { get; set; }

    [Display(Name = "Сотрудник", Order = 1)]
    [Exclude]
    [ColumnMode(AutoSizeColumnsMode = AutoSizeColumnsMode.Fill)]
    public string EmployeeName { get; protected set; } = string.Empty;

    [Display(Name = "Зар. плата", Order = 100)]
    [ColumnMode(Format = ColumnFormat.Currency, Width = 150)]
    public decimal Wage { get; set; }

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((WageEmployee)copy).Id = 0;

        return copy;
    }

    public void SetEmployeeName(string employeeName) => EmployeeName = employeeName;

    public override string ToString() => EmployeeName;
}
