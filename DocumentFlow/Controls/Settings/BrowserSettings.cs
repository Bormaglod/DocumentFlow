//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DocumentFlow.Controls.Settings;

public class BrowserSettings
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<BrowserColumn>? Columns { get; set; }

    public ReportPage Page { get; set; } = new();
}
