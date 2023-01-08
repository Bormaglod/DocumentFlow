//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//
// Версия 2023.1.8
//  - свойствам добавлены атрибуты JsonPropertyName
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

using System.Text.Json.Serialization;

namespace DocumentFlow.Controls.Settings;

public sealed class BalanceSheetBrowserSettings : BrowserSettings
{
    [JsonPropertyName("content")]
    public BalanceSheetContent Content { get; set; } = BalanceSheetContent.Material;

    [JsonPropertyName("view_amount")]
    public bool ViewAmount { get; set; } = true;

    [JsonPropertyName("view_summa")]
    public bool ViewSumma { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("date_from")]
    public DateTime? DateFrom { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("date_to")]
    public DateTime? DateTo { get; set; }
}