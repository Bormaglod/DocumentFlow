//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Data.Core;

public class Property : Identifier<Guid>, IProperty
{
    public string property_name { get; set; } = string.Empty;
    public string? title { get; set; }
    public override string ToString() => title ?? property_name;
}
