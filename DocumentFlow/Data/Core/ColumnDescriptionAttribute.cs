//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.10.2020
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

[AttributeUsage(AttributeTargets.Property)]
public class ColumnDescriptionAttribute : Attribute
{
    public ColumnDescriptionAttribute(string title) => Title = title;

    public string Title { get; }
}
