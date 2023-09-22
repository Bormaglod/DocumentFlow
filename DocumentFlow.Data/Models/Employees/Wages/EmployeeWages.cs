//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2023
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

public class EmployeeWages : Identifier<Guid>
{
    [Display(Name = "Сотрудник", Order = 100)]
    public string EmployeeName { get; protected set; } = string.Empty;

    [Display(Name = "Остаток на начало", Order = 200)]
    public decimal BeginingBalance { get; protected set; }

    [Display(Name = "Начислено", Order = 300)]
    public decimal CurrentCalc { get; protected set; }

    [Display(Name = "Выплачено", Order = 400)]
    public decimal CurrentPay { get; protected set; }

    [Display(Name = "Остаток на конец", Order = 500)]
    public decimal EndingBalance { get; protected set; }
}
