//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.02.2023
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

public class Applicator : Identifier<Guid>
{
    /// <summary>
    /// Наименование аппликатора.
    /// </summary>
    [Display(Name = "Наименование", Order = 100)]
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// Дата ввода в эксплуатацию.
    /// </summary>
    [Display(Name = "Дата ввода в экспл.", Order = 200)]
    public DateTime? Commissioning { get; set; }

    /// <summary>
    /// Количество выполненных опрессовок за период наблюдения.
    /// </summary>
    [Display(AutoGenerateField = false)]
    public int Quantity { get; set; }

    /// <summary>
    /// Количество опрессовок выполненных до начала наблюдения.
    /// </summary>
    [Display(AutoGenerateField = false)]
    public int StartingHits { get; set; }

    [Display(Name = "Кол-во опресс.", Order = 300)]
    public int TotalHits => Quantity + StartingHits;
}
