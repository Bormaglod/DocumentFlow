//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.10.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Core;

public enum MenuDestination { Document, Directory }

/// <summary>
/// Атрибут используемый для описания меню.
/// Для сортировки пунктов меню используется параметр <see cref="MenuAttribute.Order"/>. Этот параметр представляет
/// собой 6-ти значное число ABBBCC, где A - основной раздел (1 - документы, 2 - справочники), BBB - порядковый номер первого уровня вложенности,
/// CC - порядковый номер второго уровня вложенности (если второго уровня нет, то CC = 00.
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public class MenuAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="destination">Параметр определяет в какой раздел попадет реализация этого интерфейса - жокументы или справочники.</param>
    /// <param name="name">Наименование пункта меню.</param>
    /// <param name="order_id">Порядок сортировки.</param>
    /// <param name="path">Если пункт меню должен быть вложеным, то этот параметр определяет наименование вышестоящего пункта меню.</param>
    public MenuAttribute(MenuDestination destination, string name, int order_id, string? path = null) => (Destination, Name, Order, Path) = (destination, name, order_id, path);

    public MenuDestination Destination { get; }
    public string Name { get; }
    public int Order { get; }
    public string? Path { get; }
}
