//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.01.2022
//
// Версия 2023.1.9
//  - удалён метод AddNode
//  - добавлено использование методов из JsonExtension
//  - метод GetJson перемещен в JsonHelper и переименован в GetJsonText
// Версия 2023.1.15
//  - исправление незначительных ошибок
//  - функция JsonHelper.GetJsonText возвращает только string
// Версия 2023.1.24
//  - IDatabase перенесён из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Core;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using System.IO;
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
                return jsonObj.Deserealize<T>(key);
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
            JsonObject? jsonObj = string.IsNullOrEmpty(JsonText) ? new JsonObject() : JsonNode.Parse(JsonText)?.AsObject();
            
            if (jsonObj != null)
            {
                jsonObj.Add(key, section);
                WriteJson(jsonObj.ToJsonString(JsonHelper.StandardOptions()));
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

    protected void WriteJson(string jsonText)
    {
        if (FileName != null) 
        {
            json = jsonText;
            File.WriteAllText(FileName, json);
        }
    }
}
