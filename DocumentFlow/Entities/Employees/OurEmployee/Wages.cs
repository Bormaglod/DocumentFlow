//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Employees;

public class Wages : Identifier<Guid>
{
#pragma warning disable IDE1006 // Стили именования
    [Display(Name = "Сотрудник", Order = 100)]
    public string employee_name { get; protected set; } = string.Empty;

    [Display(Name = "Остаток на начало", Order = 200)]
    public decimal begining_balance { get; protected set; }

    [Display(Name = "Начислено", Order = 300)]
    public decimal current_calc { get; protected set; }

    [Display(Name = "Выплачено", Order = 400)]
    public decimal current_pay { get; protected set; }

    [Display(Name = "Остаток на конец", Order = 500)]
    public decimal ending_balance { get; protected set; }
#pragma warning restore IDE1006 // Стили именования
}
