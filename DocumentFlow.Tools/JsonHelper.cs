﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.01.2023
//-----------------------------------------------------------------------

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DocumentFlow.Tools;

public class JsonHelper
{
    public static JsonSerializerOptions StandardOptions()
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
            Converters = {
                new JsonStringEnumConverter()
            },
            IgnoreReadOnlyProperties = true
        };
        options.Converters.Add(new JsonStringEnumConverter());

        return options;
    }

    public static string GetJsonText(object section) => JsonSerializer.Serialize(section, StandardOptions());

    public static JsonNode? GetJsonNode(object section) => JsonSerializer.SerializeToNode(section, StandardOptions());
}
