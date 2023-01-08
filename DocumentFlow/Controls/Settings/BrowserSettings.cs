//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//
// Версия 2023.1.8
//  - свойствам добавлены атрибуты JsonPropertyName
//
//-----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DocumentFlow.Controls.Settings;

public class BrowserSettings
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("columns")]
    public IReadOnlyList<BrowserColumn>? Columns { get; set; }

    [JsonPropertyName("page")]
    public ReportPage Page { get; set; } = new();
}
