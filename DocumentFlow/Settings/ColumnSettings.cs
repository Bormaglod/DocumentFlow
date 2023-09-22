//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DocumentFlow.Settings;

public class ColumnSettings
{
    [Display(AutoGenerateField = false)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Наименование")]
    public string Header { get; set; } = string.Empty;

    [Display(Name = "Visible")]
    public bool Visible { get; set; } = true;

    [Display(AutoGenerateField = false)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Width { get; set; } = null;

    [Display(AutoGenerateField = false)]
    public AutoSizeColumnsMode AutoSizeMode { get; set; } = AutoSizeColumnsMode.None;

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public bool Hidden { get; set; }

    [Display(Name = "Ширина")]
    [JsonIgnore]
    public string WidthText => (Width is not null and > 0) ? (Width.ToString() ?? string.Empty) : "(Auto)";
}
