//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Tools;

/// <summary>
/// Specifies that this is a computed column.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ComputedAttribute : Attribute
{
}
