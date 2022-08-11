//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DocumentFlow.Controls.Settings;

public class BrowserColumn
{
    public BrowserColumn() { }

    public BrowserColumn(GridColumn column)
    {
        Name = column.MappingName;
        Header = column.HeaderText;
        Visible = column.Visible;
        Width = column.AutoSizeColumnsMode == AutoSizeColumnsMode.None ? (double.IsNormal(column.Width) ? Convert.ToInt32(column.Width) : 0) : null;
        AutoSizeMode = column.AutoSizeColumnsMode;
    }

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

#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
    [Display(Name = "Ширина")]
    [JsonIgnore]
    public string WidthText => (Width is not null and > 0) ? Width.ToString() : "(Auto)";
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
}
