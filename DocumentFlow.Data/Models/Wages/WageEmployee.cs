//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

public class WageEmployee : Entity<long>, ICopyable, IDependentEntity
{
    private Guid employeeId;
    private decimal wage;

    [Display(AutoGenerateField = false)]
    public Guid EmployeeId 
    { 
        get => employeeId;
        set => SetProperty(ref employeeId, value);
    }

    [Display(Name = "Сотрудник", Order = 1)]
    [ColumnMode(AutoSizeColumnsMode = AutoSizeColumnsMode.Fill)]
    [Write(false)]
    public string EmployeeName { get; set; } = string.Empty;

    [Display(Name = "Зар. плата", Order = 100)]
    [ColumnMode(Format = ColumnFormat.Currency, Width = 150)]
    public decimal Wage 
    { 
        get => wage;
        set => SetProperty(ref wage, value);
    }

    public object Copy()
    {
        var copy = (ProductPrice)MemberwiseClone();
        copy.Id = 0;

        return copy;
    }

    public void SetOwner(Guid ownerId) => OwnerId = ownerId;

    public override string ToString() => EmployeeName;
}
