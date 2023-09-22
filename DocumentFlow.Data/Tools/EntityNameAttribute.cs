//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.10.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Tools;

/// <summary>
/// Атрибут устанавливается для классов, которые будут редактироваться.
/// Наименование окна формируется исходя из свойств этого атрибута
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class EntityNameAttribute : Attribute
{
    public EntityNameAttribute(string name) => Name = name;

    public string Name { get; }
}
