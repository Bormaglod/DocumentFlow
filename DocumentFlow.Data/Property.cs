//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.07.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data;

public class Property : Identifier<Guid>
{
    public string PropertyName { get; set; } = string.Empty;
    public string? Title { get; set; }
    public override string ToString() => Title ?? PropertyName;
}
