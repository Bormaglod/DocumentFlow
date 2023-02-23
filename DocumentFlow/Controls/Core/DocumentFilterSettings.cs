//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2023
//
// Версия 2023.1.15
//  - переименован в DocumentFilterSettings
//
//-----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DocumentFlow.Controls.Core;

public class DocumentFilterSettings
{
    [JsonPropertyName("date_from_enabled")]
    public bool DateFromEnabled { get; set; } = false;

    [JsonPropertyName("date_to_enabled")]
    public bool DateToEnabled { get; set; } = false;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("date_from")]
    public DateTime? DateFrom { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("date_to")]
    public DateTime? DateTo { get; set; }
}
