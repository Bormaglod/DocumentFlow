//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.10.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

/// <summary>
/// Атрибут устанавливается для классов, которые будут редактироваться.
/// Наименование окна формируется исходя из свойств этого атрибута
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DescriptionAttribute : Attribute
{
    public DescriptionAttribute(string name) => Name = name;

    public string Name { get; }
}
