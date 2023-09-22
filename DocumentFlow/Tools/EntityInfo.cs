//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.09.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Tools;

[AttributeUsage(AttributeTargets.Property)]
public class EntityInfo : Attribute
{
    public EntityInfo() { }
}