//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;

using Microsoft.Extensions.DependencyInjection;

using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DocumentFlow.Settings;

public abstract class BaseSettings
{
    private string? json;
    private string? file;

    public virtual T Get<T>(string key) where T : new()
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (!string.IsNullOrEmpty(JsonText))
        {
            var jsonObj = JsonNode.Parse(JsonText)?.AsObject();

            if (jsonObj != null)
            {
                string[] keys = key.Split(':', StringSplitOptions.RemoveEmptyEntries);

                JsonNode? node = jsonObj[keys[0]];
                if (node != null)
                {
                    for (int i = 1; i < keys.Length; i++)
                    {
                        node = node[keys[i]];
                        if (node == null)
                        {
                            break;
                        }
                    }
                }

                if (node != null)
                {
                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return node.Deserialize<T>(options) ?? new T();
                }
            }
        }

        return new T();
    }

    public virtual void Write<T>(string key, T section)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (!string.IsNullOrEmpty(FileName))
        {
            JsonObject? jsonObj;
            if (string.IsNullOrEmpty(JsonText))
            {
                jsonObj = new JsonObject();
            }
            else
            {
                jsonObj = JsonNode.Parse(JsonText)?.AsObject();
            }
            
            if (jsonObj != null)
            {
                var t_json = GetJson(section);
                var jsonNode = JsonNode.Parse(t_json.JsonText);
                if (jsonNode != null) 
                {
                    string[] keys = key.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    if (keys.Length == 1)
                    {
                        AddNode(jsonObj, key, jsonNode);
                    }
                    else
                    {
                        JsonObject? node = jsonObj[keys[0]]?.AsObject();
                        if (node == null)
                        {
                            node = new JsonObject();
                            jsonObj.Add(keys[0], node);
                        }

                        for (int i = 1; i < keys.Length - 1; i++)
                        {
                            var newNode = node[keys[i]]?.AsObject();
                            if (newNode == null)
                            {
                                newNode = new JsonObject();
                                node.Add(keys[i], newNode);
                            }

                            node = newNode;
                        }

                        AddNode(node, keys[^1], jsonNode);
                    }
                }

                WriteJson(jsonObj.ToJsonString(t_json.Options));
            }
        }
    }

    public string? JsonText => json;

    public string? FileName => file;

    protected void ReadJson(string file) => json = File.ReadAllText(file);

    protected void PrepareJson()
    {
        if (string.IsNullOrEmpty(JsonText) && !string.IsNullOrEmpty(FileName))
        {
            if (File.Exists(FileName))
            {
                json = File.ReadAllText(FileName);
            }
        }
    }

    protected T ReadJsonObject<T>() where T : new()
    {
        if (File.Exists(file) && !string.IsNullOrEmpty(JsonText))
        {
            ReadJson(file);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            return JsonSerializer.Deserialize<T>(JsonText, options) ?? new T();
        }
        else
        {
            return new T();
        }
    }

    protected void PrepareJsonFile(string fileName, string? folder = null)
    {
        var db = Services.Provider.GetService<IDatabase>();
        if (db == null)
        {
            return;
        }

        var path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Автоком",
            "settings",
            db.CurrentUser
        );

        if (!string.IsNullOrEmpty(folder))
        {
            path = Path.Combine(path, folder);
        }

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        file = Path.Combine(path, $"{fileName}.json");
    }

    protected static (JsonSerializerOptions Options, string JsonText) GetJson<T>(T section)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        options.Converters.Add(new JsonStringEnumConverter());

        return (options, JsonSerializer.Serialize(section, options));
    }

    protected void WriteJson(string jsonText)
    {
        if (FileName != null) 
        {
            json = jsonText;
            File.WriteAllText(FileName, json);
        }
    }

    private static void AddNode(JsonObject jsonObj, string key, JsonNode jsonNode)
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
}
