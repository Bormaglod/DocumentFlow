//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.02.2023
//
// Версия 2023.2.10
//  - добавлены атрибуты Display
//  - добавлено свойство TotalHits
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Equipments;

public class Applicator : Identifier<Guid>
{
#pragma warning disable IDE1006 // Стили именования
    /// <summary>
    /// Наименование аппликатора.
    /// </summary>
    [Display(Name = "Наименование", Order = 100)]
    public string item_name { get; set; } = string.Empty;

    /// <summary>
    /// Дата ввода в эксплуатацию.
    /// </summary>
    [Display(Name = "Дата ввода в экспл.", Order = 200)]
    public DateTime? commissioning { get; set; }

    /// <summary>
    /// Количество выполненных опрессовок за период наблюдения.
    /// </summary>
    [Display(AutoGenerateField = false)]
    public int quantity { get; set; }

    /// <summary>
    /// Количество опрессовок выполненных до начала наблюдения.
    /// </summary>
    [Display(AutoGenerateField = false)]
    public int starting_hits { get; set; }
#pragma warning restore IDE1006 // Стили именования

    [Display(Name = "Кол-во опресс.", Order = 300)]
    public int TotalHits => quantity + starting_hits;
}
