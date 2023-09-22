//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.04.2014
//-----------------------------------------------------------------------

namespace DocumentFlow.Data;

public class UserAlias : Identifier<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string PgName { get; set; } = string.Empty;
    public string? Surname { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public bool IsSystem { get; set; }
    public override string ToString() => Name;
}
