//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.01.2023
//
// Версия 2023.1.15
//  - добвавлена функция StandardOptions
//  - изменен тип который возвращает GetJsonText
//
//-----------------------------------------------------------------------

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocumentFlow.Core;

public class JsonHelper
{
    public static JsonSerializerOptions StandardOptions()
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        options.Converters.Add(new JsonStringEnumConverter());

        return options;
    }

    public static string GetJsonText<T>(T section) => JsonSerializer.Serialize(section, StandardOptions());
}
