//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//
// Версия 2023.1.8
//  - свойствам добавлены атрибуты JsonPropertyName
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

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