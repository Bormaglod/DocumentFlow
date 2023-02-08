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

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Equipments;

[Description("Оборудование")]
public class Equipment : Directory
{
#pragma warning disable IDE1006 // Стили именования
    /// <summary>
    /// Определяет является ли оборудование инструментом.
    /// </summary>
    public bool is_tools { get; set; }

    /// <summary>
    /// Серийный номер оборудования.
    /// </summary>
    public string? serial_number { get; set; }

    /// <summary>
    /// Дата ввода в эксплуатацию.
    /// </summary>
    public DateTime? commissioning { get; set; }

    /// <summary>
    /// Начальное количество опрессовок. Используется только для аппликаторов.
    /// </summary>
    public int? starting_hits { get; set; }
#pragma warning restore IDE1006 // Стили именования
}
