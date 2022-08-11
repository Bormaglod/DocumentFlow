//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.07.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

public class Report : Identifier<Guid>
{
    public string code { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
    public string? schema_report { get; set; }
}