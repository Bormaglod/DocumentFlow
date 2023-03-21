//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.04.2014
//
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Data;

public class UserAlias : Identifier<Guid>
{
    public string? Name { get; set; }
    public string? PgName { get; set; }
    public string? Surname { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public bool IsSystem { get; set; }
    public override string ToString() => Name ?? string.Empty;
}
