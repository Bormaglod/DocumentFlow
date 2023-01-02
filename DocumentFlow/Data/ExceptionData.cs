//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.01.2023
//-----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DocumentFlow.Data;

public class ExceptionData
{
    [JsonPropertyName("table")]
    public string TableName { get; set; } = string.Empty;

    [JsonPropertyName("trigger")]
    public string TriggerName { get; set; } = string.Empty;

    [JsonPropertyName("function_name")]
    public string FunctionName { get; set; } = string.Empty;

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}
