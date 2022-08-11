//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.10.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

public class EnumTypeAttribute : Attribute
{
    public EnumTypeAttribute(string name) => Name = name;
    public string Name { get; }
}
