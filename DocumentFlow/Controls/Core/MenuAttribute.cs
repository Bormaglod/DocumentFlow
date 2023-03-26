﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.10.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Core;

public enum MenuDestination { Document, Directory }

/// <summary>
/// Атрибут используемый для описания меню.
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public class MenuAttribute : Attribute
{
    public MenuAttribute(MenuDestination destination, string name, int order, string? parent = null) => (Destination, Name, Order, Parent) = (destination, name, order, parent);

    public string Name { get; }
    public MenuDestination Destination { get; }
    public int Order { get; }
    public string? Parent { get; }
}
