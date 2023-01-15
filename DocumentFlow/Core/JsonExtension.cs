//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.01.2023
//
// Версия 2023.1.15
//  - функция JsonHelper.GetJsonText возвращает только string
//
//-----------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DocumentFlow.Core;

public static class JsonExtension
{
    public static T DeserializeDefault<T>(this JsonNode node) where T : new()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());
        return node.Deserialize<T>(options) ?? new T();
    }

    public static T Deserealize<T>(this JsonObject jsonObj, string path) where T : new()
    {
        string[] keys = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

        JsonNode? node = null;
        if (keys.Length == 1)
        {
            node = jsonObj[path];
        }
        else
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == "#")
                {
                    node = jsonObj.Root;
                }
                else
                {
                    if (node == null)
                    {
                        break;
                    }

                    node = node[keys[i]];
                }
            }
        }

        if (node != null)
        {
            return node.DeserializeDefault<T>();
        }

        return new T();
    }

    public static void AddOrReplace(this JsonObject jsonObj, string key, JsonNode jsonNode)
    {
        if (jsonObj.ContainsKey(key))
        {
            jsonObj[key] = jsonNode;
        }
        else
        {
            jsonObj.Add(key, jsonNode);
        }
    }

    public static JsonObject AddEmpty(this JsonObject jsonObj, string objectName) 
    {
        var newNode = new JsonObject();
        jsonObj.Add(objectName, newNode);

        return newNode;
    }

    public static void Add<T>(this JsonObject jsonObj, string path, T value)
    {
        var json = JsonHelper.GetJsonText(value);
        var jsonNode = JsonNode.Parse(json);
        if (jsonNode == null)
        {
            return;
        }

        string[] keys = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (keys.Length == 1)
        {
            jsonObj.AddOrReplace(path, jsonNode);
        }
        else
        {
            JsonNode? node = null;
            for (int i = 0; i < keys.Length - 1; i++)
            {
                if (keys[i] == "#")
                {
                    node = jsonObj.Root;
                }
                else
                {
                    if (node == null)
                    {
                        throw new ArgumentNullException(nameof(node));
                    }

                    node = node[keys[i]] ?? node.AsObject().AddEmpty(keys[i]);
                }
            }

            if (node != null)
            {
                node.AsObject().AddOrReplace(keys[^1], jsonNode);
            }
        }
    }
}
