//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Equipments;

public class Applicator : Identifier<Guid>
{
#pragma warning disable IDE1006 // Стили именования
    /// <summary>
    /// Наименование аппликатора.
    /// </summary>
    public string item_name { get; set; } = string.Empty;

    /// <summary>
    /// Дата ввода в эксплуатацию.
    /// </summary>
    public DateTime? commissioning { get; set; }

    /// <summary>
    /// Количество выполненных опрессовок за период наблюдения.
    /// </summary>
    public int quantity { get; set; }

    /// <summary>
    /// Количество опрессовок выполненных до начала наблюдения.
    /// </summary>
    public int starting_hits { get; set; }
#pragma warning restore IDE1006 // Стили именования
}
