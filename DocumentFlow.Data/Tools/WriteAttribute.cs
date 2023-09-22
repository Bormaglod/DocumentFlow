//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.07.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Tools;

/// <summary>
/// Specifies whether a field is writable in the database.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class WriteAttribute : Attribute
{
    /// <summary>
    /// Specifies whether a field is writable in the database.
    /// </summary>
    /// <param name="write">Whether a field is writable in the database.</param>
    public WriteAttribute(bool write)
    {
        Write = write;
    }

    /// <summary>
    /// Whether a field is writable in the database.
    /// </summary>
    public bool Write { get; }
}
