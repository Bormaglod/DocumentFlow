//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.11.2021
//
// Версия 2023.2.7
//  - добавлено свойство commissioning
// Версия 2023.2.8
//  - добавлено свойство starting_hits
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Equipments;

[Description("Оборудование")]
public class Equipment : Directory
{
    /// <summary>
    /// Определяет является ли оборудование инструментом.
    /// </summary>
    public bool IsTools { get; set; }

    /// <summary>
    /// Серийный номер оборудования.
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Дата ввода в эксплуатацию.
    /// </summary>
    public DateTime? Commissioning { get; set; }

    /// <summary>
    /// Начальное количество опрессовок. Используется только для аппликаторов.
    /// </summary>
    public int? StartingHits { get; set; }
}
