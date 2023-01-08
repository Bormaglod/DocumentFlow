//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Settings.Infrastructure;

namespace DocumentFlow.Settings;

public class StandaloneSettings : BaseSettings, IStandaloneSettings
{
    private object? settings;

    public void Configure(string key)
    {
        PrepareJsonFile(key, "standalone");
    }

    public T Get<T>() where T : new()
    {
        if (string.IsNullOrEmpty(FileName))
        {
            throw new InvalidOperationException();
        }

        if (settings == null)
        {
            settings = ReadJsonObject<T>()!;
            return (T)settings;
        }
        else
        {
            if (settings is not T t_settings)
            {
                throw new InvalidOperationException();
            }

            return t_settings;
        }
    }

    public void Write<T>()
    {
        if (string.IsNullOrEmpty(FileName))
        {
            throw new InvalidOperationException();
        }

        if (settings != null)
        {
            if (settings is not T t_settings)
            {
                throw new InvalidOperationException();
            }

            var t_json = GetJson(t_settings);
            WriteJson(t_json.JsonText);
        }
    }
}
