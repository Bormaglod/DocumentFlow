//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Tools;

using System.IO;
using System.Text.Json.Nodes;

namespace DocumentFlow.Settings;

public class LocalSettings
{
    public LoginSettings Login { get; set; } = new();
    public StartPageSettings StartPage { get; set; } = new();
    public MainFormSettings MainForm { get; set; } = new();
    public PreviewRowSettings PreviewRows { get; set; } = new();
    public ReportSettings Report { get; set;} = new();

    public void Save()
    {
        var file = Path.Combine(
#if !DEBUG
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Автоком",
            "settings",
#endif
            "appsettings.local.json"
        );

        string json = JsonHelper.GetJsonText(this);

        JsonObject? jsonObj = new()
        {
            ["LocalSettings"] = JsonNode.Parse(json)
        };

        File.WriteAllText(file, jsonObj.ToJsonString(JsonHelper.StandardOptions()));
    }
}
